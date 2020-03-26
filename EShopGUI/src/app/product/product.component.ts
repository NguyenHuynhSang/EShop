import { Component } from "@angular/core";
import { templateJitUrl } from '@angular/compiler';
import { AppService,Product} from '../core/sercices/api.client.generated';


@Component({
    selector:'product',
    templateUrl:'./product.component.html'
})

export class ProductComponent{


    public listProduct:Product[];
    constructor(appService: AppService){
        appService.getAll().subscribe(response=>{
            this.listProduct=response;
        });

    }


}