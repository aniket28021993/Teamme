
import { Component, OnInit, Input } from '@angular/core';
import { DashboardService } from '../../miscellaneous/dashboard.service';
import { MessageService } from 'primeng/api';
import { ActivatedRoute, Router } from '@angular/router';
import { GenericService } from '../../miscellaneous/generic.service';

@Component({
  selector: 'app-recentactivity',
  templateUrl: './recentactivity.component.html',
  styleUrls: ['./recentactivity.component.css']
})
export class RecentActivityComponent implements OnInit {

  RecentActicityList: any[] = [];
  @Input() CommonId: number;
  @Input() RecentActivityId: number;

  constructor(private route: ActivatedRoute, private dashboardService: DashboardService, private messageService: MessageService, private router: Router, private genericService: GenericService) { }

  ngOnInit() {
    this.GetAllRecentActivity();
  }

  GetAllRecentActivity() {
    this.RecentActicityList = [];

    this.genericService.GetAllRecentActivity(this.CommonId, this.RecentActivityId).subscribe(model => {
      if (model != null) {
        this.RecentActicityList = model;
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });

  }

  GoBackToPhase() {
    if (this.RecentActivityId == 1) {
      this.router.navigate(['/backlog']);
    }
    else if (this.RecentActivityId == 2) {
      this.router.navigate(['/bug']);
    }
    else {
      this.router.navigate(['/task']);
    }
  }
}
