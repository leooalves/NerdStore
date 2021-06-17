import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EditarProdutoPageComponent } from './pages/admin/editar-produto-page/editar-produto-page.component';
import { NovoProdutoPageComponent } from './pages/admin/novo-produto-page/novo-produto-page.component';
import { AdminProdutosPageComponent } from './pages/admin/produtos-page/produtos-page.component';
import { MasterPageComponent } from './pages/master-page/master-page.component';

const routes: Routes = [
  {
    path: "",
    component: MasterPageComponent,
    children: [
      { path: "", component: AdminProdutosPageComponent },
      { path: "editar-produto/:id", component: EditarProdutoPageComponent },
      { path: "novo-produto", component: NovoProdutoPageComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
