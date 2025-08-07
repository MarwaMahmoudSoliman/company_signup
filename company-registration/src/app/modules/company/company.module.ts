import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { SignUpComponent } from './sign-up/sign-up.component';
import { SetPasswordComponent } from './set-password/set-password.component';
import { OtpComponent } from './otp/otp.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';


@@NgModule({
  imports: [
    SignUpComponent,
    SetPasswordComponent,
    
    OtpComponent,
    LoginComponent,
    HomeComponent
  ],
imports: [
    CommonModule, 
    ReactiveFormsModule,
    FormsModule,
    RouterModule
  ]
})


@NgModule({
  imports: [HttpClientModule],
})
export class CompanyModule { }

