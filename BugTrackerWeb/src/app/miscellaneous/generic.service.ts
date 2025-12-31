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
export class GenericService {

  private ApiUri = this.apiurl.baseurl + "/Generic";

  constructor(private http: HttpClient, private apiurl: APIUrl) { }

  //COMMENT
  GetAllComment(CommonId, CommentTypeId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllComment', { params: { CommonId: CommonId, CommentTypeId: CommentTypeId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  CreateComment(Comment): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/CreateComment', Comment).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  //RECENT ACTIVITY
  GetAllRecentActivity(CommonId, RecentActivityTypeId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllRecentActivity', { params: { CommonId: CommonId, RecentActivityTypeId: RecentActivityTypeId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  //UPDATE ORG PLAN
  UpdateOrgPlan(OrgPlanId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/UpdateOrgPlan', { params: { OrgPlanId: OrgPlanId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  GetProductBacklogData(ProductBacklogDataId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetProductBacklogData', { params: { ProductBacklogDataId: ProductBacklogDataId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  DeleteProductBacklogDataFile(ProductBacklogDataFileId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/DeleteProductBacklogDataFile', { params: { ProductBacklogDataFileId: ProductBacklogDataFileId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
}
