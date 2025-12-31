import { Component, OnInit, ViewChild } from '@angular/core';
import { MessageService } from 'primeng/api';
import { AdminService } from '../miscellaneous/Admin.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MiscellaneousService } from '../miscellaneous/miscellaneous.service';

@Component({
  selector: 'app-orgplan',
  templateUrl: './orgplan.component.html',
  styleUrls: ['./orgplan.component.css']
})
export class OrgPlanComponent implements OnInit {

  displayedColumns: string[] = ['PlanId', 'OrgName', 'OrgPlanId', 'Approve'];
  Plan = new MatTableDataSource<any[]>();
  PlanLength: number = 0;

  SearchTaskval: string;

  //View Child
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;

  constructor(private adminService: AdminService, private messageService: MessageService, private miscellaneousService: MiscellaneousService) { }

  ngOnInit() {
    this.GetAllOrgPlan();
    this.miscellaneousService.ChangeBreadcrumbData("ORGANIZATION PLAN");
  }

  GetAllOrgPlan() {
    this.Plan = new MatTableDataSource<any[]>();
    this.PlanLength = 0;

    this.adminService.GetAllOrgPlan().subscribe(proj => {
      if (proj != null) {
        this.Plan = new MatTableDataSource<any[]>(proj);
        this.Plan.paginator = this.paginator;
        this.PlanLength = this.Plan.data.length;
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  ApproveOrgPlan(OrgId, OrgPlanId, UpdateOrgPlanId) {

    this.adminService.ApproveOrgPlan(OrgId, OrgPlanId, UpdateOrgPlanId).subscribe(proj => {
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: "Succesfully aprroved plan." });
      this.GetAllOrgPlan();
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
}
