import { Component, OnInit } from '@angular/core';
import { navItems, ClientItems, TeamMemberItems, TeamLeadItems, ClientMainItems, ClientLeadItems, ClienTeamItems } from '../../_nav';
import { MiscellaneousService } from '../../miscellaneous/miscellaneous.service';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import { APIUrl } from '../../appsetting';
import { DashboardService } from '../../miscellaneous/dashboard.service';
import { LocalStorageService } from 'angular-2-local-storage';
import { PhaseService } from '../../miscellaneous/phase.service';


@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html'
})
export class DefaultLayoutComponent implements OnInit {
  public sidebarMinimized = false;
  public navItems = null;

  IsMainDashboard: boolean = false;
  createdisplay: boolean = false;
  
  UserImage: string;
  UserName: string;
  Breadcrumb: string;
  ProjectName: string;

  Phaseobj: any[] = [];
  ProjectId: number = 0;
  PhaseId: number;


  constructor(private router: Router, private Oauthservice: MiscellaneousService, private messageservice: MessageService, private ImageUrl: APIUrl,
    private dashboardService: DashboardService, private _localStorageService: LocalStorageService, private phaseService: PhaseService) {
    this.Oauthservice.ImageData.subscribe(currentData => {
      if (currentData != null && currentData != "") {
        this.UserImage = this.ImageUrl.tokenurl + "/Uploads/" + currentData;
      }
    });

    this.Oauthservice.BreadcrumbData.subscribe(currentData => {
      if (currentData != null && currentData != "") {
        this.Breadcrumb = currentData;
      }
    });

    this.Oauthservice.NavBarData.subscribe(currentData => {
      if (currentData != null && currentData != "") {
        this.IsMainDashboard = this._localStorageService.get("IsMainDashboard");

        if (this.Oauthservice.UserInfo.OrgUserTypeId == 2 || this.Oauthservice.UserInfo.OrgUserTypeId == 3) {
          if (this.IsMainDashboard == true) {
            this.navItems = ClientMainItems;
          }
          else {
            this.navItems = ClientItems;
          }
        }
        else if (this.Oauthservice.UserInfo.OrgUserTypeId == 4) {
          if (this.IsMainDashboard == true) {
            this.navItems = ClientLeadItems;
          }
          else {
            this.navItems = TeamLeadItems;
          }
        }
        else if (this.Oauthservice.UserInfo.OrgUserTypeId == 5) {
          if (this.IsMainDashboard == true) {
            this.navItems = ClienTeamItems;
          }
          else {
            this.navItems = TeamMemberItems;
          }
        }
      }
    });

    this.Oauthservice.PhaseData.subscribe(currentData => {
      if (currentData != null && currentData != "") {
        this.ProjectId = this._localStorageService.get("ProjectId");
        this.ProjectName = this._localStorageService.get("ProjectName");
        this.GetAllPhase();
      }
    });
  }

  ngOnInit() {

    this.IsMainDashboard = this._localStorageService.get("IsMainDashboard");
    this.ProjectId = this._localStorageService.get("ProjectId");
    this.ProjectName = this._localStorageService.get("ProjectName");

    if (this.Oauthservice.UserInfo.ProfileImage != null && this.Oauthservice.UserInfo.ProfileImage != "") {
      this.UserImage = this.ImageUrl.tokenurl + "/Uploads/" + this.Oauthservice.UserInfo.ProfileImage;
    }

    if (this.Oauthservice.UserInfo != null) {
      this.UserName = this.Oauthservice.UserInfo.FirstName + " " + this.Oauthservice.UserInfo.LastName;
    }

    if (this.Oauthservice.UserInfo.OrgUserTypeId == 1) {
      this.navItems = navItems;
    }
    else if (this.Oauthservice.UserInfo.OrgUserTypeId == 2 || this.Oauthservice.UserInfo.OrgUserTypeId == 3) {
      if (this.IsMainDashboard == true) {
        this.navItems = ClientMainItems;
      }
      else {
        this.navItems = ClientItems;
      }
    }
    else if (this.Oauthservice.UserInfo.OrgUserTypeId == 4) {
      if (this.IsMainDashboard == true) {
        this.navItems = ClientLeadItems;
      }
      else {
        this.navItems = TeamLeadItems;
      }
    }
    else if (this.Oauthservice.UserInfo.OrgUserTypeId == 5) {
      if (this.IsMainDashboard == true) {
        this.navItems = ClienTeamItems;
      }
      else {
        this.navItems = TeamMemberItems;
      }
    }


    if (this.ProjectId != 0) {
      this.GetAllPhase();
    }
  }

  Logout() {
    this.Oauthservice.Logout(this.Oauthservice.UserInfo.OrgUserId, this.Oauthservice.UserInfo.OrgId).subscribe(logout => {
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      }
    )

  }

  UserProfile() {
    this.router.navigate(['/userprofile']);
  }

  ChangePassword() {
    this.router.navigate(['/changepassword']);
  }

  toggleMinimize(e) {
    this.sidebarMinimized = e;
  }

  GetAllPhase() {
    this.Phaseobj = [];

    this.phaseService.GetAllPhase(this.ProjectId).subscribe(proj => {
      if (proj != null) {
        this.Phaseobj = proj;
        this.PhaseId = this._localStorageService.get("PhaseId");
        if (this.PhaseId == null || this.PhaseId == 0) {
          this.PhaseId = this.Phaseobj[0].PhaseId;
          this._localStorageService.add("PhaseId", this.Phaseobj[0].PhaseId);
        }
      }
      else {
        this._localStorageService.set("PhaseId", 0);
        this.PhaseId = 0;
      }
    },
      (error) => {
        this.messageservice.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  BindPhasetoLoggedIn() {
    this._localStorageService.add("PhaseId", this.PhaseId);
    this._localStorageService.set("BacklogDropdown", null);
    this.Oauthservice.ChangeOverallData("1");
  }

  GoToMainDashboard() {
    this._localStorageService.add("ProjectId", 0);
    this._localStorageService.add("ProjectName", "");
    this._localStorageService.add("PhaseId", 0);
    this._localStorageService.add("PhaseName", "");
    this._localStorageService.add("IsMainDashboard", true);
    this.Oauthservice.ChangeNavBarData("1");
    this.Oauthservice.ChangePhaseData("1");
    this.router.navigate(['/maindashboard']);
  }

  OpenDialogBox() {
    this.createdisplay = true;
  }

  openNav() {
    document.getElementById("mySidepanel").style.width = "200px";
  }

  closeNav() {
    document.getElementById("mySidepanel").style.width = "0";
  }
}
