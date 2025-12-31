import { Component, OnInit, ViewChild } from '@angular/core';
import { MessageService } from 'primeng/api';
import { AdminService } from '../miscellaneous/Admin.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MiscellaneousService } from '../miscellaneous/miscellaneous.service';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {

  displayedColumns: string[] = ['PaymentId', 'OrgName', 'Amount', 'MonthDate'];
  Payment = new MatTableDataSource<any[]>();
  PaymentLength: number = 0;

  adddisplay: boolean = false;

  OrganzationList: any[] = [];
  OrgId: number;
  OrgAmount: number;

  PopUpErroMessage: string;
  PopUpErroOrgId: string;

  //View Child
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;

  constructor(private adminService: AdminService, private messageService: MessageService, private miscellaneousService: MiscellaneousService) { }

  ngOnInit() {
    this.GetAllPayment();
    this.GetAllOrganization();
    this.miscellaneousService.ChangeBreadcrumbData("PAYMENT DETAILS");
  }

  GetAllOrganization() {

    this.adminService.GetAllOrganization().subscribe(orglist => {
      if (orglist != null) {
        this.OrganzationList = orglist;
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  GetAllPayment() {
    this.adminService.GetAllPayment().subscribe(model => {
      if (model != null) {
        this.Payment = new MatTableDataSource<any[]>(model);
        this.Payment.paginator = this.paginator;
        this.PaymentLength = this.Payment.data.length;
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  AddPayment() {

    this.PopUpErroMessage = "";
    this.PopUpErroOrgId = "";

    if (this.OrgAmount == undefined) {
      this.PopUpErroMessage = "Amount Required.";
    }

    if (this.OrgId == undefined) {
      this.PopUpErroOrgId = "select organization.";
    }

    if ((this.PopUpErroOrgId != undefined && this.PopUpErroOrgId != "") || (this.PopUpErroMessage != undefined && this.PopUpErroMessage != "")) {
      return;
    }


    this.adminService.AddPayment(this.OrgId, this.OrgAmount).subscribe(proj => {
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: "Succesfully added payment." });
      this.adddisplay = false;
      this.OrgId = undefined;
      this.OrgAmount = undefined;
      this.GetAllPayment();
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  PopUpOpen() {
    this.adddisplay = true;
    this.PopUpErroMessage = '';
    this.PopUpErroOrgId = '';
    this.OrgId = undefined;
    this.OrgAmount = undefined;
  }
}
