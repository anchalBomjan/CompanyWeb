
import { Routes } from '@angular/router';
import { LogInComponent } from './Account/log-in/log-in.component';
import { RegistrationComponent } from './Account/registration/registration.component';
import { AdminComponent } from './admin/admin.component';
import { HRComponent } from './hr/hr.component';
import { UserComponent } from './user/user.component';

export const routes: Routes = [


{path: 'login', component: LogInComponent},
{path:'signup' , component:RegistrationComponent},
{path: 'app-hr', component: HRComponent},

{path:'app-admin',component:AdminComponent},
{path:'app-user',component:UserComponent}




    
];

