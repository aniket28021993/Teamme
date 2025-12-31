import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class APIUrl {
  public tokenurl = "http://localhost:62253";
  public baseurl = "http://localhost:62253/api";
  public homeurl = "http://localhost:4200";

  //public tokenurl = "https://teammeapi.azurewebsites.net";
  //public baseurl = "https://teammeapi.azurewebsites.net/api";
  //public homeurl = "https://teamme.azurewebsites.net";
  }
