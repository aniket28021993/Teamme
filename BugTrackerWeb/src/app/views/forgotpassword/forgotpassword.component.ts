import { Component } from '@angular/core';
import { MiscellaneousService } from '../../miscellaneous/miscellaneous.service';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';

declare var $: any;

@Component({
  selector: 'app-dashboard',
  templateUrl: 'forgotpassword.component.html'
})
export class ForgotPasswordComponent {


  Email: string;
  ErrEmail: string;
  UserOTP: number;
  Password: number;

  IsOTPSend: boolean = false;
  IsOTPVerified: boolean = false;

  constructor(private _miscellaneousService: MiscellaneousService, private router: Router, private messageService: MessageService) { }

  SendOTP() {
    this.ErrEmail = "";

    if (this.Email == undefined || this.Email == "") {
      this.ErrEmail = "Email Required.";
      return;
    }
    

    this._miscellaneousService.SendOTP(this.Email).subscribe(OTP => {
      this.messageService.add({ severity: 'success', summary: 'Success Message', detail: "Successfully send OTP to your mail" });
      this.IsOTPSend = true;
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.error_description });
      });

  }

  VerifyOTP() {

    if (this.UserOTP == undefined) {
      this.messageService.add({ severity: 'error', summary: 'Error Message', detail: "OTP required." });
      return;
    }

    this._miscellaneousService.VerifyOTP(this.Email, this.UserOTP).subscribe(OTP => {
      if (OTP > 0) {
        this.messageService.add({ severity: 'success', summary: 'Success Message', detail: "Successfully verified OTP." });
        this.IsOTPVerified = true;
        this.IsOTPSend = false;
      }
      else {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: "Incorrect OTP"});
      }

    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.error_description });
      });

  }

  ChangePassword() {

    if (this.Password == undefined) {
      this.messageService.add({ severity: 'error', summary: 'Error Message', detail: "Password required." });
      return;
    }

    this._miscellaneousService.ChangePassword(this.Email, this.Password).subscribe(OTP => {
      this.router.navigate(['/login']);
      setTimeout(function () {
        this.messageService.add({ severity: 'success', summary: 'Success Message', detail: "Successfully Password Changed." });
      }, 2000);
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error Message', detail: error.error.error_description });
      });

  }
}
