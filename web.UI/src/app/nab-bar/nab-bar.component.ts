import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';

import{ BsDropdownModule} from 'ngx-bootstrap/dropdown';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-nab-bar',
  standalone: true,
  imports: [BsDropdownModule,RouterLink,CommonModule],
  templateUrl: './nab-bar.component.html',
  styleUrl: './nab-bar.component.css'
})
export class NabBarComponent {

  userRole:string |null=null;
 
  constructor(private authservice:AuthService,private router:Router){
    this.updateUserStatus();

  }
  updateUserStatus(): void {
    this.userRole = this.authservice.getUserRole();
  }

  isLoggedIn(): boolean {
    return this.authservice.isLoggedIn();
  }

  logout(): void {
    this.authservice.logout();
    this.router.navigate(['/login']);
  }
}