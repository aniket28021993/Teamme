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
import { UserService } from '../../miscellaneous/user.service';

declare var $: any;

@Component({
  selector: 'app-createproduct',
  templateUrl: './createproduct.component.html',
  styleUrls: ['./createproduct.component.css']
})
export class CreateProductComponent implements OnInit {


  PhaseId: number;
  ProjectId: number;
  AssignUserobj: any[] = [];

  //Create PB
  Title: string;
  Description: string;
  AssignedTo: number;
  AssignedDeveloper: number;
  AssignedDesigner: number;
  AssignedTester: number;
  Estimation: number = 0;
  
  //File Upload
  FileData: any;
  FileNames: any[] = [];

  FormDisabled: boolean = false;

  constructor(private route: ActivatedRoute, private router: Router, private dashboardService: DashboardService, private messageService: MessageService, private miscellaneousService: MiscellaneousService, private _localStorageService: LocalStorageService, private backlogService: BacklogService, private userService: UserService) {
    this.miscellaneousService.OverallData.subscribe(currentData => {
      if (currentData != null && currentData != "") {
        this.PhaseId = this._localStorageService.get("PhaseId");
      }
    });
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.PhaseId = +params.get('PhaseId');
      this.ProjectId = +params.get('ProjectId');
    });

    this.GetAllMapUser();

    this.miscellaneousService.ChangeBreadcrumbData("CREATE BACKLOG");
  }

  GetAllMapUser() {

    this.userService.GetAllMapUser(this.ProjectId).subscribe(proj => {
      if (proj != null) {
        this.AssignUserobj = proj;
        this.AssignedTo = +this.miscellaneousService.UserInfo.OrgUserId;
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
 
  CreateProductBacklog() {
    var _me = this;

    this.FormDisabled = true;

    if (this.Title == undefined || this.Title.trim() == "") {
      this.FormDisabled = false;
      this.messageService.add({ severity: 'error', summary: 'Error Message', detail: "Title required." });
      return;
    }
   
    var ProductBacklog = {
      'Title': this.Title,
      'Description': this.Description,
      'State': 1,
      'AssignedTo': this.AssignedTo,
      'AssignedDeveloper': this.AssignedDeveloper,
      'AssignedDesigner': this.AssignedDesigner,
      'AssignedTester': this.AssignedTester,
      'Estimation': this.Estimation,
      'PhaseId': this.PhaseId,
      'ProjectId': this.ProjectId
    };

    this.backlogService.CreateProductBacklog(ProductBacklog).subscribe(ProductBacklogId => {

      if (this.FileData != undefined) {
        for (let i = 0; i < this.FileData.length; i++) {
          this.FileUpload(ProductBacklogId, this.FileData[i]);
        }
      }

      _me.messageService.add({ severity: 'success', summary: 'Success Message', detail: "Backlog created." });
      _me.Title = "";
      _me.Description = "";
      _me.AssignedTo = +_me.miscellaneousService.UserInfo.OrgUserId;
      _me.AssignedDeveloper = 0;
      _me.AssignedDesigner = 0;
      _me.AssignedTester = 0;
      _me.FileData = '';
      _me.FileNames = [];
      _me.Estimation = 0;
      $("#filecontrol").val('');
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
    formData.append('Type', '1');
    this.dashboardService.FileUpload(formData).subscribe(proj => {
      if (proj != null) {
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });
  }
}
