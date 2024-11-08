import { useState } from "react";
import "../../../css/CadastrarAtividade.css"

function CadastrarAtividade() {
  const [detentoId, setDetentoId] = useState<string>("");
  const [nomeAtividade, setNomeAtividade] = useState< "trabalho" | "estudo" | "leitura" | "todos">("estudo");
  const [resposta, setResposta] = useState("");
  const [respostaClasse, setRespostaClasse] = useState("");

  function encontrarDentento(e : any){
    
    fetch("http://localhost:5291/api/detento/buscar/cpf:" + e.target.value).then(resposta => {
      return resposta.json()
    }).then(detento => {
      if(detento == null){
        setRespostaClasse("resposta-erro")
        return setResposta("detento não encontrado")
      }else{
        setDetentoId(detento.detentoId)
        setRespostaClasse("resposta-sucesso")
        return setResposta("detento encontrado")
      }
    }).catch(() => {
      setRespostaClasse("resposta-erro")
      setResposta("detento não encontrado")
    });
  }

  function handleSubmit(e : any){
    e.preventDefault();
    console.log(detentoId, nomeAtividade)
    fetch("http://localhost:5291/api/atividade/detento/cadastrar/"+ detentoId + "/" + nomeAtividade,
      {
        method: 'POST',
      }).then((response) => {
        if(response.ok){
          alert("Atividade Adicionada com Sucesso!")
        }else{
          alert("Impossível adicionar atividade!")
        }
      });
    
  }

  return(
    <div id="form_cadastro_atividade">
      <h1>Cadastrar Atividade a Detento</h1>
      <form onSubmit={handleSubmit}>
          <div>
            <label htmlFor="detentoId">CPF do Detento</label>
            <input type="text" onChange={encontrarDentento} required/>
            <div className={respostaClasse}>{resposta}</div>
          </div>

          <div>
            <label htmlFor="nomeAtividade">Nome da Atividade</label>
            <select name="SelecionarAtividade" id="SelecionarAtividade" onChange={(e : any) => setNomeAtividade(e.target.value as "estudo" | "leitura" | "trabalho" | "todos")} required>
              <option value="estudo">Estudo</option>
              <option value="leitura">Leitura</option>
              <option value="trabalho">Trabalho</option>
              <option value="todos">Todos</option>
            </select> 
          </div>

          <button type="submit">Cadastrar</button>
        </form>
      </div>
    
  );
}

export default CadastrarAtividade;