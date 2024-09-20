import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {

  const authService = inject(AuthService); // Use `inject` to get the AuthService
  const router = inject(Router); // Use `inject` to get the Router
  const expectedRole = route.data['role']; // Get the expected role from route data
  const userRole = authService.getUserRole(); // Get the user's role from the token

  if (authService.isLoggedIn() && userRole === expectedRole) {
    return true; // Allow access if the role matches
  }

  // Redirect to the login page if unauthorized
  router.navigate(['login']);
  return false;

};
