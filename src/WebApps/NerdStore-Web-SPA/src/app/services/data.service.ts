import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Produto } from '../models/produto.model';

@Injectable({
    providedIn: 'root'
})
export class DataService {

    public url: string = "https://localhost:44386/api/v1";

    constructor(private http: HttpClient) { }

    // public composeHeader() {
    //     const token = Security.getToken();
    //     const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    //     return headers;
    // }

    getProdutos() {
        return this.http.get<Produto[]>(`${this.url}/produto`)
    }

    getcategorias() {
        return this.http.get<Produto[]>(`${this.url}/produto/categoria`)
    }
}
