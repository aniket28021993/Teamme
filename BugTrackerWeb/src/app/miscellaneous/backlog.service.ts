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
export class BacklogService {

  private ApiUri = this.apiurl.baseurl + "/Backlog";

  constructor(private http: HttpClient, private apiurl: APIUrl) { }
  
  GetAllProductBacklog(PhaseId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllProductBacklog', { params: { PhaseId: PhaseId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  GetAllProductBacklogUserWise(PhaseId, OrgUserId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllProductBacklogUserWise', { params: { PhaseId: PhaseId, OrgUserId: OrgUserId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  GetProductBacklog(ProductBacklogId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetProductBacklog', { params: { ProductBacklogId: ProductBacklogId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  GetAllProductBacklogState(ProjectId, PhaseId, OrgUserId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllProductBacklogState', { params: { ProjectId: ProjectId, PhaseId: PhaseId, OrgUserId: OrgUserId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  CreateProductBacklog(ProductBacklog): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/CreateProductBacklog', ProductBacklog).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  EditProductBacklog(ProductBacklog): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/EditProductBacklog', ProductBacklog).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  DeleteProductBacklog(ProductBacklogId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/DeleteProductBacklog', { params: { ProductBacklogId: ProductBacklogId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  DeleteProductBacklogFile(ProductBacklogFileId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/DeleteProductBacklogFile', { params: { ProductBacklogFileId: ProductBacklogFileId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  SearchProductBacklog(SearchTaskobj): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/SearchProductBacklog', SearchTaskobj).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  PhaseMoveTo(ProductBacklogId, PhaseId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/PhaseMoveTo', { params: { ProductBacklogId: ProductBacklogId, PhaseId: PhaseId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  
}
