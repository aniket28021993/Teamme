import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { DashboardService } from '../../miscellaneous/dashboard.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MessageService } from 'primeng/api';
import { APIUrl } from '../../appsetting';
import { MiscellaneousService } from '../../miscellaneous/miscellaneous.service';
import { EnhancementService } from '../../miscellaneous/enhancement.service';
import { UserService } from '../../miscellaneous/user.service';
import { GenericService } from '../../miscellaneous/generic.service';
import { BacklogService } from '../../miscellaneous/backlog.service';

declare var $: any;

@Component({
  selector: 'app-editenhancement',
  templateUrl: './editenhancement.component.html',
  styleUrls: ['./editenhancement.component.css']
})
export class EditEnhancementComponent implements OnInit {

  //number
  ProductBacklogDataId: number;
  ProjectId: number;
  AssignedTo: number;

  //obj
  ProductBacklogDataEdit: any;
  FileData: any;
  VideoData: any;

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

  constructor(private route: ActivatedRoute, private router: Router, private dashboardService: DashboardService, private messageService: MessageService, private apiurl: APIUrl, private miscellaneousService: MiscellaneousService, private enhancementService: EnhancementService, private userService: UserService, private genericService: GenericService,private backlogService: BacklogService) { }

  ngOnInit() {

    this.route.paramMap.subscribe(params => {
      this.ProductBacklogDataId = +params.get('ProductBacklogDataId');
      this.ProjectId = +params.get('ProjectId');
    });

    this.Url = this.apiurl.baseurl + "/Dashboard/DownloadFile?RefFileName=";

    this.GetProductBacklogData();
    this.GetAllMapUser();
    this.miscellaneousService.ChangeBreadcrumbData("EDIT ENHANCEMENT");
  }

  GetProductBacklogData() {

    this.genericService.GetProductBacklogData(this.ProductBacklogDataId).subscribe(proj => {
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

  EditEnhancement() {
    this.FormDisabled = true;

    if (this.ProductBacklogDataEdit.Title == undefined || this.ProductBacklogDataEdit.Title == "") {
      this.messageService.add({ severity: 'error', summary: 'Error Message', detail: "Title required." });
      this.FormDisabled = false;
      return;
    }

    this.enhancementService.EditEnhancement(this.ProductBacklogDataEdit).subscribe(proj => {
      this.FormDisabled = false;
      setTimeout(() => {
        this.messageService.add({ severity: 'success', summary: 'Success Message', detail: "Enhancement updated." });
      }, 2000);
      this.GoBackToPhase();
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
        this.FormDisabled = false;
      });

  }


  onEnhancementFileChange(event, ProductBacklogDataId, ) {
    if (event.target.files.length > 0) {
      this.FileData = event.target.files;
      for (let i = 0; i < this.FileData.length; i++) {
        this.EnhancementFileUpload(ProductBacklogDataId, this.FileData[i]);
      }
    }
  }

  EnhancementFileUpload(ProductBacklogDataId, FileData) {

    const formData = new FormData();
    formData.append('DocFile', FileData);
    formData.append('CommonId', ProductBacklogDataId);
    formData.append('Type', '2');
    this.dashboardService.FileUpload(formData).subscribe(proj => {
      if (proj != null) {
        this.ProductBacklogDataEdit.productBacklogFileDTO = proj;
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });
  }
  DeleteEnhancementFile(ProductBacklogDataFileId) {

    this.backlogService.DeleteProductBacklogFile(ProductBacklogDataFileId).subscribe(proj => {
      this.ProductBacklogDataEdit.productBacklogFileDTO = this.ProductBacklogDataEdit.productBacklogFileDTO.filter(x => x.ProductBacklogFileId != ProductBacklogDataFileId);
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: 'Enhancement file deleted.' });
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  GoBackToPhase() {
    this.router.navigate(['/enhancement']);
  }
}

