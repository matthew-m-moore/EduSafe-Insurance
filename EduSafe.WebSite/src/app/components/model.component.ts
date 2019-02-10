import { Component, Input, OnInit } from '@angular/core';

import { ModelInputEntry } from '../classes/modelInputEntry';
import { ModelOutputSummary } from '../classes/modelOutputSummary';

import { ModelCalculationService } from '../services/modelCalculation.service';
import { CollegeDataSearchService } from '../services/collegeDataSearch.service';

import { Subject, Observable } from 'rxjs';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';

@Component({
  selector: 'edusafe-model',
  templateUrl: '../views/model.component.html',
  styleUrls: ['../styles/model.component.css']
})

export class ModelComponent implements OnInit {
  @Input() modelInputEntry: ModelInputEntry;
  modelOutputSummary: ModelOutputSummary;

  collegeTypesList = [ 'Public School', 'Private School', 'For-Profit College'];
  collegesList: Observable<string[]>;
  collegeMajorsList: Observable<string[]>;

  public isCalculated = false;
  private collegeSearchTerms = new Subject<string>();
  private collegeMajorSearchTerms = new Subject<string>();

  constructor(
    private modelCalculationService: ModelCalculationService,
    private collegeDataSearchService: CollegeDataSearchService,
  ) { }

  searchCollege(searchText: string): void {
    if (!searchText.trim()) {
      this.collegeSearchTerms.next('');
      return; }
    this.collegeSearchTerms.next(searchText.toUpperCase());
  }

  searchCollegeMajor(searchText: string): void {
    if (!searchText.trim()) {
      this.collegeMajorSearchTerms.next('');
      return; }
    this.collegeMajorSearchTerms.next(searchText.toUpperCase());
  }

  updateIncomeCoverageAmount(collegeMajor) {
    this.modelInputEntry.IncomeCoverageAmount = collegeMajor.medianIncome;
  }

  submitForCalculation(): void {
    this.modelCalculationService.calcModelOutput(this.modelInputEntry)
      .then(modelCalculationOutput => {
        this.modelOutputSummary = modelCalculationOutput;
        this.isCalculated = true;
      })
  }

  revealModelInputsAgain(): void {
    this.isCalculated = false;
  }

  ngOnInit(): void {
    this.modelInputEntry = new ModelInputEntry();
    this.modelCalculationService.getModelOutput()
      .then(modelCalculationOuput => {
        this.modelOutputSummary = modelCalculationOuput;
      });

    this.collegesList = this.collegeSearchTerms.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      switchMap((searchText: string) =>
        this.collegeDataSearchService.searchColleges(searchText)));

    this.collegeMajorsList = this.collegeMajorSearchTerms.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      switchMap((searchText: string) =>
        this.collegeDataSearchService.searchCollegeMajors(searchText)));
  }
}
