import { Atividade } from "./Atividade";
import { Pessoa } from "./Pessoa";

export interface DetentoInativo extends Pessoa {
    detentoInativoId? : string;
    inicioPena : string;
    fimPena : string;
}