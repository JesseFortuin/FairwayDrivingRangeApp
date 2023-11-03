import { IClubsForHire } from "./IClubsForHire";

export interface IAddBooking{
  start : Date,
  end: Date,
  email: string,
  name: string,
  phone: number,
  golfClubsForHire: IClubsForHire[];
}
