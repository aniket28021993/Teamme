//angular
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';

//rxjs
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { APIUrl } from '../appsetting';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  private ApiUri = this.apiurl.baseurl + "/Task";

  constructor(private http: HttpClient, private apiurl: APIUrl) { }
  
  GetAllProductTask(ProductBacklogId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllProductTask', { params: { ProductBacklogId: ProductBacklogId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  GetAllDashboardProductTask(PhaseId, OrgUserId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllDashboardProductTask', { params: { PhaseId: PhaseId, OrgUserId: OrgUserId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  GetAllProductTaskState(ProjectId, PhaseId, OrgUserId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllProductTaskState', { params: { ProjectId: ProjectId, PhaseId: PhaseId, OrgUserId: OrgUserId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  CreateTask(Task): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/CreateTask', Task).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  EditTask(Task): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/EditTask', Task).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  DeleteTask(TaskId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/DeleteTask', { params: { TaskId: TaskId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  TaskToEnhancement(TaskId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/TaskToEnhancement', { params: { TaskId: TaskId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  SearchTask(SearchTaskobj): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/SearchTask', SearchTaskobj).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  } 
}
