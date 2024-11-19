import React from "react";
import { useEffect, useState } from "react";
import { Detento } from "../../../interfaces/Detento";
import "../../../css/detento/Detento.css"

function ListarDetentos() {

    const [detentos, setDetentos] = useState<Detento[]>([]);

    useEffect(() => {
        fetch("http://localhost:5291/api/detento/listar")
             .then(resposta => {
                 return resposta.json();
             })
             .then(detentos => {
                 setDetentos(detentos);
             })

        
        });
  return (
    <div id="form_listar_detento">
    <h1>Listagem de Detentos</h1>
    <table>
    <thead>
      <tr>
        <th>ID</th>
        <th>Nome</th>
        <th>Data de Nascimento</th>
        <th>CPF</th>
        <th>Sexo</th>
        {/* <th>Tempo de Pena Inicial</th> */}
        {/* <th>Pena Restante</th> */}
        <th>Início da Pena</th>
        <th>Fim da Pena</th>
      </tr>
    </thead>
    <tbody>
      {detentos.map(detento => (
        <tr key={detento.detentoId}>
        <td>{detento.detentoId}</td>
        <td>{detento.nome}</td>
        <td>{new Date(detento.dataNascimento).toLocaleDateString()}</td>
        <td>{detento.cpf}</td>
        <td>{detento.sexo}</td>
        <td>{new Date(detento.inicioPena).toLocaleDateString()}</td>
        <td>{new Date(detento.fimPena).toLocaleDateString()}</td>
        </tr>
      ))}
    </tbody>
    </table>
  </div>
  );
}

export default ListarDetentos;