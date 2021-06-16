import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminProdutosPageComponent } from './pages/admin-produtos-page/admin-produtos-page.component';
import { MasterPageComponent } from './pages/master-page/master-page.component';

const routes: Routes = [
  {
    path: "",
    component: MasterPageComponent,
    children: [
      { path: "", component: AdminProdutosPageComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
