import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from '../miscellaneous/Admin.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MessageService } from 'primeng/api';
import { MiscellaneousService } from '../miscellaneous/miscellaneous.service';
declare var $: any;

@Component({
  selector: 'app-admindashboard',
  templateUrl: './admindashboard.component.html',
  styleUrls: ['./admindashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {

  displayedColumns: string[] = ['OrganizationName', 'Email', 'PhoneNumber','Plan','Status','CreateUser','ViewUser'];
  Organization = new MatTableDataSource<any[]>();
  OrganizationLength: number = 0;

  //View Child
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  //Array
  OrganzationList: any[] = [];

  //Variable
  OrgStatusId: number=2;
  OrgId: number;

  //Create Variable
  FirstName: string;
  LastName: string;
  EmailId: string;
  PhoneNo: string;

  //Error Msg
  ErrFirstName: string;
  ErrLastName: string;
  ErrEmailId: string;
  ErrPhoneNo: string;

  FormDisabled: boolean = false;
  createdisplay: boolean = false;
  updatedisplay: boolean = false;

  constructor(private router: Router, private adminservice: AdminService, private messageService: MessageService, private miscellaneousService: MiscellaneousService) { }

  ngOnInit() {
    this.GetAllOrganization();
    this.miscellaneousService.ChangeBreadcrumbData("ADMIN DASHBOARD");
  }

  GetAllOrganization() {

    this.adminservice.GetAllOrganization().subscribe(orglist => {
      if (orglist != null) {
        this.OrganzationList = orglist;
        this.Organization = new MatTableDataSource<any[]>(orglist);
        this.Organization.paginator = this.paginator;
        this.Organization.sort = this.sort;
        this.OrganizationLength = this.Organization.data.length;
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  UpdateOrgStatus() {
    this.adminservice.UpdateOrgStatus(this.OrgId, this.OrgStatusId).subscribe(orglist => {
      this.updatedisplay = false;
      this.OrgStatusId = 2;
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: "Successfully Updated." });
      this.GetAllOrganization();
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  CreateAdmin() {
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
      'OrgId': this.OrgId,
      'FirstName': this.FirstName,
      'LastName': this.LastName,
      'EmailId': this.EmailId,
      'PhoneNo': this.PhoneNo
    };


    this.adminservice.CreateAdmin(OrgUser).subscribe(orglist => {
      this.createdisplay = false;
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: "Successfully Created." });
      this.FormDisabled = false;
    },
      (error) => {
        this.FormDisabled = false;
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  CleanError(element) {
    this.ErrFirstName = "";
    this.ErrLastName = "";
    this.ErrEmailId = "";
    this.ErrPhoneNo = "";

    this.OrgId = element._OrgId;
    this.EmailId = element._OrgEmail;
    this.PhoneNo = element._OrgNumber;
    this.createdisplay = true;
  }

}
