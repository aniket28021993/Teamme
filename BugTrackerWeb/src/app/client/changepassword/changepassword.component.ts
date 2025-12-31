
import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { Router } from '@angular/router';

import { DashboardService } from '../../miscellaneous/dashboard.service';

import { MessageService } from 'primeng/api';
import { MiscellaneousService } from '../../miscellaneous/miscellaneous.service';
import { APIUrl } from '../../appsetting';
import { UserService } from '../../miscellaneous/user.service';

declare var $: any;

@Component({
  selector: 'app-changepassword',
  templateUrl: './changepassword.component.html',
  styleUrls: ['./changepassword.component.css']
})
export class ChangePasswordComponent implements OnInit {

  FormDisabled: boolean = false;
  UserIamge: string;
  OldPassword: string;
  NewPassword: string;

  //Error Msg
  ErrNewPassword: string;

  constructor(private router: Router, private dashboardService: DashboardService, private messageService: MessageService, private miscellaneousService: MiscellaneousService, private ImageUrl: APIUrl, private userService: UserService) { }

  ngOnInit() {
    if (this.miscellaneousService.UserInfo.ProfileImage != null) {
      this.UserIamge = this.ImageUrl.tokenurl + "/Uploads/" + this.miscellaneousService.UserInfo.ProfileImage;
    }

    this.miscellaneousService.ChangeBreadcrumbData("CHANGE PASSWORD");

  }

  ChangePassword() {
    this.FormDisabled = true;
    this.ErrNewPassword = "";

    //New Password
    if (this.NewPassword == undefined || this.NewPassword == "") {
      this.ErrNewPassword = "New Password Required.";
      this.FormDisabled = false;
      return;
    }

    this.userService.ChangePassword(this.NewPassword, this.OldPassword).subscribe(orglist => {
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: "Password updated." });
      this.FormDisabled = false;
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
        this.FormDisabled = false;
      });

  }
}
