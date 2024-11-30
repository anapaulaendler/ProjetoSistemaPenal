import { useEffect, useState } from "react";
import { Atividade } from "../../../interfaces/Atividade";
import { Detento } from "../../../interfaces/Detento";
import { useParams } from "react-router-dom";
import axios from "axios";
import DetentoNav from "../nav/DetentoNav";

function ListarAtividadesDetento(){
  const [respostaClasse, setRespostaClasse] = useState("")
  const [resposta, setResposta] = useState("")

  const [atividades, setAtividades] = useState<Atividade[]>([])
  const [detento, setDetento] = useState<Detento>();

  const { id } = useParams();
  
  useEffect(() => {
      if(id){
        fetch("http://localhost:5291/api/detento/buscar/id:" + id)
        .then((resposta) => {
            return resposta.json();
        })
        .then((detento) => {
          if (detento) {
            setDetento(detento);
            setAtividades(detento.atividades);
          }
        })
        .catch((error) => {
          console.error("Erro ao buscar detento:", error);
        });
    }
  })

  function alterar(idAtividade: string) {

    console.log("ID enviado para alteração:", idAtividade);

    axios
      .put(`http://localhost:5291/api/atividade/alterar/${idAtividade}`)
      .then((resposta) => {
        console.log(resposta.data);
      });
  }

  return(
    <div className="main-content">
        <DetentoNav/>
      <div>
          <h1>Listar de atividades</h1>
              <div>
                  <table>
                  <thead>
                      <th>ID</th>
                      <th>Nome da Atividade</th>
                      <th>Contador</th>
                      <th>DetentoId</th>
                      <th>Limite</th>
                      <th>Ano Atual</th>
                      <th>Atualizar</th>
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
                          <td>
                            <button onClick={() => alterar(atividade.atividadeId!)}>
                              Atualizar
                            </button>
                          </td>
                      </tr>
                      ))}
                  </tbody>
                  </table>
              </div>
          </div>
        </div>
  );
}

export default ListarAtividadesDetento;