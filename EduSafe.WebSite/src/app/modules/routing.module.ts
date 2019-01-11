import { ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Routes } from '@angular/router';

import { HomeComponent } from '../components/home.component';
import { ModelComponent } from '../components/model.component';

const appRoutes: Routes = [
  {
    path: 'edusafe-home',
    component: HomeComponent
  },
  {
    path: 'edusafe-model',
    component: ModelComponent
  },
];

export const RoutingModule: ModuleWithProviders = RouterModule.forRoot(appRoutes);
