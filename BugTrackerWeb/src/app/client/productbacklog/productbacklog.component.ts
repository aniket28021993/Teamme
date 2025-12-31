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
import { ProjectService } from '../../miscellaneous/project.service';
import { PhaseService } from '../../miscellaneous/phase.service';
import { BacklogService } from '../../miscellaneous/backlog.service';

@Component({
  selector: 'app-productbacklog',
  templateUrl: './productbacklog.component.html',
  styleUrls: ['./productbacklog.component.css']
})
export class ProductBacklogComponent implements OnInit {
  //Data Table
  displayedColumns: string[] = ['BacklogId', 'BacklogTitle', 'BacklogState', 'BacklogAssignedTo', 'MoveTo', 'Delete'];
  ProductBacklog = new MatTableDataSource<any[]>();
  ProductBacklogLength: number = 0;

  //View Child
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  @ViewChild('epltable', { static: false }) epltable: ElementRef;

  //Search
  SearchTaskval: string;

  //Array
  Projectobj: any[] = [];
  Phaseobj: any[] = [];
  ProductBacklogobj: any[] = [];

  //Variable
  ProjectId: number;
  PhaseId: number;
  PhaseMoveToId: number;
  ProductBacklogId: number;

  //View PB
  ProductBacklogData: any;

  FormDisabled: boolean = false;
  deletedisplay: boolean = false;

  constructor(private router: Router, private dashboardService: DashboardService, private messageservice: MessageService, private miscellaneousService: MiscellaneousService, private _localStorageService: LocalStorageService, private projectService: ProjectService, private phaseService: PhaseService, private backlogService: BacklogService) {
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

    this.GetAllPhase();
    this.GetAllProductBacklog();
    this.miscellaneousService.ChangeBreadcrumbData("BACKLOG");
  }

  OnKeyPress() {
    if (this.SearchTaskval != undefined && this.SearchTaskval != "") {
      this.SearchProductBacklog();
    }
    else {
      this.GetAllProductBacklog();
    }
  }

  GetAllPhase() {
    this.Phaseobj = [];
    this.ProductBacklog = new MatTableDataSource<any[]>();
    this.ProductBacklogLength = 0;
    this.phaseService.GetAllPhase(this.ProjectId).subscribe(proj => {
      if (proj != null) {
        this.Phaseobj = proj;
        //this.PhaseId = this.Phaseobj[0].PhaseId;
        this.PhaseMoveToId = this.PhaseId;
        this.GetAllProductBacklog();
      }
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  GetAllProductBacklog() {
    this.ProductBacklog = new MatTableDataSource<any[]>();
    this.ProductBacklogLength = 0;
    this.backlogService.GetAllProductBacklog(this.PhaseId).subscribe(backlog => {
      if (backlog != null) {
        this.PhaseMoveToId = this.PhaseId;
        this.ProductBacklog = new MatTableDataSource<any[]>(backlog);
        this.ProductBacklogLength = this.ProductBacklog.data.length;
        this.ProductBacklog.paginator = this.paginator;
        this.ProductBacklog.sort = this.sort;
        this.ProductBacklog.paginator.pageIndex = this._localStorageService.get("BacklogPaginator");
      }
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  DeleteProductBacklog(ProductBacklogId) {
    this.FormDisabled = true;

    this.backlogService.DeleteProductBacklog(ProductBacklogId).subscribe(proj => {
      this.deletedisplay = false;
      this.GetAllProductBacklog();
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: 'Backlog deleted.' });
      this.FormDisabled = false;
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
        this.FormDisabled = false;
      });

  }

  SearchProductBacklog() {

    var SearchTaskObj = {
      'SearchTask': this.SearchTaskval,
      'PhaseId': this.PhaseId,
      'ProjectId': this.ProjectId
    };

    this.ProductBacklog = new MatTableDataSource<any[]>();
    this.ProductBacklogLength = 0;

    this.backlogService.SearchProductBacklog(SearchTaskObj).subscribe(proj => {
      if (proj != null) {
        this.ProductBacklog = new MatTableDataSource<any[]>(proj);
        this.ProductBacklog.paginator = this.paginator;
        this.ProductBacklog.sort = this.sort;
        this.ProductBacklogLength = this.ProductBacklog.data.length;
      }
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  PhaseMoveTo(ProductBacklogId) {
    this.backlogService.PhaseMoveTo(ProductBacklogId, this.PhaseMoveToId).subscribe(proj => {
      this.GetAllProductBacklog();
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: 'Backlog moved to another phase.' });
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  GoToCreateProduct() {
    this.router.navigate(['/backlog/create', this.PhaseId, this.ProjectId]);
  }

  GoToEditProduct(model) {
    this.router.navigate(['/backlog/edit', model.ProductBacklogId, this.ProjectId]);
  }

  GoToRecentActivity(model) {
    this.router.navigate(['/recentactivity', model.ProductBacklogId,1]);
  }

  exportToExcel() {
    const ws: xlsx.WorkSheet =
      xlsx.utils.table_to_sheet(this.epltable.nativeElement);
    const wb: xlsx.WorkBook = xlsx.utils.book_new();
    xlsx.utils.book_append_sheet(wb, ws, 'Sheet1');
    xlsx.writeFile(wb, 'Backlog.xlsx');
  }

  pageEvents(event: any) {
    this._localStorageService.set("BacklogPaginator", event.pageIndex);
  }
}
