
import { Routes } from '@angular/router';
import { LogInComponent } from './Account/log-in/log-in.component';
import { RegistrationComponent } from './Account/registration/registration.component';
import { AdminComponent } from './admin/admin.component';
import { authGuard } from './guards/auth.guard';
import { HRComponent } from './hr/hr.component';
import { UserComponent } from './user/user.component';

export const routes: Routes = [


{path: 'login', component: LogInComponent},
{path:'signup' , component:RegistrationComponent},

// { path: 'app-hr', component: HRComponent, canActivate: [authGuard], data: { roles: ['Hr'] } },
// { path: 'app-admin', component: AdminComponent, canActivate: [authGuard], data: { roles: ['Admin'] } },
// { path: 'app-user', component: UserComponent, canActivate: [authGuard], data: { roles: ['User'] } },
{path:'app-admin',component:AdminComponent},
{path:'app-hr',component:HRComponent},
{path:'app-user',component:UserComponent},
  
];

