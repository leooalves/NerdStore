import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Produto } from '../models/produto.model';
import { RespostaPadrao } from '../models/resposta.padrao.model';
import { Carrinho } from '../models/carrinho.model';

@Injectable({
    providedIn: 'root'
})
export class VendasService {

    public url: string = "http://localhost:5002/api/v1";

    constructor(private http: HttpClient) { }

    // public composeHeader() {
    //     const token = Security.getToken();
    //     const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    //     return headers;
    // }



    enviaItemCarrinho(itemCarrinho: any) {
        return this.http.post<RespostaPadrao>(`${this.url}/carrinho/item/`, itemCarrinho)
    }

    retornaCarrinho() {
        return this.http.get<Carrinho>(`${this.url}/carrinho`);
    }

    removeItemCarrinho(item: string) {
        return this.http.delete<RespostaPadrao>(`${this.url}/carrinho/item/${item}`)
    }

    limparCarrinho() {
        return this.http.delete<RespostaPadrao>(`${this.url}/carrinho`)
    }

    aplicarVoucher(voucher: any) {
        return this.http.post<RespostaPadrao>(`${this.url}/carrinho/voucher/`, voucher)
    }

}
