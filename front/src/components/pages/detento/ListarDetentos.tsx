import React from "react";
import { useEffect, useState } from "react";
import { Detento } from "../../../interfaces/Detento";
import DetentoNav from "../nav/DetentoNav";
import { Link } from "react-router-dom";

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
    <div className="main-content">
      <DetentoNav/>
      <div id="form">

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
          <th>Atividades</th>
          <th>Alterar</th>
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
          <td>
            <select>
              {detento.atividades?.map((atividade) => (
                <option key={atividade.atividadeId}>{atividade.tipo}</option>
              )) || "N/A"}
            </select>
          </td>
          <td>
            <Link to={`/operacoesDetento/editar/${detento.detentoId}`}>Alterar</Link>
          </td>
          </tr>
        ))}
      </tbody>
      </table>
    </div>
    </div>
  );
}

export default ListarDetentos;