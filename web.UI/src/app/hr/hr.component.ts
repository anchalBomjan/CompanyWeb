import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { MessageService } from '../services/message.service';

@Component({
  selector: 'app-hr',
  standalone: true,
  imports: [CommonModule,RouterModule,BsDropdownModule,FormsModule],
  templateUrl: './hr.component.html',
  styleUrl: './hr.component.css'
})
export class HRComponent {
 

}
