import { Component, OnInit } from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import { RepositoryService } from 'src/app/shared/services/repository.service';
import { SchuelerDto } from 'src/app/models/schuelerDto';

@Component({
  selector: 'app-schueler-details-page',
  templateUrl: './schueler-details-page.component.html',
  styleUrls: ['./schueler-details-page.component.css']
})
export class SchuelerDetailsPageComponent implements OnInit {
  public id: number = -1;
  public schueler: SchuelerDto;

  constructor(private activatedRoute: ActivatedRoute, private repository: RepositoryService) { }

  ngOnInit() {
    let id = this.activatedRoute.snapshot.params["id"];
    this.id = id;

    this.getSchuelerDetails(this.id );
  }

  public getSchuelerDetails(id: number) {
    let apiUrl: string = `api/schueler/${id}`;
    this.repository.getData(apiUrl)
    .subscribe(res => 
      { this.schueler = res as SchuelerDto
      });
  }

}
