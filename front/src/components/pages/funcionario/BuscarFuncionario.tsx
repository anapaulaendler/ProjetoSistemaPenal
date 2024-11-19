import { useState } from "react";
import { Funcionario } from "../../../interfaces/Funcionario";
import "../../../css/funcionario/Funcionario.css"

function BuscarFuncionario(){

    const [funcionario, setFuncionario] = useState<Funcionario>()
    const [ cpf, setCpf  ] = useState<string>('');
    const [resposta, setResposta] = useState("");

    function encontrarFuncionario(){
    
        
        fetch("http://localhost:5291/api/funcionario/buscar/cpf:" + cpf)
        .then(resposta => {
            return resposta.json()
        }).then( funcionario => {
            setFuncionario(funcionario)
            setResposta("")
          }
        )
        .catch((error) => {
            console.log(error)
            setResposta("Funcionario não encontrado")
        })
    
      }

    return(
        <div id="form_buscar_funcionario">
            <h1>Buscar Funcionario</h1>
            <form onSubmit={encontrarFuncionario}>
                <label htmlFor="cpf"> CPF do Funcionário:
                    <input type="text" onChange={e => setCpf(e.target.value)} required/>
                </label>
                <div>{resposta}</div>
                <button type="submit">Buscar</button>
            </form>
            {funcionario &&
                <div>
                    <p><strong>FuncionarioId:</strong> {funcionario.funcionarioId}</p>
                    <p><strong>Nome:</strong> {funcionario.nome}</p>
                    <p><strong>Data Nascimento:</strong> {new Date(funcionario.dataNascimento).toLocaleDateString()}</p>
                    <p><strong>CPF: </strong> {funcionario.cpf}</p>
                    <p><strong>Sexo:</strong> {funcionario.sexo}</p>
                    <p><strong>Tempo de Pena Inicial:</strong> {funcionario.cargo}</p>
                </div>
            }
        </div>
    )
}

export default BuscarFuncionario;