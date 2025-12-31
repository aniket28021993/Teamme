import { Component } from '@angular/core';
import { MiscellaneousService } from '../../miscellaneous/miscellaneous.service';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';

declare var $: any;

@Component({
  selector: 'app-dashboard',
  templateUrl: 'thankyou.component.html'
})
export class ThankYouComponent {

  constructor(private _miscellaneousService: MiscellaneousService, private router: Router, private messageService: MessageService) { }
}
