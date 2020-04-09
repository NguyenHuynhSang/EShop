import { Component } from '@angular/core';
import {ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs/operators';

declare let gtag: Function;


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'EShopGUI';
  constructor(public router: Router){   
    this.router.events.subscribe(event => {
       if(event instanceof NavigationEnd){
           gtag('config', 'UA-163192265-1', 
                 {
                   'page_path': event.urlAfterRedirects
                 }
                );
        }
     }
  )}
}
