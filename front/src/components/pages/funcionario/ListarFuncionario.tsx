import { useEffect, useState } from "react";
import { Funcionario } from "../../../interfaces/Funcionario";
import "../../../css/ListarFuncionarios.css"

function ListarFuncionario(){
  const [funcionarios, setFuncionarios] = useState<Funcionario[]>([])
  useEffect(() => {
    fetch("http://localhost:5291/api/funcionario/listar")
    .then(resposta => {
      return resposta.json()
    })
    .then((funcionarios) => {
      setFuncionarios(funcionarios);
    })
    .catch(() => console.log("funcionarios não encontrados ou inexistentes"))
  })

  return(
    <div id="form_listar_funcionarios">
        <table>
          <thead>
              <tr>
                <th>funcionarioId</th>
                <th>Nome</th>
                <th>cargo</th>
                <th>Data de Nascimento</th>
                <th>Cpf</th>
                <th>Sexo</th>
              </tr>
          </thead>
          <tbody>
              {funcionarios.map(funcionario =>  (
                <tr key={funcionario.funcionarioId}>
                  <td>{funcionario.funcionarioId}</td>
                  <td>{funcionario.nome}</td>
                  <td>{funcionario.cargo}</td>
                  <td>{new Date(funcionario.dataNascimento).toLocaleDateString()}</td>
                  <td>{funcionario.cpf}</td>
                  <td>{funcionario.sexo}</td>
                </tr>
              )
              )}
          </tbody>
        </table>
    </div>
  )
}
export default ListarFuncionario;