import { HttpClient, HttpClientJsonpModule, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../model/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  url="https://localhost:7178/api/User";

  constructor(private http:HttpClient) { }
  getUserById(id:any): Observable<User> {
    return this.http.get<User>(`${this.url}/getById/${id}`);
  }
  getUserByusername(username:string): Observable<User> {
    return this.http.get<User>(this.url+"/user/"+username);
  }
  sendVerifEmail(username:any): Observable<any> {
    return this.http.post<any>(`${this.url}/envoyerEmail`,username);
  }
  VerifEmail(code: string, email: string): Observable<any> {
    let params = new HttpParams()
    .set('code', code)
    .set('email', email);
    return this.http.post<any>(`${this.url}/verifierCode`,{},{ params: params });
  }
}
