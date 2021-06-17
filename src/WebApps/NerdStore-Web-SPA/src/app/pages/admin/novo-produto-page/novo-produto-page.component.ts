import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'app-novo-produto-page',
  templateUrl: './novo-produto-page.component.html'
})
export class NovoProdutoPageComponent implements OnInit {

  public form: FormGroup;
  public categorias$: Observable<any[]>;
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
      quantidadeEstoque: ['0', Validators.compose([
        Validators.min(0),
        Validators.required
      ])],
      categoriaId: []
    });
  }

  ngOnInit(): void {

    this.categorias$ = this.data.getCategorias();
    this.categorias$.subscribe(resposta => {
      this.categorias = resposta
    })
  }

  submit() {

    this.data.criaProduto(this.form.value)
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
