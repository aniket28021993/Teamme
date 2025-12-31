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
export class AdminService {

  private ApiUri = this.apiurl.baseurl + "/Admin";

  constructor(private http: HttpClient, private apiurl: APIUrl) { }

  //Admin
  GetAllOrganization(): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllOrganization').pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  GetAllOrgUser(OrgId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllOrgUser', { params: { OrgId: OrgId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  GetAllOrgPlan(): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllOrgPlan').pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  GetAllPayment(): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllPayment').pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  SearchUser(SearchTaskobj): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/SearchUser', SearchTaskobj).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  ApproveOrgPlan(OrgId, OrgPlanId, UpdateOrgPlanId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/ApproveOrgPlan', { params: { OrgId: OrgId, OrgPlanId: OrgPlanId, UpdateOrgPlanId: UpdateOrgPlanId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  AddPayment(OrgId, OrgAmount): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/AddPayment', { params: { OrgId: OrgId, OrgAmount: OrgAmount } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  UpdateOrgStatus(OrgId, OrgStatusId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/UpdateOrgStatus',{ params: { OrgId: OrgId, OrgStatusId: OrgStatusId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  CreateAdmin(OrgUser): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/CreateAdmin', OrgUser).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
 



}
