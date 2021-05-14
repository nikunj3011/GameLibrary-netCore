import { RouterModule } from "@angular/router";
import { GamePage } from "../pages/gamePage.component";
import { GameSystem } from "../pages/gameSystem.component";
import { LoginPage } from "../pages/loginPage.component";
import { AuthActivator } from "../services/authActivator.services";

const routes = [
    { path: "game", component: GamePage },
    { path: "", component: GamePage },
    { path:"gameSystem",component : GameSystem, canActivate:[AuthActivator]},
    { path: "login", component: LoginPage },
    { path: "**", redirectTo:"/" }
];
const router = RouterModule.forRoot(routes, {
    useHash: true
});
export default router;