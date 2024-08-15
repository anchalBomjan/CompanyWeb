import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { IDepartment } from '../../interface/Department';
import { DepartmentService } from '../../services/department.service';

@Component({
  selector: 'app-department-list',
  standalone: true,
  imports: [FormsModule,CommonModule,RouterModule],
  templateUrl: './department-list.component.html',
  styleUrl: './department-list.component.css'
})
export class DepartmentListComponent {
departments:IDepartment[]=[];

constructor(private departmentservice:DepartmentService,private router:Router)
{
  this.loadDepartments();
}
loadDepartments(): void {
  this.departmentservice.getDepartments().subscribe((data: IDepartment[]) => {
    this.departments = data;
  });
}

edit(id: number): void {

  
  this.router.navigate(['/Department/'+id]);


}

delete(id: number ): void {

  this.departmentservice.deleteDepartment(id).subscribe(() => {
    this.departments = this.departments.filter(dep => dep.departmentId !== id);
  });


}

}
