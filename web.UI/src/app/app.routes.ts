
import { Routes } from '@angular/router';
import { LogInComponent } from './Account/log-in/log-in.component';
import { RegistrationComponent } from './Account/registration/registration.component';
import { AdminComponent } from './admin/admin.component';
import { RolesComponent } from './admin/roles/roles.component';

import { authGuard } from './guards/auth.guard';
import { HRComponent } from './hr/hr.component';
import { UserComponent } from './user/user.component';
import { DepartmentListComponent } from './admin/department-list/department-list.component';
import { DepartmentFormComponent } from './admin/department-form/department-form.component';
import { DesignationListComponent } from './admin/designation-list/designation-list.component';
import { DesignationFormComponent } from './admin/designation-form/designation-form.component';
import { DapartmentwithDesignationComponent } from './admin/dapartmentwith-designation/dapartmentwith-designation.component';


export const routes: Routes = [


{path: 'login', component: LogInComponent},
{path:'signup' , component:RegistrationComponent},


{
    path: 'app-admin',
    component: AdminComponent,
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
     {path: 'app-dapartmentwith-designation',component:DapartmentwithDesignationComponent}

    ]
  },
{path:'app-hr',component:HRComponent},
{path:'app-user',component:UserComponent},




];

