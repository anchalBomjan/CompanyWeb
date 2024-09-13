
import { HttpClient, HttpParams } from "@angular/common/http";
import { map } from 'rxjs/operators';
import { PaginatedResult } from "../interface/pagination";
import { environment } from '../environment/environment'; // Import your environment

export function getPaginatedResult<T>(endpoint: string, params: HttpParams, http: HttpClient) {
  const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();

 
  const url = endpoint;

  return http.get<T>(url, { observe: 'response', params }).pipe(
    map(response => {
      
     // paginatedResult.result!= response.body;

      paginatedResult.result = response.body;
      const paginationHeader = response.headers.get('Pagination');
      if (paginationHeader) {
        paginatedResult.pagination = JSON.parse(paginationHeader);
      }
      return paginatedResult;
    })
  );
}

export function getPaginationHeaders(pageNumber: number, pageSize: number) {
  let params = new HttpParams();

  params = params.append('pageNumber', pageNumber.toString());
  params = params.append('pageSize', pageSize.toString());

  return params;
}


