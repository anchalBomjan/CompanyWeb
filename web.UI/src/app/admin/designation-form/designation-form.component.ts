import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IDepartment } from '../../interface/Department';
import { IDesignation } from '../../interface/Designation';
import { DepartmentService } from '../../services/department.service';
import { DesignationService } from '../../services/designation.service';

@Component({
  selector: 'app-designation-form',
  standalone: true,
  imports: [CommonModule,FormsModule,RouterModule],
  templateUrl: './designation-form.component.html',
  styleUrl: './designation-form.component.css'
})
export class DesignationFormComponent {
  designation: IDesignation = { title: "", salary: 0, description: "", departmentId: 0 };
  departments: IDepartment[] = []; // Add this property to hold departments
  designationId!: number;
  isEdit: boolean = false;

  constructor(
    private designationService: DesignationService,
    private departmentService: DepartmentService, // Update service name
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) {

    this.route.paramMap.subscribe(params => {
      this.designationId = +params.get('id')!; // Capture the ID from the route parameters if it exists
      this.onload();
    });
  }

  onload(): void {
    this.loadDepartments(); // Fetch departments on initialization
    if (this.designationId) {
      this.isEdit = true;
      this.loadDesignation(this.designationId);
    }
  }
  
  loadDepartments(): void {
    this.departmentService.getDepartments().subscribe((departments: IDepartment[]) => {
      this.departments = departments;
    });
  }

  loadDesignation(id: number): void {
    this.designationService.getDesignation(id).subscribe((designation: IDesignation) => {
      this.designation = designation;
    });
  }
  onSubmit(): void {
    if (this.isEdit) {
      this.designationService.updateDesignation(this.designationId, this.designation).subscribe(() => {
        this.toastr.success("Designation edited successfully");
        this.router.navigate(['/app-admin/app-designation-list']); // Adjust navigation
      });
    } else {
      this.designationService.createDesignation(this.designation).subscribe(() => {
        this.toastr.success("Designation created successfully");
        this.router.navigate(['/app-admin/app-designation-list']); // Adjust navigation
      });
    }
  }
  
}
