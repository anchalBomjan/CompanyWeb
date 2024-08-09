import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NabBarComponent } from './nab-bar/nab-bar.component';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,NabBarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'web.UI';
}
