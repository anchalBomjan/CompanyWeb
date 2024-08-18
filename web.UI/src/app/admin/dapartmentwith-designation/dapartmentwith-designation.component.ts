import { Component } from '@angular/core';
import { IDepartmentWithDesignations } from '../../interface/DepartmentWithDesignations';
import { DepartmentService } from '../../services/department.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-dapartmentwith-designation',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './dapartmentwith-designation.component.html',
  styleUrl: './dapartmentwith-designation.component.css'
})
export class DapartmentwithDesignationComponent {
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
