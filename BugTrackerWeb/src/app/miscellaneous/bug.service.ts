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
export class BugService {

  private ApiUri = this.apiurl.baseurl + "/Bug";

  constructor(private http: HttpClient, private apiurl: APIUrl) { }

  GetAllProductBug(ProductBacklogId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllProductBug', { params: { ProductBacklogId: ProductBacklogId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  GetAllProductBugState(ProjectId, PhaseId, OrgUserId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllProductBugState', { params: { ProjectId: ProjectId, PhaseId: PhaseId, OrgUserId: OrgUserId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  CreateBug(Bug): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/CreateBug', Bug).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  EditBug(Bug): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/EditBug', Bug).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  DeleteBug(BugId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/DeleteBug', { params: { BugId: BugId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  BugToEnhancement(BugId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/BugToEnhancement', { params: { BugId: BugId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  SearchBug(SearchTaskobj): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/SearchBug', SearchTaskobj).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
}
