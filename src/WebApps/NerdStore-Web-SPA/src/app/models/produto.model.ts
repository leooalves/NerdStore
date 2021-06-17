export class Produto {
    public id: string;
    public categoriaId: string;
    public nome: string;
    public descricao: string;
    public ativo: boolean;
    public valor: number;
    public dataCadastro: Date;
    public imagem: string;
    public quantidadeEstoque: number;
    public altura: number;
    public largura: number;
    public profundidade: number;
    public categorias: any[];

    constructor() {


    }


}