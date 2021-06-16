import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/shared/navbar/navbar.component';
import { MasterPageComponent } from './pages/master-page/master-page.component';
import { AdminProdutosPageComponent } from './pages/admin-produtos-page/admin-produtos-page.component';
import { CardProdutoComponent } from './components/card-produto/card-produto.component';
import { DataService } from './services/data.service';


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    MasterPageComponent,
    AdminProdutosPageComponent,
    CardProdutoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [DataService],
  bootstrap: [AppComponent]
})
export class AppModule { }
