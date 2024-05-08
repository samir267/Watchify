export class User{
    id!: number;
    username!: string;
    passwordHash!: Uint8Array;
    passwordSalt!: Uint8Array;
    refreshToken!: string;
    tokenCreated!: Date;
    tokenExpires!: Date;
    secretCode!: number;
    role!: string;
    etat!: string;
    PhoneNumber!:string;
}