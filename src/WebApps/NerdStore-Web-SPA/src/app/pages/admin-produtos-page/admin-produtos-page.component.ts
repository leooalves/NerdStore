import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Produto } from 'src/app/models/produto.model';
import { DataService } from 'src/app/services/data.service';


@Component({
  selector: 'app-admin-produtos-page',
  templateUrl: './admin-produtos-page.component.html',
  styleUrls: ['./admin-produtos-page.component.css']
})
export class AdminProdutosPageComponent implements OnInit {

  public produtos$: Observable<Produto[]>;

  constructor(private data: DataService) { }

  ngOnInit(): void {
    this.produtos$ = this.data.getProdutos();
    console.log(this.produtos$);
  }

}
