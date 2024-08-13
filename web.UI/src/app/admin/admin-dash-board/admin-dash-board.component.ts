import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@Component({
  selector: 'app-admin-dash-board',
  standalone: true,
  imports: [CommonModule,BsDropdownModule],
  templateUrl: './admin-dash-board.component.html',
  styleUrl: './admin-dash-board.component.css'
})
export class AdminDashBoardComponent {

}
