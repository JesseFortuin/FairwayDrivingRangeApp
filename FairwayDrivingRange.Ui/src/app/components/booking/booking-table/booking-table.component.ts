import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  OnInit,
  ViewEncapsulation,
} from '@angular/core';
import { CalendarEvent, CalendarWeekViewBeforeRenderEvent,  } from 'angular-calendar';
import { WeekViewHourSegment } from 'calendar-utils';
import { fromEvent } from 'rxjs';
import { finalize, takeUntil } from 'rxjs/operators';
import { addDays, addMinutes, endOfWeek, parseISO } from 'date-fns';
import { BookingService } from 'src/app/services/booking/booking.service';
import { ceilToNearest, floorToNearest } from '../../../shared/functions/mathHelperFunctions';
import { CustomerInformationService } from 'src/app/services/customer-information.service';
import { IAddBooking } from 'src/app/shared/interfaces/IAddBooking';
import { IApiResponse } from 'src/app/shared/interfaces/IApiResponse';
import { Router } from '@angular/router';
import { IGolfClub } from 'src/app/shared/interfaces/IGolfClub';
import { IClubsForHire } from 'src/app/shared/interfaces/IClubsForHire';

@Component({
  selector: 'app-booking-table',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './booking-table.component.html',
  styleUrls: ['./booking-table.component.css'],
  styles: [
    `
      .disable-hover {
        pointer-events: none;
      }
    `,
  ],
  encapsulation: ViewEncapsulation.None,
})

export class BookingTableComponent implements OnInit {
  viewDate = new Date();

  currentDate = new Date();

  events: CalendarEvent[] = [];

  dragToCreateActive = false;

  weekStartsOn: 0 = 0;

  dayStartHour: number = 7;

  dayEndHour: number = 19;

  driverClubs: IGolfClub[] = [];

  puttingClubs: IGolfClub[] = [];

  ironClubs: IGolfClub[] = [];

  driversNeeded: any[] = [];

  puttersNeeded: any[] = [];

  ironsNeeded: any[] = [];

  private driversForHire: IClubsForHire = {
    golfClubTypes: 0,
    quantity: 0
  }

  private ironsForHire: IClubsForHire = {
    golfClubTypes: 0,
    quantity: 0
  }

  private puttersForHire: IClubsForHire = {
    golfClubTypes: 0,
    quantity: 0
  }

  constructor(private cdr: ChangeDetectorRef,
              private bookingService : BookingService,
              private infoService: CustomerInformationService,
              public router : Router) {}

  ngOnInit(): void {
    this.bookingService.getGolfClubs().subscribe({
      next: (golfClubs) => {
        // this.golfClubs = golfClubs.value;
        golfClubs.value.forEach(club => {
          if (club.clubType === 'Driver') {
            this.driverClubs.push(club);
          };
          if (club.clubType === 'Putter') {
            this.puttingClubs.push(club);
          };
          if (club.clubType === 'Iron') {
            this.ironClubs.push(club);
          };
        });
      },
      error: (response) => {
        console.log(response);
    }});

    this.bookingService.getBookings()
    .subscribe({
      next: (bookings) => {

        bookings.value.forEach((booking) =>
        {
          var session: CalendarEvent = {
            start: new Date(booking.start),
            end: new Date(booking.end!),
            title: 'booked',
            cssClass: 'green'
          }
          this.events.push(session);

          // if (session.end!.getTime() > this.currentDate.getTime() ){
          //   this.events.push(session)
          // }
        })
        this.refresh();
      },
      error: (response) => {
        console.log(response);
    }});
  }

  minDate: Date = new Date();

  dateIsValid(date: Date): boolean {
    return date >= this.minDate;
  }

  beforeViewRender(body: CalendarWeekViewBeforeRenderEvent): void {
    body.hourColumns.forEach(hourCol => {
      hourCol.hours.forEach(hour => {
        hour.segments.forEach(segment => {
          if (!this.dateIsValid(segment.date)) {
            segment.cssClass = 'cal-disabled';
          }
        });
      });
    });
  }

  startDragToCreate(
    segment: WeekViewHourSegment,
    mouseDownEvent: MouseEvent,
    segmentElement: HTMLElement
  ) {
    const dragToSelectEvent: CalendarEvent = {
      id: this.events.length,
      title: 'Booked',
      start: segment.date,
      meta: {
        tmpEvent: true,
      },
    };

    this.events = [...this.events, dragToSelectEvent];
    const segmentPosition = segmentElement.getBoundingClientRect();
    this.dragToCreateActive = true;
    const endOfView = endOfWeek(this.viewDate, {
      weekStartsOn: this.weekStartsOn,
    });

    fromEvent(document, 'mousemove')
      .pipe(
        finalize(() => {
          delete dragToSelectEvent.meta.tmpEvent;
          this.dragToCreateActive = false;
          this.refresh();
        }),
        takeUntil(fromEvent(document, 'mouseup'))
      )
      .subscribe(mouseMoveEvent => {
        const minutesDiff = ceilToNearest(
          (mouseMoveEvent as MouseEvent).clientY - segmentPosition.top,
          30
        );

        const daysDiff = floorToNearest(
            (mouseMoveEvent as MouseEvent).clientX - segmentPosition.left,
            segmentPosition.width
          ) / segmentPosition.width;

        const newEnd = addDays(addMinutes(segment.date, minutesDiff), daysDiff);
        if (newEnd > segment.date && newEnd < endOfView) {
          dragToSelectEvent.end = newEnd;
        }

        this.bookingObj.start = dragToSelectEvent.start;

        this.bookingObj.end = dragToSelectEvent.end!;

        this.refresh();
      });
  }

  private refresh() {
    this.events = [...this.events];
    this.cdr.detectChanges();
  }

  customerObj: any = {
    name: '',
    email: '',
    phone: 0
  }

  private booking = {} as CalendarEvent;

  bookingObj: IAddBooking = {
    start: new Date,
    end: new Date,
    name: '',
    email: '',
    phone: 0,
    golfClubsForHire: []
  }

  eventClicked ({event}: {event: CalendarEvent}): void {
  }

  registerProcess() {
    this.booking = JSON.parse(sessionStorage.getItem('booking')!);

    this.bookingObj.name = this.customerObj.name;

    this.bookingObj.email = this.customerObj.email;

    this.bookingObj.phone = this.customerObj.phone;

    this.bookingObj.golfClubsForHire = [this.driversForHire, this.ironsForHire, this.puttersForHire]

    console.log(this.bookingObj)

    this.infoService.makeBooking(this.bookingObj).subscribe((result: IApiResponse) =>{
      if (result.isSuccess){
        this.router.navigate(['confirmation'])
      }

      if (!result.isSuccess){
         alert(result.errorMessage)
      }
    })
  }

  driverSelect (data: number) {
    this.driversForHire.golfClubTypes = 1;

    this.driversForHire.quantity = data;
  }

  ironSelect (data: number) {
    this.ironsForHire.golfClubTypes = 2;

    this.ironsForHire.quantity = data;
  }

  putterSelect (data: number) {
    this.puttersForHire.golfClubTypes = 3;

    this.puttersForHire.quantity = data;
  }

  onClick(event: any) {
    var clubSelection = document.getElementById('clubs');

    if (event.target.checked) {
        clubSelection!.style.display = 'block';
    } else {
      clubSelection!.style.display = 'none';
    }
  }
}
