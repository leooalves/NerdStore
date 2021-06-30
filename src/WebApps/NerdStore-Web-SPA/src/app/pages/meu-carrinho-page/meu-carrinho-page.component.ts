
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Carrinho } from 'src/app/models/carrinho.model';
import { VendasService } from 'src/app/services/vendas.service.';

@Component({
  selector: 'app-meu-carrinho-page',
  templateUrl: './meu-carrinho-page.component.html',
  styleUrls: ['./meu-carrinho-page.component.css']
})
export class MeuCarrinhoPageComponent implements OnInit {

  public carrinho$: Observable<Carrinho>;
  public carrinho: Carrinho

  constructor(
    public service: VendasService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.carregarCarrinho();
  }

  carregarCarrinho() {
    this.carrinho$ = this.service.retornaCarrinho();
    this.carrinho$.subscribe(res => this.carrinho = res);
  }

  removerItem(produtoId: string) {


    this.service.removeItemCarrinho(produtoId).subscribe(resposta => {
      if (resposta.sucesso) {
        this.toastr.success("Item removido do carrinho com sucesso")
        this.carregarCarrinho();
      } else {
        this.toastr.error(resposta.mensagem);
        console.log(resposta)
      }
    });
  }


}
