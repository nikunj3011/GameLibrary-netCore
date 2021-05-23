import { HttpClient } from '@angular/common/http';
import { Component,OnInit } from '@angular/core'; 
import { SignalRService } from './services/signal-r.service';

@Component({
    selector: 'the-games',
    templateUrl:"app.component.html" ,
  styles: []
})
export class AppComponent implements OnInit{
    title = 'Games Library';
    constructor(public signalRService: SignalRService, private http: HttpClient) { }
    ngOnInit() {
        this.signalRService.startConnection();
        this.signalRService.addTransferChartDataListner();
        this.startHttpRequest();
    }
    private startHttpRequest = () => {
        this.http.get('https://localhost:44376/CryptoAPI/')
            .subscribe(res => {
                console.log(res);
            });
    } 
}
