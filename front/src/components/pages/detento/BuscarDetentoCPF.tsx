import React from "react";
import { useEffect, useState } from "react";
import { Detento } from "../../../interfaces/Detento";
import { Atividade } from "../../../interfaces/Atividade";
import DetentoNav from "../nav/DetentoNav";

function BuscarDetentoCPF() {

    const [cpf, setCpf] = useState<string>();
    const [respostaClasse, setRespostaClasse] = useState("respostaClasse")
    const [resposta, setResposta] = useState("")

    const [detento, setDetento] = useState<Detento>();
    const [atividades, setAtividades] = useState<Atividade[]>([])

    function digitar(e : any){
        setCpf(e.target.value);
    }

    function clicar(e:any){
        e.preventDefault();

        if (!cpf) {
            setResposta("Por favor, insira um CPF válido.");
            return;
        }

        fetch("http://localhost:5291/api/detento/buscar/cpf:" + formatCPF(cpf))
        .then(resposta => {
        return resposta.json()
        })
        .then(detento => {
        if(detento == null){
            setRespostaClasse("resposta-erro")
            return setResposta("detento não encontrado")
        }else{
            setRespostaClasse("resposta-sucesso")
            setDetento(detento);
            setAtividades(detento.atividades)
            return setResposta("detento encontrado")
        }
        })
        .catch(() => {
        setRespostaClasse("resposta-erro")
        setResposta("detento não encontrado")
        });

        function formatCPF(cpf: string): string {
            // Remove qualquer caractere que não seja um número
            const numericCPF = cpf.replace(/\D/g, "");
            
            // Verifica se o CPF tem 11 dígitos
            if (numericCPF.length !== 11) {
                throw new Error("CPF inválido. Certifique-se de que ele contém 11 dígitos.");
            }
            
            // Aplica a formatação XXX.XXX.XXX-XX
            return numericCPF.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4");
        }
    }

  return (
    <div className="main-content">
        <DetentoNav/>
        <div id="form">
        <h1>Buscar Detento</h1>
        <form onSubmit={clicar}>

                <label htmlFor="detetoCpf">Digite o CPF do detento
                <input 
                    type="text" 
                    onChange={digitar} 
                />
                </label>
                <button type="submit">Consultar</button>
                <div className={respostaClasse}>{resposta}</div>
                
        </form>
        {detento && (
            <div id="detento_encontrado">
                <p><strong>DetentoId:</strong> {detento.detentoId}</p>
                <p><strong>Nome:</strong> {detento.nome}</p>
                <p><strong>Data Nascimento:</strong> {detento.dataNascimento}</p>
                <p><strong>CPF: </strong> {detento.cpf}</p>
                <p><strong>Sexo:</strong> {detento.sexo}</p>
                <p><strong>Início Pena:</strong> {detento.inicioPena}</p>
                <p><strong>Fim da Pena:</strong> {detento.fimPena}</p>
            </div>
        )}
        {atividades.length > 1 && (
            <div>
                <h1>Listar de atividades</h1>
                    <div>
                        <table>
                        <thead>
                            <th>ID</th>
                            <th>Nome da Atividade</th>
                            <th>Contador</th>
                            <th>DetentoId</th>
                            <th>Limite</th>
                            <th>Ano Atual</th>
                        </thead>
                        <tbody>
                            {atividades && 
                            atividades.map(atividade => (
                            <tr key={atividade.atividadeId}>
                                <td>{atividade.atividadeId}</td>
                                <td>{atividade.tipo}</td>
                                <td>{atividade.contador}</td>
                                <td>{atividade.detentoId}</td>
                                <td>{atividade.limite || 'N/A'}</td>
                                <td>{atividade.anoAtual || 'N/A'}</td>
                            </tr>
                            ))}
                        </tbody>
                        </table>
                    </div>
                </div>
            )}
        </div>
    </div>
  );
}

export default BuscarDetentoCPF;