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
import { EnhancementService } from '../../miscellaneous/enhancement.service';

@Component({
  selector: 'app-productenhancement',
  templateUrl: './productenhancement.component.html',
  styleUrls: ['./productenhancement.component.css']
})
export class ProductEnhancementComponent implements OnInit {

  //Data Table
  displayedColumns: string[] = ['EnhancementId', 'EnhancementTitle', 'EnhancementState', 'EnhancementAssignedTo', 'Change', 'Delete'];
  DataEnhancement = new MatTableDataSource<any[]>();
  DataEnhancementLength: number = 0;

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
  TypeId: number;

  //Create PB
  Searchval: string;

  //View PB
  ProductBacklogData: any;

  FormDisabled: boolean = false;
  deletedisplay: boolean = false;

  constructor(private router: Router, private dashboardService: DashboardService, private messageService: MessageService, private miscellaneousService: MiscellaneousService, private _localStorageService: LocalStorageService, private backlogService: BacklogService,private enhancementService: EnhancementService) {
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
    this.miscellaneousService.ChangeBreadcrumbData("ENHANCEMENT");
  }
  

  OnKeyPress() {
    if (this.Searchval != undefined && this.Searchval != "") {
      this.SearchEnhancement();
    }
    else {
      if (this.ProductBacklogId != undefined) {
        this.GetAllProductEnhancement();
      }
    }
  }

  GetAllProductBacklog() {
    this.ProductBacklogobj = [];
    this.DataEnhancement = new MatTableDataSource<any[]>();
    this.DataEnhancementLength = 0;
    this.backlogService.GetAllProductBacklog(this.PhaseId).subscribe(backlog => {
      if (backlog != null) {
        this.ProductBacklogobj = backlog;

        if (this._localStorageService.get("BacklogDropdown") == null) {
          this.ProductBacklogId = this.ProductBacklogobj[0].ProductBacklogId;
        }
        else {
          this.ProductBacklogId = this._localStorageService.get("BacklogDropdown");
        }
        this.GetAllProductEnhancement();
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  GetAllProductEnhancement() {
    this.DataEnhancement = new MatTableDataSource<any[]>();
    this.DataEnhancementLength = 0;
    this.enhancementService.GetAllProductEnhancement(this.ProductBacklogId).subscribe(backlogenhancement => {
      if (backlogenhancement != null) {
        this.DataEnhancement = new MatTableDataSource<any[]>(backlogenhancement);
        this.DataEnhancement.sort = this.sort;
        this.DataEnhancement.paginator = this.paginator;
        this.DataEnhancementLength = this.DataEnhancement.data.length;
        this.DataEnhancement.paginator.pageIndex = this._localStorageService.get("EnhancementPaginator");

      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  DeleteEnhancement(EnhancementId) {
    this.FormDisabled = true;

    this.enhancementService.DeleteEnhancement(EnhancementId).subscribe(proj => {
      this.deletedisplay = false;
      this.GetAllProductEnhancement();
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: 'Enhancement deleted.' });
      this.FormDisabled = false;
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
        this.FormDisabled = false;
      });

  }

  EnhancementToTaskBug(EnhancementId) {

    this.enhancementService.EnhancementToTaskBug(EnhancementId, this.TypeId).subscribe(proj => {
      this.GetAllProductEnhancement();
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: 'Enhancement changed to bug/task.' });
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  SearchEnhancement() {
    var SearchTaskObj = {
      'SearchTask': this.Searchval,
      'ProductBacklogId': this.ProductBacklogId,
      'PhaseId': this.PhaseId,
      'ProjectId': this.ProjectId
    };
    this.DataEnhancement = new MatTableDataSource<any[]>();
    this.DataEnhancementLength = 0;

    this.enhancementService.SearchEnhancement(SearchTaskObj).subscribe(proj => {
      if (proj != null) {
        this.DataEnhancement = new MatTableDataSource<any[]>(proj);
        this.DataEnhancement.paginator = this.paginator;
        this.DataEnhancement.sort = this.sort;
        this.DataEnhancementLength = this.DataEnhancement.data.length;
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  GotoCreateEnhancement() {
    this.router.navigate(['/enhancement/create', this.ProductBacklogId, this.PhaseId, this.ProjectId]);
  }
  
  GotoEditEnhancement(model) {
    this.router.navigate(['/enhancement/edit', model, this.ProjectId]);
  }

  exportToExcel() {
    const ws: xlsx.WorkSheet =
      xlsx.utils.table_to_sheet(this.epltable.nativeElement);
    const wb: xlsx.WorkBook = xlsx.utils.book_new();
    xlsx.utils.book_append_sheet(wb, ws, 'Sheet1');
    xlsx.writeFile(wb, 'Backlog.xlsx');
  }

  pageEvents(event: any) {
    this._localStorageService.set("EnhancementPaginator", event.pageIndex);
  }

  BacklogEvents() {
    this._localStorageService.set("BacklogDropdown", this.ProductBacklogId);
  }
}
