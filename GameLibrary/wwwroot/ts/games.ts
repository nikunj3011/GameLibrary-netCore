class GamesName { 

    constructor(private game: string,  private rating : Number) { 
    }
    showGame() {
        alert(`${this.game}${this.rating}`);
    }
}