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
import { addDays, addMinutes, endOfWeek } from 'date-fns';
import { BookingService } from 'src/app/services/booking/booking.service';
import { ceilToNearest, floorToNearest } from '../../../shared/functions/mathHelperFunctions';

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

  events: CalendarEvent[] = [];

  dragToCreateActive = false;

  weekStartsOn: 0 = 0;

  dayStartHour: number = 7;

  dayEndHour: number = 19;

  constructor(private cdr: ChangeDetectorRef,
              private bookingService : BookingService) {}

  ngOnInit(): void {

    this.bookingService.getBookings()
    .subscribe({
      next: (bookings) => {

        bookings.value.forEach((booking) =>
        {
          var session: CalendarEvent = {
            start: new Date(booking.start),
            end: new Date(booking.end!),
            title: 'booked'
          }
          this.events.push(session)
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
    console.log(this.events);

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
        this.refresh();

        sessionStorage.setItem('booking', JSON.stringify(dragToSelectEvent))
      });
  }

  private refresh() {
    this.events = [...this.events];
    this.cdr.detectChanges();
  }
}
