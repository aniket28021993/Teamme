import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { LocalStorageService } from 'angular-2-local-storage';
import { MessageService } from 'primeng/api';
import { MiscellaneousService } from '../../miscellaneous/miscellaneous.service';
import { TaskService } from '../../miscellaneous/task.service';

@Component({
  selector: 'app-dailytask',
  templateUrl: './dailytask.component.html',
  styleUrls: ['./dailytask.component.css']
})
export class DailyTaskComponent implements OnInit {
  displayedColumns: string[] = ['TaskId', 'TaskTitle', 'TaskState', 'TaskAssignedTo', 'DailyStatus'];
  DataTask = new MatTableDataSource<any[]>();
  DataTaskLength: number = 0;

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  PhaseId: number;
  AssignedTo: number = 0;
  CurrentDateLabel: string;

  TaskViewOptions = [
    { label: 'All sprint tasks', value: 0 },
    { label: 'My sprint tasks', value: 'mine' }
  ];
  selectedTaskView: any = 0;

  dailyStatusKey: string;
  dailyStatusMap: { [key: number]: boolean } = {};

  constructor(private messageService: MessageService, private miscellaneousService: MiscellaneousService, private _localStorageService: LocalStorageService, private taskService: TaskService) {
    this.miscellaneousService.OverallData.subscribe(currentData => {
      if (currentData != null && currentData != '') {
        this.initializeContext();
      }
    });
  }

  ngOnInit() {
    this.initializeContext();
    this.miscellaneousService.ChangeBreadcrumbData('DAILY TASK');
  }

  initializeContext() {
    this.PhaseId = this._localStorageService.get('PhaseId');
    this.setDailyKey();
    this.loadDailyStatus();
    this.loadDailyTasks();
  }

  setDailyKey() {
    const today = new Date();
    const dayKey = today.toISOString().slice(0, 10);
    this.dailyStatusKey = `DailyTaskStatus-${dayKey}-${this.PhaseId || 0}`;
    this.CurrentDateLabel = today.toDateString();
  }

  loadDailyStatus() {
    const storedStatus = this._localStorageService.get(this.dailyStatusKey);
    if (storedStatus) {
      this.dailyStatusMap = storedStatus as { [key: number]: boolean };
    } else {
      this.dailyStatusMap = {};
    }
  }

  loadDailyTasks() {
    this.DataTaskLength = 0;
    this.DataTask = new MatTableDataSource<any[]>();

    if (!this.PhaseId || this.PhaseId === 0) {
      return;
    }

    this.taskService.GetAllDashboardProductTask(this.PhaseId, this.AssignedTo).subscribe(backlogTask => {
      if (backlogTask != null) {
        this.DataTask = new MatTableDataSource<any[]>(backlogTask);
        this.DataTask.sort = this.sort;
        this.DataTask.paginator = this.paginator;
        this.DataTaskLength = this.DataTask.data.length;
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
      });
  }

  onTaskViewChange(value) {
    this.selectedTaskView = value;
    if (value === 'mine') {
      this.AssignedTo = this.miscellaneousService.UserInfo.OrgUserId;
    } else {
      this.AssignedTo = 0;
    }

    this.loadDailyTasks();
  }

  toggleDailyStatus(taskId: number, checked: boolean) {
    this.dailyStatusMap[taskId] = checked;
    this._localStorageService.set(this.dailyStatusKey, this.dailyStatusMap);
  }

  isDailyStatusChecked(taskId: number): boolean {
    return !!this.dailyStatusMap[taskId];
  }
}
