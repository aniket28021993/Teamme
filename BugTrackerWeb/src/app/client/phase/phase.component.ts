import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { DashboardService } from '../../miscellaneous/dashboard.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MessageService } from 'primeng/api';
import { MiscellaneousService } from '../../miscellaneous/miscellaneous.service';
import { LocalStorageService } from 'angular-2-local-storage';

declare var $: any;
import * as xlsx from 'xlsx';
import { PhaseService } from '../../miscellaneous/phase.service';

@Component({
  selector: 'app-phase',
  templateUrl: './phase.component.html',
  styleUrls: ['./phase.component.css']
})
export class PhaseComponent implements OnInit {

  //Data Table
  displayedColumns: string[] = ['PhaseId', 'PhaseName','StartDate','EndDate','Edit','Delete'];
  Phase = new MatTableDataSource<any[]>();
  PhaseLength: number = 0;
  PhaseEdit: any;
  PhaseObj: any;

  //View Child
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild('epltable', { static: false }) epltable: ElementRef;

  //Search
  SearchTaskval: string;

  //Array
  Projectobj: any[] = [];
  AssignUserobj: any[] = [];

  //Variable
  ProjectId: number;
  Description: string;
  StartDate: Date = new Date();
  EndDate: Date = new Date();

  //Error msg
  PopUperrortitle: string;
  PopUperrordescription: string;
  PopUpErroMessage: string;
  PopUpStartErrMessage: string;
  PopUpEndErrMessage: string;

  FormDisabled: boolean = false;
  createdisplay: boolean = false;
  editdisplay: boolean = false;
  deletedisplay: boolean = false;

  constructor(private router: Router, private dashboardService: DashboardService, private messageService: MessageService, private miscellaneousService: MiscellaneousService, private _localStorageService: LocalStorageService, private phaseService: PhaseService) { }

  ngOnInit() {
    this.miscellaneousService.ChangeBreadcrumbData("PHASE");
    this.ProjectId = this._localStorageService.get("ProjectId");
    this.GetAllPhase();
  }

  OnKeyPress() {
    if (this.SearchTaskval != undefined && this.SearchTaskval != "") {
      this.SearchPhase();
    }
    else{
      this.GetAllPhase();
    }
  }

  GetAllPhase() {
    this.Phase = new MatTableDataSource<any[]>();
    this.PhaseLength = 0;

    this.phaseService.GetAllPhase(this.ProjectId).subscribe(proj => {
      if (proj != null) {
        this.Phase = new MatTableDataSource<any[]>(proj);
        this.Phase.paginator = this.paginator;
        this.Phase.sort = this.sort;
        this.PhaseLength = this.Phase.data.length;
        this.Phase.paginator.pageIndex = this._localStorageService.get("PhasePaginator");

      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  CreatePhase() {
    this.FormDisabled = true;
    this.PopUpErroMessage = "";
    this.PopUpStartErrMessage = "";
    this.PopUpEndErrMessage = "";

    if (this.Description == undefined || this.Description == "") {
      this.PopUpErroMessage = "Name Required.";
    }

    if (this.StartDate == undefined) {
      this.PopUpStartErrMessage = "Start Date Required."
    }

    if (this.EndDate == undefined) {
      this.PopUpEndErrMessage = "End Date Required."
    }

    if ((this.PopUpErroMessage != undefined && this.PopUpErroMessage != "") || (this.PopUpStartErrMessage != undefined && this.PopUpStartErrMessage != "") || (this.PopUpEndErrMessage != undefined && this.PopUpEndErrMessage != "")) {
      this.FormDisabled = false;
      return;
    }

    if (this.StartDate > this.EndDate) {
      this.messageService.add({ severity: 'error', summary: 'Error Message', detail: "Start Date should be lesser than End Date." });
      this.FormDisabled = false;
      return;
    }

    var StartDate = this.convertDateTime(this.StartDate);
    var EndDate = this.convertDateTime(this.EndDate);

    var PhaseData = {
      'ProjectId': this.ProjectId,
      'Description': this.Description,
      'StartDate': StartDate,
      'EndDate': EndDate
    }

    this.phaseService.CreatePhase(PhaseData).subscribe(phase => {
      this.GetAllPhase();
      this.miscellaneousService.ChangePhaseData("1");
      this.createdisplay = false;
      this.StartDate = new Date();
      this.EndDate = new Date();
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: 'Phase Created.' });
      this.FormDisabled = false;
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
        this.FormDisabled = false;
      });

  }

  EditPhase() {
    this.FormDisabled = true;

    if (this.PhaseEdit.Description == undefined || this.PhaseEdit.Description == "") {
      this.PopUperrordescription = "Name Required.";
      this.FormDisabled = false;
      return;
    }

    if (Date.parse(this.PhaseEdit.StartDate) > Date.parse(this.PhaseEdit.EndDate)) {
      this.messageService.add({ severity: 'error', summary: 'Error Message', detail: "Start Date should be lesser than End Date." });
      this.FormDisabled = false;
      return;
    }

    var StartDate = this.convertDateTime(this.PhaseEdit.StartDate);
    var EndDate = this.convertDateTime(this.PhaseEdit.EndDate);

    var Phase = {
      'Description': this.PhaseEdit.Description,
      'PhaseId': this.PhaseEdit.PhaseId,
      'StartDate': StartDate,
      'EndDate': EndDate,
    };
    
    this.phaseService.EditPhase(Phase).subscribe(proj => {
      this.editdisplay = false;
      this.GetAllPhase();
      this.miscellaneousService.ChangePhaseData("1");
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: 'Phase update.' });
      this.FormDisabled = false;
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
        this.FormDisabled = false;
      });

  }

