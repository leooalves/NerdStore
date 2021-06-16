import { Component, Input, OnInit } from '@angular/core';
import { Produto } from 'src/app/models/produto.model';

@Component({
  selector: 'app-card-produto',
  templateUrl: './card-produto.component.html',
  styleUrls: ['./card-produto.component.css']
})
export class CardProdutoComponent implements OnInit {
  @Input() produto: Produto;

  constructor() { }

  ngOnInit(): void {
  }

}
