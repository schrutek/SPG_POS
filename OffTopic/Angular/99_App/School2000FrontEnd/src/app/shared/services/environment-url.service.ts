import { Injectable } from '@angular/core';
import { environment } from './../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EnvironmentUrlService {

  public urlAddress: string = environment.urlAddress;

  constructor() { }
}
