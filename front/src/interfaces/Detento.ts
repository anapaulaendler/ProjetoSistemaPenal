import { Atividade } from "./Atividade";
import { Pessoa } from "./Pessoa";

export interface Detento extends Pessoa {
    tempoPenaInicial : number;
    penaRestante : number;
    inicioPena : string;
    fimPena : string;
    atividades: Atividade[];
}