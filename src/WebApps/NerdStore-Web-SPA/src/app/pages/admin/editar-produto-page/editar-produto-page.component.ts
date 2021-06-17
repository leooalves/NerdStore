import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Produto } from 'src/app/models/produto.model';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'app-editar-produto-page',
  templateUrl: './editar-produto-page.component.html',
  styleUrls: ['./editar-produto-page.component.css']
})
export class EditarProdutoPageComponent implements OnInit {

  public produto$: Observable<Produto>;
  public produto: Produto;
  public produtoId: string;
  public form: FormGroup;
  public categorias: any[];

  constructor(
    private data: DataService,
    private fb: FormBuilder,
    private route: Router,
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

      // this.form.controls['nome'].setValue(this.produto.nome);
      // this.form.controls['descricao'].setValue(this.produto.descricao);
      // this.form.controls['ativo'].setValue(this.produto.ativo);
      // this.form.controls['valor'].setValue(this.produto.valor);
      // this.form.controls['imagem'].setValue(this.produto.imagem);
      // this.form.controls['altura'].setValue(this.produto.largura);
      // this.form.controls['largura'].setValue(this.produto.largura);
      // this.form.controls['profundidade'].setValue(this.produto.profundidade);
      // this.form.controls['categoriaId'].setValue(this.produto.categoriaId)
      this.categorias = this.produto.categorias;
    })
  }

  Salvar() {

    //this.produto = this.form.value

    //console.log(this.produto)

    console.log(this.form.getRawValue())
    console.log(this.form.value)
    console.log(this.produto)

    this.data.atualizaProduto(this.form.getRawValue()).subscribe(resposta => {
      console.log(resposta)
    })

    //this.route.navigate(['/'])
  }

}
