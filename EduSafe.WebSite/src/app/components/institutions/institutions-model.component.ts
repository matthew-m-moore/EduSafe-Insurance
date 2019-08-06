import { Component, Input, OnInit } from '@angular/core';

import { AppRootComponent } from '../../components/app-root.component';

import { InstitutionInputEntry } from '../../classes/institutions/institutionInputEntry';
import { InstitutionOutputSummary } from '../../classes/institutions/institutionOutputSummary';
import { InstitutionalGradData } from '../../classes/institutions/institutionalGradData';

import { ActivityCaptureService } from '../../services/activityCapture.service';
import { ModelCalculationService } from '../../services/modelCalculation.service';
import { CollegeDataService } from '../../services/collegeData.service';

import { Subject, Observable } from 'rxjs';
import { debounceTime, distinctUntilChanged, switchMap, debounce } from 'rxjs/operators';

@Component({
  selector: 'institutions-model',
  templateUrl: '../views/institutions/institutions-model.component.html',
  styleUrls: ['../styles/institutions/institutions-model.component.css']
})

export class InstitutionsModelComponent implements OnInit{
  @Input() institutionInputEntry: InstitutionInputEntry;
  institutionOutputSummary: InstitutionOutputSummary;
  ipAddress: Observable<string>;

  degreeTypesList = ['Two-Year', 'Four-Year']
  collegeTypesList = ['Public School', 'Private School', 'For-Profit College'];
  collegesList: Observable<string[]>;
  insitutionalGradData: Observable<InstitutionalGradData>;

  public labelYears2 = 'Within 2 Years';
  public labelYears3 = 'Within 3 Years';
  public labelYears4 = 'Within 4 Years';
  public labelYears5 = 'Within 5 Years';
  public labelYears6 = 'Within 6 Years';

  public isCalculated = false;
  public isCalculating = false;
  public canPaymentBeEstimated = false;
  public isTwoYearDegree = false;

  private collegeSearchTerms = new Subject<string>();

  constructor(
    private appRootComponent: AppRootComponent,
    private activityCaptureService: ActivityCaptureService,
    private modelCalculationService: ModelCalculationService,
    private collegeDataService: CollegeDataService
  ) { }

  searchCollege(searchText: string): void {
    if (!searchText.trim()) {
      this.collegeSearchTerms.next('');
      return;
    }
    this.collegeSearchTerms.next(searchText.toUpperCase());
  }

  updateSchoolType(schoolType): void {
    this.institutionInputEntry.PublicOrPrivateSchool = schoolType;

    if (this.institutionInputEntry.TwoYearOrFourYearSchool)
      this.updateInstitutionalGradData();

    this.checkIfPaymentCanBeCalculated();
  }

  updateDegreeType(degreeType): void {
    this.institutionInputEntry.TwoYearOrFourYearSchool = degreeType;

    if (degreeType == 'Two-Year')
      this.isTwoYearDegree = true;
    else
      this.isTwoYearDegree = false;

    if (this.institutionInputEntry.PublicOrPrivateSchool)
      this.updateInstitutionalGradData();

    this.checkIfPaymentCanBeCalculated();
  }

  updateInstitutionalGradData(): void {
    this.collegeDataService.getInstitutionalGradData(this.institutionInputEntry)
      .then(institutionalGradData => {
        this.institutionInputEntry.GraduationWithinYears1 = institutionalGradData.GradTargetYear1;
        this.institutionInputEntry.GraduationWithinYears2 = institutionalGradData.GradTargetYear2;
        this.institutionInputEntry.GraduationWithinYears3 = institutionalGradData.GradTargetYear3;
        this.institutionInputEntry.AverageLoanDebtAtGraduation = institutionalGradData.AverageLoanDebt;
        this.institutionInputEntry.StartingCohortDefaultRate = institutionalGradData.CohortDefaultRate;
      })
  }

  checkIfPaymentCanBeCalculated(): void {
    this.canPaymentBeEstimated =
      this.institutionInputEntry.PublicOrPrivateSchool &&
      this.institutionInputEntry.TwoYearOrFourYearSchool &&
      this.institutionInputEntry.StudentsPerStartingClass > 0 &&
      this.institutionInputEntry.GraduationWithinYears1 > 0 &&
      this.institutionInputEntry.GraduationWithinYears2 > 0 &&
      this.institutionInputEntry.GraduationWithinYears3 > 0 &&
      this.institutionInputEntry.AverageLoanDebtAtGraduation > 0 &&
      this.institutionInputEntry.StartingCohortDefaultRate > 0;
  }

  submitForCalculation(): void {
    this.isCalculating = true;
    this.modelCalculationService.calcInstitutionOutput(this.institutionInputEntry)
      .then(institutionCalculationOutput => {
        this.institutionOutputSummary = institutionCalculationOutput;
        this.isCalculating = false;
        this.isCalculated = true;
        window.scroll(0, 0);
      })

    this.activityCaptureService.captureInstitutionCalculationActivity(this.institutionInputEntry);
  }

  ngOnInit(): void {
    this.institutionInputEntry = new InstitutionInputEntry();
    this.institutionInputEntry.IpAddress = this.appRootComponent.ipAddress;

    this.collegesList = this.collegeSearchTerms.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      switchMap((searchText: string) =>
        this.collegeDataService.searchColleges(searchText)));
  }
}
