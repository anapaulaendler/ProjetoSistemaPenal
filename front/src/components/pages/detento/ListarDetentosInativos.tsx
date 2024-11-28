import React from "react";
import { useEffect, useState } from "react";
import { Detento } from "../../../interfaces/Detento";
import DetentoNav from "../nav/DetentoNav";
import { Link } from "react-router-dom";
import { DetentoInativo } from "../../../interfaces/DetentoInativo";

function ListarDetentosInativos() {

    const [detentosInativos, setDetentosInativos] = useState<DetentoInativo[]>([]);

    useEffect(() => {
        fetch("http://localhost:5291/api/detentoinativo/listar")
             .then(resposta => {
                 return resposta.json();
             })
             .then(detentosInativos => {
                 setDetentosInativos(detentosInativos);
             }).catch((e) => console.log("erro ao listar detentosInativos, erro:" + e))

        
        });
  return (
    <div className="main-content">
      <DetentoNav/>
      <div id="form">

      <h1>Listagem de Detentos Inativos</h1>
      <table>
      <thead>
        <tr>
          <th>ID</th>
          <th>Nome</th>
          <th>Data de Nascimento</th>
          <th>CPF</th>
          <th>Sexo</th>
          <th>Início da Pena</th>
          <th>Fim da Pena</th>
        </tr>
      </thead>
      <tbody>
        {detentosInativos.map(detento => (
          <tr key={detento.detentoInativoId}>
          <td>{detento.detentoInativoId}</td>
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
    </div>
  );
}

export default ListarDetentosInativos;