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
import { TaskService } from '../../miscellaneous/task.service';
import { UserService } from '../../miscellaneous/user.service';

declare var $: any;

@Component({
  selector: 'app-createtask',
  templateUrl: './createtask.component.html',
  styleUrls: ['./createtask.component.css']
})
export class CreateTaskComponent implements OnInit {

  ProductBacklogId: number;
  PhaseId: number;
  ProjectId: number;
  AssignUserobj: any[] = [];
  ProductBacklogobj: any[] = [];

  //Create PB
  Title: string;
  Description: string;
  AssignedTo: number;
  Estimation: number;

  //File Upload
  FileData: any;
  FileNames: any[] = [];

  FormDisabled: boolean = false;

  constructor(private route: ActivatedRoute, private router: Router, private dashboardService: DashboardService, private messageService: MessageService, private miscellaneousService: MiscellaneousService, private _localStorageService: LocalStorageService, private backlogService: BacklogService, private taskService: TaskService, private userService: UserService) {
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

    this.miscellaneousService.ChangeBreadcrumbData("CREATE TASK");
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

  CreateTask() {
    this.FormDisabled = true;

    if (this.Title == undefined || this.Title == "") {
      this.messageService.add({ severity: 'error', summary: 'Error Message', detail: "Title required." });
      this.FormDisabled = false;
      return;
    }
    
    var task = {
      'Title': this.Title,
      'Description': this.Description,
      'State': 1,
      'AssignedTo': this.AssignedTo,
      'Estimation': this.Estimation,
      'PhaseId': this.PhaseId,
      'ProjectId': this.ProjectId,
      'ProductBacklogId': this.ProductBacklogId
    };

    this.taskService.CreateTask(task).subscribe(task => {
      if (this.FileData != undefined) {
        for (let i = 0; i < this.FileData.length; i++) {
          this.FileUpload(task, this.FileData[i]);
        }
      }

      this.Title = "";
      this.Description = "";
      this.Estimation = 0;
      this.AssignedTo = this.AssignUserobj[0].UserId;
      this.FileNames = [];
      $("#filecontrol").val('');
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: "Task created." });
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
    this.router.navigate(['/task']);
  }

  BacklogEvents() {
    this._localStorageService.set("BacklogDropdown", this.ProductBacklogId);
  }
}
