import { Component, OnInit } from '@angular/core';
import { MiscellaneousService } from '../../miscellaneous/miscellaneous.service';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { LocalStorageService } from 'angular-2-local-storage';

declare var $: any;

@Component({
  selector: 'app-dashboard',
  templateUrl: 'login.component.html'
})
export class LoginComponent implements OnInit{


  _Email: string;
  _Password: string;

  ErrEmail: string;
  ErrPassword: string;

  ProjectId: number;

  constructor(private _miscellaneousService: MiscellaneousService, private router: Router, private messageService: MessageService, private _localStorageService: LocalStorageService) { }

  ngOnInit() {
    if (this._miscellaneousService.UserInfo.IsloggedIn == true) {
      if (this._miscellaneousService.UserInfo.OrgUserTypeId == 1) {
        this.router.navigate(['/admin-dashboard']);
      }
      else if (this._miscellaneousService.UserInfo.OrgUserTypeId == 2 || this._miscellaneousService.UserInfo.OrgUserTypeId == 3 || this._miscellaneousService.UserInfo.OrgUserTypeId == 4 || this._miscellaneousService.UserInfo.OrgUserTypeId == 5) {
        this._localStorageService.set("ProjectId", 0);
        this._localStorageService.set("IsMainDashboard", true);
        this.router.navigate(['/maindashboard']);
      }
    }
  }

  UserLogin() {
    this.ErrEmail = "";
    this.ErrPassword = "";

    if (this._Email == undefined || this._Email == "") {
      this.ErrEmail = "Email Required.";
    }

    if (this._Password == undefined || this._Password == "") {
      this.ErrPassword = "Password Required.";
    }

    if ((this.ErrEmail != undefined && this.ErrEmail != "") || (this.ErrPassword != undefined && this.ErrPassword != "")) {
      return;
    }

    this._miscellaneousService.UserLogin(this._Email, this._Password).subscribe(userData => {
      if (userData != null) {
        if (userData.OrgUserTypeId == 1) {
          this.router.navigate(['/admin-dashboard']);
        }
        else if (userData.OrgUserTypeId == 2 || userData.OrgUserTypeId == 3 || userData.OrgUserTypeId == 4 || userData.OrgUserTypeId == 5) {
          this.router.navigate(['/maindashboard']);
        }
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.error_description });
      });

  }
}
