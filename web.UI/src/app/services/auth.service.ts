import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ILoginRequest } from '../interface/LoginRequest';


import { IRegisterRequest } from '../interface/RegisterRequest';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
  private tokenKey = 'authToken'; // Key for storing token in local storage
  private apiUrl = 'https://localhost:44386/api/Auth'; // Updated base URL
  constructor(private http:HttpClient) { }


  register(registerRequest: IRegisterRequest): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/register`, registerRequest);
  }

   // Login method
  


   login(loginrequest: ILoginRequest): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login`, loginrequest);
  }

  storeToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
  }


  isLoggedIn(): boolean {
    return !!localStorage.getItem(this.tokenKey);
  }
  // Error handling

   // Decode JWT token
   decodeToken(token: string): any {
    if (!token) {
      return null;
    }
    const payload = token.split('.')[1];
    return JSON.parse(atob(payload));
  }

  getUserRole(): string | null {
    const token = this.getToken();
    if (token) {
      const decodedToken = this.decodeToken(token);
      return decodedToken ? decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] : null;
    }
    return null;
  }
}
