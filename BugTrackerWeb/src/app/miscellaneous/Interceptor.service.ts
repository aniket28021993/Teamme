import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LocalStorageService } from 'angular-2-local-storage';


@Injectable()
export class TokenInterceptorService implements HttpInterceptor {

  constructor(private _localStorageService: LocalStorageService) { }


  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {


    var access_token = this._localStorageService.get("access_token");

    if (access_token != null) {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${access_token}`
        }
      });
    }

    return next.handle(req);
  }
}
