import { Component, OnInit } from '@angular/core';

import { ModelComponent } from '../components/model.component';

import { ModelOutputSummary } from '../classes/modelOutputSummary';

@Component({
  selector: 'edusafe-output',
  templateUrl: '../views/output.component.html',
  styleUrls: ['../styles/output.component.css']
})

export class ModelOuputComponent implements OnInit {
  modelOutputSummary: ModelOutputSummary;

  constructor(private modelComponent: ModelComponent) { }

  revealModelInputsAgain(): void {
    this.modelComponent.isCalculated = false;
  }

  ngOnInit(): void {
    this.modelOutputSummary = this.modelComponent.modelOutputSummary;
  }
}
