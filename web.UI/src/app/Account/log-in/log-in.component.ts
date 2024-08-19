import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ILoginRequest } from '../../interface/LoginRequest';
import { AuthService } from '../../services/auth.service';

import { Router } from '@angular/router';

@Component({
  selector: 'app-log-in',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './log-in.component.html',
  styleUrl: './log-in.component.css'
})
export class LogInComponent {

loginrequest:ILoginRequest;


passwordVisible = false;
constructor(private authservice: AuthService,private toastr:ToastrService,private router:Router ){
  this.loginrequest={
    username: '',
    password: ''
  };
}

togglePasswordVisibility() {
  this.passwordVisible = !this.passwordVisible;
}


onSubmit() {
  this.authservice.login(this.loginrequest).subscribe({
    next: (response) => {
      console.log('Login Response:', response);
      if (response.token) {
        this.authservice.storeToken(response.token); // Store the token
        this.toastr.success('Successfully Logged In');
        console.log('Token:', response.token);

        const userRole = this.authservice.decodeToken(response.token)["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];


        if (userRole === 'Admin') {
          this.router.navigate(['/app-admin']);
        } else if (userRole === 'Hr') {
          this.router.navigate(['/app-hr']);
        } else if (userRole === 'User') {
          this.router.navigate(['/app-user']);
        } else {
          this.toastr.error('Role not recognized. Access denied.');
        }
      } else {
        this.toastr.error('Login failed. Token not found.');
      }
    },
    error: (error) => {
      console.error('Login error:', error);
      this.toastr.error('Login failed. Please check your credentials.');
    },
    complete: () => {
      console.log('Login request completed');
    }
  });
}



}


