import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { DashboardService } from '../../miscellaneous/dashboard.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MessageService } from 'primeng/api';
import { APIUrl } from '../../appsetting';
import { MiscellaneousService } from '../../miscellaneous/miscellaneous.service';
import { LocalStorageService } from 'angular-2-local-storage';
import { BacklogService } from '../../miscellaneous/backlog.service';
import { UserService } from '../../miscellaneous/user.service';

declare var $: any;

@Component({
  selector: 'app-editproduct',
  templateUrl: './editproduct.component.html',
  styleUrls: ['./editproduct.component.css']
})
export class EditProductComponent implements OnInit {

  //number
  ProductBacklogId: number;
  ProjectId: number;
  AssignedTo: number;

  //obj
  ProductBacklogDataEdit: any;
  FileData: any;

  //string
  Url: string;

  //arr
  AssignUserobj: any[] = [];
  TaskStateArr = [
    { 'id': 1, 'title': 'New' },
    { 'id': 2, 'title': 'Active' },
    { 'id': 3, 'title': 'Paused' },
    { 'id': 4, 'title': 'Testing' },
    { 'id': 5, 'title': 'Done' }
  ];

  FormDisabled: boolean = false;

  constructor(private route: ActivatedRoute, private router: Router, private dashboardService: DashboardService, private messageService: MessageService, private apiurl: APIUrl, private miscellaneousService: MiscellaneousService, private _localStorageService: LocalStorageService, private backlogService: BacklogService, private userService: UserService) { }

  ngOnInit() {

    this.route.paramMap.subscribe(params => {
      this.ProductBacklogId = +params.get('ProductBacklogId');
      this.ProjectId = +params.get('ProjectId');
    });

    this.Url = this.apiurl.baseurl + "/Dashboard/DownloadFile?RefFileName=";

    this.GetProductBacklog();
    this.GetAllMapUser();

    this.miscellaneousService.ChangeBreadcrumbData("EDIT BACKLOG");
  }

  GetProductBacklog() {

    this.backlogService.GetProductBacklog(this.ProductBacklogId).subscribe(proj => {
      if (proj != null) {
        this.ProductBacklogDataEdit = proj;
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

  EditProductBacklog() {
    this.FormDisabled = true;

    if (this.ProductBacklogDataEdit.Title == undefined || this.ProductBacklogDataEdit.Title.trim() == "") {
      this.messageService.add({ severity: 'error', summary: 'Error Message', detail: "Title required." });
      this.FormDisabled = false;
      return;
    }

    this.backlogService.EditProductBacklog(this.ProductBacklogDataEdit).subscribe(proj => {
      this.FormDisabled = false;
      setTimeout(() => {
        this.messageService.add({ severity: 'success', summary: 'Success Message', detail: "Backlog updated." });
      }, 2000);
      this.router.navigate(["/backlog"]);
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
        this.FormDisabled = false;
      });

  }

  onFileChange(event, ProductBacklogId) {
    if (event.target.files.length > 0) {
      this.FileData = event.target.files;
      for (let i = 0; i < this.FileData.length; i++) {
        this.FileUpload(ProductBacklogId, this.FileData[i]);
      }
    }
  }

  FileUpload(ProductBacklogId, FileData) {

    const formData = new FormData();
    formData.append('DocFile', FileData);
    formData.append('CommonId', ProductBacklogId);
    formData.append('Type', '1');
    this.dashboardService.FileUpload(formData).subscribe(proj => {
      if (proj != null) {
        this.ProductBacklogDataEdit.productBacklogFileDTO = proj;
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });
  }

  DeleteProductBacklogFile(ProductBacklogFileId) {

    this.backlogService.DeleteProductBacklogFile(ProductBacklogFileId).subscribe(proj => {
      this.ProductBacklogDataEdit.productBacklogFileDTO = this.ProductBacklogDataEdit.productBacklogFileDTO.filter(x => x.ProductBacklogFileId != ProductBacklogFileId);
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: 'Backlog Deleted.' });
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

}

