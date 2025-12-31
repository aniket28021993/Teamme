import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { DashboardService } from '../../miscellaneous/dashboard.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MessageService } from 'primeng/api';
import { lang } from 'moment';
import { MiscellaneousService } from '../../miscellaneous/miscellaneous.service';
import { LocalStorageService } from 'angular-2-local-storage';
import { BacklogService } from '../../miscellaneous/backlog.service';
import { TestCaseService } from '../../miscellaneous/testcase.service';

declare var $: any;

@Component({
  selector: 'app-createtestcase',
  templateUrl: './createtestcase.component.html',
  styleUrls: ['./createtestcase.component.css']
})
export class CreateTestCaseComponent implements OnInit {

  ProductBacklogId: number;
  PhaseId: number;
  ProjectId: number;
  ProductBacklogobj: any[] = [];

  //Create PB
  Title: string;
  Description: string;
  PreCondition: string;
  ExpectedResult: string;
  ActualResult: string;
  
  TestCaseSteps: any[] = [];
  TestCaseStepTitle: any;

  //Error msg
  PopUperrortitle: string;  
  FormDisabled: boolean = false;

  constructor(private route: ActivatedRoute, private router: Router, private dashboardService: DashboardService, private messageService: MessageService, private miscellaneousService: MiscellaneousService, private _localStorageService: LocalStorageService, private backlogService: BacklogService, private testcaseService: TestCaseService) {
    this.miscellaneousService.OverallData.subscribe(currentData => {
      if (currentData != null && currentData != "") {
        this.ProjectId = this._localStorageService.get("ProjectId");
        this.PhaseId = this._localStorageService.get("PhaseId");
        this.ProductBacklogId = 0;
        this.GetAllProductBacklog();
      }
    });}

  ngOnInit() {

    this.route.paramMap.subscribe(params => {
      this.PhaseId = +params.get('PhaseId');
      this.ProjectId = +params.get('ProjectId');
      this.ProductBacklogId = +params.get('ProductBacklogId');
    });

    this.GetAllProductBacklog();
    this.miscellaneousService.ChangeBreadcrumbData("CREATE TEST CASE");
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

  CreateTestCase() {
    this.FormDisabled = true;
    this.PopUperrortitle = "";

    if (this.Title == undefined || this.Title == "") {
      this.PopUperrortitle = "Title Required.";
      this.FormDisabled = false;
      return;
    }

    var TestCase = {
      'Title': this.Title,
      'Description': this.Description,
      'PreCondition': this.PreCondition,
      'ExpectedResult': this.ExpectedResult,
      'ActualResult': this.ActualResult,
      'TestCaseStep': this.TestCaseSteps,
      'PhaseId': this.PhaseId,
      'ProjectId': this.ProjectId,
      'ProductBacklogId': this.ProductBacklogId
    };

    this.testcaseService.CreateTestCase(TestCase).subscribe(testcase => {

      this.Title = "";
      this.Description = "";
      this.PreCondition = "";
      this.ExpectedResult = "";
      this.ActualResult = "";
      this.TestCaseSteps = [];

      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: "Test Case created successfully." });
      this.FormDisabled = false;
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
        this.FormDisabled = false;
      });

  }

  AddTestCaseStep() {
    if (this.TestCaseStepTitle == undefined || this.TestCaseStepTitle.trim() == "") {
      this.messageService.add({ severity: 'error', summary: 'Error Message', detail: "Please enter step description." });
      return;
    }

    this.TestCaseSteps.push({'Description': this.TestCaseStepTitle, 'IsChecked': false, 'IsDelete': false });
    this.TestCaseStepTitle = "";
  }

  DeleteTestCaseStep(step) {
    this.TestCaseSteps = this.TestCaseSteps.filter(x => x.Description != step.Description);
  }

  BacklogEvents() {
    this._localStorageService.set("BacklogDropdown", this.ProductBacklogId);
  }
}
