export class Carrinho {
    public pedidoId: string;
    public clienteId: string;
    public subTotal: number;
    public valorTotal: number;
    public valorDesconto: number;
    public voucherCodigo: string = "";

    public items: ItemCarrinho[] = [];
    public pagamento: any;

    constructor() {


    }



}

export class ItemCarrinho {
    produtoId: string;
    produtoNome: string;
    quantidade: number;
    valorUnitario: number;
    valorTotal: number;
}