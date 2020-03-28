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
            this.LoadDoughnutChart();
        });



    }

    LoadChart() {

        let data: Array<{ y: number, label: string }> = [];


        this.listProduct.forEach(element => {
            var temp = { y:  element.unitPrice, label:  element.productName }
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


    LoadDoughnutChart() {

        let data: Array<{ y: number, name: string }> = [];


        this.listProduct.forEach(element => {
            var temp = { y:  element.unitPrice, name:  element.productName }
            console.log(temp);
            data.push(temp);
        });



        let chart2 = new CanvasJS.Chart("chartContainer2", {
            theme: "light2",
            animationEnabled: true,
            exportEnabled: true,
            title: {
                text: "Monthly Expense"
            },
            data: [{
                type: "pie",
                showInLegend: true,
                toolTipContent: "<b>{name}</b>: ${y} (#percent%)",
                indexLabel: "{name} - #percent%",
                dataPoints: data
            }]
        });

        chart2.render();


    }


    Create(): void {

        if (this.product.productName == "") return;

        console.log(this.listProduct.length);
        this._appService.create(this.product).subscribe(result => {
            console.log("Created");
            window.location.reload();
        })




    }




}