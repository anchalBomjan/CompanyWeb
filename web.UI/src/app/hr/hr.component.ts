import { Component } from '@angular/core';

import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatTabsModule } from '@angular/material/tabs'; // Import MatTabsModule
import { FormsModule } from '@angular/forms';

import { AssignJobComponent } from './assign-job/assign-job.component';
import { CreateEmployeeComponent } from './create-employee/create-employee.component';

import { EmployeeListComponent } from './employee-list/employee-list.component';
import { EmployeeDetailComponent } from './employee-detail/employee-detail.component';

@Component({
  selector: 'app-hr',
  standalone: true,
  imports: [MatButtonModule,
        MatInputModule,
        FormsModule,
        CommonModule,
        MatTabsModule,
        CreateEmployeeComponent,
        AssignJobComponent,
        EmployeeListComponent,
        EmployeeDetailComponent


  
  
  
  
  ],
  templateUrl: './hr.component.html',
  styleUrl: './hr.component.css'
})
export class HRComponent {

}
