import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Produto } from 'src/app/models/produto.model';
import { DataService } from 'src/app/services/data.service';


@Component({
  selector: 'app-estoque-page',
  templateUrl: './estoque-page.component.html'
})
export class EstoquePageComponent implements OnInit {

  public produto$: Observable<Produto>;
  public produto: Produto;
  public produtoId: string;
  public form: FormGroup;

  constructor(
    private data: DataService,
    private fb: FormBuilder,
    private route: Router,
    private toastr: ToastrService,
    private activatedRoute: ActivatedRoute) {
    this.form = this.fb.group({
      quantidadeEstoque: ['', Validators.compose([
        Validators.min(-999),
        Validators.max(999),
        Validators.required
      ])],
      id: []
    });
  }

  ngOnInit(): void {

    this.activatedRoute.params.subscribe(parameter => {
      this.produtoId = parameter.id
    })

    this.produto$ = this.data.getProdutoPorId(this.produtoId);
    this.produto$.subscribe(resposta => {

      this.produto = resposta;
      //this.form.patchValue(this.produto)
    })
  }

  Salvar() {

    this.data.atualizaProduto(this.form.value)
      .subscribe(resposta => {
        if (resposta.sucesso) {
          this.toastr.success(resposta.mensagem);
          this.route.navigate(['/'])
        } else {
          this.toastr.error(resposta.mensagem);
        }

        console.log(resposta)
      })


  }

}
