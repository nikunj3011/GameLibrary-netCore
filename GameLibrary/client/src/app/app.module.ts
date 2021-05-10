import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http";
import { AppComponent } from './app.component';
import { Game } from './services/store.services';
import GameListView from './views/gameListView.component';
import router from './router';
import { GamePage } from './pages/gamePage.component';
import { GameSystem } from './pages/gameSystem.component';

@NgModule({
  declarations: [
        AppComponent,
        GameListView,
        GamePage,
        GameSystem
  ],
  imports: [
      BrowserModule,
      HttpClientModule,
      router
  ],
    providers: [
        Game],
  bootstrap: [AppComponent]
})
export class AppModule { }
