import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

@Injectable()
export class Game {
    constructor(private http: HttpClient) {

    }
    public games: Game[] = [];

    loadGames() : Observable<void> {
        return this.http.get<[]>("/api/GameAPI")
            .pipe(map(data => {
                this.games = data;
                return;
            }));
    }
}
