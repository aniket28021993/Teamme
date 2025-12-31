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
import { TaskService } from '../../miscellaneous/task.service';

@Component({
  selector: 'app-producttask',
  templateUrl: './producttask.component.html',
  styleUrls: ['./producttask.component.css']
})
export class ProductTaskComponent implements OnInit {

  //Data Table
  displayedColumns: string[] = ['TaskId', 'TaskTitle', 'TaskState', 'TaskAssignedTo', 'Change', 'Delete'];
  DataTask = new MatTableDataSource<any[]>();
  DataTaskLength: number = 0;

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
  changedisplay: boolean = false;

  constructor(private router: Router, private dashboardService: DashboardService, private messageService: MessageService, private miscellaneousService: MiscellaneousService, private _localStorageService: LocalStorageService, private backlogService: BacklogService, private taskService: TaskService) {
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
    this.miscellaneousService.ChangeBreadcrumbData("TASK");
  }

  OnKeyPress() {
    if (this.SearchTaskval != undefined && this.SearchTaskval != "") {
      this.SearchTask();
    }
    else {
      if (this.ProductBacklogId != undefined) {
        this.GetAllProductTask();
      }
    }
  }

  GetAllProductBacklog() {
    this.DataTaskLength = 0;
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

        this.GetAllProductTask();
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  GetAllProductTask() {
    this.DataTaskLength = 0;
    this.DataTask = new MatTableDataSource<any[]>();

    this.taskService.GetAllProductTask(this.ProductBacklogId).subscribe(backlogTask => {
      if (backlogTask != null) {
        this.DataTask = new MatTableDataSource<any[]>(backlogTask);
        this.DataTask.sort = this.sort;
        this.DataTask.paginator = this.paginator;
        this.DataTaskLength = this.DataTask.data.length;
        this.DataTask.paginator.pageIndex = this._localStorageService.get("TaskPaginator");

      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  DeleteTask(TaskId) {
    this.FormDisabled = true;

    this.taskService.DeleteTask(TaskId).subscribe(proj => {
      this.deletedisplay = false;
      this.GetAllProductTask();
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: 'Task deleted.' });
      this.FormDisabled = false;
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
        this.FormDisabled = false;
      });

  }

  TaskToEnhancement(TaskId) {
    this.FormDisabled = true;

    this.taskService.TaskToEnhancement(TaskId).subscribe(proj => {
      this.changedisplay = false;
      this.GetAllProductTask();
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: 'Task changed to enhancement.' });
      this.FormDisabled = false;
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
    this.DataTask = new MatTableDataSource<any[]>();
    this.DataTaskLength = 0;

    this.taskService.SearchTask(SearchTaskObj).subscribe(proj => {
      if (proj != null) {
        this.DataTask = new MatTableDataSource<any[]>(proj);
        this.DataTask.paginator = this.paginator;
        this.DataTask.sort = this.sort;
        this.DataTaskLength = this.DataTask.data.length;
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  GotoCreateTask() {
    this.router.navigate(['/task/create', this.ProductBacklogId, this.PhaseId, this.ProjectId]);
  }

  GotoEditTask(model) {
    this.router.navigate(['/task/edit', model, this.ProjectId]);
  }

  exportToExcel() {
    const ws: xlsx.WorkSheet =
      xlsx.utils.table_to_sheet(this.epltable.nativeElement);
    const wb: xlsx.WorkBook = xlsx.utils.book_new();
    xlsx.utils.book_append_sheet(wb, ws, 'Sheet1');
    xlsx.writeFile(wb, 'Backlog.xlsx');
  }

  pageEvents(event: any) {
    this._localStorageService.set("TaskPaginator", event.pageIndex);
  }

  BacklogEvents() {
    this._localStorageService.set("BacklogDropdown", this.ProductBacklogId);
  }
}


