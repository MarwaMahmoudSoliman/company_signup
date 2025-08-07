import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CompanyService } from '../services/company.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-otp',
  templateUrl: './otp.component.html',
  styleUrl:'./otp.component.css',
    imports: [CommonModule, ReactiveFormsModule, FormsModule]
})
export class OtpComponent {
  form: FormGroup;

  constructor(private fb: FormBuilder, private companyService: CompanyService, private router: Router) {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      otp: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.form.invalid) return;

    const data = this.form.value;

    this.companyService.verifyOtp(data.email, data.otp).subscribe({
      next: (res) => {
        alert('OTP Verified Successfully');
        this.router.navigate(['/company/set-password'], { queryParams: { email: data.email } });
      },
      error: (err) => {
        alert('Invalid OTP');
      }
    });
  }
}
