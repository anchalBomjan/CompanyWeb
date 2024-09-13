import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter, RouterModule } from '@angular/router';
import { BrowserAnimationsModule, provideAnimations } from '@angular/platform-browser/animations';


import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { jwtInterceptor } from './_interceptors/jwt.interceptor';
import { TimeagoModule } from 'ngx-timeago';


export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(),
    provideAnimations(),

    
    provideHttpClient(withInterceptors([jwtInterceptor])),
    importProvidersFrom(
      BrowserAnimationsModule,
      RouterModule, // Ensure RouterModule is provided
      ToastrModule.forRoot(),
      TimeagoModule.forRoot(),
     
     
    ),

  
  ],
};
