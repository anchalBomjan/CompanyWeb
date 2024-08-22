import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink, RouterModule } from '@angular/router';
import { IEmployee } from '../../interface/Employee';
import { EmployeeService } from '../../services/employee.service';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports: [CommonModule,FormsModule,RouterModule],
  templateUrl: './employee-list.component.html',
  styleUrl: './employee-list.component.css'
})
export class EmployeeListComponent {
  employees: IEmployee[] = [];

  constructor(private employeeService: EmployeeService,private router:Router)
   {
    this.loadEmployees();
   }

   loadEmployees(): void {
    this.employeeService.getAllEmployees().subscribe({
      next: (data: IEmployee[]) => this.employees = data,
      error: (error) => console.error('Error loading employees', error)
    });
  }

 

  editEmployee(id: number): void {
    // Navigate to edit page
    this.router.navigate(['/app-hr/app-employee-edit', id]);
  }

  deleteEmployee(id: number): void {
    if (confirm('Are you sure you want to delete this employee?')) {
      this.employeeService.deleteEmployee(id).subscribe({
        next: () => this.loadEmployees(),
        error: (error) => console.error('Error deleting employee', error)
      });
    }
  }
}
