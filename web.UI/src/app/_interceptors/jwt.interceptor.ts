import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { take } from 'rxjs';
import { AuthService } from '../services/auth.service';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
    const authService = inject(AuthService); // Inject the AuthService

    // Subscribe to the currentUser$ observable to get the current user's token
    authService.currentUser$.pipe(take(1)).subscribe((currentUser) => {
        if (currentUser && currentUser.Token) {
            // Clone the request and set the Authorization header
            req = req.clone({
                setHeaders: {
                    Authorization: `Bearer ${currentUser.Token}`
                }
            });
        }
    });

    // Pass the request to the next handler
    return next(req);
};


