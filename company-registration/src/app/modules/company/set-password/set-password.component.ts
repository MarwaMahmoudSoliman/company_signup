import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl, ValidatorFn, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanyService } from '../services/company.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-set-password',
  templateUrl: './set-password.component.html',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule]
})
export class SetPasswordComponent {
  form: FormGroup;
  email!: string;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private companyService: CompanyService
  ) {
    this.email = this.route.snapshot.queryParamMap.get('email') || '';

    this.form = this.fb.group(
      {
        password: ['', [
          Validators.required,
          Validators.minLength(6),
          Validators.pattern(/^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$/)
        ]],
        confirmPassword: ['']
      },
      { validators: this.passwordsMatchValidator }
    );
  }

  passwordsMatchValidator: ValidatorFn = (group: AbstractControl) => {
    const password = group.get('password')?.value;
    const confirm = group.get('confirmPassword')?.value;
    return password === confirm ? null : { notMatching: true };
  };

  onSubmit() {
    if (this.form.invalid) return;

    const password = this.form.get('password')?.value;

    this.companyService.setPassword(this.email, password).subscribe({
      next: () => {
        alert('Password set successfully');
        this.router.navigate(['/login']);
      },
      error: () => {
        alert('Something went wrong');
      }
    });
  }
}
