import { useState } from "react";
import { Atividade } from "../../../interfaces/Atividade";
import { Detento } from "../../../interfaces/Detento";

function ListarAtividadesDetento(){
  const [respostaClasse, setRespostaClasse] = useState("")
  const [resposta, setResposta] = useState("")

  const [atividades, setAtividades] = useState<Atividade[]>([])
  const [detento, setDetento] = useState<Detento>();

  function EncontrarDentento(e : any){
    
    fetch("http://localhost:5291/api/detento/buscar/cpf:" + e.target.value)
    .then(resposta => {
      return resposta.json()
    })
    .then(detento => {
      if(detento == null){
        setRespostaClasse("resposta-erro")
        return setResposta("detento não encontrado")
      }else{
        setRespostaClasse("resposta-sucesso")
        setResposta("detento encontrado")
        return setDetento(detento);
      }
    })
    .catch(() => {
      setRespostaClasse("resposta-erro")
      setResposta("detento não encontrado")
    });
  }

  function BuscarAtividade(){
    console.log(detento?.atividades)
    setAtividades(detento?.atividades || [])
  }

  return(
    <div id="form">
      <h1>Listar atividades de Detento</h1>
      <div>
        <input type="text" placeholder="CPF do Detento" onChange={EncontrarDentento}/>
        <button onClick={BuscarAtividade}>Buscar</button>
        <div className={respostaClasse}>{resposta}</div>
      </div>
      <div>
        <table>
          <thead>
            <th>ID</th>
            <th>Nome da Atividade</th>
            <th>Contador</th>
            <th>DetentoId</th>
            <th>Limite</th>
            <th>Ano Atual</th>
          </thead>
          <tbody>
            {atividades && 
            atividades.map(atividade => (
              <tr key={atividade.atividadeId}>
                <td>{atividade.atividadeId}</td>
                <td>{atividade.tipo}</td>
                <td>{atividade.contador}</td>
                <td>{atividade.detentoId}</td>
                <td>{atividade.limite || 'N/A'}</td>
                <td>{atividade.anoAtual || 'N/A'}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default ListarAtividadesDetento;