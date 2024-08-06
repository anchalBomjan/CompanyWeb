import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MatTabsModule } from '@angular/material/tabs'; // Import Angular Material modules
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes),
     provideAnimationsAsync(),
     MatTabsModule, // Include Angular Material modules here
    MatToolbarModule,
    MatButtonModule
    
    
    ]
};
