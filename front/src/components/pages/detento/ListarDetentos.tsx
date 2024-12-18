import React from "react";
import { useEffect, useState } from "react";
import { Detento } from "../../../interfaces/Detento";
import DetentoNav from "../nav/DetentoNav";
import { Link } from "react-router-dom";
import axios from "axios";

function ListarDetentos() {

    const [detentos, setDetentos] = useState<Detento[]>([]);

    useEffect(() => {
        fetch("http://localhost:5291/api/detento/listar")
             .then(resposta => {
                 return resposta.json();
             })
             .then(detentos => {
                 setDetentos(detentos);
             }).catch((e) => console.log("erro ao listar detentos, erro:" + e))

        
        });

        function deletar(id: string) {
          axios
            .delete(`http://localhost:5291/api/detento/deletar/${id}`)
            .then((resposta) => {
              console.log(resposta.data);
            });
        }

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
          <th>Deletar</th>
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
          <Link to={`/operacoesDetento/listar/atividades/${detento.detentoId}`}>Alterar</Link>
          </td>
          <td>
                <button onClick={() => deletar(detento.detentoId!)}>
                  Deletar
                </button>
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