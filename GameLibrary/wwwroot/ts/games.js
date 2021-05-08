var GamesName = /** @class */ (function () {
    function GamesName(game, rating) {
        this.game = game;
        this.rating = rating;
    }
    GamesName.prototype.showGame = function () {
        alert("" + this.game + this.rating);
    };
    return GamesName;
}());
//# sourceMappingURL=games.js.map