import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SessionDataService {

  public myCountStore: number = 0;

  constructor() { }
}
