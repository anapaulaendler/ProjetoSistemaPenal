import { useState } from "react";
import "../../../css/funcionario/EditarFuncionario.css"
import { Funcionario } from "../../../interfaces/Funcionario";
import axios from "axios";

function EditarFuncionario(){

    const [resposta, setResposta] = useState("");
    const [respostaClasse, setRespostaClasse] = useState("");
    const [funcionario , setFuncionario] = useState<Funcionario>()

    const [ nome, setNome] = useState<string>('');
    const [ cargo, setCargo ] = useState<string>('');
    const [ dataNascimento, setDataNascimento ] = useState<string>("");
    const [ cpf, setCpf  ] = useState<string>('');
    const [ sexo, setSexo  ] = useState<'M' | 'F'>('M');

    const [resposta2, setResposta2] = useState("");

    function encontrarFuncionario(){
    
        if(cpf.length != 11){
          setResposta("O cpf deve conter 11 dígitos")
          return setRespostaClasse("")
        }
        fetch("http://localhost:5291/api/funcionario/buscar/cpf:" + cpf)
        .then(resposta => {
          if(resposta.ok){
            setResposta("Funcionário encontrado!")
            setRespostaClasse("resposta-sucesso")
            return resposta.json()
          }else{
            setResposta("Funcionário não encontrado!")
            return setRespostaClasse("resposta-erro")
          }
        }).then( funcionario => {
          setFuncionario(funcionario)
          setNome(funcionario.nome)
          setCargo(funcionario.cargo)
          setDataNascimento(funcionario.dataNascimento)
          setSexo(funcionario.sexo)
          }
        )
        .catch((error) => {
            console.log(error)

        })
    
      }

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
      }
      }
    return(
        <div id="form_alterar_funcionario">
            <h1>Alterar Funcionario</h1>
            <div>
              <form onSubmit={encontrarFuncionario}>
                <label htmlFor="cpf"> CPF do Funcionário:
                    <input type="text" onChange={e => setCpf(e.target.value)} required/>
                </label>
                <div className={respostaClasse}>{resposta}</div>
                <button  type="submit">Buscar</button>
              </form>
              
            </div>
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
    )
}

export default EditarFuncionario;