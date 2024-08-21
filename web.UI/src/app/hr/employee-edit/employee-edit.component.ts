import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { IEmployee } from '../../interface/Employee';
import { EmployeeService } from '../../services/employee.service';

@Component({
  selector: 'app-employee-edit',
  standalone: true,
  imports: [CommonModule,FormsModule,RouterModule],
  templateUrl: './employee-edit.component.html',
  styleUrl: './employee-edit.component.css'
})
export class EmployeeEditComponent {


  employee: IEmployee = {
    employeeId: 0,
    name: '',
    email: '',
    phone: '',
    dateOfBirth: '',
    address: '',
    hireDate: ''
  };
  image?: File;

  constructor(
    private employeeService: EmployeeService,
    private route: ActivatedRoute,
    private router: Router
  ) 
  { 
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.employeeService.getEmployee(id).subscribe(
      data => this.employee = data,
      error => console.error('Error fetching employee', error)
    );
  }

  

  onSubmit(): void {
    const id = this.employee.employeeId!;
    this.employeeService.updateEmployee(id, this.employee, this.image).subscribe(
      () => this.router.navigate(['/employees']),
      error => console.error('Error updating employee', error)
    );
  }

  onFileChange(event: any): void {
    if (event.target.files.length > 0) {
      this.image = event.target.files[0];
    }
  }
}
