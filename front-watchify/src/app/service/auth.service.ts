import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Auth } from '../model/auth';
import { Observable } from 'rxjs';
import { Register } from '../model/register';
import { RegisterS } from '../model/registerSocial';
import { AuthS } from '../model/loginSocial';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  url="https://localhost:7178/api/Auth/";

  constructor(private http:HttpClient) { }
  login(auth:Auth): Observable<any> {
    return this.http.post<string>(this.url+"login", auth);

  }
  loginS(auth:AuthS): Observable<any> {
    return this.http.post<string>(this.url+"loginSocial", auth);

  }
  register(reg:Register): Observable<any> {
    return this.http.post<any>(this.url+"register", reg);

  }
  registerS(reg:RegisterS): Observable<any> {
    return this.http.post<any>(this.url+"registerSocial", reg);

  }
}
