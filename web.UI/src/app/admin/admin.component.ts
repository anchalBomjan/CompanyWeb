import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { AdminDashBoardComponent } from './admin-dash-board/admin-dash-board.component';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule,BsDropdownModule,AdminDashBoardComponent],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css'
})
export class AdminComponent {

}
