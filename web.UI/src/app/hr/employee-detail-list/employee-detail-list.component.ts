import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IEmployeeDetailDTO } from '../../interface/EmployeeDetailDTO';
import { EmployeeDetailService } from '../../services/employee-detail.service';

@Component({
  selector: 'app-employee-detail-list',
  standalone: true,
  imports: [CommonModule,FormsModule,RouterModule],
  templateUrl: './employee-detail-list.component.html',
  styleUrl: './employee-detail-list.component.css'
})
export class EmployeeDetailListComponent {

employeeDetails:IEmployeeDetailDTO[]=[];
constructor(private employeeDetailService:EmployeeDetailService,private router:Router,private toastr:ToastrService)
{
  this.loadEmployeeDetails();
}
loadEmployeeDetails(): void {
  this.employeeDetailService. getAllEmployeeDetails().subscribe((details: IEmployeeDetailDTO[]) => {
    this.employeeDetails = details;
  });
}

editEmployeeDetail(id: number): void {
  this.router.navigate(['/app-hr/app-employee-detail-form', id]);
}


deleteEmployeeDetail(id: number): void {
  if (confirm('Are you sure you want to delete this employee detail?')) {
    this.employeeDetailService.deleteEmployeeDetail(id).subscribe({
      next: () => {
        this.toastr.success('Employee detail deleted successfully');
        this.loadEmployeeDetails();
      },
      error: (err) => {
        this.toastr.error('Failed to delete employee detail');
      }
    });
  }
}


}
