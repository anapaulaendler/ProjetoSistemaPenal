import React from "react";
import { useEffect, useState } from "react";
import { Detento } from "../../../interfaces/Detento";
import "../../../css/detento/Detento.css"

function BuscarDetento() {

    const [detento, setDetento] = useState<Detento>();
    const [id, setId] = useState<string>();
    const [erro, setErro] = useState<string>();

    function digitar(e : any){
        setId(e.target.value);
    }

    function clicar(){

        if (!id) {
            setErro("Por favor, insira um ID válido.");
            console.log(erro);
            return;
        }

        fetch("http://localhost:5291/api/detento/buscar/" + id)
            .then(resposta => {
                return resposta.json();
            })
            .then((data) => {
                if (data) {
                    setDetento(data); 
                } else {
                    setErro("Detento não encontrado.");
                    console.log(erro);
                }
            })
            .catch((erro) => {
                setErro("Erro ao buscar o detento."); 
                console.log(erro);
            });
    }

  return (
    <div>
    <div id="form_buscar_detento">
        <h1>Buscar Detento</h1>
        <form onSubmit={clicar}>
            <input 
                type="text" 
                placeholder="Digite o ID do detento"
                onChange={digitar} 
                required
            />
            <button type="submit">Consultar</button>
        </form>
    </div>
    {detento && (
        <div>
            <p><strong>DetentoId:</strong> {detento.detentoId}</p>
            <p><strong>Nome:</strong> {detento.nome}</p>
            <p><strong>Data Nascimento:</strong> {detento.dataNascimento}</p>
            <p><strong>CPF: </strong> {detento.cpf}</p>
            <p><strong>Sexo:</strong> {detento.sexo}</p>
            <p><strong>Início Pena:</strong> {detento.inicioPena}</p>
            <p><strong>Fim da Pena:</strong> {detento.fimPena}</p>
        </div>
    )}
</div>
  );
}

export default BuscarDetento;