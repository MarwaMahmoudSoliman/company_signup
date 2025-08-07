import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CompanyService } from '../services/company.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
    imports: [CommonModule]

})
export class HomeComponent implements OnInit {
  company: any;

  constructor(private companyService: CompanyService, private router: Router) {}

  ngOnInit() {
    this.companyService.getCurrentCompany().subscribe({
      next: (res) => {
        this.company = res;
      },
      error: () => {
        alert('Unauthorized. Please login again.');
        this.router.navigate(['/login']);
      }
    });
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
}
