import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CalendarEvent } from 'angular-calendar';
import { Observable } from 'rxjs';
import { IBooking } from 'src/assets/IBooking';
import { IEvent } from 'src/assets/IEvent';
import { IResponse } from 'src/assets/IResponse';

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  private url = "https://localhost:7250/api/"

  constructor(private http : HttpClient) { }

  getBookings(): Observable<IBooking> {
    return this.http.get<IBooking>(this.url + 'Booking');
  }

  addBooking(data: CalendarEvent): Observable<any> {
    return this.http.post(`${this.url}Booking/email`, data)
  }
}
