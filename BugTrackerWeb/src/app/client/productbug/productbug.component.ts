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
import { BacklogService } from '../../miscellaneous/backlog.service';
import { BugService } from '../../miscellaneous/bug.service';

@Component({
  selector: 'app-productbug',
  templateUrl: './productbug.component.html',
  styleUrls: ['./productbug.component.css']
})
export class ProductBugComponent implements OnInit {
  //Data Table
  displayedColumns: string[] = ['BugId', 'BugTitle', 'BugState', 'Priority', 'BugAssignedTo', 'Change', 'Delete'];
  DataBug = new MatTableDataSource<any[]>();
  DataBugLength: number = 0;

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

  //Search
  SearchTaskval: string;

  //View PB
  ProductBacklogData: any;

  FormDisabled: boolean = false;
  deletedisplay: boolean = false;
  changedisplay: boolean = false;

  constructor(private router: Router, private dashboardService: DashboardService, private messageService: MessageService, private miscellaneousService: MiscellaneousService, private _localStorageService: LocalStorageService, private backlogService: BacklogService, private bugService: BugService) {
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
    this.miscellaneousService.ChangeBreadcrumbData("BUG");
  }

  OnKeyPress() {
    if (this.SearchTaskval != undefined && this.SearchTaskval != "") {
      this.SearchBug();
    }
    else {
      if (this.ProductBacklogId != undefined) {
        this.GetAllProductBug();
      }
    }
  }

  GetAllProductBacklog() {
    this.ProductBacklogobj = [];
    this.DataBug = new MatTableDataSource<any[]>();
    this.DataBugLength = 0;
    this.backlogService.GetAllProductBacklog(this.PhaseId).subscribe(backlog => {
      if (backlog != null) {
        this.ProductBacklogobj = backlog;
        if (this._localStorageService.get("BacklogDropdown") == null) {
          this.ProductBacklogId = this.ProductBacklogobj[0].ProductBacklogId;
        }
        else {
          this.ProductBacklogId = this._localStorageService.get("BacklogDropdown");
        }
        this.GetAllProductBug();
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  GetAllProductBug() {
    this.DataBug = new MatTableDataSource<any[]>();
    this.DataBugLength = 0;
    this.bugService.GetAllProductBug(this.ProductBacklogId).subscribe(backlogTask => {
      if (backlogTask != null) {
        this.DataBug = new MatTableDataSource<any[]>(backlogTask);
        this.DataBugLength = this.DataBug.data.length;
        this.DataBug.paginator = this.paginator;
        this.DataBug.sort = this.sort;
        this.DataBug.paginator.pageIndex = this._localStorageService.get("BugPaginator");

      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  DeleteBug(BugId) {
    this.FormDisabled = true;

    this.bugService.DeleteBug(BugId).subscribe(proj => {
      this.deletedisplay = false;
      this.GetAllProductBug();
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: 'Bug deleted.' });
      this.FormDisabled = false;
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
        this.FormDisabled = false;
      });

  }

  BugToEnhancement(BugId) {
    this.FormDisabled = true;
    this.bugService.BugToEnhancement(BugId).subscribe(proj => {
      this.changedisplay = false;
      this.GetAllProductBug();
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: 'Bug changed to enhancement.' });
      this.FormDisabled = false;
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
        this.FormDisabled = false;
      });

  }

  SearchBug() {
    var SearchTaskObj = {
      'SearchTask': this.SearchTaskval,
      'ProductBacklogId': this.ProductBacklogId,
      'PhaseId': this.PhaseId,
      'ProjectId': this.ProjectId
    };

    this.DataBug = new MatTableDataSource<any[]>();
    this.DataBugLength = 0;
    this.bugService.SearchBug(SearchTaskObj).subscribe(proj => {
      if (proj != null) {
        this.DataBug = new MatTableDataSource<any[]>(proj);
        this.DataBug.paginator = this.paginator;
        this.DataBug.sort = this.sort;
        this.DataBugLength = this.DataBug.data.length;
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  GotoCreateBug() {
    this.router.navigate(['/bug/create', this.ProductBacklogId, this.PhaseId, this.ProjectId]);
  }

  GotoEditBug(model) {
    this.router.navigate(['/bug/edit', model, this.ProjectId]);
  }

  GoToRecentActivity(model) {
    this.router.navigate(['/recentactivity', model, 2]);
  }

  exportToExcel() {
    const ws: xlsx.WorkSheet =
      xlsx.utils.table_to_sheet(this.epltable.nativeElement);
    const wb: xlsx.WorkBook = xlsx.utils.book_new();
    xlsx.utils.book_append_sheet(wb, ws, 'Sheet1');
    xlsx.writeFile(wb, 'Backlog.xlsx');
  }

  pageEvents(event: any) {
    this._localStorageService.set("BugPaginator", event.pageIndex);
  }

  BacklogEvents() {
    this._localStorageService.set("BacklogDropdown", this.ProductBacklogId);
  }
}
