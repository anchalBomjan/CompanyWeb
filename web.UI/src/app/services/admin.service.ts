import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { environment } from '../environment/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  // private apiUrl = 'https://localhost:44386/api/Admin';
  private apiUrl = `${environment.apiUrl}Admin`;
  constructor(private http:HttpClient) { }



  seedRoles(): Observable<any> {
    return this.http.post(`${this.apiUrl}/SeedRoles`, {});
  }

  assignRoleToUser(username: string, roleName: string): Observable<any> {
    let params = new HttpParams().set('username', username).set('roleName', roleName);
    return this.http.post(`${this.apiUrl}/AssignRole`, {}, { params });
  }

  getAllRoles(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/GetAllRoles`);
  }

  getAllUsersWithRoles(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/GetAllUsersWithRoles`);
  }
  removeRoleFromUser(username: string, roleName: string): Observable<any> {
    if (!username || !roleName) {
        console.error("Username and roleName are required.");
        return throwError(() => new Error("Username and roleName are required."));
    }
    
    let params = new HttpParams().set('username', username).set('roleName', roleName);
    return this.http.post(`${this.apiUrl}/RemoveRoleFromUser`, {}, { params });
}


}
