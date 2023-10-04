import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IResponse} from 'src/assets/IResponse';

@Injectable({
  providedIn: 'root'
})
export class CustomerInformationService {
  private url = "https://localhost:7250/api/Customer/";

  constructor(private http: HttpClient) { }

  register(data: IResponse): Observable<any>{
    return this.http.post(`${this.url}add`, data);
  }

  login(date: string): Observable<any>{

    return this.http.get(`${this.url}email`)
  }
}
