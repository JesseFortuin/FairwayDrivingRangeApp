import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IApiResponse} from 'src/app/shared/interfaces/IApiResponse';

@Injectable({
  providedIn: 'root'
})
export class CustomerInformationService {
  private url = "https://localhost:7250/api/Customer/";

  constructor(private http: HttpClient) { }

  register(data: IApiResponse): Observable<any>{
    return this.http.post(`${this.url}add`, data);
  }

  login(): Observable<any>{

  let queryParams = new HttpParams().set('email', '');

    return this.http.get(`${this.url}email`, {params: queryParams})
  }
}



