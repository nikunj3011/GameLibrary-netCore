import { RouterModule } from "@angular/router";
import { GamePage } from "../pages/gamePage.component";
import { GameSystem } from "../pages/gameSystem.component";

const routes = [
    { path: "game", component: GamePage },
    { path: "", component: GamePage },
    { path:"gameSystem",component : GameSystem}
];
const router = RouterModule.forRoot(routes, {
    useHash: true
});
export default router;