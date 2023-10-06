import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IApiResponse } from 'src/app/shared/interfaces/IApiResponse';

@Injectable({
  providedIn: 'root'
})
export class AuthentificationService {

  private url = "https://localhost:7250/api/Authentication/Login";

  constructor(private http: HttpClient) { }

  login(data: IApiResponse): Observable<any>{
    return this.http.post(`${this.url}`, data);
  }
}
