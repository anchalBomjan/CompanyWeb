import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ILoginRequest } from '../../interface/LoginRequest';
import { AuthService } from '../../services/auth.service';
import jwt_decode from 'jwt-decode';
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
constructor(private authservice: AuthService,private toastr:ToastrService,private route:Router ){
  this.loginrequest={
    username: '',
    password: ''
  };
}

togglePasswordVisibility() {
  this.passwordVisible = !this.passwordVisible;
}
 onSubmit(){
   this.authservice.login(this.loginrequest).subscribe(
     response=>{
      console.log('Login Response:', response);
      if(response.token) {
        this.toastr.success("Successfully Logged In");
        console.log('Token:', response.token);
      } else {
        this.toastr.error("Login failed. Token not found.");
      }
      

       


     }
     

   );
 }


}
