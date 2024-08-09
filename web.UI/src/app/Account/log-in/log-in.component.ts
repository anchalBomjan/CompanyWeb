import { Component } from '@angular/core';
import { ILoginRequest } from '../../interface/LoginRequest';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-log-in',
  standalone: true,
  imports: [],
  templateUrl: './log-in.component.html',
  styleUrl: './log-in.component.css'
})
export class LogInComponent {

loginrequest:ILoginRequest;

constructor(private authservice: AuthService){
  this.loginrequest={
    username: '',
    password: ''
  };
}
 onSubmit(){
   this.authservice.login(this.loginrequest).subscribe(
     response=>{

      console.log("successfully")
     }


   )
 }


}
