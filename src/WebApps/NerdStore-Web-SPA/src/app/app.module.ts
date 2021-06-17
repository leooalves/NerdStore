import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { ToastrModule } from 'ngx-toastr';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/shared/navbar/navbar.component';
import { MasterPageComponent } from './pages/master-page/master-page.component';
import { AdminProdutosPageComponent } from './pages/admin/produtos-page/produtos-page.component';
import { CardProdutoComponent } from './components/card-produto/card-produto.component';
import { DataService } from './services/data.service';
import { EditarProdutoPageComponent } from './pages/admin/editar-produto-page/editar-produto-page.component';
import { NovoProdutoPageComponent } from './pages/admin/novo-produto-page/novo-produto-page.component';





@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    MasterPageComponent,
    AdminProdutosPageComponent,
    CardProdutoComponent,
    EditarProdutoPageComponent,
    NovoProdutoPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule
  ],
  providers: [DataService],
  bootstrap: [AppComponent]
})
export class AppModule { }
