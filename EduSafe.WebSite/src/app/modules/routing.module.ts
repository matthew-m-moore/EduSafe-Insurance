import { ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Routes } from '@angular/router';

import { HomeComponent } from '../components/home.component';
import { ModelComponent } from '../components/model.component';
import { ModelOuputComponent } from '../components/output.component';
import { ContactComponent } from '../components/contact.component';

const appRoutes: Routes = [
  {
    path: 'edusafe-home',
    component: HomeComponent
  },
  {
    path: 'edusafe-model',
    component: ModelComponent
  },
  {
    path: 'edusafe-output',
    component: ModelOuputComponent
  },
  {
    path: 'edusafe-contact',
    component: ContactComponent
  },
];

export const RoutingModule: ModuleWithProviders = RouterModule.forRoot(appRoutes);