  DeletePhase(PhaseId) {
    this.FormDisabled = true;

    var SetPhaseId = this._localStorageService.get("PhaseId");
    if (PhaseId == SetPhaseId) {
      this._localStorageService.set("PhaseId", 0);
    }

    this.phaseService.DeletePhase(PhaseId).subscribe(proj => {
      this.deletedisplay = false;
      this.GetAllPhase();
      this.miscellaneousService.ChangePhaseData("1");
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: 'Phase deleted.' });
      this.FormDisabled = false;
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
        this.FormDisabled = false;
      });

  }

  SearchPhase() {
    var SearchTaskObj = {
      'SearchTask': this.SearchTaskval,
      'ProjectId': this.ProjectId
    };

    this.Phase = new MatTableDataSource<any[]>();
    this.PhaseLength = 0;

    this.phaseService.SearchPhase(SearchTaskObj).subscribe(proj => {
      if (proj != null) {
        this.Phase = new MatTableDataSource<any[]>(proj);
        this.Phase.paginator = this.paginator;
        this.Phase.sort = this.sort;        
        this.PhaseLength = this.Phase.data.length;
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  BindEditPhase(model) {
    this.PhaseEdit = {
      'Description': model.Description,
      'PhaseId': model.PhaseId,
      'StartDate': model.StartDate,
      'EndDate':model.EndDate
    }
    this.PopUperrordescription = "";
  }

  exportToExcel() {
    const ws: xlsx.WorkSheet =
      xlsx.utils.table_to_sheet(this.epltable.nativeElement);
    const wb: xlsx.WorkBook = xlsx.utils.book_new();
    xlsx.utils.book_append_sheet(wb, ws, 'Sheet1');
    xlsx.writeFile(wb, 'phase.xlsx');
  }

  pageEvents(event: any) {
    this._localStorageService.set("PhasePaginator", event.pageIndex);
  }

  OpenPopUpCreate() {
    this.PopUpErroMessage = "";
    this.PopUpStartErrMessage = "";
    this.PopUpEndErrMessage = "";
    this.Description = "";
    this.createdisplay = true;    
  }

  convertDateTime(str) {
    var month, day, year, hours, minutes, seconds;
    var date = new Date(str);
    month = ("0" + (date.getMonth() + 1)).slice(-2);
    day = ("0" + date.getDate()).slice(-2);
    hours = ("0" + date.getHours()).slice(-2);
    minutes = ("0" + date.getMinutes()).slice(-2);
    seconds = ("0" + date.getSeconds()).slice(-2);

    var mySQLDate = [date.getFullYear(), month, day].join("-");
    var mySQLTime = [hours, minutes, seconds].join(":");
    return [mySQLDate, mySQLTime].join(" ");
  }
}


