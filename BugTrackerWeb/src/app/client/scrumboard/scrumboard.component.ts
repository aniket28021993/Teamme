import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { DashboardService } from '../../miscellaneous/dashboard.service';
import { MessageService } from 'primeng/api';
import { LocalStorageService } from 'angular-2-local-storage';
import { MiscellaneousService } from '../../miscellaneous/miscellaneous.service';
import { MatTabGroup } from '@angular/material/tabs';
import { APIUrl } from '../../appsetting';
import { PhaseService } from '../../miscellaneous/phase.service';
import { BacklogService } from '../../miscellaneous/backlog.service';
import { TaskService } from '../../miscellaneous/task.service';
import { EnhancementService } from '../../miscellaneous/enhancement.service';
import { BugService } from '../../miscellaneous/bug.service';
import { TestCaseService } from '../../miscellaneous/testcase.service';
import { UserService } from '../../miscellaneous/user.service';

@Component({
  selector: 'app-scrumboard',
  templateUrl: './scrumboard.component.html',
  styleUrls: ['./scrumboard.component.css']
})
export class ScrumBoardComponent implements OnInit {

  //Map user
  ProjectId: number;
  AssignUserobj: any[] = [];
  Phaseobj: any[] = [];

  changedisplay: boolean = false;
  changebugdisplay: boolean = false;
  DeleteProductBacklogDataId: number;
  TypeId: number = 1;

  BacklogLength: number = 0;
  TaskLength: number = 0;
  BugLength: number = 0;
  TestCaseLength: number = 0;
  EnhancementLength: number = 0;

  //Backlog
  PhaseId: number;
  PhaseMoveToId: number;
  deletedisplay: boolean = false;
  BacklogData: any[] = [];
  BacklogDataProgress: any[] = [];
  BacklogDataPaused: any[] = [];
  BacklogDataTesting: any[] = [];
  BacklogDataDone: any[] = [];
  EditBacklogData: any;
  FileData: any;
  Url: string;
  IsCreateBacklog: boolean = false;
  BacklogTitle: string;

  //Task
  TaskData: any[] = [];
  EditTaskData: any;
  deletetaskdisplay: boolean = false;
  IsAddTask: boolean = false;
  TaskTitle: any;

  //Bug
  BugData: any[] = [];
  EditBugData: any;
  deletebugdisplay: boolean = false;
  IsAddBug: boolean = false;
  BugTitle: any;

  //Testcase
  TestCaseData: any[] = [];
  EditTestCaseData: any;
  deletetestcasedisplay: boolean = false;
  TestCaseSteps: any[] = [];
  TestCaseStepTitle: any;
  IsAddTestCase: boolean = false;
  TestCaseTitle: any;

  //Enhancement
  EnhancementData: any[] = [];
  EditEnhancementData: any;
  deleteenhancementdisplay: boolean = false;
  IsAddEnhancement: boolean = false;
  EnhancementTitle: any;

  IsEditBacklog: boolean = false;
  IsEditTask: boolean = false;
  IsEditBug: boolean = false;
  IsEditEnhancement: boolean = false;
  IsEditTestCase: boolean = false;

  IsTaskShow: boolean = false;
  IsBugShow: boolean = false;
  IsTestCaseShow: boolean = false;
  IsEnhancementShow: boolean = false;

  BacklogStateArr = [
    { 'id': 1, 'title': 'New' },
    { 'id': 2, 'title': 'Active' },
    { 'id': 3, 'title': 'Paused' },
    { 'id': 4, 'title': 'Testing' },
    { 'id': 5, 'title': 'Done' }
  ];

  TaskStateArr = [
    { 'id': 1, 'title': 'New' },
    { 'id': 2, 'title': 'Active' },
    { 'id': 3, 'title': 'Paused' },
    { 'id': 5, 'title': 'Done' }
  ];

  EnhancementStateArr = [
    { 'id': 1, 'title': 'New' },
    { 'id': 2, 'title': 'Active' },
    { 'id': 3, 'title': 'Paused' },
    { 'id': 4, 'title': 'Testing' },
    { 'id': 5, 'title': 'Done' }
  ];

