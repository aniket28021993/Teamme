
import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { Router } from '@angular/router';

import { DashboardService } from '../../miscellaneous/dashboard.service';

import { MessageService } from 'primeng/api';
import { MiscellaneousService } from '../../miscellaneous/miscellaneous.service';
import { APIUrl } from '../../appsetting';
import { UserService } from '../../miscellaneous/user.service';

declare var $: any;

@Component({
  selector: 'app-userprofile',
  templateUrl: './userprofile.component.html',
  styleUrls: ['./userprofile.component.css']
})
export class UserProfileComponent implements OnInit {

  FormDisabled: boolean = false;
  OrguserData: any;
  UserName: string;
  UserIamge: string;
  BioData: string;

  //Error Msg
  ErrFirstName: string;
  ErrLastName: string;
  ErrEmailId: string;
  ErrPhoneNo: string;

  constructor(private router: Router, private dashboardService: DashboardService, private messageService: MessageService, private miscellaneousService: MiscellaneousService, private ImageUrl: APIUrl, private userService: UserService) { }

  ngOnInit() {
    this.OrguserData = this.miscellaneousService.UserInfo;
    this.UserName = this.miscellaneousService.UserInfo.FirstName + " " + this.miscellaneousService.UserInfo.LastName;

    if (this.miscellaneousService.UserInfo.ProfileImage != null && this.miscellaneousService.UserInfo.ProfileImage != "") {
      this.UserIamge = this.ImageUrl.tokenurl + "/Uploads/" + this.miscellaneousService.UserInfo.ProfileImage;
    }

    if (this.miscellaneousService.UserInfo.BioData != null) {
      this.BioData = this.miscellaneousService.UserInfo.BioData;
    }
    this.miscellaneousService.ChangeBreadcrumbData("PROFILE");
  }

  onBasicUpload(event) {
    if (event.target.files.length != 0) {
      if (event.target.files[0].size > 500000) {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: "Please select file size less than 500mb." });
        return;
      }

      if (event.target.files[0].size <= 0) {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: "Please select file size Greater than 0kb." });
        return;
      }


      const formData = new FormData();
      formData.append('ImgFile', event.target.files[0]);

      this.dashboardService.ImageUpload(formData).subscribe(image => {
        if (image != null) {
          this.messageService.add({ severity: 'success', summary: 'Success Message', detail: "Photo uploaded." });

          this.miscellaneousService.UserInfo.ProfileImage = image;
          this.UserIamge = this.ImageUrl.tokenurl + "/Uploads/" + this.miscellaneousService.UserInfo.ProfileImage;

          this.miscellaneousService.CallImageChanger(image);
          this.miscellaneousService.setloggedinuseronchangeimage(this.miscellaneousService.UserInfo);
          $('#videocontrol').val("");
        }
      },
        (error) => {
          this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
        });
    }
  }

  EditUser() {
    this.FormDisabled = true;
    this.ErrFirstName = "";
    this.ErrLastName = "";
    this.ErrEmailId = "";
    this.ErrPhoneNo = "";

    //FirstName
    if (this.OrguserData.FirstName != undefined && this.OrguserData.FirstName != "") {
      if (!this.OrguserData.FirstName.match(/^[a-zA-z]*$/)) {
        this.ErrFirstName = "Invalid FirstName.";
      }
    }
    else {
      this.ErrFirstName = "FirstName Required.";
    }

    //LastName
    if (this.OrguserData.LastName != undefined && this.OrguserData.LastName != "") {
      if (!this.OrguserData.LastName.match(/^[a-zA-z]*$/)) {
        this.ErrLastName = "Invalid LastName.";
        this.messageService.add({ severity: 'success', summary: 'Success Message', detail: "Invalid LastName." });
      }
    }
    else {
      this.ErrLastName = "LastName Required.";
    }

    //EmailId
    if (this.OrguserData.EmailId != undefined && this.OrguserData.EmailId != "") {
      if (!this.OrguserData.EmailId.match(/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/)) {
        this.ErrEmailId = "Invalid EmailId.";
      }
    }
    else {
      this.ErrEmailId = "EmailId Required.";
    }

    //PhoneNo
    if (this.OrguserData.PhoneNo != undefined && this.OrguserData.PhoneNo != "") {
      if (!this.OrguserData.PhoneNo.match(/^[0-9]*$/)) {
        this.ErrPhoneNo = "Invalid Phone number.";
      }
    }
    else {
      this.ErrPhoneNo = "Phone number Required.";
    }

    if ((this.ErrFirstName != undefined && this.ErrFirstName != "") || (this.ErrLastName != undefined && this.ErrLastName != "") || (this.ErrEmailId != undefined && this.ErrEmailId != "") || (this.ErrPhoneNo != undefined && this.ErrPhoneNo != "")) {
      this.FormDisabled = false;
      return;
    }

    var OrgUser = {
      'FirstName': this.OrguserData.FirstName,
      'LastName': this.OrguserData.LastName,
      'EmailId': this.OrguserData.EmailId,
      'PhoneNo': this.OrguserData.PhoneNo,
      'BioData': this.BioData
    };

    this.userService.EditUser(OrgUser).subscribe(orglist => {
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: "User details Changed." });

      this.miscellaneousService.UserInfo.BioData = this.BioData;
      this.miscellaneousService.setloggedinuseronchangeimage(this.miscellaneousService.UserInfo);

      this.FormDisabled = false;
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
        this.FormDisabled = false;
      });

  }
}
