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
export class DashboardService {

  private ApiUri = this.apiurl.baseurl + "/Dashboard";

  constructor(private http: HttpClient, private apiurl: APIUrl) { }

  //FILE UPLOAD
  FileUpload(data) {
    return this.http.post<string>(this.ApiUri + '/FileUpload', data).pipe(tap((res) => {

    }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  VideoUpload(data) {
    return this.http.post<string>(this.ApiUri + '/VideoUpload', data).pipe(tap((res) => {

    }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  ImageUpload(data): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/ImageUpload', data).pipe(tap((res) => {

    }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  BulkUpload(data) {
    return this.http.post<string>(this.ApiUri + '/BulkUpload', data).pipe(tap((res) => {

    }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  //COMMON METHOD
  GetAllDashboardCount(ProjectId, PhaseId,OrgUserId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllDashboardCount', { params: { ProjectId: ProjectId, PhaseId: PhaseId, OrgUserId: OrgUserId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  GetAllUserDashboardProductTask(PhaseId, OrgUserId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllUserDashboardProductTask', { params: { PhaseId: PhaseId, OrgUserId: OrgUserId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  GetAllProductBugPriorityWise(ProjectId, PhaseId, OrgUserId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllProductBugPriorityWise', { params: { ProjectId: ProjectId, PhaseId: PhaseId, OrgUserId: OrgUserId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  GetAllUserDashboardCount(ProjectId, PhaseId,UserId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllUserDashboardCount', { params: { ProjectId: ProjectId, PhaseId: PhaseId,UserId: UserId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  GetAllDashboardUser(ProjectId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllDashboardUser', { params: { ProjectId: ProjectId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  GetProductBacklogGraph(Model): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/GetProductBacklogGraph', Model).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  GetAllDashboardProductBacklog(PhaseId,OrgUserId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllDashboardProductBacklog', { params: { PhaseId: PhaseId, OrgUserId: OrgUserId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
}
