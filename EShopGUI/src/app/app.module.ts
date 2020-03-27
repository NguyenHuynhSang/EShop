import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule} from '@angular/forms';    
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppService } from './core/sercices/api.client.generated';
import { ProductComponent } from './product/product.component';
import { HttpClientModule } from '@angular/common/http'; 
import { ChartsModule, WavesModule } from 'angular-bootstrap-md' ///User option
@NgModule({
  declarations: [
    AppComponent,
    ProductComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule, 
    ChartsModule,
    WavesModule
  ],
  providers: [AppService],
  bootstrap: [AppComponent]
})
export class AppModule { }
