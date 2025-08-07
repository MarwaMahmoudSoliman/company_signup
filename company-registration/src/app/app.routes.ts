import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignUpComponent } from './modules/company/sign-up/sign-up.component';
import { HomeComponent } from './modules/company/home/home.component';
import { SetPasswordComponent } from './modules/company/set-password/set-password.component';
import { OtpComponent } from './modules/company/otp/otp.component';
import { LoginComponent } from './modules/company/login/login.component';

export const routes: Routes = [

  { path: '', redirectTo: 'signup', pathMatch: 'full' },
    { path: 'signup', component: SignUpComponent },
  { path: 'verify-otp', component: OtpComponent },
  { path: 'company/set-password', component: SetPasswordComponent },
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent }
,

  {
    path: '',
    loadChildren: () =>
      import('./modules/company/company-routing.module').then(m => m.CompanyRoutingModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
