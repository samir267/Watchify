import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http:HttpClient) { }

  private url="https://localhost:7178/api/"

 

  getAllUsers(): Observable<any> {
    return this.http.get(this.url + "User/getAll")
      .pipe(
        catchError(error => {
          console.error('Erreur lors de la récupération des utilisateurs:', error);
          return throwError(error);
        })
      );
  }


  updateUser(id: number, userDto: any): Observable<any> {
    return this.http.post<any>(this.url + `User/update/${id}`, userDto)
      .pipe(
        catchError(error => {
          console.error('Erreur lors de la mise à jour de l\'utilisateur:', error);
          return throwError(error);
        })
      );
  }


  deactivateUser(id: number): Observable<any> {
    return this.http.post<any>(this.url + `User/deactivate/${id}`, {})
      .pipe(
        catchError(error => {
          console.error('Erreur lors de la désactivation de l\'utilisateur:', error);
          return throwError(error);
        })
      );
  }
  deleteUser(id: number): Observable<any> {
    return this.http.delete<any>(this.url + `User/delete/${id}`)
  }


  getUserById(id: number): Observable<any> {
    return this.http.get<any>(this.url + `User/getById/${id}`)
      .pipe(
        catchError(error => {
          console.error('Erreur lors de la récupération de l\'utilisateur par ID:', error);
          return throwError(error);
        })
      );
  }

  getAllFilms():Observable<any>{
    return this.http.get<any>(this.url+ `Film/all`) .pipe(
      catchError(error => {
        console.error('Erreur lors de la récupération des utilisateurs:', error);
        return throwError(error);
      })
    );
  }
}



