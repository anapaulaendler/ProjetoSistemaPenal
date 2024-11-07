import React from "react";
import { useEffect, useState } from "react";
import { Detento } from "../../../interfaces/Detento";

function ListarDetentos() {

    const [detento, setDetento] = useState<Detento>();

    useEffect(() => {
        fetch("http://localhost:5291/api/detento/buscar" + id)
             .then(resposta => {
                 return resposta.json();
             })
             .then(detentos => {
                 setDetentos(detentos);
             })
        });
  return (
    <div>
    <h1>Listagem de Detentos</h1>
    <table>
    <thead>
      <tr>
        <th>ID</th>
        <th>Nome</th>
        <th>Data de Nascimento</th>
        <th>CPF</th>
        <th>Sexo</th>
        <th>Tempo de Pena Inicial</th>
        <th>Pena Restante</th>
        <th>Início da Pena</th>
        <th>Fim da Pena</th>
      </tr>
    </thead>
    <tbody>
      {detentos.map(detento => (
        <tr key={detento.detentoId}>
        <td>{detento.detentoId}</td>
        <td>{detento.nome}</td>
        <td>{detento.dataNascimento}</td>
        <td>{detento.cpf}</td>
        <td>{detento.sexo}</td>
        <td>{detento.tempoPenaInicial}</td>
        <td>{detento.penaRestante}</td>
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