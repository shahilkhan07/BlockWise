import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'sign-up', 
    loadComponent: () =>
      import('./signup/signup.component').then(
        m => m.SignupComponent
      )
  },
  { path: 'sign-in', 
    loadComponent: () =>
      import('./signin/signin.component').then(
        m => m.SigninComponent
      )
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule {}
