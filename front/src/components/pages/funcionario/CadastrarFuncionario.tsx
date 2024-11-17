import { useState } from "react";
import "../../../css/funcionario/CadastrarFuncionario.css"
import { Funcionario } from "../../../interfaces/Funcionario";
import axios from "axios";
function CadastrarFuncionario(){
  
  const [ nome, setNome] = useState<string>('');
  const [ cargo, setCargo ] = useState<string>('');
  const [ dataNascimento, setDataNascimento ] = useState<string>('');
  const [ cpf, setCpf  ] = useState<string>('');
  const [ sexo, setSexo  ] = useState<'M' | 'F'>('M');
  const [resposta, setResposta] = useState("");
  const [respostaClasse, setRespostaClasse] = useState("");

  function encontrarFuncionario(){
    
    if(cpf.length != 11){
      setResposta("O cpf deve conter 11 dígitos")
      return setRespostaClasse("")
    }
    fetch("http://localhost:5291/api/funcionario/buscar/cpf:" + cpf)
    .then(resposta => {
      if(resposta.ok){
        setResposta("Funcionário com mesmo CPF encontrado no Sistema!")
        setRespostaClasse("resposta-erro")
        return true
      }else{
        setResposta("CPF disponível")
        setRespostaClasse("resposta-sucesso")

        return false
      }
    }).catch((error) => {
        console.log(error)
        return true
    })

    return true
  }

  function handleSubmit(e : any){
    e.preventDefault();
   

    if(encontrarFuncionario()){
      return alert("Funcionário com mesmo CPF encontrado, impossível registrar")
    }

    const funcionario : Funcionario = {
      nome: nome,
      cargo: cargo,
      dataNascimento: dataNascimento,
      cpf: cpf,
      sexo: sexo
    }

    axios.post("http://localhost:5291/api/funcionario/cadastrar", funcionario)
    .then(() => alert("Funcionário registrado com sucesso!"))

  }

  return(
    <div id="form_cadastro_funcionario">
      <h1>Cadastrar Funcionarios</h1>
      <form onSubmit={handleSubmit}>
        <label htmlFor="nome">Nome:
          <input type="text" value={nome} onChange={e => setNome(e.target.value)} required/>
        </label>
        <label htmlFor="cargo">Cargo:
          <input type="text" value={cargo} onChange={e => setCargo(e.target.value)} required/>
        </label>
        <label htmlFor="dataNascimento">Data de Nascimento:
          <input type="date" value={dataNascimento} onChange={e => {setDataNascimento(e.target.value)}} required/>
        </label>
        <label htmlFor="cpf">CPF:
          <input type="text" value={cpf} onChange={x => setCpf(x.target.value)} onBlur={encontrarFuncionario} required/>
          <div className={respostaClasse}>{resposta}</div>
        </label>
        <label htmlFor="sexo">Sexo:
          <select value={sexo} onChange={e => setSexo(e.target.value as 'M' | 'F')} required>
            <option value="M">Masculino</option>
            <option value="F">Feminino</option>
          </select>
        </label>
          <button type="submit">Cadastrar</button>
      </form>
    </div>
  )
}
export default CadastrarFuncionario;