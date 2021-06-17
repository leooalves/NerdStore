import { Component, Input, OnInit } from '@angular/core';
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

  constructor(private data: DataService) { }

  ngOnInit(): void {
  }

}
