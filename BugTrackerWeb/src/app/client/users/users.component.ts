import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { DashboardService } from '../../miscellaneous/dashboard.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MessageService } from 'primeng/api';
import { MiscellaneousService } from '../../miscellaneous/miscellaneous.service';
import { APIUrl } from '../../appsetting';
import { UserService } from '../../miscellaneous/user.service';
declare var $: any;

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  displayedColumns: string[] = ['FirstName', 'LastName', 'EmailId', 'PhoneNo', 'UserType', 'Status','Edit'];
  Users = new MatTableDataSource<any[]>();
  UsersLength: number = 0;

  //View Child
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  //Variable
  SearchTaskval: string;
  Description: string;

  //Array
  Usersobj: any[] = [];
  UserEdit: any;

  //Create Variable
  FirstName: string;
  LastName: string;
  EmailId: string;
  PhoneNo: string;
  UserTypeId: number = 3;

  //Error Msg
  ErrFirstName: string;
  ErrLastName: string;
  ErrEmailId: string;
  ErrPhoneNo: string;

  UserTypes = [
    { 'id': 3, 'Desc': 'MANAGER/ARCHITECT' },
    { 'id': 4, 'Desc': 'TEAM LEAD/MODULE LEAD' },
    { 'id': 5, 'Desc': 'TEAM MEMBER/BUSINESS ANALYSIS' }
  ];

  TaskStateArr = [
    //{ 'id': 1, 'title': 'CREATE' },
    { 'id': 2, 'title': 'ACTIVE' },
    { 'id': 3, 'title': 'PASUED' },
    { 'id': 4, 'title': 'SUSPEND' }
  ];

  FormDisabled: boolean = false;
  createdisplay: boolean = false;
  createbulkdisplay: boolean = false;
  editdisplay: boolean = false;

  OrgUserTypeId: number;
  OrgUserId: number;

  IsFreePlan: boolean = false;
  IsPluslan: boolean = false;
  OrgPlanId: number;

  Url: string;

  constructor(private router: Router, private dashboardService: DashboardService, private messageService: MessageService, private miscellaneousService: MiscellaneousService, private apiurl: APIUrl, private userService: UserService) { }

  ngOnInit() {
    this.Url = this.apiurl.baseurl + "/Dashboard/DownloadFile?RefFileName=" + "sample.csv" + "&FileName=" + "sample.csv";


    this.OrgUserTypeId = this.miscellaneousService.UserInfo.OrgUserTypeId;
    this.OrgUserId = this.miscellaneousService.UserInfo.OrgUserId;
    this.OrgPlanId = this.miscellaneousService.UserInfo.OrgPlanId;
    this.GetAllUser();
    this.miscellaneousService.ChangeBreadcrumbData("USER");
  }

  OnKeyPress() {
    if (this.SearchTaskval != undefined && this.SearchTaskval != "") {
      this.SearchUser();
    }
    else {
      this.GetAllUser();
    }
  }

  GetAllUser() {
    this.Users = new MatTableDataSource<any[]>();
    this.UsersLength = 0;

    this.userService.GetAllUser().subscribe(user => {
      if (user != null) {
        this.Users = new MatTableDataSource<any[]>(user);
        this.Users.paginator = this.paginator;
        this.Users.sort = this.sort;
        this.UsersLength = this.Users.data.length;

        if (this.UsersLength == 10 && +this.miscellaneousService.UserInfo.OrgPlanId == 1) {
          this.IsFreePlan = true;
        }

        if (this.UsersLength == 30 && +this.miscellaneousService.UserInfo.OrgPlanId == 2) {
          this.IsPluslan = true;
        }
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  SearchUser() {
    if (this.SearchTaskval == undefined || this.SearchTaskval == "") {
      this.messageService.add({ severity: 'warn', summary: 'Warn Message', detail: 'Please give your input..!!' });
      return;
    }

    var SearchTaskObj = {
      'SearchTask': this.SearchTaskval
    };

    this.Users = new MatTableDataSource<any[]>();
    this.UsersLength = 0;

    this.userService.SearchUser(SearchTaskObj).subscribe(proj => {
      if (proj != null) {
        this.Users = new MatTableDataSource<any[]>(proj);
        this.Users.paginator = this.paginator;
        this.Users.sort = this.sort;
        this.UsersLength = this.Users.data.length;
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  CreateUser() {
    this.FormDisabled = true;
    this.ErrFirstName = "";
    this.ErrLastName = "";
    this.ErrEmailId = "";
    this.ErrPhoneNo = "";

    //FirstName
    if (this.FirstName != undefined && this.FirstName != "") {
      if (!this.FirstName.match(/^[a-zA-z]*$/)) {
        this.ErrFirstName = "Invalid FirstName.";
      }
    }
    else {
      this.ErrFirstName = "FirstName Required.";
    }

    //LastName
    if (this.LastName != undefined && this.LastName != "") {
      if (!this.LastName.match(/^[a-zA-z]*$/)) {
        this.ErrLastName = "Invalid LastName.";
      }
    }
    else {
      this.ErrLastName = "LastName Required.";
    }

    //EmailId
    if (this.EmailId != undefined && this.EmailId != "") {
      if (!this.EmailId.match(/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/)) {
        this.ErrEmailId = "Invalid EmailId.";
      }
    }
    else {
      this.ErrEmailId = "EmailId Required.";
    }

    //PhoneNo
    if (this.PhoneNo != undefined && this.PhoneNo != "") {
      if (!this.PhoneNo.match(/^[0-9]*$/)) {
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
      'FirstName': this.FirstName,
      'LastName': this.LastName,
      'EmailId': this.EmailId,
      'PhoneNo': this.PhoneNo,
      'OrgUserTypeId': this.UserTypeId
    };

    this.userService.CreateUser(OrgUser).subscribe(orglist => {
      this.createdisplay = false;
      this.GetAllUser();
      this.FirstName = "";
      this.LastName = "";
      this.EmailId = "";
      this.PhoneNo = "";
      this.UserTypeId = 3;
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: "User Created." });
      this.FormDisabled = false;
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
        this.FormDisabled = false;
      });

  }

  ChangeUserStatus(user) {

    this.userService.ChangeUserStatus(user.OrgUserStatusId, user.OrgUserId).subscribe(orglist => {
      this.GetAllUser();
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: "User status changed." });
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  CleanError() {
    this.ErrFirstName = "";
    this.ErrLastName = "";
    this.ErrEmailId = "";
    this.ErrPhoneNo = "";
  }

  onFileChange(event) {
    if (event.target.files.length > 0) {
      if (event.target.files[0].size > 10000) {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: "Please select file size less than 10mb." });
        return;
      }

      if (event.target.files[0].size <= 0) {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: "Please select file size Greater than 0kb." });
        return;
      }

      this.FileUpload(event.target.files[0]);
    }
  }

  FileUpload(Model) {

    const formData = new FormData();
    formData.append('DocFile', Model);
    this.dashboardService.BulkUpload(formData).subscribe(proj => {
      $("#filecontrol").val('');
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: "All user created." });
      this.createbulkdisplay = false;
      this.GetAllUser();
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
        $("#filecontrol").val('');
      });
  }

  BindEditUser(model) {
    this.UserEdit = {
      'OrgUserId':model.OrgUserId,
      'FirstName': model.FirstName,
      'LastName': model.LastName,
      'EmailId': model.EmailId,
      'PhoneNo': model.PhoneNo,
      'OrgUserTypeId': model.OrgUserTypeId
    }

    this.editdisplay = true;
  }

  EditUser() {
    this.FormDisabled = true;
    this.ErrFirstName = "";
    this.ErrLastName = "";
    this.ErrEmailId = "";
    this.ErrPhoneNo = "";

    //FirstName
    if (this.UserEdit.FirstName != undefined && this.UserEdit.FirstName != "") {
      if (!this.UserEdit.FirstName.match(/^[a-zA-z]*$/)) {
        this.ErrFirstName = "Invalid FirstName.";
      }
    }
    else {
      this.ErrFirstName = "FirstName Required.";
    }

    //LastName
    if (this.UserEdit.LastName != undefined && this.UserEdit.LastName != "") {
      if (!this.UserEdit.LastName.match(/^[a-zA-z]*$/)) {
        this.ErrLastName = "Invalid LastName.";
      }
    }
    else {
      this.ErrLastName = "LastName Required.";
    }

    //EmailId
    if (this.UserEdit.EmailId != undefined && this.UserEdit.EmailId != "") {
      if (!this.UserEdit.EmailId.match(/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/)) {
        this.ErrEmailId = "Invalid EmailId.";
      }
    }
    else {
      this.ErrEmailId = "EmailId Required.";
    }

    //PhoneNo
    if (this.UserEdit.PhoneNo != undefined && this.UserEdit.PhoneNo != "") {
      if (!this.UserEdit.PhoneNo.match(/^[0-9]*$/)) {
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

    this.userService.EditOrgUser(this.UserEdit).subscribe(orglist => {
      this.editdisplay = false;
      this.GetAllUser();
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: "User edited." });
      this.FormDisabled = false;
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
        this.FormDisabled = false;
      });

  }

}
