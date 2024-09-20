
import { Routes } from '@angular/router';
import { LogInComponent } from './Account/log-in/log-in.component';
import { RegistrationComponent } from './Account/registration/registration.component';
import { AdminComponent } from './admin/admin.component';
import { DepartmentFormComponent } from './admin/department-form/department-form.component';
import { DepartmentListComponent } from './admin/department-list/department-list.component';
import { DepartmentwithDesignationComponent } from './admin/departmentwith-designation/departmentwith-designation.component';
import { DesignationFormComponent } from './admin/designation-form/designation-form.component';
import { DesignationListComponent } from './admin/designation-list/designation-list.component';
import { RolesComponent } from './admin/roles/roles.component';

import { authGuard } from './guards/auth.guard';
import { EmployeeCreateComponent } from './hr/employee-create/employee-create.component';
import { EmployeeDetailFormComponent } from './hr/employee-detail-form/employee-detail-form.component';
import { EmployeeDetailListComponent } from './hr/employee-detail-list/employee-detail-list.component';
import { EmployeeEditComponent } from './hr/employee-edit/employee-edit.component';
import { EmployeeListComponent } from './hr/employee-list/employee-list.component';

import { HRComponent } from './hr/hr.component';
import { MessageThreadComponent } from './message-thread/message-thread.component';
import { MessageComponent } from './message/message.component';
import { UserComponent } from './user/user.component';

export const routes: Routes = [


{path: 'login', component: LogInComponent},
{path:'signup' , component:RegistrationComponent},


{
    path: 'app-admin',
    component: AdminComponent,
    canActivate: [authGuard],
    data: { role: 'Admin' }, // Admin role required
    children: [
      { path: 'app-roles', component: RolesComponent },
      { path: 'get-all-roles', component: RolesComponent }, // Example child route
      { path: 'get-all-users-with-roles', component: RolesComponent }, // Example child route
      { path: 'remove-roles-from-user-role', component: RolesComponent }, // Example child route
      { path: 'app-department-list', component: DepartmentListComponent },
      { path: 'Department/:id', component: DepartmentFormComponent }, // For editing a department
      { path: 'app-department-form', component: DepartmentFormComponent },
      { path: 'users', component: RolesComponent }, // Example child route for Users
      { path: 'settings', component: RolesComponent }, // Example child route for Settings
      { path: 'app-designation-list', component: DesignationListComponent }, // For listing all designations
      { path: 'app-designation-form/:id', component: DesignationFormComponent }, // For editing a designation
      { path: 'app-designation-form', component: DesignationFormComponent }, // For creating a new designation
     
      {path: 'app-dapartmentwith-designation',component:DepartmentwithDesignationComponent},
      {path:'app-message',component:MessageComponent},
      { path: 'app-message-thread/:username', component:MessageThreadComponent }

    ]
  },

 
{ 

  
   path:'app-hr',
   component:HRComponent,
   canActivate: [authGuard],
   data: { role: 'Hr' }, // HR role required
   children: [
      {path:'app-employee-create', component:EmployeeCreateComponent},
      {path:'app-employee-list',component:EmployeeListComponent},
      {path:'app-employee-edit/:id',component:EmployeeEditComponent},

      {path:'app-employee-detail-form',component:EmployeeDetailFormComponent},
   
      {path:'app-employee-detail-list',component:EmployeeDetailListComponent},
      { path: 'app-employee-detail-form/:id', component: EmployeeDetailFormComponent }, // Path for employee detail form with ID


      {path:'app-message',component:MessageComponent}, // Add this route for member profile navigation
    //  { path: 'members/:username', component: UserComponent },
       { path: 'app-message-thread/:username', component:MessageThreadComponent }



     ]   
   },
{path:'app-user',component:UserComponent},
];

