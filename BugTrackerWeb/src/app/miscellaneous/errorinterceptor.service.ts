//angular
import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Router } from '@angular/router';

//rxjs
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { LocalStorageService } from 'angular-2-local-storage';
import { MiscellaneousService } from './miscellaneous.service';
import { MessageService } from 'primeng/api';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(public router: Router, private _localstorageService: LocalStorageService, private miscellaneousService: MiscellaneousService, private messageService: MessageService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request)
      .pipe(catchError(err => {
        if (err.status === 401) {
          this.miscellaneousService.ClearUserData();      
        }
        throw err;
      })
      )
  }
}
