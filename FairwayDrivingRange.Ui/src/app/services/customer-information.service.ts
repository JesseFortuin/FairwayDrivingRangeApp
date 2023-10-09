import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IApiResponse} from 'src/app/shared/interfaces/IApiResponse';
import { IAddBooking } from '../shared/interfaces/IAddBooking';

@Injectable({
  providedIn: 'root'
})
export class CustomerInformationService {
  private url = "https://localhost:7250/api/Customer/";

  private bookingUrl = "https://localhost:7250/api/Booking/";

  constructor(private http: HttpClient) { }

  register(data: IApiResponse): Observable<any>{
    return this.http.post(`${this.url}add`, data);
  }

  makeBooking(data: IAddBooking): Observable<any>{
    return this.http.post(`${this.bookingUrl}add`, data);
  }
}