  BugStateArr = [
    { 'id': 1, 'title': 'New' },
    { 'id': 2, 'title': 'Active' },
    { 'id': 3, 'title': 'Paused' },
    { 'id': 4, 'title': 'Testing' },
    { 'id': 5, 'title': 'Done' },
    { 'id': 6, 'title': 'Duplicate' },
    { 'id': 7, 'title': 'Reopen' }
  ];

  //common
  ProductBacklogId: number;

  @ViewChild('tabGroup', { static: false }) tabGroup: MatTabGroup;

  IsBacklogComment: boolean = false;
  IsBacklogActivity: boolean = false;

  IsTaskComment: boolean = false;
  IsTaskActivity: boolean = false;

  IsBugComment: boolean = false;
  IsBugActivity: boolean = false;

  IsEnhancementComment: boolean = false;
  IsEnhancementActivity: boolean = false;

  constructor(private dashboardService: DashboardService, private messageservice: MessageService, private _localStorageService: LocalStorageService, private miscellaneousService: MiscellaneousService, private apiurl: APIUrl, private phaseService: PhaseService, private backlogService: BacklogService, private taskService: TaskService, private enhancementService: EnhancementService, private bugService: BugService, private testcaseService: TestCaseService, private userService: UserService) {
    this.miscellaneousService.OverallData.subscribe(currentData => {
      if (currentData != null && currentData != "") {
        this.PhaseId = this._localStorageService.get("PhaseId");
        this.PhaseMoveToId = this._localStorageService.get("PhaseId");
        this.GetAllProductBacklog();
      }
    });
  }

  ngOnInit() {
    this.miscellaneousService.ChangeBreadcrumbData("SCRUMBOARD");
    this.PhaseId = this._localStorageService.get("PhaseId");
    this.PhaseMoveToId = this._localStorageService.get("PhaseId");
    this.ProjectId = this._localStorageService.get("ProjectId");

    this.Url = this.apiurl.baseurl + "/Dashboard/DownloadFile?RefFileName=";

    this.GetAllProductBacklog();
    this.GetAllMapUser();
    this.GetAllPhase();
  }

