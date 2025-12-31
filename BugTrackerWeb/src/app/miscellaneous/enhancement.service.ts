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
export class EnhancementService {

  private ApiUri = this.apiurl.baseurl + "/Enhancement";

  constructor(private http: HttpClient, private apiurl: APIUrl) { }
  
  GetAllProductEnhancement(ProductBacklogId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllProductEnhancement', { params: { ProductBacklogId: ProductBacklogId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  GetAllProductEnhancementState(ProjectId, PhaseId, OrgUserId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllProductEnhancementState', { params: { ProjectId: ProjectId, PhaseId: PhaseId, OrgUserId: OrgUserId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  CreateEnhancement(Enhancement): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/CreateEnhancement', Enhancement).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  EditEnhancement(Enhancement): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/EditEnhancement', Enhancement).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  DeleteEnhancement(EnhancementId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/DeleteEnhancement', { params: { EnhancementId: EnhancementId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  EnhancementToTaskBug(EnhancementId, TypeId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/EnhancementToTaskBug', { params: { EnhancementId: EnhancementId, TypeId: TypeId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  SearchEnhancement(SearchTaskobj): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/SearchEnhancement', SearchTaskobj).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

}
