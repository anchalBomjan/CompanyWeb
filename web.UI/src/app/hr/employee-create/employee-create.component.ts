import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { IEmployee } from '../../interface/Employee';
import { EmployeeService } from '../../services/employee.service';

@Component({
  selector: 'app-employee-create',
  standalone: true,
  imports: [CommonModule,RouterModule,FormsModule],
  templateUrl: './employee-create.component.html',
  styleUrl: './employee-create.component.css'
})
export class EmployeeCreateComponent {
  employee: IEmployee = {
    name: '',
    email: '',
    phone: '',
    dateOfBirth: '',
    address: '',
    hireDate: ''
  };
  image?: File;

  constructor(private employeeService: EmployeeService, private router: Router) { }

  onSubmit(): void {
    this.employeeService.createEmployee(this.employee, this.image).subscribe(
      () => this.router.navigate(['/employees']),
      error => console.error('Error creating employee', error)
    );
  }

  onFileChange(event: any): void {
    if (event.target.files.length > 0) {
      this.image = event.target.files[0];
    }
  }


}
