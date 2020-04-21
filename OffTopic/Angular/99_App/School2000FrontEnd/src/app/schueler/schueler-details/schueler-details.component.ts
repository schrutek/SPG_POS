import { Component, OnInit, Input } from '@angular/core';
import { SchuelerDto } from 'src/app/models/schuelerDto';

@Component({
  selector: 'app-schueler-details',
  templateUrl: './schueler-details.component.html',
  styleUrls: ['./schueler-details.component.css']
})
export class SchuelerDetailsComponent implements OnInit {

  @Input()
  public schueler: SchuelerDto;

  constructor() { }

  ngOnInit() {
  }

}
