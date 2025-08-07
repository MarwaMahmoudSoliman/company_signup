import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule]
})
export class SignUpComponent {
  form: FormGroup;
  selectedLogoFile: File | null = null;
  logoPreview: string | null = null;

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router
  ) {
    this.form = this.fb.group({
      arabicName: ['', Validators.required],
      englishName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.pattern(/^(\+?\d{10,15})$/)]],
      website: [''],
      logo: [null]
    });
  }

  onLogoSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.selectedLogoFile = file;
      const reader = new FileReader();
      reader.onload = () => {
        this.logoPreview = reader.result as string;
      };
      reader.readAsDataURL(file);
    }
  }

  onSubmit() {
    if (this.form.invalid) return;

    const formData = new FormData();
    formData.append('arabicName', this.form.get('arabicName')?.value);
    formData.append('englishName', this.form.get('englishName')?.value);
    formData.append('email', this.form.get('email')?.value);
    formData.append('phone', this.form.get('phone')?.value || '');
    formData.append('website', this.form.get('website')?.value || '');

    if (this.selectedLogoFile) {
      formData.append('logo', this.selectedLogoFile);
    }

    this.http.post<any>('http://localhost:5021/api/company/register', formData).subscribe({
      next: res => {
        Swal.fire({
          icon: 'success',
          title: 'تم التسجيل بنجاح',
          html: ` تم إرسال رمز التحقق إلى بريدك الإلكتروني.<br><br><strong>OTP:</strong> ${res.otp}`,
          confirmButtonText: 'التحقق الآن'
        }).then(() => {
          this.router.navigate(['/verify-otp'], {
            queryParams: { email: this.form.get('email')?.value }
          });
        });

        this.form.reset();
        this.selectedLogoFile = null;
        this.logoPreview = null;
      },
      error: err => {
        if (err.status === 409) {
          Swal.fire({
            icon: 'error',
            title: 'خطأ',
            text: 'البريد الإلكتروني مستخدم بالفعل'
          });
        } else {
          Swal.fire({
            icon: 'error',
            title: 'فشل في التسجيل',
            text: err.message || 'حدث خطأ غير متوقع'
          });
        }
      }
    }
    );
  }
}