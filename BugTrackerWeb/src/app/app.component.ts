import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { SpinnerComponent } from './spinner.component';

@Component({
  // tslint:disable-next-line
  selector: 'body',
  template: '<ng-http-loader  [entryComponent]="spinnerComponent"></ng-http-loader>\
    <router-outlet > </router-outlet>\
    <tour-step-template></tour-step-template>'
})
export class AppComponent implements OnInit {
  public spinnerComponent = SpinnerComponent;

  constructor(private router: Router) { }

  ngOnInit() {

  

    this.router.events.subscribe((evt) => {
      if (!(evt instanceof NavigationEnd)) {
        return;
      }
      window.scrollTo(0, 0);
    });
  }
}
