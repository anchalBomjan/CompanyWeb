import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { IDepartmentWithDesignations } from '../../interface/DepartmentWithDesignations';
import { DepartmentService } from '../../services/department.service';

@Component({
  selector: 'app-departmentwith-designation',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './departmentwith-designation.component.html',
  styleUrl: './departmentwith-designation.component.css'
})
export class DepartmentwithDesignationComponent {
  departmentsWithDesignations: IDepartmentWithDesignations[] = [];

  constructor(private departmentService: DepartmentService) {
    this.onload();
  }

  onload():void
  {this.departmentService.getDepartmentsWithDesignations().subscribe({
    next: (data) => {
      this.departmentsWithDesignations = data;
    },
    error: (err) => {
      console.error('Error fetching departments with designations', err);
    }
  });
}
}
