import { CommonModule, DatePipe } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { IEmployee } from '../../interface/Employee';
import { EmployeeService } from '../../services/employee.service';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-employee-create',
  standalone: true,
  imports: [CommonModule,RouterModule,FormsModule,BsDatepickerModule],
  providers: [DatePipe], // Provide DatePipe here

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
  

  constructor(private employeeService: EmployeeService, private router: Router,private datePipe:DatePipe,private toastr: ToastrService) { }
  onSubmit(): void {




     // Format dates before sending to the backend  usingDatePipe Packages 
     if (this.employee.dateOfBirth) {
      this.employee.dateOfBirth = this.datePipe.transform(this.employee.dateOfBirth, 'yyyy-MM-dd') || '';
    }
    if (this.employee.hireDate) {
      this.employee.hireDate = this.datePipe.transform(this.employee.hireDate, 'yyyy-MM-dd') || '';
    }
    this.employeeService.createEmployee(this.employee, this.image).subscribe({
      next: () => {
        this.toastr.success("The employee registration was successful");
        this.router.navigate(['/app-hr/app-employee-list']);
      },
    error: (error) => {
        console.error('Error creating employee', error);
        alert('An error occurred: ' + (error.message || error.statusText));
      }
      
    });
    
  }



  onFileChange(event: any): void {
    if (event.target.files.length > 0) {
      this.image = event.target.files[0];
    }
  }


  
  cancel() {
    this.router.navigate(['/app-hr/app-employee-list']);
  }

}




