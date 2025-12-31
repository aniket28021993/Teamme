import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, Data, ActivatedRoute } from '@angular/router';
import { DashboardService } from '../../miscellaneous/dashboard.service';
import { MessageService, TreeNode } from 'primeng/api';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { APIUrl } from '../../appsetting';
import * as CanvasJS from '../../../assets/canvasjs.min';
import { MiscellaneousService } from '../../miscellaneous/miscellaneous.service';
import { LocalStorageService } from 'angular-2-local-storage';
import * as Chart from 'chart.js';
import { count } from 'rxjs/internal/operators/count';
import { BacklogService } from '../../miscellaneous/backlog.service';
import { TaskService } from '../../miscellaneous/task.service';
import { EnhancementService } from '../../miscellaneous/enhancement.service';
import { BugService } from '../../miscellaneous/bug.service';
import { TestCaseService } from '../../miscellaneous/testcase.service';
import { UserService } from '../../miscellaneous/user.service';
import { GenericService } from '../../miscellaneous/generic.service';

declare var $: any;

@Component({
  selector: 'app-Clientdashboard',
  templateUrl: './clientdashboard.component.html',
  styleUrls: ['./clientdashboard.component.css']
})
export class ClientDashboardComponent implements OnInit {

  Phaseobj: any[] = [];
  Projectobj: any[] = [];

  PhaseId: number;
  ProjectId: number;
  AssignedTo: number = 0;
  UserType: number;
  UserName: string;
  
  DashboardCount: any;
  DashboardEvent: any;
  DashboardBacklogState: any;
  DashboardBugPriorityState: any;

  dropdownSettings = {};
  taskdropdownSettings = {};
  Userobj: any[] = [];
  selectedItems = [];
  TaskselectedItems = [];
  PopUpErroMessage: string;

  FormDisabled: boolean = false;
  IsPriorityBug: boolean = false;
  IsEstimation: boolean = false;
  IsEstimationTask: boolean = false;
  display: boolean = false;
  backlogdisplay: boolean = false;
  taskdisplay: boolean = false;
  tourdisplay: boolean = false;
  IsEvent: boolean = false;

  TaskChart: any;
  EnhancementChart: any;
  BugChart: any;
  TestCaseChart: any;

  BacklogData: any[] = [];
  TaskData: any[] = [];
  AssignUserobj: any[] = [];
  ShowAssignedUser: any[] = [];
  userpagination: number = 0;

  showright: boolean = false;
  showleft: boolean = false;
  DashboardName: string = "All";

  CurrentDate = new Date();

  SelectedBacklogItem: any[] = [];
  SelectedTaskItem: any[] = [];

  canvas: any;
  ctx: any;

  constructor(private route: ActivatedRoute, private router: Router, private dashboardService: DashboardService, private messageservice: MessageService, private apiurl: APIUrl, private miscellaneousService: MiscellaneousService, private _localStorageService: LocalStorageService, private backlogService: BacklogService, private taskService: TaskService, private enhancementService: EnhancementService, private bugService: BugService, private testcaseService: TestCaseService, private userService: UserService, private genericService: GenericService) {
    this.miscellaneousService.OverallData.subscribe(currentData => {
      if (currentData != null && currentData != "") {
        setTimeout(() => {
          this.ProjectId = this._localStorageService.get("ProjectId");
          this.PhaseId = this._localStorageService.get("PhaseId");
          if (this.PhaseId == null || this.PhaseId == 0) {
            this.PhaseId = 0;
            this.tourdisplay = true;
          }
          this.GetAllApiCall();
        }, 1000);
      }
    });
  }

  ngOnInit() {
    setTimeout(() => {
      this.UserName = this.miscellaneousService.UserInfo.FirstName + " " + this.miscellaneousService.UserInfo.LastName;
      this.ProjectId = this._localStorageService.get("ProjectId");
      this.PhaseId = this._localStorageService.get("PhaseId");
      this.UserType = this.miscellaneousService.UserInfo.OrgUserTypeId;

      if (this.PhaseId == null || this.PhaseId == 0) {
        this.PhaseId = 0;
        this.tourdisplay = true;
      }
      
      this.GetAllMapUser();
      this.GetAllApiCall();
    }, 1000);

    this.miscellaneousService.ChangeBreadcrumbData("ANALYTICS");
  }

