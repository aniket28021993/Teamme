import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { DashboardService } from '../../miscellaneous/dashboard.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MessageService } from 'primeng/api';
import { APIUrl } from '../../appsetting';
import { MiscellaneousService } from '../../miscellaneous/miscellaneous.service';
import { TestCaseService } from '../../miscellaneous/testcase.service';

declare var $: any;

@Component({
  selector: 'app-edittestcase',
  templateUrl: './edittestcase.component.html',
  styleUrls: ['./edittestcase.component.css']
})
export class EditTestCaseComponent implements OnInit {

  //number
  TestCaseId: number;
  ProjectId: number;

  TestCaseSteps: any[] = [];
  TestCaseStepTitle: any;

  //obj
  TestCaseData: any;

  //string
  ErrorMsgTitle: string;
  FormDisabled: boolean = false;

  constructor(private route: ActivatedRoute, private router: Router, private dashboardService: DashboardService, private messageService: MessageService, private apiurl: APIUrl, private miscellaneousService: MiscellaneousService, private testcaseService: TestCaseService) { }

  ngOnInit() {

    this.route.paramMap.subscribe(params => {
      this.TestCaseId = +params.get('ProductBacklogDataId');
      this.ProjectId = +params.get('ProjectId');
    });

    this.GetTestCaseData();
    this.miscellaneousService.ChangeBreadcrumbData("EDIT TEST CASE");
  }

  GetTestCaseData() {

    this.testcaseService.GetTestCaseData(this.TestCaseId).subscribe(proj => {
      if (proj != null) {
        this.TestCaseData = proj;
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  EditTestCase() {
    this.FormDisabled = true;
    this.ErrorMsgTitle = "";

    if (this.TestCaseData.Title == undefined || this.TestCaseData.Title.trim() == "") {
      this.ErrorMsgTitle = "Title Required.";
      this.FormDisabled = false;
      return;
    }

    this.testcaseService.EditTestCase(this.TestCaseData).subscribe(proj => {
      this.FormDisabled = false;
      setTimeout(() => {
        this.messageService.add({ severity: 'success', summary: 'Success Message', detail: "Successfully updated test case." });
      }, 2000);
      this.GoBackToPhase();
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

    var TestCaseStep = {
      'Description': this.TestCaseStepTitle,
      'TestCaseId': this.TestCaseData.TestCaseId
    }

    this.testcaseService.CreateTestCaseStep(TestCaseStep).subscribe(proj => {
      this.FormDisabled = false;
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: "Successfully added test case step." });
      this.GetTestCaseData();
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
        this.FormDisabled = false;
      });
    

    this.TestCaseData.TestCaseStep.push({ 'Description': this.TestCaseStepTitle, 'IsChecked': false, 'IsDelete': false });
    this.TestCaseStepTitle = "";
  }

  DeleteTestCaseStep(step) {
    this.testcaseService.DeleteTestCaseStep(step.TestCaseStepId).subscribe(proj => {

      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: "Successfully deleted test case step." });
      this.TestCaseData.TestCaseStep = this.TestCaseData.TestCaseStep.filter(x => x.TestCaseStepId != step.TestCaseStepId);
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
        this.FormDisabled = false;
      });

  }

  IsTestCasePass(IsPass) {

    this.testcaseService.IsTestCasePass(this.TestCaseId, IsPass).subscribe(proj => {
      setTimeout(() => {
        this.messageService.add({ severity: 'success', summary: 'Success Message', detail: "Successfully updated test case." });
      }, 2000);
      this.GoBackToPhase();
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  GoBackToPhase() {
    this.router.navigate(['/testcase']);
  }
}

