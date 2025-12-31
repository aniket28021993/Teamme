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
export class TestCaseService {

  private ApiUri = this.apiurl.baseurl + "/TestCase";

  constructor(private http: HttpClient, private apiurl: APIUrl) { }
  
  GetAllTestCase(ProductBacklogId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllTestCase', { params: { ProductBacklogId: ProductBacklogId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  GetTestCaseData(TestCaseId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetTestCaseData', { params: { TestCaseId: TestCaseId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  GetAllProductTestCaseState(ProjectId, PhaseId, OrgUserId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/GetAllProductTestCaseState', { params: { ProjectId: ProjectId, PhaseId: PhaseId, OrgUserId: OrgUserId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  CreateTestCase(TestCase): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/CreateTestCase', TestCase).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  EditTestCase(TestCase): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/EditTestCase', TestCase).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  CreateTestCaseStep(TestCase): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/CreateTestCaseStep', TestCase).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  DeleteTestCase(TestCaseId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/DeleteTestCase', { params: { TestCaseId: TestCaseId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  DeleteTestCaseStep(TestCaseStepId): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/DeleteTestCaseStep', { params: { TestCaseStepId: TestCaseStepId } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  SearchTestCase(SearchTaskobj): Observable<any> {
    return this.http.post<any>(this.ApiUri + '/SearchTestCase', SearchTaskobj).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  IsTestCasePass(TestCaseId, IsPass): Observable<any> {
    return this.http.get<any>(this.ApiUri + '/IsTestCasePass', { params: { TestCaseId: TestCaseId, IsPass: IsPass } }).pipe(
      tap((res) => {
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

}
