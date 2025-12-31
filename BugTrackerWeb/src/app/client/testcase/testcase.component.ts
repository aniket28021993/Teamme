import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { DashboardService } from '../../miscellaneous/dashboard.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MessageService } from 'primeng/api';
import { MiscellaneousService } from '../../miscellaneous/miscellaneous.service';
import { LocalStorageService } from 'angular-2-local-storage';
declare var $: any;
import * as xlsx from 'xlsx';
import { BacklogService } from '../../miscellaneous/backlog.service';
import { TestCaseService } from '../../miscellaneous/testcase.service';

@Component({
  selector: 'app-testcase',
  templateUrl: './testcase.component.html',
  styleUrls: ['./testcase.component.css']
})
export class TestCaseComponent implements OnInit {

  //Data Table
  displayedColumns: string[] = ['Id', 'Title','Owner','Result','Delete'];
  DataTestCase = new MatTableDataSource<any[]>();
  DataTestCaseLength: number = 0;

  //View Child
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  @ViewChild('epltable', { static: false }) epltable: ElementRef;

  //Array
  Projectobj: any[] = [];
  Phaseobj: any[] = [];
  ProductBacklogobj: any[] = [];

  //Variable
  ProjectId: number;
  PhaseId: number;
  ProductBacklogId: number;
  TaskChangeId: number;

  //Search
  SearchTaskval: string;

  //View/Edit/Delete PB
  ProductBacklogData: any;

  //Error Msg
  ErrorMsgTitle: string;
  ErrorMsgDescription: string;

  FormDisabled: boolean = false;
  deletedisplay: boolean = false;

  constructor(private router: Router, private dashboardService: DashboardService, private messageService: MessageService, private miscellaneousService: MiscellaneousService, private _localStorageService: LocalStorageService, private backlogService: BacklogService, private testcaseService: TestCaseService) {
    this.miscellaneousService.OverallData.subscribe(currentData => {
      if (currentData != null && currentData != "") {
        this.ProjectId = this._localStorageService.get("ProjectId");
        this.PhaseId = this._localStorageService.get("PhaseId");
        this.GetAllProductBacklog();
      }
    });
  }

  ngOnInit() {
    this.ProjectId = this._localStorageService.get("ProjectId");
    this.PhaseId = this._localStorageService.get("PhaseId");
    this.GetAllProductBacklog();
    this.miscellaneousService.ChangeBreadcrumbData("TEST CASE");
  }

  OnKeyPress() {
    if (this.SearchTaskval != undefined && this.SearchTaskval != "") {
      this.SearchTask();
    }
    else {
      if (this.ProductBacklogId != undefined) {
        this.GetAllTestCase();
      }
    }
  }
  
  GetAllProductBacklog() {
    this.DataTestCaseLength = 0;
    this.ProductBacklogobj = [];

    this.backlogService.GetAllProductBacklog(this.PhaseId).subscribe(backlog => {
      if (backlog != null) {
        this.ProductBacklogobj = backlog;
        if (this._localStorageService.get("BacklogDropdown") == null) {
          this.ProductBacklogId = this.ProductBacklogobj[0].ProductBacklogId;
        }
        else {
          this.ProductBacklogId = this._localStorageService.get("BacklogDropdown");
        }
        this.GetAllTestCase();
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  GetAllTestCase() {
    this.DataTestCaseLength = 0;
    this.DataTestCase = new MatTableDataSource<any[]>();

    this.testcaseService.GetAllTestCase(this.ProductBacklogId).subscribe(backlogTask => {
      if (backlogTask != null) {
        this.DataTestCase = new MatTableDataSource<any[]>(backlogTask);
        this.DataTestCase.sort = this.sort;
        this.DataTestCase.paginator = this.paginator;
        this.DataTestCaseLength = this.DataTestCase.data.length;
        this.DataTestCase.paginator.pageIndex = this._localStorageService.get("TestCasePaginator");

      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  DeleteTestCase(TestCaseId) {
    this.FormDisabled = true;

    this.testcaseService.DeleteTestCase(TestCaseId).subscribe(proj => {
      this.deletedisplay = false;
      this.ProductBacklogData = null;
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: 'Successfully Deleted.' });
      this.FormDisabled = false;
      this.GetAllTestCase();
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
        this.FormDisabled = false;
      });

  }

  SearchTask() {

    var SearchTaskObj = {
      'SearchTask': this.SearchTaskval,
      'ProductBacklogId': this.ProductBacklogId,
      'PhaseId': this.PhaseId,
      'ProjectId': this.ProjectId
    };
    this.DataTestCase = new MatTableDataSource<any[]>();
    this.DataTestCaseLength = 0;

    this.testcaseService.SearchTestCase(SearchTaskObj).subscribe(proj => {
      if (proj != null) {
        this.DataTestCase = new MatTableDataSource<any[]>(proj);
        this.DataTestCase.paginator = this.paginator;
        this.DataTestCase.sort = this.sort;
        this.DataTestCaseLength = this.DataTestCase.data.length;
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  GotoCreateTask() {
    this.router.navigate(['/testcase/create', this.ProductBacklogId, this.PhaseId, this.ProjectId]);
  }

  GotoViewTask(model) {
    this.router.navigate(['/testcase/view', model]);
  }

  GotoEditTask(model) {
    this.router.navigate(['/testcase/edit', model, this.ProjectId]);
  }

  exportToExcel() {
    const ws: xlsx.WorkSheet =
      xlsx.utils.table_to_sheet(this.epltable.nativeElement);
    const wb: xlsx.WorkBook = xlsx.utils.book_new();
    xlsx.utils.book_append_sheet(wb, ws, 'Sheet1');
    xlsx.writeFile(wb, 'Backlog.xlsx');
  }

  pageEvents(event: any) {
    this._localStorageService.set("TestCasePaginator", event.pageIndex);
  }

  BacklogEvents() {
    this._localStorageService.set("BacklogDropdown", this.ProductBacklogId);
  }
}


