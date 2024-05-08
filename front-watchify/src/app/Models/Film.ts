export class Film {
  Id!: number;

  Titre!: string;

  Description!: string;

  Duree!: string;

  Genre!: string;

  DateDeSortie!: Date;

  IsFree: boolean;

  UserId: number;

  VideoFilePath!: string;

  LogoFilePath!: string;
}
