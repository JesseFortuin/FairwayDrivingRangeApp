import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IResponse} from 'src/assets/IResponse';

@Injectable({
  providedIn: 'root'
})
export class CustomerInformationService {
  private url = "https://localhost:7250/api/Customer/add";

  constructor(private http: HttpClient) { }

  register(data: IResponse): Observable<any>{
    return this.http.post(`${this.url}`, data);
  }
}
