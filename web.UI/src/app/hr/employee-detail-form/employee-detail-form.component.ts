import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IDepartment } from '../../interface/Department';
import { IDesignation } from '../../interface/Designation';
import { IEmployee } from '../../interface/Employee';
import { IEmployeeDetailCreateDTO } from '../../interface/EmployeeDetailCreateDTO';
import { IEmployeeDetailDTO } from '../../interface/EmployeeDetailDTO';
import { DepartmentService } from '../../services/department.service';
import { DesignationService } from '../../services/designation.service';
import { EmployeeDetailService } from '../../services/employee-detail.service';
import { EmployeeService } from '../../services/employee.service';

@Component({
  selector: 'app-employee-detail-form',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './employee-detail-form.component.html',
  styleUrl: './employee-detail-form.component.css'
})
export class EmployeeDetailFormComponent {
  employeeDetail: IEmployeeDetailCreateDTO = { employeeId: 0, departmentId: 0, designationId: 0 };
  employees: IEmployee[] = [];
  departments: IDepartment[] = [];
  designations: IDesignation[] = [];

  constructor(
    private employeeService: EmployeeService,
    private departmentService: DepartmentService,
    private designationService: DesignationService,
    private employeeDetailService: EmployeeDetailService,
    private router: Router,
    private toastr: ToastrService
  ) {
    this.loadEmployees();
    this.loadDepartments();
  }

  loadEmployees(): void {
    this.employeeService.getAllEmployees().subscribe((employees: IEmployee[]) => {
      this.employees = employees;
    });
  }

  loadDepartments(): void {
    this.departmentService.getDepartments().subscribe((departments: IDepartment[]) => {
      this.departments = departments;
    });
  }

  loadDesignations(departmentId: number): void {
    this.designationService.getDesignationsByDepartment(departmentId).subscribe((designations: IDesignation[]) => {
      this.designations = designations;
    });
  }

  onDepartmentChange(event: Event): void {
    const selectedDepartmentId = (event.target as HTMLSelectElement).value;
    this.loadDesignations(Number(selectedDepartmentId));
  }

  onSubmit(): void {
    this.employeeDetailService.createEmployeeDetail(this.employeeDetail).subscribe(() => {
      this.toastr.success("Employee detail created successfully");
      this.router.navigate(['/app-admin/app-employee-detail-list']); // Adjust navigation after successful form submission
    });
  }
}

