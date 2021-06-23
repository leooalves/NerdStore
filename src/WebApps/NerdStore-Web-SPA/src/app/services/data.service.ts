import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Produto } from '../models/produto.model';
import { RespostaPadrao } from '../models/resposta.padrao.model';

@Injectable({
    providedIn: 'root'
})
export class DataService {

    public url: string = "http://localhost:5000/api/v1";

    constructor(private http: HttpClient) { }

    // public composeHeader() {
    //     const token = Security.getToken();
    //     const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    //     return headers;
    // }

    getProdutos() {
        return this.http.get<Produto[]>(`${this.url}/produto`)
    }

    getProdutoPorId(id: string) {
        return this.http.get<Produto>(`${this.url}/produto/${id}`)
    }

    atualizaProduto(produto: any) {
        return this.http.put<RespostaPadrao>(`${this.url}/produto`, produto)
    }

    criaProduto(produto: any) {
        return this.http.post<RespostaPadrao>(`${this.url}/produto`, produto)
    }

    atualizaEstoqueProduto(idProduto: string, quantidade: number) {
        return this.http.post<RespostaPadrao>(`${this.url}/produto/atualiza-estoque/${idProduto}?quantidade=${quantidade}`, '')
    }


    getCategorias() {
        return this.http.get<any[]>(`${this.url}/produto/categoria`)
    }
}
