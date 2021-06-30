import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Produto } from 'src/app/models/produto.model';
import { DataService } from 'src/app/services/data.service';
import { VendasService } from 'src/app/services/vendas.service.';

@Component({
  selector: 'app-catalogo-page',
  templateUrl: './catalogo-page.component.html',
  styleUrls: ['./catalogo-page.component.css']
})
export class CatalogoPageComponent implements OnInit {

  public produtos$: Observable<Produto[]>;

  constructor(
    private data: DataService,
    private vendas: VendasService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.produtos$ = this.data.getProdutos();
  }

  AdicionarAoCarrinho(produto: Produto) {

    var item = {
      produtoId: produto.id,
      nomeProduto: produto.nome,
      quantidade: 1,
      valorUnitario: produto.valor
    }

    this.vendas.enviaItemCarrinho(item).subscribe(resposta => {
      if (resposta.sucesso) {
        this.toastr.success("Produto adicionado ao carrinho com sucesso")
      } else {
        this.toastr.error(resposta.mensagem);
      }
    });

  }


  public PossuiEstoque(produto: Produto) {
    if (produto.quantidadeEstoque > 0)
      return true;
    return false;
  }
}
