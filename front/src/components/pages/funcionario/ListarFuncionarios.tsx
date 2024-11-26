import { useEffect, useState } from "react";
import { Funcionario } from "../../../interfaces/Funcionario";
import FuncionarioNav from "../nav/FuncionarioNav";
import { Link } from "react-router-dom";

function ListarFuncionarios(){
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
      <div className="main-content">
        <FuncionarioNav/>
        <div id="form">
          <h1>Listar Funcionários</h1>
            <table>
              <thead>
                  <tr>
                    <th>funcionarioId</th>
                    <th>Nome</th>
                    <th>cargo</th>
                    <th>Data de Nascimento</th>
                    <th>Cpf</th>
                    <th>Sexo</th>
                    <th>Alterar</th>
                  </tr>
              </thead>
              <tbody>
                  {funcionarios.length > 1 && 
                  funcionarios.map(funcionario =>  (
                    <tr key={funcionario.funcionarioId}>
                      <td>{funcionario.funcionarioId}</td>
                      <td>{funcionario.nome}</td>
                      <td>{funcionario.cargo}</td>
                      <td>{new Date(funcionario.dataNascimento).toLocaleDateString()}</td>
                      <td>{funcionario.cpf}</td>
                      <td>{funcionario.sexo}</td>
                      <td>
                      <Link to={`/operacoesFuncionario/editar/${funcionario.funcionarioId}`}>Alterar</Link>
                      </td>
                    </tr>
                  )
                  )}
              </tbody>
            </table>
        </div>
    </div>
  )
}
export default ListarFuncionarios;