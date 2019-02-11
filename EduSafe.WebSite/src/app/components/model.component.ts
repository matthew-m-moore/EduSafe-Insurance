import { Component, Input, OnInit } from '@angular/core';

import { ModelInputEntry } from '../classes/modelInputEntry';
import { ModelOutputSummary } from '../classes/modelOutputSummary';
import { CollegeMajorData } from '../classes/collegeMajorData';

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
  collegeMajorsDataList: Observable<CollegeMajorData[]>;

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

  updateSchoolType() {
    // It might make sense to look this up at some point, rather than leave it to the user
  }

  updateIncomeCoverageAmount(collegeMajor) {
    if (this.collegeMajorsDataList.subscribe(result =>
        result.some(d => d.CollegeMajor === collegeMajor))) {

      this.collegeMajorsDataList.subscribe(result => {
          var collegeMajorData = result.find(d => d.CollegeMajor === collegeMajor);
          this.modelInputEntry.IncomeCoverageAmount = collegeMajorData.MedianSalary;
        });
    }
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

    this.collegeMajorsDataList = this.collegeMajorSearchTerms.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      switchMap((searchText: string) =>
        this.collegeDataSearchService.searchCollegeMajors(searchText)));
  }
}
