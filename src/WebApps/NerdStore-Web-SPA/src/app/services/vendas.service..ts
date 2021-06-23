import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Produto } from '../models/produto.model';
import { RespostaPadrao } from '../models/resposta.padrao.model';

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



}
