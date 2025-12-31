import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { DashboardService } from '../../miscellaneous/dashboard.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MessageService } from 'primeng/api';
import { MiscellaneousService } from '../../miscellaneous/miscellaneous.service';
import { LocalStorageService } from 'angular-2-local-storage';
import { BacklogService } from '../../miscellaneous/backlog.service';
import { BugService } from '../../miscellaneous/bug.service';
import { UserService } from '../../miscellaneous/user.service';

declare var $: any;

@Component({
  selector: 'app-createbug',
  templateUrl: './createbug.component.html',
  styleUrls: ['./createbug.component.css']
})
export class CreateBugComponent implements OnInit {

  ProductBacklogId: number;
  PhaseId: number;
  ProjectId: number;
  AssignUserobj: any[] = [];
  ProductBacklogobj: any[] = [];

  //Create PB
  Title: string;
  Description: string;
  AssignedTo: number;
  PriorityTo: number = 1;

  //File Upload
  FileData: any;
  FileNames: any[] = [];

  PriorityArr = [
    { 'id': 1, 'title': 'P1' },
    { 'id': 2, 'title': 'P2' },
    { 'id': 3, 'title': 'P3' },
    { 'id': 4, 'title': 'P4' },
  ];

  FormDisabled: boolean = false;

  constructor(private route: ActivatedRoute, private router: Router, private dashboardService: DashboardService, private messageService: MessageService, private miscellaneousService: MiscellaneousService, private _localStorageService: LocalStorageService, private backlogService: BacklogService, private bugService: BugService, private userService: UserService) {
    this.miscellaneousService.OverallData.subscribe(currentData => {
      if (currentData != null && currentData != "") {
        this.ProjectId = this._localStorageService.get("ProjectId");
        this.PhaseId = this._localStorageService.get("PhaseId");
        this.ProductBacklogId = 0;
        this.GetAllProductBacklog();
      }
    });
  }

  ngOnInit() {

    this.route.paramMap.subscribe(params => {
      this.PhaseId = +params.get('PhaseId');
      this.ProjectId = +params.get('ProjectId');
      this.ProductBacklogId = +params.get('ProductBacklogId');
    });

    this.GetAllProductBacklog();
    this.GetAllMapUser();
    this.miscellaneousService.ChangeBreadcrumbData("CREATE BUG");
  }

  GetAllProductBacklog() {
    this.ProductBacklogobj = [];

    this.backlogService.GetAllProductBacklog(this.PhaseId).subscribe(backlog => {
      if (backlog != null) {
        this.ProductBacklogobj = backlog;
        if (this.ProductBacklogId == 0) {
          this.ProductBacklogId = this.ProductBacklogobj[0].ProductBacklogId;
        }
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  GetAllMapUser() {

    this.userService.GetAllMapUser(this.ProjectId).subscribe(proj => {
      if (proj != null) {
        this.AssignUserobj = proj;
        this.AssignedTo = this.AssignUserobj[0].UserId;
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  CreateBug() {
    this.FormDisabled = true;

    if (this.Title == undefined || this.Title == "") {
      this.messageService.add({ severity: 'error', summary: 'Error Message', detail: "Title required." });
      this.FormDisabled = false;
      return;
    }

    var Bug = {
      'Title': this.Title,
      'Description': this.Description,
      'State': 1,
      'AssignedTo': this.AssignedTo,
      'PhaseId': this.PhaseId,
      'ProjectId': this.ProjectId,
      'ProductBacklogId': this.ProductBacklogId,
      'PriorityId': this.PriorityTo
    };

    this.bugService.CreateBug(Bug).subscribe(Bug => {
      if (this.FileData != undefined) {
        for (let i = 0; i < this.FileData.length; i++) {
          this.FileUpload(Bug, this.FileData[i]);
        }
      };

      this.Title = "";
      this.Description = "";
      this.AssignedTo = this.AssignUserobj[0].UserId;
      this.FileNames = [];
      $("#filecontrol").val('');
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: "Bug created." });
      this.FormDisabled = false;
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
        this.FormDisabled = false;
      });

  }

  onFileChange(event) {
    if (event.target.files.length > 0) {
      this.FileData = event.target.files;
      for (let i = 0; i < this.FileData.length; i++) {
        this.FileNames.push(this.FileData[i].name);
      }
    }
  }

  RemoveFile(FileName) {
    this.FileNames = this.FileNames.filter(x => x != FileName);
    this.FileData = this.FileData.filter(x => x.name != FileName);
  }

  FileUpload(ProductBacklogId, FileData) {

    const formData = new FormData();
    formData.append('DocFile', FileData);
    formData.append('CommonId', ProductBacklogId);
    formData.append('Type', '2');
    this.dashboardService.FileUpload(formData).subscribe(proj => {
      if (proj != null) {
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });
  }

  GoBackToPhase() {
    this.router.navigate(['/bug']);
  }

  BacklogEvents() {
    this._localStorageService.set("BacklogDropdown", this.ProductBacklogId);
  }
}
