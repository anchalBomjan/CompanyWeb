import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IDepartment } from '../../interface/Department';
import { DepartmentService } from '../../services/department.service';

@Component({
  selector: 'app-department-form',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './department-form.component.html',
  styleUrl: './department-form.component.css'
})
export class DepartmentFormComponent {

  department: IDepartment = { name: "", description: "" };
  departmentId!: number;
  isEdit: boolean = false;

  constructor(
    private departmentService: DepartmentService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) {
    this.departmentId = this.route.snapshot.params["id"];
    if (this.departmentId) {
      this.isEdit = true;
      this.loadDepartment(this.departmentId);
    }
  }

  loadDepartment(id: number): void {
    this.departmentService.getDepartment(id).subscribe((department: IDepartment) => {
      this.department = department;
    });
  }

  onSubmit(): void {
    if (this.isEdit) {
      this.departmentService.updateDepartment(this.departmentId, this.department).subscribe(() => {
        this.toastr.success("Department edited successfully");
        this.router.navigate(['/app-admin/app-department-list']);
      });
    } else {
      this.departmentService.createDepartment(this.department).subscribe(() => {
        this.toastr.success("Department created successfully");
        this.router.navigate(['/app-admin/app-department-list']);
      });
    }
  } 

}
