import React from "react";
import { useEffect, useState } from "react";
import { Detento } from "../../../interfaces/Detento";

function BuscarDetento() {

    const [detento, setDetento] = useState<Detento>();
    const [id, setId] = useState<string>();
    const [erro, setErro] = useState("");

    function digitar(e : any){
        setId(e.target.value);
    }

    function clicar(){

        if (!id) {
            setErro("Por favor, insira um ID válido.");
            return;
        }

        fetch("http://localhost:5291/api/detento/buscar" + id)
            .then(resposta => {
                return resposta.json();
            })
            .then((data) => {
                if (data) {
                    setDetento(data); 
                    setErro(""); 
                } else {
                    setErro("Detento não encontrado.");
                }
            })
            .catch((erro) => {
                setErro("Erro ao buscar o detento."); 
                console.error(erro);
            });
    }

  return (
    <div>
    <h1>Buscar Detento</h1>

    <input 
        type="text" 
        placeholder="Digite o ID do detento"
        onChange={digitar} 
    />

    <button onClick={clicar}>Consultar</button>

    {detento && (
        <div>
            <p><strong>DetentoId:</strong> {detento.detentoId}</p>
            <p><strong>Nome:</strong> {detento.nome}</p>
            <p><strong>Data Nascimento:</strong> {detento.dataNascimento}</p>
            <p><strong>CPF: </strong> {detento.cpf}</p>
            <p><strong>Sexo:</strong> {detento.sexo}</p>
            <p><strong>Tempo de Pena Inicial:</strong> {detento.tempoPenaInicial}</p>
            <p><strong>Pena Restante:</strong> {detento.penaRestante}</p>
            <p><strong>Início Pena:</strong> {detento.inicioPena}</p>
            <p><strong>Fim da Pena:</strong> {detento.fimPena}</p>
        </div>
    )}
</div>
  );
}

export default BuscarDetento;