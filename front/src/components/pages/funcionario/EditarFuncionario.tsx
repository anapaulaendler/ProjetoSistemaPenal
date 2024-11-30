import { useEffect, useState } from "react";
import { Funcionario } from "../../../interfaces/Funcionario";
import axios from "axios";
import FuncionarioNav from "../nav/FuncionarioNav";
import { useParams } from "react-router-dom";

function EditarFuncionario(){

    const [resposta, setResposta] = useState("");
    const [respostaClasse, setRespostaClasse] = useState("");
    const [funcionario , setFuncionario] = useState<Funcionario>()

    const [ nome, setNome] = useState<string>('');
    const [ cargo, setCargo ] = useState<string>('');
    const [ dataNascimento, setDataNascimento ] = useState<string>("");
    const [ cpf, setCpf  ] = useState<string>('');
    const [ sexo, setSexo  ] = useState<'M' | 'F'>('M');

    const { id } = useParams()

    const [resposta2, setResposta2] = useState("");

    useEffect(() => {
    
        fetch("http://localhost:5291/api/funcionario/buscar/id:" + id)
        .then(resposta => {
            return resposta.json()
          })
          .then( funcionario => {
          setFuncionario(funcionario)
          setCpf(funcionario.cpf)
          setNome(funcionario.nome)
          setCargo(funcionario.cargo)
          setDataNascimento(funcionario.dataNascimento)
          setSexo(funcionario.sexo)
          }
        )
        .catch((error) => {
            console.log(error)

        })
    
      }, [])

      function handleSubmit(e: any){
        e.preventDefault();
        
        if(cpf.length != 11){
          return setResposta2("O cpf deve conter 11 dígitos")
        }else{

          console.log(dataNascimento)
        const funcionarioAlterado : Funcionario = {
          nome: nome,
          cargo: cargo,
          dataNascimento: dataNascimento,
          cpf: cpf,
          sexo: sexo
        }

        axios.put("http://localhost:5291/api/funcionario/alterar/" + funcionario?.funcionarioId, funcionarioAlterado)
        .then(resposta => alert("Funcionario Editado Com sucesso!"))
      }
      }
    return(
      <div className="main-content">
            <FuncionarioNav/>
        <div id="form">
            <h1>Alterar Funcionario</h1>
            {funcionario &&
                <div>
                  <form onSubmit={handleSubmit}>
                    <label htmlFor="nome">Nome:
                      <input type="text" value={nome} onChange={e => setNome(e.target.value)} required/>
                    </label>
                    <label htmlFor="cargo">Cargo:
                      <input type="text" value={cargo} onChange={e => setCargo(e.target.value)} required/>
                    </label>
                    <label htmlFor="dataNascimento">Data de Nascimento:
                    <input 
                        type="date" 
                        value={dataNascimento.split('T')[0]} 
                        onChange={e => setDataNascimento(e.target.value)} 
                        required/>
                    </label>
                    <label htmlFor="cpf">CPF:
                      <input type="text" value={cpf} onChange={x => setCpf(x.target.value)} required/>
                      <div>{resposta2}</div>
                    </label>
                    <label htmlFor="sexo">Sexo:
                      <select value={sexo} onChange={e => setSexo(e.target.value as 'M' | 'F')} required>
                        <option value="M">Masculino</option>
                        <option value="F">Feminino</option>
                      </select>
                    </label>
                      <button type="submit">Atualizar</button>
                  </form>
                </div>
            }
        </div>
      </div>
    )
}

export default EditarFuncionario;