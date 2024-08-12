import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ILoginRequest } from '../interface/LoginRequest';


import { IRegisterRequest } from '../interface/RegisterRequest';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  

  private apiUrl = 'https://localhost:44386/api/Auth'; // Updated base URL
  constructor(private http:HttpClient) { }


  register(registerRequest: IRegisterRequest): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/register`, registerRequest);
  }

   // Login method
  


   login(loginrequest: ILoginRequest): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login`, loginrequest);
  }

  // Error handling
 

}