  GetAllApiCall() {
    this.GetAllDashboardCount();
    this.GetAllProductBacklogState();
    this.GetAllProductTaskState();
    this.GetAllProductEnhancementState();
    this.GetAllProductBugState();
    this.GetAllProductTestCaseState();
    this.GetAllProductBugPriorityWise();
    this.GetAllDashboardProductBacklog();
    this.GetAllDashboardProductTask();
  }

  GetAllMapUser() {
    var _me = this;
    this.AssignUserobj = [];
    this.AssignedTo = 0;
    this.userService.GetAllMapUser(this.ProjectId).subscribe(proj => {
      if (proj != null) {
        this.AssignUserobj.push({ 'UserId': 0, 'UserName': 'ALL', 'UserInitial': 'all' });

        proj.map(function (x) {
          var initailarr = x.UserName.split(" ");
          var mapinitail = "";
          initailarr.map(function (y) {
            mapinitail  = mapinitail + y.charAt(0);
          })
          x.UserInitial = mapinitail;
          _me.AssignUserobj.push(x);
        })

        if (_me.AssignUserobj.length > 10) {
          var count = 0;
          _me.AssignUserobj.map(function (x) {
            if (count < 10) {
              _me.ShowAssignedUser.push(x);
              count++;
            }
          })
          _me.showright = true;
        }
        else {
          _me.ShowAssignedUser = _me.AssignUserobj;
        }

        this.AssignedTo = this.AssignUserobj[0].UserId;
        this.userpagination = 0;
      }
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });
  }
  
