import { useEffect, useState } from "react";
import { Atividade } from "../../../interfaces/Atividade";
import { Detento } from "../../../interfaces/Detento";
import { useParams } from "react-router-dom";

function ListarAtividadesDetento(){
  const [respostaClasse, setRespostaClasse] = useState("")
  const [resposta, setResposta] = useState("")

  const [atividades, setAtividades] = useState<Atividade[]>([])
  const [detento, setDetento] = useState<Detento>();

  const { id } = useParams();
  
  useEffect(() => {
    function EncontrarDentento(e : any){
      if(id){
        fetch("http://localhost:5291/api/detento/buscar/id:" + id)
        .then((resposta) => {
            return resposta.json();
        })
        .then((detento) => {
          if (detento) {
            setAtividades(detento.atividades);
          }
        })
        .catch((error) => {
          console.error("Erro ao buscar detento:", error);
        });
      }
    }
  
    function BuscarAtividade(){
      console.log(detento?.atividades)
      setAtividades(detento?.atividades || [])
    }
  })

  return(
    <div id="form">
      <h1>Listar atividades de Detento</h1>
      <div>
        {/* <input type="text" placeholder="CPF do Detento" onChange={EncontrarDentento}/>
        <button onClick={BuscarAtividade}>Buscar</button> */}
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