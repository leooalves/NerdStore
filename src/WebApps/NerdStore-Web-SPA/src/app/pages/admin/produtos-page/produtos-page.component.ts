import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Produto } from 'src/app/models/produto.model';
import { DataService } from 'src/app/services/data.service';


@Component({
  selector: 'app-produtos-page',
  templateUrl: './produtos-page.component.html'
})
export class AdminProdutosPageComponent implements OnInit {

  public produtos$: Observable<Produto[]>;

  constructor(private data: DataService) { }

  ngOnInit(): void {
    this.produtos$ = this.data.getProdutos();
  }

}
