import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IDesignation } from '../interface/Designation';

@Injectable({
  providedIn: 'root'
})
export class DesignationService {

  private apiUrl = 'https://localhost:44386/api/Designation'; // Adjust API URL as needed

  constructor(private http: HttpClient) {}

  getDesignations(): Observable<IDesignation[]> {
    return this.http.get<IDesignation[]>(this.apiUrl);
  }

  getDesignation(id: number): Observable<IDesignation> {
    return this.http.get<IDesignation>(`${this.apiUrl}/${id}`);
  }

  createDesignation(designation: IDesignation): Observable<IDesignation> {
    return this.http.post<IDesignation>(this.apiUrl, designation);
  }

  updateDesignation(id: number, designation: IDesignation): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, designation);
  }

  deleteDesignation(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getDesignationsByDepartment(departmentId: number): Observable<IDesignation[]> {
    return this.http.get<IDesignation[]>(`${this.apiUrl}/by-department/${departmentId}`);
  }

}
