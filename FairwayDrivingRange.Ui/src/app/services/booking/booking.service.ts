import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CalendarEvent } from 'angular-calendar';
import { Observable } from 'rxjs';
import { IAddBooking } from 'src/app/shared/interfaces/IAddBooking';
import { IGetBookings } from 'src/app/shared/interfaces/IGetBookings';

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  private url = "https://localhost:7250/api/"

  constructor(private http : HttpClient) { }

  getBookings(): Observable<IGetBookings> {
    return this.http.get<IGetBookings>(this.url + 'Booking');
  }

  addBooking(data: IAddBooking): Observable<any> {
    return this.http.post(`${this.url}Booking/email`, data)
  }
}
