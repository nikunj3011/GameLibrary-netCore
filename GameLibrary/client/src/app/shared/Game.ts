export interface Game {
        gameLibraryID: number;
        name: string;
        description: string;
        gameSystemID: number;
        gameSystems?: any;
        rating: number;
        discType: string;
        creationDate: Date;
}


export interface Gamee {
    gameLibraryID?: number;
    name: string;
    description: string;
    gameSystemID?: number;
    gameSystems?: any;
    rating: number;
    discType: string;
    creationDate?: Date;
}


