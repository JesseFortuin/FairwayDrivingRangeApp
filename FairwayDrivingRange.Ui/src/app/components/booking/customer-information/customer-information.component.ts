import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CalendarEvent } from 'angular-calendar';
import { CustomerInformationService } from 'src/app/services/customer-information.service';
import { IAddBooking } from 'src/app/shared/interfaces/IAddBooking';
import { IApiResponse } from 'src/app/shared/interfaces/IApiResponse';

@Component({
  selector: 'app-register-customer',
  templateUrl: './customer-information.component.html',
  styleUrls: ['./customer-information.component.css']
})
export class CustomerInformationComponent {
  constructor(private infoService: CustomerInformationService,
              public router : Router) {}

  customerObj: any = {
    name: '',
    email: '',
    phone: 0
  }

  booking: CalendarEvent = JSON.parse(sessionStorage.getItem('booking')!);

    bookingObj: IAddBooking = {
    start: this.booking.start,
    end: this.booking.end!,
    name: '',
    email: '',
    phone: 0,
    golfClubsForHire: []
  }

  // registerProcess() {
  //   this.bookingObj.name = this.customerObj.name;

  //   this.bookingObj.email = this.customerObj.email;

  //   this.bookingObj.phone = this.customerObj.phone;

  //   this.infoService.makeBooking(this.bookingObj).subscribe((result: IApiResponse) =>{
  //     if (result.isSuccess){
  //       this.router.navigate(['confirmation'])
  //     }

  //     if (!result.isSuccess){
  //        alert(result.errorMessage)
  //     }
  //   })
  // }
}
