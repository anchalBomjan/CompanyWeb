import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { IEmployee } from '../../interface/Employee';
import { EmployeeService } from '../../services/employee.service';

@Component({
  selector: 'app-employee',
  standalone: true,
  imports: [CommonModule,FormsModule,RouterModule],
  templateUrl: './employee.component.html',
  styleUrl: './employee.component.css'
})
export class EmployeeComponent {
  employees: IEmployee[] = [];
  selectedEmployee: IEmployee = {
    name: '',
    email: '',
    phone: '',
    dateOfBirth: '',
    address: '',
    hireDate: ''
  };
  selectedFile: File | null = null;

  constructor(private employeeService: EmployeeService) 
  {
    this.getAllEmployees();
  }
  // Get All Employees


   // Create Employee
   createEmployee() {
    if (this.selectedFile) {
      this.selectedEmployee.image = this.selectedFile;
    }
    this.employeeService.createEmployee(this.selectedEmployee).subscribe({
      next: (response) => {
        console.log('Employee created:', response);
        this.getAllEmployees(); // Refresh the list
      },
      error: (error) => {
        console.error('Error creating employee:', error);
      }
    });
  }

  // Read Employee
  getEmployee(id: number) {
    this.employeeService.getEmployee(id).subscribe({
      next: (response) => {
        this.selectedEmployee = response;
      },
      error: (error) => {
        console.error('Error fetching employee:', error);
      }
    });
  }

  // Update Employee
  updateEmployee(id: number) {
    if (this.selectedFile) {
      this.selectedEmployee.image = this.selectedFile;
    }
    this.employeeService.updateEmployee(id, this.selectedEmployee).subscribe({
      next: (response) => {
        console.log('Employee updated:', response);
        this.getAllEmployees(); // Refresh the list
      },
      error: (error) => {
        console.error('Error updating employee:', error);
      }
    });
  }

  // Delete Employee
  deleteEmployee(id: number) {
    this.employeeService.deleteEmployee(id).subscribe({
      next: () => {
        console.log('Employee deleted');
        this.getAllEmployees(); // Refresh the list
      },
      error: (error) => {
        console.error('Error deleting employee:', error);
      }
    });
  }
  getAllEmployees() {
    this.employeeService.getAllEmployees().subscribe({
      next: (response) => {
        this.employees = response;
      },
      error: (error) => {
        console.error('Error fetching employees:', error);
      }
    });
  }
 // Handle file selection
 onFileSelected(event: any) {
  this.selectedFile = event.target.files[0];
}
}
