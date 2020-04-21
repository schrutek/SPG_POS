import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from "@angular/router";
import { FormsModule } from '@angular/forms';

import { SchuelerListComponent } from './schueler-list/schueler-list.component';
import { SchuelerDetailsComponent } from './schueler-details/schueler-details.component';
import { SchuelerDetailsPageComponent } from './schueler-details-page/schueler-details-page.component';

@NgModule({
  declarations: [
    SchuelerListComponent, 
    SchuelerDetailsComponent, 
    SchuelerDetailsPageComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild([
      { path: 'list', component: SchuelerListComponent },
      { path: 'details/:id', component: SchuelerDetailsPageComponent }
    ])
  ]
})
export class SchuelerModule { }
