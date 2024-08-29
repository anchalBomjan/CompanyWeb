import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IEmployee } from '../../interface/Employee';
import { EmployeeService } from '../../services/employee.service';

@Component({
  selector: 'app-employee-edit',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './employee-edit.component.html',
  styleUrls: ['./employee-edit.component.css']
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
    private router: Router,
    private toastr:ToastrService
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.employeeService.getEmployee(id).subscribe({
      next: (data) => {
        this.employee = data;
        this.employee.dateOfBirth = this.formatDate(this.employee.dateOfBirth);
        this.employee.hireDate = this.formatDate(this.employee.hireDate);


      },
      error: (error) => console.error('Error fetching employee', error),
    });
  }

  formatDate(dateString: string): string {
    if (!dateString) return '';
    const date = new Date(dateString);
    return date.toISOString().split('T')[0]; // Convert to "yyyy-MM-dd"
  }
  convertDateForBackend(dateString: string): string {
    if (!dateString) return '';
    const date = new Date(dateString);
    return date.toISOString();
  }



  onSubmit(): void {
    const id = this.employee.employeeId!;
    
    // Convert dates to backend-compatible format
    const updatedEmployee = {
      ...this.employee,
      dateOfBirth: this.convertDateForBackend(this.employee.dateOfBirth),
      hireDate: this.convertDateForBackend(this.employee.hireDate)
    };
  
    // Create FormData
    const formData = new FormData();
    formData.append('Name', updatedEmployee.name);
    formData.append('Email', updatedEmployee.email);
    formData.append('Phone', updatedEmployee.phone);
    formData.append('DateOfBirth', updatedEmployee.dateOfBirth);
    formData.append('Address', updatedEmployee.address);
    formData.append('HireDate', updatedEmployee.hireDate);
    
    // Append image only if present
    if (this.image) {
      formData.append('Image', this.image);
    }
  

    this.employeeService.updateEmployee(id, formData).subscribe({
      next: () => {
        this.toastr.success('Employee updated successfully', 'Success');
        this.router.navigate(['/app-hr/app-employee-list/']);
      },
      error: (error) => {
        console.error('Error updating employee', error);
        console.log('Error details:', error.error);
    
        this.toastr.error('Failed to update employee', 'Error');
      },
    });
    

  }
  

  onFileChange(event: any): void {
    if (event.target.files.length > 0) {
      this.image = event.target.files[0];
    }
  }
}

