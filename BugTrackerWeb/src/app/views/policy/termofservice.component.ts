import { Component } from '@angular/core';
import { MiscellaneousService } from '../../miscellaneous/miscellaneous.service';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';

declare var $: any;

@Component({
  selector: 'app-dashboard',
  templateUrl: 'termofservice.component.html'
})
export class TermOfServiceComponent {

  constructor(private _miscellaneousService: MiscellaneousService, private router: Router, private messageService: MessageService) { }
}
