import { CalendarEvent } from "angular-calendar";

export interface IGetBookings{
  isSuccess: boolean;
  value: CalendarEvent[];
  errorMessage: string;
}
