import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http";
import { AppComponent } from './app.component';
import { Game } from './services/store.services';
import GameListView from './views/gameListView.component';
import router from './router';
import { GamePage } from './pages/gamePage.component';
import { GameSystem } from './pages/gameSystem.component';
import { LoginPage } from './pages/loginPage.component';
import { AuthActivator } from './services/authActivator.services';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
        AppComponent,
        GameListView,
        GamePage,
        GameSystem,
        LoginPage,
  ],
  imports: [
      BrowserModule,
      HttpClientModule,
      router,
      FormsModule
  ],
    providers: [
        Game,
        AuthActivator],
  bootstrap: [AppComponent]
})
export class AppModule { }
