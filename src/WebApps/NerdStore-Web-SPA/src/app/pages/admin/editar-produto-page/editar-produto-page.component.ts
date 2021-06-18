import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Produto } from 'src/app/models/produto.model';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'app-editar-produto-page',
  templateUrl: './editar-produto-page.component.html'
})
export class EditarProdutoPageComponent implements OnInit {

  public produto$: Observable<Produto>;
  private produto: Produto;
  public produtoId: string;
  public form: FormGroup;
  public categorias: any[];

  constructor(
    private data: DataService,
    private fb: FormBuilder,
    private route: Router,
    private toastr: ToastrService,
    private activatedRoute: ActivatedRoute) {
    this.form = this.fb.group({
      nome: ['', Validators.compose([
        Validators.minLength(3),
        Validators.maxLength(80),
        Validators.required
      ])],
      descricao: ['', Validators.compose([
        Validators.minLength(3),
        Validators.maxLength(80),
        Validators.required
      ])],
      ativo: [true],
      valor: ['', Validators.compose([
        Validators.min(0),
        Validators.required
      ])],
      imagem: ['', Validators.compose([
        Validators.minLength(3),
        Validators.maxLength(80),
        Validators.required
      ])],
      altura: ['0', Validators.compose([
        Validators.min(0),
        Validators.required
      ])],
      largura: ['0', Validators.compose([
        Validators.min(0),
        Validators.required
      ])],
      profundidade: ['0', Validators.compose([
        Validators.min(0),
        Validators.required
      ])],
      categoriaId: [],
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
      this.form.patchValue(this.produto)

      this.categorias = this.produto.categorias;
    })
  }

  Salvar() {

    this.data.atualizaProduto(this.form.value)
      .subscribe(resposta => {
        if (resposta.sucesso) {
          this.toastr.success(resposta.mensagem);
          this.route.navigate(['/admin'])
        } else {
          this.toastr.error(resposta.mensagem);
        }

        console.log(resposta)
      })


  }

}
