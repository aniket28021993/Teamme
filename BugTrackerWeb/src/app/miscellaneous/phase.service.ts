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
export class PhaseService {

  private ApiUri = this.apiurl.baseurl + "/Phase";

  constructor(private http: HttpClient, private apiurl: APIUrl) { }

  GetAllPhase(ProjectId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllPhase', { params: { ProjectId: ProjectId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  CreatePhase(Model): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/CreatePhase', Model).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  EditPhase(Phase): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/EditPhase', Phase).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  DeletePhase(PhaseId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/DeletePhase', { params: { PhaseId: PhaseId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  SearchPhase(SearchTaskobj): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/SearchPhase', SearchTaskobj).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
}
