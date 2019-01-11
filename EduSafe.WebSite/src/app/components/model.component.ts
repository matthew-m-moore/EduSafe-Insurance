import { Component } from '@angular/core';
import { OnInit } from '@angular/core';

import { ModelOutputSummary } from '../classes/modelOutputSummary';

import { ModelCalculationService } from '../services/modelCalculationService';

@Component({
  selector: 'edusafe-model',
  templateUrl: '../views/model.component.html',
  styleUrls: ['../styles/model.component.css']
})

export class ModelComponent implements OnInit {
  modelOutputSummary: ModelOutputSummary;

  constructor(
    private modelCalculationService: ModelCalculationService
  ) { }

  ngOnInit(): void {
    this.modelCalculationService.getModelOutput()
      .then(modelCalculationOuput => {
        this.modelOutputSummary = modelCalculationOuput;
      });
  }
}
