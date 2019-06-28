import { NgModule } from '@angular/core';
import { RouterModule, NavigationEnd, Routes, Router, ActivatedRoute } from '@angular/router';
import { Title, Meta } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { HomeComponent } from '../components/home.component';
import { ModelComponent } from '../components/model.component';
import { ModelOuputComponent } from '../components/output.component';
import { ContactComponent } from '../components/contact.component';
import { ArticlesComponent } from '../components/articles.component';

import { AuthenticationComponent } from '../components/authentication.component';
import { InstitutionalProfileComponent } from '../components/institutional-profile.component';
import { IndividualProfileComponent } from '../components/individual-profile.component';
import { ClaimsComponent } from '../components/claims.component';

import { filter, mergeMap, map } from 'rxjs/operators';

const appRoutes: Routes = [
  {
    path: '',
    component: HomeComponent,
    data: {
      title: 'Edu$afe, Securing Your Future',
      metaDescription: 'Edu$afe provides unemployment insurance for new college graduates. Importantly, Edu$afe offers income protection for those burdened with student loans.'
    }
  },
  {
    path: 'edusafe-home',
    component: HomeComponent,
    data: {
      title: 'Edu$afe, Securing Your Future',
      metaDescription: 'Edu$afe provides unemployment insurance for new college graduates. Importantly, Edu$afe offers income protection for those burdened with student loans.'
    }
  },
  {
    path: 'edusafe-model',
    component: ModelComponent,
    data: {
      title: 'Edu$afe, Calculate Your Payment',
      metaDescription: 'Calculate your monthly payments for unemployment insurance protection after graduation. Choose the level of annual income protection you want based on your needs.'
    }
  },
  {
    path: 'edusafe-output',
    component: ModelOuputComponent,
    data: {
      title: 'Edu$afe, Your Monthly Payment Results',
      metaDescription: 'View your monthly payment results for various levels of coverage. Send your results to yourself by email for future reference.'
    }
  },
  {
    path: 'edusafe-contact',
    component: ContactComponent,
    data: {
      title: 'Edu$afe, Contact Us',
      metaDescription: 'Contact Edu$afe, Inc. about how to setup a policy or any other questions you have. We would love to hear from you!'
    }
  },
  {
    path: 'edusafe-news',
    component: ArticlesComponent,
    data: {
      title: 'Edu$afe, Read Our Recommended Articles',
      metaDescription: 'Have a look at the articles Edu$afe recommends for more information about the problems we are trying to solve for new college graduates.'
    }
  },
  {
    path: 'portal-authentication',
    component: AuthenticationComponent,
    data: {
      title: 'Edu$afe, Portal Login',
    }
  },
  {
    path: 'institutional-profile',
    component: InstitutionalProfileComponent,
    data: {
      title: 'Edu$afe, Institional Customer Portal',
    }
  },
  {
    path: 'individual-profile',
    component: IndividualProfileComponent,
    data: {
      title: 'Edu$afe, Individual Customer Portal',
    }
  },
  {
    path: 'claims-inventory',
    component: ClaimsComponent,
    data: {
      title: 'Edu$afe, Claims',
    }
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes),
    BrowserAnimationsModule
  ],
  exports: [RouterModule]
})

export class AppRoutingModule {

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private titleService: Title,
    private metaService: Meta
  ) {
    this.router.events.pipe(
        filter(event => event instanceof NavigationEnd),
        map(() => this.activatedRoute),
        map(route => {
          while (route.firstChild) route = route.firstChild;
          return route;
        }),
        filter(route => route.outlet === 'primary'),
        mergeMap(route => route.data)
      )
      .subscribe((event) => {
        this.titleService.setTitle(event['title']);
        var tag = { name: 'description', content: event['metaDescription'] };
        let attributeSelector: string = 'name="description"';
        this.metaService.removeTag(attributeSelector);
        this.metaService.addTag(tag, false);
      });
  }
}
