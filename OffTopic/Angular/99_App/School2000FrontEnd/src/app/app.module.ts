import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule} from '@angular/router'
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { MenuComponent } from './menu/menu.component';
import { LehrerComponent } from './lehrer/lehrer.component';
import { NotFoundComponent } from './error-pages/not-found/not-found.component';

import { SessionDataService } from './shared/services/session-data.service';
import { EnvironmentUrlService } from './shared/services/environment-url.service'
import { RepositoryService } from './shared/services/repository.service';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    MenuComponent,
    LehrerComponent,
    NotFoundComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: "home", component: HomeComponent },
      { path: "schueler", loadChildren: './schueler/schueler.module#SchuelerModule' },
      { path: "lehrer", component: LehrerComponent },
      { path: "404", component: NotFoundComponent },
      { path: "", redirectTo: '/home', pathMatch: 'full' },
      { path: "**", redirectTo: '/404', pathMatch: 'full' }
    ])
  ],
  providers: [
    SessionDataService,
    EnvironmentUrlService,
    RepositoryService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
