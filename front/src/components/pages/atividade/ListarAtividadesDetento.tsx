import { useState } from "react";
import { Atividade } from "../../../interfaces/Atividade";
import "../../../css/ListarAtividadesDetento.css"
import { Leitura } from "../../../interfaces/atividades/Leitura";
function ListarAtividadesDetento(){
  const [respostaClasse, setRespostaClasse] = useState("")
  const [resposta, setResposta] = useState("")
  const [detentoId, setDetentoId] = useState("")

  const [atividades, setAtividades] = useState<Atividade[]>([])

  function EncontrarDentento(e : any){
    
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

  function BuscarAtividade(){
    fetch("http://localhost:5291/api/atividade/listar/detento/"+ detentoId)
    .then(resposta => resposta.json())
    .then(atividades => setAtividades(atividades))
    .catch(() => {
      setRespostaClasse("resposta-erro")
      setResposta("detento não encontrado")
    });
  }

  return(
    <div id="form_listar_atividades_detento">
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
            {atividades.map(atividade => (
              <tr key={atividade.atividadeId}>
                <td>{atividade.atividadeId}</td>
                <td>{atividade.tipo}</td>
                <td>{atividade.contador}</td>
                <td>{atividade.detentoId}</td>
                <td>{(atividade as Leitura).limite || 'N/A'}</td>
                <td>{(atividade as Leitura).anoAtual || 'N/A'}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default ListarAtividadesDetento;