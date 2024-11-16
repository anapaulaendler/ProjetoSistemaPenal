import { Pessoa } from "./Pessoa";

export interface Funcionario extends Pessoa {
    funcionarioId? : string;
    cargo : string;
}