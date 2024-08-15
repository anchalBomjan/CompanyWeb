import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IDepartment } from '../interface/Department';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
private baseUrl ='https://localhost:44386/api/Department';
  constructor(private http:HttpClient) { }

  getDepartments(): Observable<IDepartment[]> {
    return this.http.get<IDepartment[]>(this.baseUrl);
  }

  getDepartment(id: number): Observable<IDepartment> {
    return this.http.get<IDepartment>(`${this.baseUrl}/${id}`);
  }

  createDepartment(department: IDepartment): Observable<IDepartment> {
    return this.http.post<IDepartment>(this.baseUrl, department);
  }

  updateDepartment(id: number, department: IDepartment): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, department);
  }

  deleteDepartment(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

}
