import { IGolfClub } from "./IGolfClub";

export interface IAddBooking{
  start : Date,
  end: Date,
  email: string,
  name: string,
  phone: number,
  golfClubsForHire: any[];
}
