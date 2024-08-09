
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

import{ BsDropdownModule} from 'ngx-bootstrap/dropdown';

@Component({
  selector: 'app-nab-bar',
  standalone: true,
  imports: [BsDropdownModule,RouterLink],
  templateUrl: './nab-bar.component.html',
  styleUrl: './nab-bar.component.css'
})
export class NabBarComponent {

}
