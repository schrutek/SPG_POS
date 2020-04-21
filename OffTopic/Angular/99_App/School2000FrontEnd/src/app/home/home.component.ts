import { Component, OnInit } from '@angular/core';
import { SessionDataService } from './../shared/services/session-data.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public myCount: number = 0;

  constructor(private sessionDataService: SessionDataService) { }

  ngOnInit() {
    this.myCount = this.sessionDataService.myCountStore;
  }

  public increment() {
    this.myCount++;
    this.sessionDataService.myCountStore = this.myCount;
  }

  public decrement() {
    this.myCount--;
    this.sessionDataService.myCountStore = this.myCount;
  }
}
