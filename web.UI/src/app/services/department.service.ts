import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environment/environment';
import { IDepartment } from '../interface/Department';
import { IDepartmentWithDesignations } from '../interface/DepartmentWithDesignations';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
 //private apiUrl ='https://localhost:44386/api/Department';




private apiUrl = `${environment.apiUrl}Department`;
  constructor(private http:HttpClient) { }

  getDepartments(): Observable<IDepartment[]> {
    return this.http.get<IDepartment[]>(this.apiUrl);
  }

  getDepartment(id: number): Observable<IDepartment> {
    return this.http.get<IDepartment>(`${this.apiUrl}/${id}`);
  }

  createDepartment(department: IDepartment): Observable<IDepartment> {
    return this.http.post<IDepartment>(this.apiUrl, department);
  }

  updateDepartment(id: number, department: IDepartment): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, department);
  }

  deleteDepartment(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }


  getDepartmentsWithDesignations(): Observable<IDepartmentWithDesignations[]> {
    return this.http.get<IDepartmentWithDesignations[]>(`${this.apiUrl}/GetDepartmentsWithDesignations`);
  }
}
