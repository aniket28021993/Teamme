
import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { Router } from '@angular/router';

import { DashboardService } from '../../miscellaneous/dashboard.service';

import { MessageService } from 'primeng/api';
import { LocalStorageService } from 'angular-2-local-storage';
import { UserService } from '../../miscellaneous/user.service';
import { GenericService } from '../../miscellaneous/generic.service';
declare var $: any;

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent implements OnInit {
  
  items: string[] = [];
 
  //Variable
  Description: string;
  @Input() ProductBacklogId: number;
  @Input() CommentTypeId: number;

  ProjectId: number;

  CommentList: any[] = [];

  FormDisabled: boolean = false;

  constructor(private router: Router, private dashboardService: DashboardService, private messageService: MessageService, private _localStorageService: LocalStorageService, private userService: UserService, private genericService: GenericService) { }

  ngOnInit() {
    this.ProjectId = this._localStorageService.get("ProjectId");

    this.GetAllComment();
    this.GetAllMapUser();
  }

  GetAllComment() {
    this.CommentList = [];

    this.genericService.GetAllComment(this.ProductBacklogId, this.CommentTypeId).subscribe(model => {
      if (model != null) {
        this.CommentList = model;
        
        this.CommentList.map(function (x) {
          var mapinitail = "";
          var initailarr = x.UserName.split(" ");

          initailarr.map(function (y) {
            mapinitail = mapinitail + y.charAt(0);
          })
          x.OwnerInitial = mapinitail;
        })
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  CreateComment() {
    this.FormDisabled = true;

    if (this.Description == undefined || this.Description == "") {
      this.messageService.add({ severity: 'error', summary: 'Error Message', detail: "please type comment." });
      this.FormDisabled = false;
      return;
    }
    
    var CommentObj = {
      'Description': this.Description,
      'ProductBacklogId': this.ProductBacklogId,
      'CommentTypeId': this.CommentTypeId
    };

    this.genericService.CreateComment(CommentObj).subscribe(orglist => {
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: "Comment added." });
      this.Description = "";
      this.GetAllComment();
      this.FormDisabled = false;
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
        this.FormDisabled = false;
      });

  }

  GetAllMapUser() {
    var _me = this;
    

    this.userService.GetAllMapUser(this.ProjectId).subscribe(user => {
      if (user != null) {
        var arrlist = [];
        user.map(function (x) {
          arrlist.push(x.UserName);
        })
        _me.items = arrlist;
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }
}
