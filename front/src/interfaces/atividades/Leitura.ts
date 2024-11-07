import { Atividade } from "../Atividade";

export interface Leitura extends Atividade {

    limite : number;
    anoAtual : number;
    // depois, lembrar de usar o new Date().getFullYear()
    equivalencia : number;
}