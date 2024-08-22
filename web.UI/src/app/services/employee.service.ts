import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IEmployee } from '../interface/Employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
private baseUrl='https://localhost:44386/api/Employee';
  constructor(private http:HttpClient) { }

   // Create Employee
   createEmployee(employee: IEmployee, image?: File): Observable<IEmployee> {
    const formData: FormData = new FormData();
    formData.append('Name', employee.name);
    formData.append('Email', employee.email);
    formData.append('Phone', employee.phone);
    formData.append('DateOfBirth', employee.dateOfBirth);
    formData.append('Address', employee.address);
    formData.append('HireDate', employee.hireDate);

    if (image) {
      formData.append('Image', image);
    }

    return this.http.post<IEmployee>(this.baseUrl, formData);
  }

  // Get Employee by ID
  getEmployee(id: number): Observable<IEmployee> {
    return this.http.get<IEmployee>(`${this.baseUrl}/${id}`);
  }

  // updateEmployee(id: number, employee: IEmployee, image?: File): Observable<IEmployee> {
  //   const formData = new FormData();
  //   formData.append('Name', employee.name);
  //   formData.append('Email', employee.email);
  //   formData.append('Phone', employee.phone);
  //   formData.append('DateOfBirth', employee.dateOfBirth);
  //   formData.append('Address', employee.address);
  //   formData.append('HireDate', employee.hireDate);
  
  //   if (image) {
  //     formData.append('Image', image);
  //   }
  
  //   return this.http.put<IEmployee>(`${this.baseUrl}/${id}`, formData);
  // }
  
  // Update Employee
  updateEmployee(id: number, formData: FormData): Observable<IEmployee> {
    return this.http.put<IEmployee>(`${this.baseUrl}/${id}`, formData);
  }


  // Delete Employee
  deleteEmployee(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  // Get All Employees
  getAllEmployees(): Observable<IEmployee[]> {
    return this.http.get<IEmployee[]>(this.baseUrl);
  }

}
