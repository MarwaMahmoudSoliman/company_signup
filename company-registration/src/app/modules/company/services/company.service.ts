import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  private apiUrl = 'http://localhost:5021/api/company'; 

  constructor(private http: HttpClient) { }

  registerCompany(formData: FormData): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, formData);
  }

  verifyOtp(email: string, otp: string) {
    return this.http.post(`${this.apiUrl}/verify-otp`, { email, otp });
  }

  setPassword(email: string, password: string) {
    return this.http.post(`${this.apiUrl}/set-password`, { email, password });
  }
  
  login(credentials: { email: string; password: string }) {
    return this.http.post(`${this.apiUrl}/login`, credentials);
  }
  getCurrentCompany() {
    return this.http.get(`${this.apiUrl}/me`);
  }
}