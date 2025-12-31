import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { DashboardService } from '../../miscellaneous/dashboard.service';
import { Router } from '@angular/router';
import { MiscellaneousService } from '../../miscellaneous/miscellaneous.service';
import { LocalStorageService } from 'angular-2-local-storage';
import { ProjectService } from '../../miscellaneous/project.service';
import { UserService } from '../../miscellaneous/user.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './maindashboard.component.html',
})
export class MainDashboardComponent implements OnInit {
  
  //variable
  Description: string;
  EditDescription: string;
  SearchTaskval: string;
  ProjectLength: number;
  ProjectId: number;
  UserType: number;

  dropdownSettings = {};

  IsCreateProject: boolean = false;
  deletedisplay: boolean = false;
  mapdisplay: boolean = false;

  Userobj: any[] = [];
  selectedItems = [];
  MappeduserArr: any[] = [];

  ProjectObj: any;

  //Array
  Project: any[] = [];

  //Error msg
  PopUpErroMessage: string;

  constructor(private dashboardService: DashboardService, private messageService: MessageService, private router: Router, private miscellaneousService: MiscellaneousService, private projectService: ProjectService, private _localStorageService: LocalStorageService, private userService: UserService) { }

  ngOnInit() {
    this.UserType = this.miscellaneousService.UserInfo.OrgUserTypeId;
    this.miscellaneousService.ChangeBreadcrumbData("PROJECT");
    this.GetAllProject();
    this.GetAllUser();
  }

  OnKeyPress() {
    if (this.SearchTaskval != undefined && this.SearchTaskval != "") {
      this.SearchProject();
    }
    else {
      this.GetAllProject();
    }
  }

  GetAllProject() {
    
    this.projectService.GetAllProject().subscribe(proj => {
      if (proj != null) {
        this.Project = proj;
        this.ProjectLength = proj.length;
        this.Project.map(function (x) {
          x.IsCreateProject = false;
        })
      }
      else {
        this.ProjectLength = 0;
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  CreateProject() {
    this.PopUpErroMessage = "";

    if (this.Description == undefined || this.Description == "") {
      this.PopUpErroMessage = "Name Required.";
      return;
    }

    this.projectService.CreateProject(this.Description).subscribe(proj => {
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: 'Successfully Created.' });
      this.Description = "";

      this.GetAllProject();
      this.IsCreateProject = false;
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  EditProject(ProjectId) {

    if (this.EditDescription == undefined || this.EditDescription == "") {
      this.PopUpErroMessage = "Name Required.";
      return;
    }

    var Project = {
      '_ProjectName': this.EditDescription,
      '_ProjectId': ProjectId
    };

    this.projectService.EditProject(Project).subscribe(proj => {
      this.GetAllProject();
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: 'Project update successfully.' });
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  GoToDashboard(ProjectId, ProjectName) {

    this.userService.GetAllMapUser(ProjectId).subscribe(user => {
      if (user != null) {
        this._localStorageService.set("ProjectId", ProjectId);
        this._localStorageService.set("ProjectName", ProjectName);
        this._localStorageService.set("IsMainDashboard", false);

        this.miscellaneousService.ChangePhaseData("1");
        this.miscellaneousService.ChangeNavBarData("1");
        this.router.navigate(['/client-dashboard']);
      }
      else {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: "Please map user to get access" });
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });
  }

  GetAllUser() {

    this.userService.GetAllUser().subscribe(user => {
      if (user != null) {
        this.Userobj = user;
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  GetAllMapUser(ProjectId) {
    var _me = this;
    
    this.MappeduserArr = [];
    this.selectedItems = [];
    this.PopUpErroMessage = "";
    this.ProjectId = ProjectId;

    this.userService.GetAllMapUser(ProjectId).subscribe(user => {
      var arrlist = [];

      if (user != null) {

        this.dropdownSettings = {
          singleSelection: false,
          idField: 'id',
          textField: 'text',
          selectAllText: 'Select All',
          unSelectAllText: 'UnSelect All',
          itemsShowLimit: 3,
          allowSearchFilter: true,
          enableCheckAll: true,
        };

        var userarr = [];
        this.Userobj.map(function (x) {
          var UserName = x.FirstName + ' ' + x.LastName
          arrlist.push({ 'id': x.OrgUserId, 'text': UserName });
          user.map(function (y) {
            if (x.OrgUserId == y.UserId) {
              userarr.push({ 'id': x.OrgUserId, 'text': UserName });
            }
          })
        })

        this.selectedItems = userarr;
      }
      else {
        this.Userobj.map(function (x) {
          var UserName = x.FirstName + ' ' + x.LastName
          arrlist.push({ 'id': x.OrgUserId, 'text': UserName });
        })

      }

      if (arrlist.length != 0) {
        this.MappeduserArr = arrlist;
      }
      this.mapdisplay = true;
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  MapuserToProject() {

    if (this.selectedItems == undefined || this.selectedItems.length == 0) {
      this.PopUpErroMessage = "Please select atleast one user.";
      return;
    }

    var Userlist = [];

    this.MappeduserArr.map(function (x) {
      Userlist.push({ 'id': x.id, 'text': false });
    })

    this.selectedItems.map(function (x) {
      Userlist.map(function (y) {
        if (x.id == y.id) {
          y.text = true;
        }
      })
    })
    
    var MapUser = {
      'ProjectId': this.ProjectId,
      'Users': Userlist
    };

    this.userService.MapuserToProject(MapUser).subscribe(user => {
      this.mapdisplay = false;
      this.selectedItems = [];
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: 'User mapped.' });
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
     });
  }

  DeleteProject(ProjectId) {

    this.projectService.DeleteProject(ProjectId).subscribe(proj => {
      this.deletedisplay = false;
      this.GetAllProject();
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: 'Project deleted.' });
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  SearchProject() {
    if (this.SearchTaskval == undefined || this.SearchTaskval == "") {
      this.messageService.add({ severity: 'warn', summary: 'Warn Message', detail: 'Please give your input..!!' });
      return;
    }

    var SearchTaskObj = {
      'SearchTask': this.SearchTaskval
    };

    this.Project = [];
    this.projectService.SearchProject(SearchTaskObj).subscribe(proj => {
      if (proj != null) {
        this.Project = proj;
        this.ProjectLength = proj.length;
        this.Project.map(function (x) {
          x.IsCreateProject = false;
        })
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
}
