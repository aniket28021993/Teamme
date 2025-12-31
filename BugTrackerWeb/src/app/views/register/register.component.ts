import { Component, ElementRef, ViewChild } from '@angular/core';
import { MiscellaneousService } from '../../miscellaneous/miscellaneous.service';
import { lang } from 'moment';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import { APIUrl } from '../../appsetting';

@Component({
  selector: 'app-dashboard',
  templateUrl: 'register.component.html'
})
export class RegisterComponent {

  @ViewChild('Home') Home: ElementRef;
  @ViewChild('About') About: ElementRef;
  @ViewChild('Contact') Contact: ElementRef;
  @ViewChild('Pricing') Pricing: ElementRef;
  @ViewChild('Tour') Tour: ElementRef;

  _OrgName: string;
  _OrgEmail: string;
  _OrgNumber: string;
  _OrgAddress: string;

  ErrOrgName: string;
  ErrEmailId: string;
  ErrPhoneNo: string;
  ErrAddress: string;

  FormDisabled: boolean = false;
  IsChecked: boolean = false;

  OrgPlanId: number = 1;
  OrgPlanArr: any[] = [
    { 'id': 1, 'text': 'FREE PLAN' },
    { 'id': 2, 'text': 'PLUS PLAN' },
    { 'id': 3, 'text': 'PRO PLAN' }
  ];

  ContactName: string;
  ContactEmail: string;
  ContactMessage: string;

  ErrContactName: string;
  ErrContactEmail: string;

  TermOfService: any;
  PrivacyPolicy: any;

  //VideoUrlReg = this.apiurl.tokenurl + "/videos/Registration.mp4";
  //VideoUrlProj = this.apiurl.tokenurl + "/videos/createproject.mp4";
  //VideoUrlDash = this.apiurl.tokenurl + "/videos/Dashboard.mp4";
  //VideoUrlStat = this.apiurl.tokenurl + "/videos/stats.mp4"; 

  constructor(private _miscellaneousService: MiscellaneousService, private messageService: MessageService, private router: Router, private apiurl: APIUrl) {
    this.TermOfService = this.apiurl.homeurl + "/#/termofservice";
    this.PrivacyPolicy = this.apiurl.homeurl + "/#/privacypolicy";
  }

  moveToAbout(): void {
    this.About.nativeElement.scrollIntoView({ behavior: 'smooth', block: 'end', inline: 'start' });
  }

  moveToContact(): void {
    this.Contact.nativeElement.scrollIntoView({ behavior: 'smooth', block: 'end', inline: 'start' });
  }

  moveToPricing():void {
    this.Pricing.nativeElement.scrollIntoView({ behavior: 'smooth', block: 'end', inline: 'start' });
  }

  moveToTour(): void {
    this.Tour.nativeElement.scrollIntoView({ behavior: 'smooth', block: 'end', inline: 'start' });
  }

  moveToHome(OrgPlanId) {
    this.OrgPlanId = OrgPlanId;
    this.Home.nativeElement.scrollIntoView({ behavior: 'smooth', block: 'end', inline: 'start' });
  }

  CreateOrganization() {
    this.FormDisabled = true;
    this.ErrOrgName = "";
    this.ErrEmailId = "";
    this.ErrPhoneNo = "";
    this.ErrAddress = "";


    if (this._OrgName == undefined || this._OrgName == "" || this._OrgName.trim() == "") {
      this.ErrOrgName = "OrgName Required.";
    }

    //EmailId
    if (this._OrgEmail != undefined && this._OrgEmail != "") {
      if (!this._OrgEmail.match(/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/)) {
        this.ErrEmailId = "Invalid EmailId.";
      }
    }
    else {
      this.ErrEmailId = "EmailId Required.";
    }

    //PhoneNo
    if (this._OrgNumber != undefined && this._OrgNumber != "") {
      if (!this._OrgNumber.match(/^[0-9]*$/)) {
        this.ErrPhoneNo = "Invalid Phone number.";
      }
    }
    else {
      this.ErrPhoneNo = "Phone number Required.";
    }
    
    if (this._OrgAddress == undefined || this._OrgAddress == "") {
      this.ErrAddress = "Address Required.";
    }

    if ((this.ErrOrgName != undefined && this.ErrOrgName != "") || (this.ErrEmailId != undefined && this.ErrEmailId != "") || (this.ErrPhoneNo != undefined && this.ErrPhoneNo != "") || (this.ErrAddress != undefined && this.ErrAddress != "")) {
      this.FormDisabled = false;
      return;
    }

    var OrgData = {
      '_OrgName': this._OrgName,
      '_OrgEmail': this._OrgEmail,
      '_OrgNumber': this._OrgNumber,
      '_OrgAddress': this._OrgAddress,
      '_OrgPlanId': this.OrgPlanId
    };

    this._miscellaneousService.CreateOrganization(OrgData).subscribe(orgid => {

      this._OrgName = "";
      this._OrgEmail = "";
      this._OrgNumber = "";
      this._OrgAddress = "";
      this.FormDisabled = false;
      
      this.router.navigate(['/thankyou']);
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
        this.FormDisabled = false;
      });

  }

  ContactUs() {
    this.FormDisabled = true;
    this.ErrContactName = "";
    this.ErrContactEmail = "";


    if (this.ContactName != undefined && this.ContactName != "") {
      if (!this.ContactName.match(/^[a-zA-z ]*$/)) {
        this.ErrContactName = "Invalid Name.";
      }
    }
    else {
      this.ErrContactName = "Name Required.";
    }

    //EmailId
    if (this.ContactEmail != undefined && this.ContactEmail != "") {
      if (!this.ContactEmail.match(/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/)) {
        this.ErrContactEmail = "Invalid EmailId.";
      }
    }
    else {
      this.ErrContactEmail = "EmailId Required.";
    }

    if ((this.ErrContactName != undefined && this.ErrContactName != "") || (this.ErrContactEmail != undefined && this.ErrContactEmail != "")) {
      this.FormDisabled = false;
      return;
    }

    var ContactData = {
      'ContactName': this.ContactName,
      'ContactEmail': this.ContactEmail,
      'ContactMessage': this.ContactMessage
    };

    this._miscellaneousService.ContactUs(ContactData).subscribe(orgid => {

      this.ContactName = "";
      this.ContactEmail = "";
      this.ContactMessage = "";
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: 'Successfully send.' });
      this.FormDisabled = false;

    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.ExceptionMessage });
        this.FormDisabled = false;
      });

  }
}
