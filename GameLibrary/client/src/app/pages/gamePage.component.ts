import { Component } from "@angular/core"; 
import { Router } from "@angular/router";
import { Game } from "../services/store.services";

@Component({
    selector: "game-page",
    templateUrl: "gamePage.component.html"
})
export class GamePage {
    public errorMessage = ""; 
    constructor(public game: Game, private router:Router) {
        this.onCreateGame();
    }
    onCreateGame() {
        this.errorMessage = "";
        //this.game.addGame()
        //    .subscribe(() => {
        //        this.router.navigate(["/"]);
        //    }, err => {
        //            this.errorMessage = "Failed to add Game"; 
        //    })
    }
}