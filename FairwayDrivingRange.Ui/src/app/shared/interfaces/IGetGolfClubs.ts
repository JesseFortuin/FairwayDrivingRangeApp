import { IGolfClub } from "./IGolfClub";

export interface IGetGolfClubs{
  isSuccess: boolean;
  value: IGolfClub[];
  errorMessage: string;
}
