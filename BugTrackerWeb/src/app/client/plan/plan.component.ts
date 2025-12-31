import { Component, OnInit } from '@angular/core';
import { MiscellaneousService } from '../../miscellaneous/miscellaneous.service';
import { DashboardService } from '../../miscellaneous/dashboard.service';
import { MessageService } from 'primeng/api';
import { GenericService } from '../../miscellaneous/generic.service';

@Component({
  selector: 'app-plan',
  templateUrl: './plan.component.html',
  styleUrls: ['./plan.component.css']
})
export class PlanComponent implements OnInit {
  static OrgPlanComponent: any;
 
  PlanActiveId: number;

  constructor(private miscellaneousService: MiscellaneousService, private dashboardService: DashboardService, private messageService: MessageService, private genericService: GenericService) { }

  ngOnInit() {
    this.PlanActiveId = this.miscellaneousService.UserInfo.OrgPlanId;
    this.miscellaneousService.ChangeBreadcrumbData("PLAN");
  }

  UpdateOrgPlan(OrgPlanId) {

    this.genericService.UpdateOrgPlan(OrgPlanId).subscribe(proj => {
      
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: 'Your request to update plan has been sent to TeamMe admin.' });
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
}
