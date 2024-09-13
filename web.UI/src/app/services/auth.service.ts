import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { ILoginRequest } from '../interface/LoginRequest';
import { IRegisterRequest } from '../interface/RegisterRequest';
import { IUser } from '../interface/User';
import { environment } from '../environment/environment';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
  private tokenKey = 'authToken'; // Key for storing token in local storage
  // private apiUrl = 'https://localhost:44386/api/Auth'; // Updated base URL
  private apiUrl=`${environment.apiUrl}Auth`;
 
   currentUserSubject: BehaviorSubject<IUser | null>;
  public currentUser$: Observable<IUser | null>;

  constructor(private http: HttpClient) {
    const token = this.getToken();
    const user = token ? this.createUserFromToken(token) : null;
    this.currentUserSubject = new BehaviorSubject<IUser | null>(user);
    this.currentUser$ = this.currentUserSubject.asObservable();
  }

  register(registerRequest: IRegisterRequest): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/register`, registerRequest);
  }

  login(loginRequest: ILoginRequest): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login`, loginRequest);
  }

  storeToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
    const user = this.createUserFromToken(token);
    this.currentUserSubject.next(user);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    this.currentUserSubject.next(null);
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

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

  // Create a User object from the token
  private createUserFromToken(token: string): IUser {
    const decodedToken = this.decodeToken(token);
    return {
      Username: decodedToken.unique_name,
      Email: decodedToken.email,
      Token: token,
      Role: decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
    };
  }
}
