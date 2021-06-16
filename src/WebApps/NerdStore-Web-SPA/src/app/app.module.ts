import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/shared/navbar/navbar.component';
import { MasterPageComponent } from './pages/master-page/master-page.component';
import { AdminProdutosPageComponent } from './pages/admin-produtos-page/admin-produtos-page.component';
import { CardProdutoComponent } from './components/card-produto/card-produto.component';


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
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