  GetAllDashboardCount() {
    var _me = this;
    this.DashboardCount = null;

    this.dashboardService.GetAllDashboardCount(this.ProjectId, this.PhaseId, this.AssignedTo).subscribe(count => {
      if (count != null) {
        this.DashboardCount = count;
      }
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  GetAllProductBacklogState() {
    var _me = this;
    this.DashboardBacklogState = [];
    this.backlogService.GetAllProductBacklogState(this.ProjectId, this.PhaseId,this.AssignedTo).subscribe(count => {
      if (count != null) {
        this.DashboardBacklogState = count;
      }
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  GetAllProductTaskState() {
    var _me = this;

    this.taskService.GetAllProductTaskState(this.ProjectId, this.PhaseId, this.AssignedTo).subscribe(count => {
      
      if (this.TaskChart != undefined) {
        this.TaskChart.destroy();
      };

      if (count != null) {
        var TaskArr = [];
        
        TaskArr.push(count.New);
        TaskArr.push(count.Active);
        TaskArr.push(count.Paused);
        TaskArr.push(count.Done);

        this.canvas = document.getElementById('TaskChart');
        this.ctx = this.canvas.getContext('2d');
        
        this.TaskChart = new Chart(this.ctx, {
          type: 'bar',
          data: {
            labels: ["New", "Active", "Paused", "Done"],
            datasets: [{
              data: TaskArr,
              backgroundColor: [
                'rgba(255, 99, 132, 1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(194, 244, 154, 1)'
              ],
              borderWidth: 1
            }]
          },
          options: {
            scales: {
              yAxes: [{
                ticks: {
                  beginAtZero: true
                }
              }]
            },
            responsive: true,
            legend: {
              display: false
            }
          }
        });
      }
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  GetAllProductEnhancementState() {
    var _me = this;
    this.enhancementService.GetAllProductEnhancementState(this.ProjectId, this.PhaseId, this.AssignedTo).subscribe(count => {
      if (this.EnhancementChart != undefined) {
        this.EnhancementChart.destroy();
      };

      if (count != null) {
        
        var EnhancementArr = [];

        EnhancementArr.push(count.New);
        EnhancementArr.push(count.Active);
        EnhancementArr.push(count.Paused);
        EnhancementArr.push(count.Testing);
        EnhancementArr.push(count.Done);

        this.canvas = document.getElementById('EnhancementChart');
        this.ctx = this.canvas.getContext('2d');
        this.EnhancementChart = new Chart(this.ctx, {
          type: 'bar',
          data: {
            labels: ["New", "Active", "Paused","Testing","Done"],
            datasets: [{
              data: EnhancementArr,
              backgroundColor: [
                'rgba(255, 99, 132, 1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(194, 244, 154, 1)',
                'rgba(255, 150, 64, 1)',
              ],
              borderWidth: 1
            }]
          },
          options: {
            scales: {
              yAxes: [{
                ticks: {
                  beginAtZero: true
                }
              }]
            },
            responsive: true,
            legend: {
              display: false
            }
          }
        });
      }
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  GetAllProductBugState() {
    var _me = this;

    this.bugService.GetAllProductBugState(this.ProjectId, this.PhaseId, this.AssignedTo).subscribe(count => {

      if (this.BugChart != undefined) {
        this.BugChart.destroy();
      };

      if (count != null) {
        var BugArr = [];

        BugArr.push(count.New);
        BugArr.push(count.Active);
        BugArr.push(count.Paused);
        BugArr.push(count.ReadyForQa);
        BugArr.push(count.Done);
        BugArr.push(count.Duplicate);
        BugArr.push(count.Reopen);

        this.canvas = document.getElementById('BugChart');
        this.ctx = this.canvas.getContext('2d');
        this.BugChart = new Chart(this.ctx, {
          type: 'bar',
          data: {
            labels: ["New", "Active", "Paused","Testing", "Done", "Duplicate", "Reopen"],
            datasets: [{
              data: BugArr,
              backgroundColor: [
                'rgba(255, 99, 132, 1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(194, 244, 154, 1)',
                'rgba(224, 187, 228, 1)',
                'rgba(210, 145, 188, 1)',
                'rgba(173, 230, 208, 1)'
              ],
              borderWidth: 1
            }]
          },
          options: {
            scales: {
              yAxes: [{
                ticks: {
                  beginAtZero: true
                }
              }]
            },
            responsive: true,
            legend: {
              display: false
            }
          }
        });
      }
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  GetAllProductTestCaseState() {
    var _me = this;

    this.testcaseService.GetAllProductTestCaseState(this.ProjectId, this.PhaseId, this.AssignedTo).subscribe(count => {
      if (this.TestCaseChart != undefined) {
        this.TestCaseChart.destroy();
      };


      if (count != null) {
        
        var TestCaseArr = [];

        TestCaseArr.push(count.Pass);
        TestCaseArr.push(count.Failed);
        TestCaseArr.push(count.NotExecuted);

        this.canvas = document.getElementById('TestCaseChart');
        this.ctx = this.canvas.getContext('2d');
        this.TestCaseChart = new Chart(this.ctx, {
          type: 'bar',
          data: {
            labels: ["Pass", "Failed", "Not Executed"],
            datasets: [{
              data: TestCaseArr,
              backgroundColor: [
                'rgba(255, 99, 132, 1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(194, 244, 154, 1)'
              ],
              borderWidth: 1
            }]
          },
          options: {
            scales: {
              yAxes: [{
                ticks: {
                  beginAtZero: true
                }
              }]
            },
            responsive: true,
            legend: {
              display: false
            }
          }
        });
      }
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  GetAllProductBugPriorityWise() {
    var _me = this;
    this.DashboardBugPriorityState = [];
    this.dashboardService.GetAllProductBugPriorityWise(this.ProjectId, this.PhaseId, this.AssignedTo).subscribe(count => {
      if (count != null) {
        this.DashboardBugPriorityState = count;
      }
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  GetAllDashboardProductBacklog() {
    var _me = this;

    var backlog = new CanvasJS.Chart("EstimationContainer", {});
    backlog.render();
    this.IsEstimation = true;

    this.dashboardService.GetAllDashboardProductBacklog(this.PhaseId, this.AssignedTo).subscribe(backlog => {
      if (backlog != null) {
        this.IsEstimation = false;
        this.BacklogData = backlog;
        var Model = [];
        var Count = 0;

        backlog.map(function (x) {
          if (Count < 10) {
            if (x.Estimation != 0) {
              var CreatedDate = new Date(x.CreatedOn);
              if (CreatedDate < _me.CurrentDate) {
                var DateDiff = _me.miscellaneousService.CalculateDateDiff(x.CreatedOn);
                var calDiff = (DateDiff * 8);

                if (calDiff != 0) {
                  if (calDiff >= x.Estimation) {
                    x.UsedEstimation = calDiff - x.Estimation;
                    x.RemainingEstimation = 0;
                  }
                  else {
                    x.UsedEstimation = x.Estimation - calDiff;
                    x.RemainingEstimation = x.Estimation - x.UsedEstimation;
                  }
                }
                else {
                  x.RemainingEstimation = x.Estimation;
                  x.UsedEstimation = 0
                }
              }
              else {
                x.RemainingEstimation = x.Estimation;
                x.UsedEstimation = 0
              }
            }
            else {
              x.RemainingEstimation = 0;
              x.UsedEstimation = 0
            }
            Model.push(x);
          }
          Count++;
        })

        this.SelectedBacklogItem = Model;
        this.BindBackLogEstimation(Model);
      }
    },
      (error) => {
        this.IsEstimation = false
          ;
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  GetAllDashboardProductTask() {
    var _me = this;

    var task = new CanvasJS.Chart("TaskContainer", {});
    task.render();
    this.IsEstimationTask = true;

    this.taskService.GetAllDashboardProductTask(this.PhaseId, this.AssignedTo).subscribe(backlogTask => {
      if (backlogTask != null) {
        this.IsEstimationTask = false;
        this.TaskData = backlogTask;
        var Model = [];
        var Count = 0;

        backlogTask.map(function (x) {
          if (Count < 10) {
            Model.push(x);
          }
          Count++;
        })

        this.SelectedTaskItem = Model;
        this.BindTaskEstimation(Model);
      }
    },
      (error) => {
        this.IsEstimationTask = false;
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  BindBackLogEstimation(BacklogEstimationGraph) {
    var EstimationCount = [];
    var UsedEstiamtionCount = [];
    var RemainingEstimationCount = [];
    var BindBacklog = [];

    BacklogEstimationGraph.forEach(function (x) {
      BindBacklog.push({ id: x.ProductBacklogDataId, text: x.Title });
      EstimationCount.push({ 'y': x.Estimation, 'label': x.Title });
      UsedEstiamtionCount.push({ 'y': x.UsedEstimation, 'label': x.Title });
      RemainingEstimationCount.push({ 'y': x.RemainingEstimation, 'label': x.Title });
    });

    var chart = new CanvasJS.Chart("EstimationContainer", {
      animationEnabled: true,
      axisY: {
        title: "Hours"
      },
      legend: {
        cursor: "pointer",
        itemclick: toggleDataSeries
      },
      toolTip: {
        shared: true,
        content: toolTipFormatter
      },
      data: [{
        type: "bar",
        showInLegend: true,
        name: "ESTIMATION",
        color: "#64e3df",
        dataPoints: EstimationCount
      },
      {
        type: "bar",
        showInLegend: true,
        name: "USED ESTIMATION",
        color: "blue",
        dataPoints: UsedEstiamtionCount
      },
      {
        type: "bar",
        showInLegend: true,
        name: "REMAINING ESTIMATION",
        color: "#656e63",
        dataPoints: RemainingEstimationCount
      }]
    });
    chart.render();

    function toolTipFormatter(e) {
      var str = "";
      var total = 0;
      var str3;
      var str2;
      for (var i = 0; i < e.entries.length; i++) {
        var str1 = "<span style= \"color:" + e.entries[i].dataSeries.color + "\">" + e.entries[i].dataSeries.name + "</span>: <strong>" + e.entries[i].dataPoint.y + "</strong> <br/>";
        total = e.entries[i].dataPoint.y + total;
        str = str.concat(str1);
      }
      str2 = "<strong>" + e.entries[0].dataPoint.label + "</strong> <br/>";
      //str3 = "<span style = \"color:Tomato\">Total: </span><strong>" + total + "</strong><br/>";
      return (str2.concat(str));
    }

    function toggleDataSeries(e) {
      if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
        e.dataSeries.visible = false;
      }
      else {
        e.dataSeries.visible = true;
      }
      chart.render();
    }

  }

  BindTaskEstimation(TaskEstimationGraph) {
    var EstimationCount = [];
    var UsedEstiamtionCount = [];
    var RemainingEstimationCount = [];

    TaskEstimationGraph.forEach(function (x) {

      if (x.UsedEstimation == 0) {
        x.RemainingEstimation = x.Estimation;
      }
      EstimationCount.push({ 'y': x.Estimation, 'label': x.Title });
      UsedEstiamtionCount.push({ 'y': x.UsedEstimation, 'label': x.Title });
      RemainingEstimationCount.push({ 'y': x.RemainingEstimation, 'label': x.Title });
    });


    var chart = new CanvasJS.Chart("TaskContainer", {
      animationEnabled: true,
      axisY: {
        title: "Hours"
      },
      legend: {
        cursor: "pointer",
        itemclick: toggleDataSeries
      },
      toolTip: {
        shared: true,
        content: toolTipFormatter
      },
      data: [{
        type: "bar",
        showInLegend: true,
        name: "ESTIMATION",
        color: "#b8f54e",
        dataPoints: EstimationCount
      },
      {
        type: "bar",
        showInLegend: true,
        name: "USED ESTIMATION",
        color: "blue",
        dataPoints: UsedEstiamtionCount
      },
      {
        type: "bar",
        showInLegend: true,
        name: "REMAINING ESTIMATION",
        color: "#656e63",
        dataPoints: RemainingEstimationCount
      }]
    });
    chart.render();

    function toolTipFormatter(e) {
      var str = "";
      var total = 0;
      var str3;
      var str2;
      for (var i = 0; i < e.entries.length; i++) {
        var str1 = "<span style= \"color:" + e.entries[i].dataSeries.color + "\">" + e.entries[i].dataSeries.name + "</span>: <strong>" + e.entries[i].dataPoint.y + "</strong> <br/>";
        total = e.entries[i].dataPoint.y + total;
        str = str.concat(str1);
      }
      str2 = "<strong>" + e.entries[0].dataPoint.label + "</strong> <br/>";
      //str3 = "<span style = \"color:Tomato\">Total: </span><strong>" + total + "</strong><br/>";
      return (str2.concat(str));
    }

    function toggleDataSeries(e) {
      if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
        e.dataSeries.visible = false;
      }
      else {
        e.dataSeries.visible = true;
      }
      chart.render();
    }

  }

  BindBacklogEstimationFilter() {
    this.PopUpErroMessage = "";

    if (this.BacklogData == undefined || this.BacklogData.length == 0) {
      this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: "No Backlog Found." });
      return;
    }

    this.dropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'text',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 3,
      allowSearchFilter: true,
      enableCheckAll: false,
      limitSelection: 10
    };

    var arr = [];
    this.BacklogData.forEach(function (x) {
      arr.push({ id: x.ProductBacklogId, text: x.Title });
    });

    if (this.selectedItems == undefined || this.selectedItems.length == 0) {
      var arrItem = [];
      this.SelectedBacklogItem.map(function (x) {
        arrItem.push({ id: x.ProductBacklogId, text: x.Title });
      })
      this.selectedItems = arrItem;
    }

    this.Userobj = arr;
    this.backlogdisplay = true;
  }

  BindBacklogEstimationFilterValue() {
    var _me = this;
    var Model = [];
    var Count = 0;
    this.PopUpErroMessage = "";

    if (this.selectedItems == undefined || this.selectedItems.length == 0) {
      this.PopUpErroMessage = "Please select atleast one backlog.";
      this.FormDisabled = false;
      return;
    }

    this.BacklogData.map(function (x) {
      _me.selectedItems.map(function (y) {
        if (y.id == x.ProductBacklogId) {
          Model.push(x);
        }
      });
    });

    Model.map(function (x) {
      if (Count < 10) {
        if (x.Estimation != 0) {
          var CreatedDate = new Date(x.CreatedOn);
          if (CreatedDate < _me.CurrentDate) {
            var DateDiff = _me.miscellaneousService.CalculateDateDiff(x.CreatedOn);
            var calDiff = (DateDiff * 8);

            if (calDiff != 0) {
              if (calDiff >= x.Estimation) {
                x.UsedEstimation = calDiff - x.Estimation;
                x.RemainingEstimation = 0;
              }
              else {
                x.UsedEstimation = x.Estimation - calDiff;
                x.RemainingEstimation = x.Estimation - x.UsedEstimation;
              }
            }
            else {
              x.RemainingEstimation = x.Estimation;
              x.UsedEstimation = 0
            }
          }
          else {
            x.RemainingEstimation = x.Estimation;
            x.UsedEstimation = 0
          }
        }
        else {
          x.RemainingEstimation = 0;
          x.UsedEstimation = 0
        }
      }
      Count++;
    })

    this.SelectedBacklogItem = Model;
    this.BindBackLogEstimation(Model);
    this.backlogdisplay = false;
  }

  BindTaskEstimationFilter() {
    this.PopUpErroMessage = "";

    if (this.TaskData == undefined || this.TaskData.length == 0) {
      this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: "No Task Found." });
      return;
    }

    this.taskdropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'text',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 3,
      allowSearchFilter: true,
      enableCheckAll: false,
      limitSelection: 10
    };

    var arr = [];
    this.TaskData.forEach(function (x) {
      arr.push({ id: x.ProductBacklogDataId, text: x.Title });
    });

    if (this.TaskselectedItems == undefined || this.TaskselectedItems.length == 0) {
      var arrItem = [];
      this.SelectedTaskItem.map(function (x) {
        arrItem.push({ id: x.ProductBacklogDataId, text: x.Title });
      })
      this.TaskselectedItems = arrItem;
    }

    this.Userobj = arr;
    this.taskdisplay = true;
  }

  BindTaskEstimationFilterValue() {
    var _me = this;
    var Model = [];
    var Count = 0;
    this.PopUpErroMessage = "";

    if (this.TaskselectedItems == undefined || this.TaskselectedItems.length == 0) {
      this.PopUpErroMessage = "Please select atleast one task.";
      this.FormDisabled = false;
      return;
    }

    this.TaskData.map(function (x) {
      _me.TaskselectedItems.map(function (y) {
        if (y.id == x.ProductBacklogDataId) {
          Model.push(x);
        }
      });
    });

    this.SelectedTaskItem = Model;
    this.BindTaskEstimation(Model);
    this.taskdisplay = false;
  }

  UserFilter(model) {
    this.DashboardName = model;
    var $cols = $('.user-filter').click(function (e) {
      $('div#a0').removeAttr('id');
      $cols.removeClass('user-filters');
      $(this).addClass('user-filters');
    });
  }

  GoToMainDashboard() {
    this._localStorageService.add("ProjectId", 0);
    this._localStorageService.add("ProjectName", "");
    this._localStorageService.add("PhaseId", 0);
    this._localStorageService.add("PhaseName", "");
    this._localStorageService.add("IsMainDashboard", true);
    this.miscellaneousService.ChangeNavBarData("1");
    this.miscellaneousService.ChangePhaseData("1");
    this.router.navigate(['/maindashboard']);
  }

  NextUserRecord() {
    this.showleft = true;
    this.userpagination = this.userpagination + 1;
    var startnum = this.userpagination * 10;
    var endnum = startnum + 10;

    if (this.AssignUserobj.length < endnum) {
      endnum = this.AssignUserobj.length;
      this.showright = false; 
    }

    this.ShowAssignedUser = [];
    for (var i = startnum; i < endnum; i++) {
      this.ShowAssignedUser.push(this.AssignUserobj[i]);
    }
  }

  PrevUserRecord() {
    this.showright = true;
    this.showleft = true;
    this.userpagination = this.userpagination - 1;
    var startnum = this.userpagination * 10;
    var endnum = startnum + 10;

    if (this.userpagination == 0) {
      this.showleft = false;
    }

    this.ShowAssignedUser = [];
    for (var i = startnum; i < endnum; i++) {
      this.ShowAssignedUser.push(this.AssignUserobj[i]);
    }
  }

  CloseEstimation() {

    var arrItem = [];
    this.SelectedBacklogItem.map(function (x) {
      arrItem.push({ id: x.ProductBacklogId, text: x.Title });
    })
    this.selectedItems = arrItem;
    this.backlogdisplay = false;
  }

  CloseTask() {

    var arrItem = [];
    this.SelectedTaskItem.map(function (x) {
      arrItem.push({ id: x.ProductBacklogDataId, text: x.Title });
    })
    this.TaskselectedItems = arrItem;
    this.taskdisplay = false;
  }
}
