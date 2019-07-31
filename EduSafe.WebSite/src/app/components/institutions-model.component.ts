import { Component, Input, OnInit } from '@angular/core';

import { AppRootComponent } from '../components/app-root.component';

import { InstitutionInputEntry } from '../classes/institutionInputEntry';
import { InstitutionOutputSummary } from '../classes/institutionOutputSummary';

import { ActivityCaptureService } from '../services/activityCapture.service';
import { ModelCalculationService } from '../services/modelCalculation.service';
import { CollegeDataSearchService } from '../services/collegeDataSearch.service';

import { Subject, Observable } from 'rxjs';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';

@Component({
  selector: 'institutions-model',
  templateUrl: '../views/institutions-model.component.html',
  styleUrls: ['../styles/institutions-model.component.css']
})

export class InstitutionsModelComponent implements OnInit{
  @Input() institutionInputEntry: InstitutionInputEntry;
  institutionOutputSummary: InstitutionOutputSummary;
  ipAddress: Observable<string>;

  degreeTypesList = ['Two-Year', 'Four-Year']
  collegeTypesList = ['Public School', 'Private School', 'For-Profit College'];
  collegesList: Observable<string[]>;

  public labelYears1 = 'Within 4 Years';
  public labelYears2 = 'Within 5 Years';
  public labelYears3 = 'Within 6 Years';
  public canPaymentBeEstimated = false;

  private collegeSearchTerms = new Subject<string>();

  constructor(
    private appRootComponent: AppRootComponent,
    private activityCaptureService: ActivityCaptureService,
    private modelCalculationService: ModelCalculationService,
    private collegeDataSearchService: CollegeDataSearchService
  ) { }

  updateDegreeType(degreeType): void {
    this.institutionInputEntry.TwoYearOrFourYearSchool = degreeType;
    this.checkIfPaymentCanBeCalculated();
  }

  checkIfPaymentCanBeCalculated(): void {
    this.canPaymentBeEstimated =
      this.institutionInputEntry.PublicOrPrivateSchool &&
      this.institutionInputEntry.TwoYearOrFourYearSchool &&
      this.institutionInputEntry.GraduationWithinYears1 > 0 &&
      this.institutionInputEntry.GraduationWithinYears2 > 0 &&
      this.institutionInputEntry.GraduationWithinYears3 > 0 &&
      this.institutionInputEntry.AverageLoanDebtAtGraduation > 0;
  }

  ngOnInit(): void {
    this.institutionInputEntry = new InstitutionInputEntry();
    this.institutionInputEntry.IpAddress = this.appRootComponent.ipAddress;

    this.collegesList = this.collegeSearchTerms.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      switchMap((searchText: string) =>
        this.collegeDataSearchService.searchColleges(searchText)));
  }
}
