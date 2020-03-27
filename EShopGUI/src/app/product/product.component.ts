import { Component } from "@angular/core";
import { templateJitUrl } from '@angular/compiler';
import { AppService, Product, News, NewsViewmodel } from '../core/sercices/api.client.generated';
import * as CanvasJS from '../../assets/script/canvasjs.min';
import { ActivatedRouteSnapshot } from '@angular/router';
import { forkJoin } from 'rxjs';



@Component({
    selector: 'product',
    templateUrl: './product.component.html'
})

export class ProductComponent {

    public _appService: AppService;
    public listProduct: Array<Product> = [];
    public product: Product = new Product();
    public newsViewModels: NewsViewmodel[];
    public isLoaded: boolean = false;
    constructor(appService: AppService) {

        this._appService = appService;

        this._appService.getNewsForView().subscribe(response => {
            this.newsViewModels = response;
        });
        this.LoadObject();


        for (let item of this.listProduct)
            console.log('ádasdsadas');

        console.log(this.listProduct.length);
        console.log(this.listProduct);

    }


    LoadProduct(): void {
        forkJoin(
            this._appService.getAll().subscribe(response => {
                this.listProduct = response;
            }));

    }


    LoadObject() {

        this._appService.getAll().subscribe(response => { //C1: loop list get từ api trong subscibe để fix lỗi bất đồng bộ
            response.forEach(element => {
                this.listProduct.push(element);
                this.isLoaded = true;
            });
            console.log(this.listProduct.length);
            this.LoadChart();

        });



    }

    LoadChart() {

        let data: Array<{ y: number, label: string }>=[];


        this.listProduct.forEach(element => {

            let price = element.unitPrice;
            var name = element.productName;

            var temp= {y:price,label:name}
            console.log(temp);
            data.push(temp);
        });


        let chart = new CanvasJS.Chart("chartContainer", {
            animationEnabled: true,
            exportEnabled: true,
            title: {
                text: "Demo Product char"
            },
            data: [{
                type: "column",
                dataPoints: data
            }]
        });

        chart.render();

    }

    Create(): void {

        if (this.product.productName == "") return;

        console.log(this.listProduct.length);
        this._appService.create(this.product).subscribe(result => {
            console.log("Created");
            window.location.reload();
        })




    }


    public chartType: string = 'horizontalBar';

    public chartDatasets: Array<any> = [
        { data: [65, 59, 80, 81, 56, 55, 40], label: 'My First dataset' }
    ];

    public chartLabels: Array<any> = ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'];

    public chartColors: Array<any> = [
        {
            backgroundColor: [
                'rgba(255, 99, 132, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(255, 206, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)',
                'rgba(153, 102, 255, 0.2)',
                'rgba(255, 159, 64, 0.2)'
            ],
            borderColor: [
                'rgba(255,99,132,1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)'
            ],
            borderWidth: 2,
        }
    ];

    public chartOptions: any = {
        responsive: true
    };
    public chartClicked(e: any): void { }
    public chartHovered(e: any): void { }




}