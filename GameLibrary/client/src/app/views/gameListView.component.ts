import { Component } from "@angular/core";

@Component({

    selector:"game-list",
    templateUrl:"gameListView.component.html"
})
export default class GameListView {
    public Games = [{
        title: "Need For Speed",
        system: "PC"
    },
    {
        title: "Spiderman",
        system: "PS4"
    }];
}