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
export class UserService {

  private ApiUri = this.apiurl.baseurl + "/User";

  constructor(private http: HttpClient, private apiurl: APIUrl) { }

  //USER
  GetAllUser(): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllUser').pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  CreateUser(OrgUser): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/CreateUser', OrgUser).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  EditUser(OrgUser): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/EditUser', OrgUser).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  EditOrgUser(OrgUser): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/EditOrgUser', OrgUser).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  ChangeUserStatus(StatusId, OrgUserId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/ChangeUserStatus', { params: { StatusId: StatusId, OrgUserId: OrgUserId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  ChangePassword(NewPassword, OldPassword): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/ChangePassword', { params: { NewPassword: NewPassword, OldPassword: OldPassword } }).pipe(
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

  //MAP USER
  GetAllMapUser(ProjectId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllMapUser', { params: { ProjectId: ProjectId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  MapuserToProject(Mapuser): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/MapuserToProject', Mapuser).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  RemoveuserFromProject(Mapuser): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/RemoveuserFromProject', Mapuser).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
}
