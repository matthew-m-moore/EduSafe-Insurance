import { Component, Input, OnInit } from '@angular/core';

import { BsDatepickerConfig, BsDatepickerViewMode } from 'ngx-bootstrap/datepicker';

import { AppRootComponent } from '../components/app-root.component';

import { ModelInputEntry } from '../classes/modelInputEntry';
import { ModelOutputSummary } from '../classes/modelOutputSummary';
import { CollegeMajorData } from '../classes/collegeMajorData';

import { ModelCalculationService } from '../services/modelCalculation.service';
import { CollegeDataSearchService } from '../services/collegeDataSearch.service';

import { Subject, Observable } from 'rxjs';
import { debounceTime, distinctUntilChanged, switchMap, map } from 'rxjs/operators';

@Component({
  selector: 'edusafe-model',
  templateUrl: '../views/model.component.html',
  styleUrls: ['../styles/model.component.css']
})

export class ModelComponent implements OnInit {
  @Input() modelInputEntry: ModelInputEntry;
  modelOutputSummary: ModelOutputSummary;
  ipAddress: Observable<string>;

  datepickerConfig: Partial<BsDatepickerConfig>;
  datepickerMinMode: BsDatepickerViewMode = 'month';
  datepickerColorTheme = 'theme-dark-blue';
  datepickerInputFormat = 'MMM YYYY';

  collegeTypesList = [ 'Public School', 'Private School', 'For-Profit College'];
  collegesList: Observable<string[]>;
  collegeMajorsList: Observable<string[]>;
  collegeMajorsDataList: Observable<CollegeMajorData[]>;

  public isCalculated = false;
  public canPaymentBeCalculated = false;
  private collegeSearchTerms = new Subject<string>();
  private collegeMajorSearchTerms = new Subject<string>();

  constructor(
    private appRootComponent: AppRootComponent,
    private modelCalculationService: ModelCalculationService,
    private collegeDataSearchService: CollegeDataSearchService
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

  updateSchoolType(schoolType): void {
    this.modelInputEntry.PublicOrPrivateSchool = schoolType;
    this.checkIfPaymentCanBeCalculated();
  }

  updateIncomeCoverageAmount(collegeMajor): void {
    if (this.collegeMajorsDataList.subscribe(result =>
        result.some(d => d.CollegeMajor === collegeMajor))) {

      this.collegeMajorsDataList.subscribe(result => {
          var collegeMajorData = result.find(d => d.CollegeMajor === collegeMajor);
          this.modelInputEntry.IncomeCoverageAmount = collegeMajorData.MedianSalary;
        });
    }
  }

  checkIfPaymentCanBeCalculated(): void {
    this.canPaymentBeCalculated =
      this.modelInputEntry.CollegeMajor &&
      this.modelInputEntry.CollegeStartDate &&
      this.modelInputEntry.ExpectedGraduationDate &&
      this.modelInputEntry.PublicOrPrivateSchool &&
      this.modelInputEntry.IncomeCoverageAmount > 0;
  }

  submitForCalculation(): void {
    this.modelCalculationService.calcModelOutput(this.modelInputEntry)
      .then(modelCalculationOutput => {
        this.modelOutputSummary = modelCalculationOutput;
        this.isCalculated = true;
      })

    window.scroll(0, 0);
  }

  ngOnInit(): void {
    this.modelInputEntry = new ModelInputEntry();
    this.modelInputEntry.IpAddress = this.appRootComponent.ipAddress;

    this.modelCalculationService.getModelOutput()
      .then(modelCalculationOuput => {
        this.modelOutputSummary = modelCalculationOuput;
      });

    this.datepickerConfig = Object.assign({}, {
      minMode: this.datepickerMinMode,
      containerClass: this.datepickerColorTheme,
      dateInputFormat: this.datepickerInputFormat
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

    this.collegeMajorsList = this.collegeMajorsDataList.pipe(
      map(result => result.map(
        collegeMajorData => collegeMajorData.CollegeMajor)));
  }
}