  //MAP USER
  GetAllMapUser() {
    this.userService.GetAllMapUser(this.ProjectId).subscribe(proj => {
      if (proj != null) {
        this.AssignUserobj = proj;
      }
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  GetAllPhase() {
    this.Phaseobj = [];

    this.phaseService.GetAllPhase(this.ProjectId).subscribe(proj => {
      if (proj != null) {
        this.Phaseobj = proj;
      }
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  PhaseMoveTo(ProductBacklogId) {
    this.backlogService.PhaseMoveTo(ProductBacklogId, this.PhaseMoveToId).subscribe(proj => {
      this.PhaseMoveToId = this.PhaseId;
      this.GetAllProductBacklog();
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: 'Backlog moved to another phase.' });
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  //BACKLOG
  CreateProductBacklog() {    

    if (this.BacklogTitle == undefined || this.BacklogTitle.trim() == "") {
      this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: "Title required." });
      return;
    }
    
    var ProductBacklog = {
      'Title': this.BacklogTitle,
      'Description': "",
      'State': 1,
      'AssignedTo': this.miscellaneousService.UserInfo.OrgUserId,
      'Estimation': 0,
      'PhaseId': this.PhaseId,
      'ProjectId': this.ProjectId
    };

    this.backlogService.CreateProductBacklog(ProductBacklog).subscribe(ProductBacklogId => {
      
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: "Backlog created." });
      this.BacklogTitle = "";
      this.IsCreateBacklog = false;
      this.GetAllProductBacklog();
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  GetAllProductBacklog() {
    this.BacklogData = [];
    this.BacklogLength = 0;

    this.backlogService.GetAllProductBacklog(this.PhaseId).subscribe(backlog => {
      if (backlog != null) {
        backlog.map(function (x) {
          var mapinitail = "";
          var initailarr = [];

          if (x.UserName != null) {
            initailarr = x.UserName.split(" ");
            initailarr.map(function (y) {
              mapinitail = mapinitail + y.charAt(0);
            })
          }
          x.OwnerInitial = mapinitail;

          mapinitail = "";
          if (x.AssignedDesignerName != null) {
            initailarr = x.AssignedDesignerName.split(" ");
            initailarr.map(function (y) {
              mapinitail = mapinitail + y.charAt(0);
            })
          }
          x.DesignerInitial = mapinitail;

          mapinitail = "";
          if (x.AssignedDeveloperName != null) {
            initailarr = x.AssignedDeveloperName.split(" ");
            initailarr.map(function (y) {
              mapinitail = mapinitail + y.charAt(0);
            })
          }
          x.DeveloperInitial = mapinitail;
 

          mapinitail = "";
          if (x.AssignedTesterName != null) {
            initailarr = x.AssignedTesterName.split(" ");
            initailarr.map(function (y) {
              mapinitail = mapinitail + y.charAt(0);
            })
          }
          x.TesterInitial = mapinitail;
          
        })

        this.BacklogData = backlog.filter(x => x.State == 1);
        this.BacklogLength = this.BacklogData.length;
        
        this.BacklogDataProgress = backlog.filter(x => x.State == 2);

        this.BacklogDataPaused = backlog.filter(x => x.State == 3);

        this.BacklogDataTesting = backlog.filter(x => x.State == 4);

        this.BacklogDataDone = backlog.filter(x => x.State == 5);
        
      }
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  EditProductBacklog() {
    if (this.EditBacklogData.Title == undefined || this.EditBacklogData.Title.trim() == "") {
      this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: "Title required." });
      return;
    }

    this.backlogService.EditProductBacklog(this.EditBacklogData).subscribe(proj => {
      this.CloseRightPanel();
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: "Backlog updated." });
      this.GetAllProductBacklog();
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  DeleteProductBacklog(ProductBacklogId) {
    this.backlogService.DeleteProductBacklog(ProductBacklogId).subscribe(proj => {
      this.deletedisplay = false;
      this.GetAllProductBacklog();
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: 'Backlog deleted.' });
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  DeleteProductBacklogFile(ProductBacklogFileId) {

    this.backlogService.DeleteProductBacklogFile(ProductBacklogFileId).subscribe(proj => {
      this.EditBacklogData.ProductBacklogFile = this.EditBacklogData.ProductBacklogFile.filter(x => x.ProductBacklogFileId != ProductBacklogFileId); 
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: 'Backlog file deleted.' });
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  ChangeBacklogState(BacklogData) {
    this.backlogService.EditProductBacklog(BacklogData).subscribe(proj => {
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: "Backlog state changed." });
      this.GetAllProductBacklog();
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  OpenRightPanel(Model) {
    this.IsEditBacklog = true;
    this.IsEditTask = false;
    this.IsEditTestCase = false;
    this.IsEditBug = false;
    this.IsEditEnhancement = false;

    this.IsBacklogComment = false;
    this.IsBacklogActivity = false;

    this.EditBacklogData = {
      'ProductBacklogId': Model.ProductBacklogId,
      'Title': Model.Title,
      'Description': Model.Description,
      'Estimation': Model.Estimation,
      'AssignedTo': Model.AssignedTo,
      'State': Model.State,
      'UserName': Model.UserName,
      'AssignedDesigner': Model.AssignedDesigner,
      'AssignedDeveloper': Model.AssignedDeveloper,
      'AssignedTester': Model.AssignedTester,
      'ProductBacklogFile': Model.productBacklogFileDTO

    };
    document.getElementById("RightSidepanel").style.display = "block";
    if (this.tabGroup != undefined) {
      this.tabGroup.selectedIndex = 0;
    }
}
  CloseRightPanel() {
    document.getElementById("RightSidepanel").style.display = "none";
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
        this.EditBacklogData.ProductBacklogFile = proj;
        if (this.EditBacklogData.State == 1) {
          this.BacklogData.map(function (x) {
            if (x.ProductBacklogId == ProductBacklogId) {
              x.productBacklogFileDTO = proj;
            }
          })
        }
        else if (this.EditBacklogData.State == 2) {
          this.BacklogDataProgress.map(function (x) {
            if (x.ProductBacklogId == ProductBacklogId) {
              x.productBacklogFileDTO = proj;
            }
          })
        }
        else if (this.EditBacklogData.State == 3) {
          this.BacklogDataPaused.map(function (x) {
            if (x.ProductBacklogId == ProductBacklogId) {
              x.productBacklogFileDTO = proj;
            }
          })
        }
        else if (this.EditBacklogData.State == 4) {
          this.BacklogDataTesting.map(function (x) {
            if (x.ProductBacklogId == ProductBacklogId) {
              x.productBacklogFileDTO = proj;
            }
          })
        }
        else if (this.EditBacklogData.State == 5) {
          this.BacklogDataDone.map(function (x) {
            if (x.ProductBacklogId == ProductBacklogId) {
              x.productBacklogFileDTO = proj;
            }
          })
        }
      }
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });
  }


  //TASK
  CreateTask() {
    if (this.TaskTitle == undefined || this.TaskTitle.trim() == "") {
      this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: "Title required." });
      return;
    }

    var task = {
      'Title': this.TaskTitle,
      'Description': "",
      'State': 1,
      'AssignedTo': this.miscellaneousService.UserInfo.OrgUserId,
      'Estimation': 0,
      'PhaseId': this.PhaseId,
      'ProjectId': this.ProjectId,
      'ProductBacklogId': this.ProductBacklogId
    };

    this.taskService.CreateTask(task).subscribe(task => {
      this.TaskTitle = "";
      this.IsAddTask = false;
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: "Task created." });
      this.GetAllProductTask();
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  GetAllProductTask() {
    this.TaskData = [];
    this.TaskLength = 0;

    this.taskService.GetAllProductTask(this.ProductBacklogId).subscribe(backlogTask => {
      if (backlogTask != null) {

        backlogTask.map(function (x) {
          var mapinitail = "";
          var initailarr = x.UserName.split(" ");
          initailarr.map(function (y) {
            mapinitail = mapinitail + y.charAt(0);
          })
          x.OwnerInitial = mapinitail;
        })

        this.TaskData = backlogTask;
        this.TaskLength = this.TaskData.length;
      }
      document.getElementById("PBDataPanel").style.display = "block";
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  EditTask() {

    if (this.EditTaskData.Title == undefined || this.EditTaskData.Title.trim() == "") {
      this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: "Title required." });
      return;
    }

    this.taskService.EditTask(this.EditTaskData).subscribe(proj => {
      this.CloseRightPanel();
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: "Task updated." });
      this.GetAllProductTask();
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  DeleteTask(TaskId) {

    this.taskService.DeleteTask(TaskId).subscribe(proj => {
      this.deletetaskdisplay = false;
      this.GetAllProductTask();
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: 'Task Deleted.' });
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  ChangeTaskState(TaskData) {
    this.taskService.EditTask(TaskData).subscribe(proj => {
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: "Task state changed." });
      this.GetAllProductTask();
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  OpenTaskPanel(ProductBacklogId) {
    this.IsBugShow = false;
    this.IsTestCaseShow = false;
    this.IsEnhancementShow = false;
    this.IsTaskShow = true;

    this.ProductBacklogId = ProductBacklogId;
    this.GetAllProductTask();
  }
  OpenRightTaskPanel(Model) {
    this.IsEditBacklog = false;
    this.IsEditTask = true;
    this.IsEditTestCase = false;
    this.IsEditBug = false;
    this.IsEditEnhancement = false;

    this.IsTaskComment = false;
    this.IsTaskActivity = false;

    this.EditTaskData = {
      'ProductBacklogDataId': Model.ProductBacklogDataId,
      'Title': Model.Title,
      'Description': Model.Description,
      'Estimation': Model.Estimation,
      'UsedEstimation': Model.UsedEstimation,
      'RemainingEstimation': Model.RemainingEstimation,
      'AssignedTo': Model.AssignedTo,
      'State': Model.State,
      'UserName': Model.UserName,
      'ProductBacklogFile': Model.productBacklogFileDTO
    };
    document.getElementById("RightSidepanel").style.display = "block";
    if (this.tabGroup != undefined) {
      this.tabGroup.selectedIndex = 0;
    }
  }
  onTaskFileChange(event, ProductBacklogDataId, ) {
    if (event.target.files.length > 0) {
      this.FileData = event.target.files;
      for (let i = 0; i < this.FileData.length; i++) {
        this.TaskFileUpload(ProductBacklogDataId, this.FileData[i]);
      }
    }
  }
  TaskFileUpload(ProductBacklogDataId, FileData) {

    const formData = new FormData();
    formData.append('DocFile', FileData);
    formData.append('CommonId', ProductBacklogDataId);
    formData.append('Type', '2');
    this.dashboardService.FileUpload(formData).subscribe(proj => {
      if (proj != null) {
        this.EditTaskData.ProductBacklogFile = proj;
        this.TaskData.map(function (x) {
          if (x.ProductBacklogDataId == ProductBacklogDataId) {
            x.productBacklogFileDTO = proj;
          }
        })
      }
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });
  }
  DeleteTaskFile(ProductBacklogDataFileId) {

    this.backlogService.DeleteProductBacklogFile(ProductBacklogDataFileId).subscribe(proj => {
      this.EditTaskData.ProductBacklogFile = this.EditTaskData.ProductBacklogFile.filter(x => x.ProductBacklogFileId != ProductBacklogDataFileId);
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: 'Task file deleted.' });
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  TaskToEnhancement(TaskId) {
    this.taskService.TaskToEnhancement(TaskId).subscribe(proj => {
      this.changedisplay = false;
      this.GetAllProductTask();
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: 'Task changed to enhancement.' });
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  //BUG
  CreateBug() {
    if (this.BugTitle == undefined || this.BugTitle.trim() == "") {
      this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: "Title required." });
      return;
    }

    var Bug = {
      'Title': this.BugTitle,
      'Description': "",
      'State': 1,
      'AssignedTo': this.miscellaneousService.UserInfo.OrgUserId,
      'PhaseId': this.PhaseId,
      'ProjectId': this.ProjectId,
      'ProductBacklogId': this.ProductBacklogId,
      'PriorityId': 1
    };

    this.bugService.CreateBug(Bug).subscribe(Bug => {
      this.BugTitle = "";
      this.IsAddBug = false;
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: "Bug created." });
      this.GetAllProductBug();
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  GetAllProductBug() {
    this.BugData = [];
    this.BugLength = 0;

    this.bugService.GetAllProductBug(this.ProductBacklogId).subscribe(backlogbug => {
      if (backlogbug != null) {

        backlogbug.map(function (x) {
          var mapinitail = "";
          var initailarr = x.UserName.split(" ");
          initailarr.map(function (y) {
            mapinitail = mapinitail + y.charAt(0);
          })
          x.OwnerInitial = mapinitail;
        })

        this.BugData = backlogbug;
        this.BugLength = this.BugData.length;
      }
      document.getElementById("PBDataPanel").style.display = "block";
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });
  }
  EditBug() {
    if (this.EditBugData.Title == undefined || this.EditBugData.Title.trim() == "") {
      this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: "Title required." });
      return;
    }
    
    this.bugService.EditBug(this.EditBugData).subscribe(proj => {
      this.CloseRightPanel();
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: "Bug updated." });
      this.GetAllProductBug();
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  DeleteBug(BugId) {
    this.bugService.DeleteBug(BugId).subscribe(proj => {
      this.deletebugdisplay = false;
      this.GetAllProductBug();
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: 'Bug deleted.' });
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  ChangeBugState(BugData) {
    this.bugService.EditBug(BugData).subscribe(proj => {
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: "Bug state changed." });
      this.GetAllProductBug();
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });


  }
  OpenBugPanel(ProductBacklogId) {
    this.IsTaskShow = false;
    this.IsTestCaseShow = false;
    this.IsEnhancementShow = false;
    this.IsBugShow = true;

    this.ProductBacklogId = ProductBacklogId;
    this.GetAllProductBug();
  }
  OpenRightBugPanel(Model) {
    this.IsEditBacklog = false;
    this.IsEditTask = false;
    this.IsEditTestCase = false;
    this.IsEditBug = true;
    this.IsEditEnhancement = false;

    this.IsBugComment = false;
    this.IsBugActivity = false;

    this.EditBugData = {
      'ProductBacklogDataId': Model.ProductBacklogDataId,
      'Title': Model.Title,
      'Description': Model.Description,
      'AssignedTo': Model.AssignedTo,
      'State': Model.State,
      'UserName': Model.UserName,
      'PriorityId': Model.PriorityId,
      'ProductBacklogFile': Model.productBacklogFileDTO

    };
    document.getElementById("RightSidepanel").style.display = "block";
    if (this.tabGroup != undefined) {
      this.tabGroup.selectedIndex = 0;
    }
  }
  onBugFileChange(event, ProductBacklogDataId, ) {
    if (event.target.files.length > 0) {
      this.FileData = event.target.files;
      for (let i = 0; i < this.FileData.length; i++) {
        this.BugFileUpload(ProductBacklogDataId, this.FileData[i]);
      }
    }
  }
  BugFileUpload(ProductBacklogDataId, FileData) {

    const formData = new FormData();
    formData.append('DocFile', FileData);
    formData.append('CommonId', ProductBacklogDataId);
    formData.append('Type', '2');
    this.dashboardService.FileUpload(formData).subscribe(proj => {
      if (proj != null) {
        this.EditBugData.ProductBacklogFile = proj;
        this.BugData.map(function (x) {
          if (x.ProductBacklogDataId == ProductBacklogDataId) {
            x.productBacklogFileDTO = proj;
          }
        })
      }
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });
  }
  DeleteBugFile(ProductBacklogDataFileId) {

    this.backlogService.DeleteProductBacklogFile(ProductBacklogDataFileId).subscribe(proj => {
      this.EditBugData.ProductBacklogFile = this.EditBugData.ProductBacklogFile.filter(x => x.ProductBacklogFileId != ProductBacklogDataFileId);
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: 'Bug file deleted.' });
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  BugToEnhancement(BugId) {
    this.bugService.BugToEnhancement(BugId).subscribe(proj => {
      this.changebugdisplay = false;
      this.GetAllProductBug();
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: 'Bug changed to enhancement.' });
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  //TestCase
  CreateTestCase() {
    if (this.TestCaseTitle == undefined || this.TestCaseTitle.trim() == "") {
      this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: "Title required." });
      return;
    }

    var TestCase = {
      'Title': this.TestCaseTitle,
      'Description': "",
      'PreCondition': "",
      'ExpectedResult': "",
      'ActualResult': "",
      'PhaseId': this.PhaseId,
      'ProjectId': this.ProjectId,
      'ProductBacklogId': this.ProductBacklogId
    };

    this.testcaseService.CreateTestCase(TestCase).subscribe(testcase => {

      this.TestCaseTitle = "";
      this.IsAddTestCase = false;
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: "TestCase created." });
      this.GetAllTestCase();
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  GetAllTestCase() {
    this.TestCaseData = [];
    this.TestCaseLength = 0;

    this.testcaseService.GetAllTestCase(this.ProductBacklogId).subscribe(testcase => {
      if (testcase != null) {
        testcase.map(function (x) {
          var mapinitail = "";
          var initailarr = x.CreatedName.split(" ");
          initailarr.map(function (y) {
            mapinitail = mapinitail + y.charAt(0);
          })
          x.OwnerInitial = mapinitail;
        })        
        this.TestCaseData = testcase;
        this.TestCaseLength = this.TestCaseData.length;
      }
      document.getElementById("PBDataPanel").style.display = "block";
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  EditTestCase() {
    if (this.EditTestCaseData.Title == undefined || this.EditTestCaseData.Title.trim() == "") {
      this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: "Title required." });
      return;
    }

    this.testcaseService.EditTestCase(this.EditTestCaseData).subscribe(proj => {
      this.CloseRightPanel();
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: "TestCase updated." });
      this.GetAllTestCase();
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  DeleteTestCase(TestCaseId) {

    this.testcaseService.DeleteTestCase(TestCaseId).subscribe(proj => {
      this.deletetestcasedisplay = false;
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: 'TestCase Deleted.' });
      this.GetAllTestCase();
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  IsTestCasePass(TestCaseId,IsPass) {

    this.testcaseService.IsTestCasePass(TestCaseId, IsPass).subscribe(proj => {
        this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: "TestCase status updated." });
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  OpenTestcasePanel(ProductBacklogId) {
    this.IsTaskShow = false;
    this.IsBugShow = false;
    this.IsEnhancementShow = false;
    this.IsTestCaseShow = true;

    this.ProductBacklogId = ProductBacklogId;
    this.GetAllTestCase();
  }
  OpenRightTestCasePanel(Model) {
    this.IsEditBacklog = false;
    this.IsEditTask = false;
    this.IsEditTestCase = true;
    this.IsEditBug = false;
    this.IsEditEnhancement = false;

    this.EditTestCaseData = {
      'TestCaseId': Model.TestCaseId,
      'Title': Model.Title,
      'Description': Model.Description,
      'CreatedName': Model.CreatedName,
      'ExpectedResult': Model.ExpectedResult,
      'ActualResult': Model.ActualResult,
      'PreCondition': Model.PreCondition,
      'TestCaseStep': Model.TestCaseStep
    };
    document.getElementById("RightSidepanel").style.display = "block";
  }
  AddTestCaseStep() {
    if (this.TestCaseStepTitle == undefined || this.TestCaseStepTitle.trim() == "") {
      this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: "Please enter step description." });
      return;
    }

    var TestCaseStep = {
      'Description': this.TestCaseStepTitle,
      'TestCaseId': this.EditTestCaseData.TestCaseId
    }

    this.testcaseService.CreateTestCaseStep(TestCaseStep).subscribe(proj => {
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: "TestCase step added." });
      this.GetAllTestCase();
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

    if (this.EditTestCaseData.TestCaseStep == null) {
      this.EditTestCaseData.TestCaseStep = [{ 'Description': this.TestCaseStepTitle, 'IsChecked': false, 'IsDelete': false }];
    }
    else {
      this.EditTestCaseData.TestCaseStep.push({ 'Description': this.TestCaseStepTitle, 'IsChecked': false, 'IsDelete': false });
    }    
    this.TestCaseStepTitle = "";
  }
  DeleteTestCaseStep(step) {
    this.testcaseService.DeleteTestCaseStep(step.TestCaseStepId).subscribe(proj => {

      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: "TestCase step deleted." });
      this.EditTestCaseData.TestCaseStep = this.EditTestCaseData.TestCaseStep.filter(x => x.TestCaseStepId != step.TestCaseStepId);
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  //Enhancement
  CreateEnhancement() {
    if (this.EnhancementTitle == undefined || this.EnhancementTitle.trim() == "") {
      this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: "Title required." });
      return;
    }

    var Enhancement = {
      'Title': this.EnhancementTitle,
      'Description': "",
      'State': 1,
      'AssignedTo': this.miscellaneousService.UserInfo.OrgUserId,
      'PhaseId': this.PhaseId,
      'ProjectId': this.ProjectId,
      'ProductBacklogId': this.ProductBacklogId
    };

    this.enhancementService.CreateEnhancement(Enhancement).subscribe(Enhancement => {

      this.EnhancementTitle = "";
      this.IsAddEnhancement = false;
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: "Enhancement created." });
      this.GetAllProductEnhancement();
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  GetAllProductEnhancement() {
    this.EnhancementData = [];
    this.EnhancementLength = 0;

    this.enhancementService.GetAllProductEnhancement(this.ProductBacklogId).subscribe(enhancement => {
      if (enhancement != null) {
        enhancement.map(function (x) {
          var mapinitail = "";
          var initailarr = x.UserName.split(" ");
          initailarr.map(function (y) {
            mapinitail = mapinitail + y.charAt(0);
          })
          x.OwnerInitial = mapinitail;
        })
        this.EnhancementData = enhancement;
        this.EnhancementLength = this.EnhancementData.length;
      }
      document.getElementById("PBDataPanel").style.display = "block";
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  EditEnhancement() {
    if (this.EditEnhancementData.Title == undefined || this.EditEnhancementData.Title.trim() == "") {
      this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: "Title required." });
      return;
    }
    
    this.enhancementService.EditEnhancement(this.EditEnhancementData).subscribe(proj => {
      this.CloseRightPanel();
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: "Enhancement updated." });
      this.GetAllProductEnhancement();
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  DeleteEnhancement(EnhancementId) {
    this.enhancementService.DeleteEnhancement(EnhancementId).subscribe(proj => {
      this.deleteenhancementdisplay = false;
      this.GetAllProductEnhancement();
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: 'Enhancement Deleted.' });
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });
  }
  ChangeEnhancementState(EnhancementData) {
    this.enhancementService.EditEnhancement(EnhancementData).subscribe(proj => {
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: "Enhancement state changed." });
      this.GetAllProductEnhancement();
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  OpenEnhancementPanel(ProductBacklogId) {
    this.IsTaskShow = false;
    this.IsBugShow = false;
    this.IsTestCaseShow = false;
    this.IsEnhancementShow = true;

    this.ProductBacklogId = ProductBacklogId;
    this.GetAllProductEnhancement();
  }
  OpenRightEnhancementPanel(Model) {
    this.IsEditBacklog = false;
    this.IsEditTask = false;
    this.IsEditTestCase = false;
    this.IsEditBug = false;
    this.IsEditEnhancement = true;

    this.IsEnhancementComment = false;
    this.IsEnhancementActivity = false;

    this.EditEnhancementData = {
      'ProductBacklogDataId': Model.ProductBacklogDataId,
      'Title': Model.Title,
      'Description': Model.Description,
      'AssignedTo': Model.AssignedTo,
      'State': Model.State,
      'UserName': Model.UserName,
      'ProductBacklogFile': Model.productBacklogFileDTO
    };
    document.getElementById("RightSidepanel").style.display = "block";
    if (this.tabGroup != undefined) {
      this.tabGroup.selectedIndex = 0;
    }
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
        this.EditEnhancementData.ProductBacklogFile = proj;
        this.EnhancementData.map(function (x) {
          if (x.ProductBacklogDataId == ProductBacklogDataId) {
            x.productBacklogFileDTO = proj;
          }
        })
      }
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });
  }
  DeleteEnhancementFile(ProductBacklogDataFileId) {

    this.backlogService.DeleteProductBacklogFile(ProductBacklogDataFileId).subscribe(proj => {
      this.EditEnhancementData.ProductBacklogFile = this.EditEnhancementData.ProductBacklogFile.filter(x => x.ProductBacklogFileId != ProductBacklogDataFileId);
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: 'Enhancement file deleted.' });
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
  EnhancementToTaskBug(EnhancementId) {

    this.enhancementService.EnhancementToTaskBug(EnhancementId, this.TypeId).subscribe(proj => {
      this.GetAllProductEnhancement();
      this.messageservice.add({ severity: 'success', summary: 'Success Message', detail: 'Enhancement changed to bug/task.' });
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  //Mat-tab
  OnTabClick() {
    if (this.tabGroup.selectedIndex == 1) {
      this.IsBacklogComment = true;
      this.IsTaskComment = true;
      this.IsBugComment = true;
      this.IsEnhancementComment = true;
    }
    else if (this.tabGroup.selectedIndex == 2) {
      this.IsBacklogActivity = true;
      this.IsTaskActivity = true;
      this.IsBugActivity = true;
      this.IsEnhancementActivity = true;
    }
  }
}
