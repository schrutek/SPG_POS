import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SchuelerDto } from './../../models/schuelerDto';
import { RepositoryService } from './../../shared/services/repository.service';
import { SchuelerDataDto } from 'src/app/models/schuelerDataDto';

@Component({
  selector: 'app-schueler-list',
  templateUrl: './schueler-list.component.html',
  styleUrls: ['./schueler-list.component.css']
})
export class SchuelerListComponent implements OnInit {

  public schuelers: SchuelerDto[];
  public rowCount: number = 0;
  public selectedSchueler: SchuelerDto;

  public filter: String = "";
  public curPage: number = 0;

  private sortOrder: String = "asc";
  private sortColumnName: String = "";
  private sortColumnNameOld: String = "";

  constructor(private repository: RepositoryService, private router: Router) { }

  ngOnInit() {
    this.getAllSchuelersSortedFiltered();
  }

  public sortBy(columnName: String) {
    this.sortColumnName = columnName;
    this.getAllSchuelersSortedFiltered();
    this.sortColumnNameOld = columnName;
  }

  public getAllSchuelers() {
    let apiAddress: string = `api/schueler/all`;
    this.repository.getData(apiAddress)
      .subscribe(res => 
      { 
        this.schuelers = res as SchuelerDto[] 
      })
  }

  public getAllSchuelersSortedFiltered() {
    if (this.sortColumnName == this.sortColumnNameOld) {
      if (this.sortOrder == "asc") {
        this.sortOrder = "desc";
      }
      else {
        this.sortOrder = "asc";
      }
    }

    let apiAddress: string = `api/schueler?query=orderby:${this.sortColumnName}:${this.sortOrder};filter:${this.filter};skip:${this.curPage * 10};take:10`;
    this.repository.getData(apiAddress)
      .subscribe((res: SchuelerDataDto) => 
      { 
        this.schuelers = res.schuelers as SchuelerDto[] 
        this.rowCount = res.rowCount;
      })
  }

  public previousPage() {
    if (this.curPage > 0){
      this.curPage--;
      this.getAllSchuelersSortedFiltered();
  
    }
  }

  public nextPage() {
    if (((this.curPage + 1) * 10) < this.rowCount) {
      this.curPage++;
      this.getAllSchuelersSortedFiltered();
    }
  }

  public onSelect(schueler: SchuelerDto) {
    this.selectedSchueler = schueler;
    $('#editModal').modal();
  }

  public redirectToDetailsPage(id) {
    let redirectUrl: string = `schueler/details/${id}`;
    this.router.navigate([redirectUrl]);
  }
}
