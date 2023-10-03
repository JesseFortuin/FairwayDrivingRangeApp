import { CalendarEvent } from "angular-calendar";

export interface IBooking{
  isSuccess: boolean;
  value: CalendarEvent[];
  errorMessage: string;
}
