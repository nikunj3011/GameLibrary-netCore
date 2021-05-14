import { Component, OnInit } from "@angular/core";
import { Game } from "../services/store.services";

@Component({
    selector:"game-list",
    templateUrl: "gameListView.component.html",
    styleUrls: ["gameListView.component.css"]
})
export default class GameListView implements OnInit {
    public gamess = [];
    constructor(public games: Game) {
        this.gamess = games.games;
    }
    ngOnInit(): void {
        this.games.loadGames()
            .subscribe(() => {
                });
    }
}