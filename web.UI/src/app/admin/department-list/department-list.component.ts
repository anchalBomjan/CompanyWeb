import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { DepartmentService } from '../../services/department.service';
import { IDepartment } from '../../interface/Department';

@Component({
  selector: 'app-department-list',
  standalone: true,
  imports: [CommonModule,FormsModule,RouterModule],
  templateUrl: './department-list.component.html',
  styleUrl: './department-list.component.css'
})
export class DepartmentListComponent {
  departments:IDepartment[]=[];

 

  constructor(private departmentService: DepartmentService, private router: Router) {
    this.loadDepartments();
  }

  loadDepartments(): void {
    this.departmentService.getDepartments().subscribe((data: IDepartment[]) => {
      this.departments = data;
    });
  }

  edit(id: number): void {
    this.router.navigate(['/app-admin/Department', id]); // Navigate to edit form
  }

  delete(id: number): void {
    this.departmentService.deleteDepartment(id).subscribe(() => {
      this.departments = this.departments.filter(dep => dep.departmentId !== id);
    });
  }

  addDepartment(): void {
    this.router.navigate(['/app-admin/app-department-form']); // Navigate to add form
  }
}
