import { CommonModule } from '@angular/common';

import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { IRegisterRequest } from '../../interface/RegisterRequest';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [CommonModule,FormsModule ],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent {


  registerRequest:IRegisterRequest;

  
  
  constructor(private authService: AuthService) {
    this.registerRequest = {
      username: '',
      email: '',
      password: '',
      firstName: '',
      lastName: ''
    };
  }

  onSubmit() {
    this.authService.register(this.registerRequest).subscribe(
      response => {
        console.log('Registration successful', response);
        // Handle successful registration (e.g., navigate to login)
      }
    
    );
}
}
