export  class User{
  id: number;
  username: string;
  passwordHash: string;
  passwordSalt: string;
  refreshToken: string;
  tokenCreated: Date;
  tokenExpires: Date;
  isEmailConfirmed: boolean;
  confirmationCode: string;
  role: number;
  secretCode:number;
  etat:any;
}
