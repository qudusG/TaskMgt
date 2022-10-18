import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError, tap, throwError, map } from "rxjs";

const url = 'https://localhost:44345/api/users/';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  constructor(private http: HttpClient) { }

  login(loginForm: any): Observable<any> {
    return this.http.post<any>(url + 'login', loginForm)
      .pipe(
        tap(data => console.log('data: ', data)),
        catchError(this.handleError)
      ); 
  }

  register(registerForm: any): Observable<any> {
    return this.http.post<any>(url, registerForm)
      .pipe(
        tap(data => console.log('data: ', data)),
        catchError(this.handleError)
      ); 
  }
  private handleError(err: HttpErrorResponse): Observable<never> {
    let errorMessage = '';
    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    console.error(errorMessage);
    return throwError(() => errorMessage);
  }
}
