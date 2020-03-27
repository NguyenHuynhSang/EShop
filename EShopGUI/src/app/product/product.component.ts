import { Component } from "@angular/core";
import { templateJitUrl } from '@angular/compiler';
import { AppService,Product} from '../core/sercices/api.client.generated';


@Component({
    selector:'product',
    templateUrl:'./product.component.html'
})

export class ProductComponent{

    public _appService:AppService;
    public listProduct:Product[];
    public product:Product=new Product();
    constructor(appService: AppService){
        this._appService=appService;
        this._appService.getAll().subscribe(response=>{
            this.listProduct=response;
        });

    }

    Create():void
    {
    
    if(this.product.productName=="" ) return;
 

    this._appService.create(this.product).subscribe(result => {
        console.log("Created");
        window.location.reload();
    })

    
    
    }


  




}