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
export class ProjectService {

  private ApiUri = this.apiurl.baseurl + "/Project";

  constructor(private http: HttpClient, private apiurl: APIUrl) { }
  
  GetAllProject(): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllProject').pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  CreateProject(Description): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/CreateProject', { params: { Description: Description } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  EditProject(Project): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/EditProject', Project).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  DeleteProject(ProjectId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/DeleteProject', { params: { ProjectId: ProjectId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  SearchProject(SearchTaskobj): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/SearchProject', SearchTaskobj).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
}
