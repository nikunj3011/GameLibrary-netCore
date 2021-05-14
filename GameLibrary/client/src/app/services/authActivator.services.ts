import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Game } from "./store.services";

@Injectable()
export class AuthActivator implements CanActivate {
    constructor(private game: Game, private router: Router) {

    }
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        if (this.game.loginRequired) {
            this.router.navigate(["login"]);
            return false;
        } else {
            return true;
        }
    }
    
}
