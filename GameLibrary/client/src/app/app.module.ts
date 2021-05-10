import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http";
import { AppComponent } from './app.component';
import { Game } from './services/store.services';
import GameListView from './views/gameListView.component';

@NgModule({
  declarations: [
        AppComponent,
        GameListView
  ],
  imports: [
      BrowserModule,
      HttpClientModule
  ],
    providers: [
        Game],
  bootstrap: [AppComponent]
})
export class AppModule { }
