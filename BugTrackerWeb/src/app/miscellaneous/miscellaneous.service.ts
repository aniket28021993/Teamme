//angular
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';

//rxjs
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { LoggedInUser } from './LoggedInUserModel';
import { LocalStorageService } from 'angular-2-local-storage';
import { Router } from '@angular/router';
import { APIUrl } from '../appsetting';

@Injectable({
  providedIn: 'root'
})
export class MiscellaneousService {


  private baseUri = this.apiurl.tokenurl + "/token";
  private ApiUri = this.apiurl.baseurl + "/Miscellaneous";

  private ChangeImage = new BehaviorSubject('');
  ImageData = this.ChangeImage.asObservable();

  private ChangeBreadcrumb = new BehaviorSubject('');
  BreadcrumbData = this.ChangeBreadcrumb.asObservable();

  private ChangeNavBar = new BehaviorSubject('');
  NavBarData = this.ChangeNavBar.asObservable();

  private ChangePhase = new BehaviorSubject('');
  PhaseData = this.ChangePhase.asObservable();

  private ChangeOverall = new BehaviorSubject('');
  OverallData = this.ChangeOverall.asObservable();

  private ChangePhaseDropdown = new BehaviorSubject('');
  PhaseDropdownData = this.ChangePhaseDropdown.asObservable();

  UserInfo = this.GetBlankLogginedClientUser();

  private GetBlankLogginedClientUser() {
    return new LoggedInUser(0, 0, 0, 0, "", "", "", "", "", "", 0, "", "", 0, "", false);
    };
  constructor(private http: HttpClient, private _localStorageService: LocalStorageService, private router: Router, private apiurl: APIUrl) {
        var TempUserInfo = this._localStorageService.get<LoggedInUser>("ClientUserInfo");

        if (TempUserInfo) {
            this.UserInfo = TempUserInfo;
        }
        else {
            this.UserInfo = this.GetBlankLogginedClientUser();
        }
    }

  //LOGIN
  UserLogin(username, password): Observable<any> {
        var _me = this;
        var userData = "username=" + username + "&password=" + password + "&grant_type=password";

        var reqHeader = new HttpHeaders(
            {
                'Content-Type': 'application/x-www-form-urlencoded'
            });

        return this.http.post<any>(this.baseUri, userData, { headers: reqHeader }).pipe(
            tap((res) => {
                _me._localStorageService.set("access_token", res.access_token);
                _me.setloggedinuser(res);
            }),
            catchError((error: HttpErrorResponse) => {
                return throwError(error);
            })
        );
    }

  Logout(OrgUserId,OrgId) {

    return this.http.get<any>(this.ApiUri + "/Logout", { params: { OrgUserId: OrgUserId, OrgId: OrgId} }).pipe(
      tap((registered) => {
        this._localStorageService.clearAll();
        localStorage.clear();
        this.UserInfo = this.GetBlankLogginedClientUser();
        this.router.navigate(['/login']);
      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  private setloggedinuser(Userobj) {
        var _me = this;

        _me.UserInfo.FirstName = Userobj.FirstName;
        _me.UserInfo.LastName = Userobj.LastName;
        _me.UserInfo.EmailId = Userobj.EmailId;
        _me.UserInfo.OrgName = Userobj.OrgNmae;
        _me.UserInfo.PhoneNo = Userobj.PhoneNo;

        _me.UserInfo.OrgId = Userobj.OrgId;
        _me.UserInfo.OrgStatusId = Userobj.OrgStatusId;
        _me.UserInfo.OrgUserId = Userobj.OrgUserId;
        _me.UserInfo.OrgUserStatusId = Userobj.OrgUserStatusId;
        _me.UserInfo.OrgUserTypeId = Userobj.OrgUserTypeId;

        _me.UserInfo.Accesstoken = Userobj.access_token;

        _me.UserInfo.ProfileImage = Userobj.ProfileImage;
        _me.UserInfo.BioData = Userobj.BioData;
        _me.UserInfo.OrgPlanId = Userobj.OrgPlanId;
        _me.UserInfo.IsloggedIn = true;
        _me._localStorageService.set("ClientUserInfo", _me.UserInfo);
        _me._localStorageService.set("ProjectId", 0);
        _me._localStorageService.set("IsMainDashboard", true);
    }

  public setloggedinuseronchangeimage(Userobj) {
    this._localStorageService.set("ClientUserInfo", Userobj);
  }

  //ADD
  CreateOrganization(OrgList): Observable<any> {

      return this.http.post<any>(this.ApiUri + "/CreateOrganization", OrgList).pipe(
      tap((registered) => {

      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  ContactUs(ContactData): Observable<any> {

    return this.http.post<any>(this.ApiUri + "/ContactUs", ContactData).pipe(
      tap((registered) => {

      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  SendOTP(Email): Observable<any> {

    return this.http.get<any>(this.ApiUri + "/SendOTP", { params: { Email: Email } }).pipe(
      tap((registered) => {

      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  VerifyOTP(Email,UserOTP): Observable<any> {

    return this.http.get<any>(this.ApiUri + "/VerifyOTP", { params: { Email: Email, UserOTP: UserOTP } }).pipe(
      tap((registered) => {

      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  ChangePassword(Email, Password): Observable<any> {

    return this.http.get<any>(this.ApiUri + "/ChangePassword", { params: { Email: Email, Password: Password } }).pipe(
      tap((registered) => {

      }),
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }

  CallImageChanger(item: any) {
    this.ChangeImage.next(item);
  }

  ChangeBreadcrumbData(item: any) {
    this.ChangeBreadcrumb.next(item);
  }

  ChangeNavBarData(item: any) {
    this.ChangeNavBar.next(item);
  }

  ChangePhaseData(item: any) {
    this.ChangePhase.next(item);
  }

  ChangeOverallData(item: any) {
    this.ChangeOverall.next(item);
  }

  ChangeDropownData(item: any) {
    this.ChangePhaseDropdown.next(item);
  }

  CalculateDateDiff(sentDate) {
    var date1: any = new Date(sentDate);
    var date2: any = new Date();
    var diffDays: any = Math.floor((date2 - date1) / (1000 * 60 * 60 * 24));
    return diffDays;
  }

  ClearUserData() {
    this._localStorageService.clearAll();
    localStorage.clear();
    this.UserInfo = this.GetBlankLogginedClientUser();
    this.router.navigate(['/404']);
  }
}
