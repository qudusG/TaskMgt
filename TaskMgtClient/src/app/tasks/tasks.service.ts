import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError, tap, throwError, map } from "rxjs";
import { IUser } from "../create-task/user";
import { ITask } from "./tasks";

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private taskUrl = 'https://localhost:44345/api/';

  constructor(private http: HttpClient) { }

  getTasks(): Observable<ITask[]> {
    return this.http.get<ITask[]>(this.taskUrl + 'tasks')
      .pipe(
        tap(data => console.log('data: ', data)),
        catchError(this.handleError)
      ); 
  }
  getTask(id: number): Observable<ITask> {
    return this.http.get<ITask>(this.taskUrl + 'tasks/' + id)
      .pipe(
        tap(data => console.log('data: ', data)),
        catchError(this.handleError)
      ); 
  }
  createTask(task: ITask): Observable<ITask> {
    return this.http.post<ITask>(this.taskUrl + 'tasks', task)
      .pipe(
        tap(data => console.log('data: ', data)),
        catchError(this.handleError)
      ); 
  }
  addComment(comment: any): Observable<any> {
    return this.http.post<any>(this.taskUrl + 'comments', comment)
      .pipe(
        tap(data => console.log('data: ', data)),
        catchError(this.handleError)
      ); 
  }
  changeStatus(task: any): Observable<any> {
    return this.http.post<any>(this.taskUrl + 'tasks/Update', task)
      .pipe(
        tap(data => console.log('data: ', data)),
        catchError(this.handleError)
      ); 
  }
  getUsers(): Observable<IUser[]> {
    return this.http.get<IUser[]>(this.taskUrl + 'users')
      .pipe(
        tap(data => console.log('data: ', data)),
        catchError(this.handleError)
      ); 
  }
  getStats(): Observable<any> {
    return this.http.get<any>(this.taskUrl + 'tasks/stats')
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
