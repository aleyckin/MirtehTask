import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EmployeeDto, EmployeeQueryParameters, EmployeeCreateOrUpdateDto } from '../models/employee.model';

@Injectable({ providedIn: 'root' })
export class EmployeeService {
  private baseUrl = 'http://localhost:5000/employees';

  constructor(private http: HttpClient) {}

  getAll(query: EmployeeQueryParameters): Observable<EmployeeDto[]> {
    let params = new HttpParams();

    Object.entries(query).forEach(([key, value]) => {
      if (value !== null && value !== undefined && value !== '') {
        params = params.set(key, String(value));
      }
    });

    return this.http.get<EmployeeDto[]>(this.baseUrl, { params });
  }

  create(employee: EmployeeCreateOrUpdateDto): Observable<any> {
    return this.http.post(this.baseUrl, employee);
  }

  update(id: string, employee: EmployeeCreateOrUpdateDto): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, employee);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}