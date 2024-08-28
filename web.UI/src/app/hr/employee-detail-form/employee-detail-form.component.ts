
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { IDepartment } from '../../interface/Department';
import { IDesignation } from '../../interface/Designation';
import { IEmployee } from '../../interface/Employee';
import { IEmployeeDetailCreateDTO } from '../../interface/EmployeeDetailCreateDTO';
import { IEmployeeDetailDTO } from '../../interface/EmployeeDetailDTO';
import { EmployeeDetailService } from '../../services/employee-detail.service';
import { DepartmentService } from '../../services/department.service';
import { DesignationService } from '../../services/designation.service';
import { EmployeeService } from '../../services/employee.service';
import { IEmployeeDetailUpdateDTO } from '../../interface/EmployeeDetailUpdateDTO';

@Component({
  selector: 'app-employee-detail-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './employee-detail-form.component.html',
  styleUrls: ['./employee-detail-form.component.css']
})
export class EmployeeDetailFormComponent {
  employeeDetail: IEmployeeDetailCreateDTO = { employeeId: 0, departmentId: 0, designationId: 0 };
  employees: IEmployee[] = [];
  departments: IDepartment[] = [];
  designations: IDesignation[] = [];
  employeeDetailId!: number;
  isEdit: boolean = false;

  constructor(
    private employeeService: EmployeeService,
    private departmentService: DepartmentService,
    private designationService: DesignationService,
    private employeeDetailService: EmployeeDetailService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) {
    this.employeeDetailId = this.route.snapshot.params['id'];
    this.initializeComponent();
  }

  initializeComponent(): void {
    if (this.employeeDetailId) {
      this.isEdit = true;
      this.loadEmployeeDetail(this.employeeDetailId);
    } else {
      this.loadEmployees();
      this.loadDepartments();
    }
  }


  cancel() {
    this.router.navigate(['/app-hr/app-employee-detail-list']);
  }
 
  //this  line of code helps to finds the all attributes of ID  and helps to map  ID with  name 
  loadEmployeeDetail(id: number): void {
    this.employeeDetailService.getEmployeeDetailById(id).subscribe({
      next: (employeeDetail: IEmployeeDetailDTO) => {
        // Load employees list
        this.loadEmployees(() => {
          // Find the employeeId using employeeName or any unique property
          const employee = this.employees.find(emp => emp.name === employeeDetail.employeeName); // Adjust employeeName if necessary
          const employeeId = employee?.employeeId ?? 0;
  
          // Load departments list
          this.loadDepartments(() => {
            // Find the departmentId using departmentName
            const department = this.departments.find(dept => dept.name === employeeDetail.departmentName);
            const departmentId = department?.departmentId ?? 0;
  
            // Load designations for the selected department
            this.loadDesignations(departmentId, () => {
              // Find the designationId using designationTitle
              const designation = this.designations.find(des => des.title === employeeDetail.designationTitle);
              const designationId = designation?.designationId ?? 0;
  
              // Populate employeeDetail object with all found ids
              this.employeeDetail = {
                employeeId: employeeId,  // Set the found employeeId
                departmentId: departmentId,  // Set the found departmentId
                designationId: designationId  // Set the found designationId
              };
            });
          });
        });
      },
      error: (error) => {
        console.error('Error fetching employee detail:', error);
        this.toastr.error("Error fetching employee detail");
      }
    });
  }


  loadEmployees(callback?: () => void): void {
    this.employeeService.getAllEmployees().subscribe({
      next: (employees: IEmployee[]) => {
        this.employees = employees;
        if (callback) callback();
      },
      error: (error) => {
        console.error('Error fetching employees:', error);
        this.toastr.error("Error fetching employees");
      }
    });
  }
  
  loadDepartments(callback?: () => void): void {
    this.departmentService.getDepartments().subscribe({
      next: (departments: IDepartment[]) => {
        this.departments = departments;
        if (callback) callback();
      },
      error: (error) => {
        console.error('Error fetching departments:', error);
        this.toastr.error("Error fetching departments");
      }
    });
  }
  
  loadDesignations(departmentId: number, callback?: () => void): void {
    this.designationService.getDesignationsByDepartment(departmentId).subscribe({
      next: (designations: IDesignation[]) => {
        this.designations = designations;
        if (callback) callback();
      },
      error: (error) => {
        console.error('Error fetching designations:', error);
        this.toastr.error("Error fetching designations");
      }
    });
  }
  
  onDepartmentChange(event: Event): void {
    const selectedDepartmentId = (event.target as HTMLSelectElement).value;
    this.loadDesignations(Number(selectedDepartmentId));
  }

  onSubmit(): void {
    if (this.isEdit) {
      const updateData: IEmployeeDetailUpdateDTO = {
        employeeDetailId: this.employeeDetailId,
        employeeId: this.employeeDetail.employeeId,
        departmentId: this.employeeDetail.departmentId,
        designationId: this.employeeDetail.designationId
        
      };
      
      console.log('Update Data:', updateData);
      
      this.employeeDetailService.updateEmployeeDetail(this.employeeDetailId, updateData).subscribe({
        next: () => {
          this.toastr.success("Employee detail edited successfully");
          this.router.navigate(['/app-hr/app-employee-detail-list']);
        },
        error: (error) => {
          console.error('Error updating employee detail:', error);
          this.toastr.error("Error updating employee detail");
        }
      });
    }
    else {
      this.employeeDetailService.createEmployeeDetail(this.employeeDetail).subscribe({
        next: () => {
          this.toastr.success("Employee detail created successfully");
          this.router.navigate(['/app-hr/app-employee-detail-list']);
        },
        error: (error) => {
          console.error('Error creating employee detail:', error);
          this.toastr.error("Error creating employee detail");
        }
      });
    }
  }
}
