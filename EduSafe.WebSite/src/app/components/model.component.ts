import { Component } from '@angular/core';
import { Input } from '@angular/core';
import { OnInit } from '@angular/core';

import { ModelInputEntry } from '../classes/modelInputEntry';
import { ModelOutputSummary } from '../classes/modelOutputSummary';

import { ModelCalculationService } from '../services/modelCalculationService';

@Component({
  selector: 'edusafe-model',
  templateUrl: '../views/model.component.html',
  styleUrls: ['../styles/model.component.css']
})

export class ModelComponent implements OnInit {
  @Input() modelInputEntry: ModelInputEntry;
  modelOutputSummary: ModelOutputSummary;

  constructor(
    private modelCalculationService: ModelCalculationService
  ) { }

  submitForCalculation(): void {
    this.modelCalculationService.calcModelOutput(this.modelInputEntry)
      .then(modelCalculationOutput => {
        this.modelOutputSummary = modelCalculationOutput;
      })
  }

  ngOnInit(): void {
    this.modelInputEntry = new ModelInputEntry();
    this.modelCalculationService.getModelOutput()
      .then(modelCalculationOuput => {
        this.modelOutputSummary = modelCalculationOuput;
      });
  }
}
