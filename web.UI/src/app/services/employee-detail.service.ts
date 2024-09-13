import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environment/environment';
import { IEmployeeDetailCreateDTO } from '../interface/EmployeeDetailCreateDTO';
import { IEmployeeDetailDTO } from '../interface/EmployeeDetailDTO';
import { IEmployeeDetailUpdateDTO } from '../interface/EmployeeDetailUpdateDTO';

@Injectable({
  providedIn: 'root'
})
export class EmployeeDetailService {



 // private apiUrl = 'https://localhost:44386/api/EmployeeAssigningDetails'; // Adjust the URL based on your API


  private apiUrl=`${environment.apiUrl}EmployeeAssigningDetails`
  constructor(private http: HttpClient) { }
  // Create EmployeeDetail
 
   createEmployeeDetail(createDto: IEmployeeDetailCreateDTO): Observable<IEmployeeDetailDTO> {
    return this.http.post<IEmployeeDetailDTO>(`${this.apiUrl}`, createDto);
  }

  // Get all EmployeeDetails
  getAllEmployeeDetails(): Observable<IEmployeeDetailDTO[]> {
    return this.http.get<IEmployeeDetailDTO[]>(`${this.apiUrl}`);
  }



  getEmployeeDetailById(id: number): Observable<IEmployeeDetailDTO> {
    return this.http.get<IEmployeeDetailDTO>(`${this.apiUrl}/${id}`);
  }

  // Update EmployeeDetail
  updateEmployeeDetail(id: number, updateDto: IEmployeeDetailUpdateDTO): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, updateDto);
  }

  // Delete EmployeeDetail
  deleteEmployeeDetail(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
