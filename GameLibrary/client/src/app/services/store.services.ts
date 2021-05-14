import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Gamee } from "../shared/Game";
import { LoginRequest, LoginResults } from "../shared/LoginResults";

@Injectable()
export class Game {
    constructor(private http: HttpClient) {

    }
    public games: Game[] = [];
    public token = "";
    
    public myThing: Gamee = { 
        name: "Unchartered",
        description: "a",
        gameSystemID: 5,  
        rating: 5,
        discType: "Digital"
    };
    public expiration = new Date();
    loadGames() : Observable<void> {
        return this.http.get<[]>("/api/GameAPI")
            .pipe(map(data => {
                this.games = data;
                return;
            }));
    }
    get loginRequired(): boolean {
        return this.token.length === 0 || this.expiration < new Date();
    }

    login(creds: LoginRequest) {
        return this.http.post<LoginResults>("/account/createtoken", creds)
            .pipe(map(data => {
                this.token = data.token;
                this.expiration = data.expiration;
            }));
         
    }

    addGame() {
        const headers = new HttpHeaders().set("Authorization", `Bearer ${this.token}`)
        return this.http.post("/api/GameAPI/", this.myThing, {
            headers: headers
        })
            .pipe(map(() => {
                this.myThing = this.myThing;
            }));
    }
}
