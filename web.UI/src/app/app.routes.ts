
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
      
    ]
  },
{path:'app-hr',component:HRComponent},
{path:'app-user',component:UserComponent},


// { path: 'app-hr', component: HRComponent, canActivate: [authGuard], data: { roles: ['Hr'] } },
// { path: 'app-admin', component: AdminComponent, canActivate: [authGuard], data: { roles: ['Admin'] } },
// { path: 'app-user', component: UserComponent, canActivate: [authGuard], data: { roles: ['User'] } },

];

