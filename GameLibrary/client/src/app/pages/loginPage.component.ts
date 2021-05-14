import { Component } from "@angular/core"; 
import { Router } from "@angular/router";
import { Game } from "../services/store.services";
import { LoginRequest } from "../shared/LoginResults";

@Component({
    selector: "login-page",
    templateUrl: "loginPage.component.html"
})
export class LoginPage {
    constructor(public game: Game, private router: Router) {

    }
    public creds: LoginRequest = {
        username: "",
        password: ""
    }
    public errorMessage = "";
    onLogin() {
        this.game.login(this.creds)
            .subscribe(() => {
                // Successfully logged in
                if (this.game.games.length > 0) {
                    this.router.navigate(["checkout"]);
                } else {
                    this.router.navigate([""]);
                }
            }, error => {
                console.log(error);
                this.errorMessage = "Failed to login";
            });
    }
}