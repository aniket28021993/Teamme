import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MessageService } from 'primeng/api';
import { AdminService } from '../miscellaneous/Admin.service';
import { MiscellaneousService } from '../miscellaneous/miscellaneous.service';

@Component({
  selector: 'app-orgusers',
  templateUrl: './orgusers.component.html',
  styleUrls: ['./orgusers.component.css']
})
export class OrgUsersComponent implements OnInit {

  displayedColumns: string[] = ['FirstName', 'LastName', 'EmailId', 'PhoneNo', 'UserType', 'Status'];
  Users = new MatTableDataSource<any[]>();
  UsersLength: number = 0;

  //View Child
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  //Variable
  SearchTaskval: string;
  OrgId: number;


  constructor(private router: Router, private adminService: AdminService, private messageService: MessageService, private route: ActivatedRoute, private miscellaneousService: MiscellaneousService) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.OrgId = +params.get('OrgId');
    });

    this.GetAllOrgUser();

    this.miscellaneousService.ChangeBreadcrumbData("USER");
  }

  OnKeyPress() {
    if (this.SearchTaskval != undefined && this.SearchTaskval != "") {
      this.SearchUser();
    }
    else {
      this.GetAllOrgUser();
    }
  }

  GetAllOrgUser() {
    this.Users = new MatTableDataSource<any[]>();
    this.UsersLength = 0;

    this.adminService.GetAllOrgUser(this.OrgId).subscribe(user => {
      if (user != null) {
        this.Users = new MatTableDataSource<any[]>(user);
        this.Users.paginator = this.paginator;
        this.Users.sort = this.sort;
        this.UsersLength = this.Users.data.length;
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  SearchUser() {
    if (this.SearchTaskval == undefined || this.SearchTaskval == "") {
      this.messageService.add({ severity: 'warn', summary: 'Warn Message', detail: 'Please give your input..!!' });
      return;
    }

    var SearchTaskObj = {
      'SearchTask': this.SearchTaskval,
      'OrgId': this.OrgId
    };

    this.Users = new MatTableDataSource<any[]>();
    this.UsersLength = 0;

    this.adminService.SearchUser(SearchTaskObj).subscribe(proj => {
      if (proj != null) {
        this.Users = new MatTableDataSource<any[]>(proj);
        this.Users.paginator = this.paginator;
        this.Users.sort = this.sort;
        this.UsersLength = this.Users.data.length;
      }
      this.SearchTaskval = "";
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
}
