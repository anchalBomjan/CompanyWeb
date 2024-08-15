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
  department: IDepartment;
  departmentId !:number;
  isEdit: boolean = false;
  constructor(private http:HttpClient, private departmentservice:DepartmentService,private router:Router,private route:ActivatedRoute,private toastr:ToastrService)
  {
    this.department={
      name:"",
      description:""
    }
   this.departmentId=this.route.snapshot.params["id"];
   if (this.departmentId){
     this.isEdit=true;
     this.loadDepartment(this.departmentId)
   }
   
 
   }


  loadDepartment(id: number): void {
    this.departmentservice.getDepartment(id).subscribe(
      (department: IDepartment) => {
        this.department = department;
      }
    );
  }

  onSubmit(): void {
    if (this.isEdit) {
      this.departmentservice.updateDepartment(this.departmentId,this.department).subscribe({
        next:(response)=>{
          this.toastr.success("Employee Edit successfully");
          console.log('Employee Edit successfully', response);
        }
      });
    }
    else{
        this.departmentservice.createDepartment(this.department).subscribe({
          next:(response)=>{
            this.toastr.success("Department is create succesfully");

          },
          error: (error) => {
            console.error('Error adding employee', error);
          }



        });
    }

  } 
  

}
