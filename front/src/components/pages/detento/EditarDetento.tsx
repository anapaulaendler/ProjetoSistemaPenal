import React, { useEffect, useState } from "react";
import { Detento } from "../../../interfaces/Detento";
import DetentoNav from "../nav/DetentoNav";
import { useParams } from "react-router-dom";
function EditarDetento() {

  const [detento, setDetento] = useState<Detento>();

  const [nome, setNome] = useState<string>("");
  const [dataNascimento, setDataNascimento] = useState<string>("");
  const [cpf, setCpf] = useState<string>("");
  const [sexo, setSexo] = useState<"M" | "F">("M");
  const [inicioPena, setInicioPena] = useState<string>("");
  const [fimPena, setFimPena] = useState<string>("");
  const [detentoId, setDetentoId] = useState<string>("");
  
  const [resposta, setResposta] = useState<string>();
  const [respostaClasse, setRespostaClasse] = useState<string>();
  const [erro, setErro] = useState<string>();
  const [isLoading, setIsLoading] = useState(false);

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
        setCpf(detento.cpf);
        setDataNascimento(detento.dataNascimento.split("T")[0]);
        setFimPena(detento.fimPena);
        setInicioPena(detento.inicioPena);
        setNome(detento.nome);
        setSexo(detento.sexo);
        setDetentoId(detento.detentoId);
      }
    })
    .catch((error) => {
      console.error("Erro ao buscar detento:", error);
    });
  }
  },[])

  function handleSubmit(e: any) {
    e.preventDefault();
    setIsLoading(true);

    const detentoAlterado: Detento = {
      nome,
      dataNascimento: new Date(dataNascimento).toISOString().split("T")[0],
      sexo,
      inicioPena,
      fimPena,
      cpf,
      detentoId,
    };

    fetch("http://localhost:5291/api/detento/alterar/" + detentoId, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(detentoAlterado),
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error("Erro na requisição: " + response.statusText);
        }
        return response.json();
      })
      .then(() => {
        setRespostaClasse("resposta-sucesso");
        setResposta("Detento atualizado com sucesso");
      })
      .catch((error) => {
        console.error("Erro:", error);
        setRespostaClasse("resposta-erro");
        setResposta("Erro ao atualizar detento");
      })
      .finally(() => {
        setIsLoading(false);
      });
  }

  return (
    <div className="main-content">
    <DetentoNav/>
      <div id="form">
        <h1>Alterar Detento</h1>
        <form onSubmit={handleSubmit}>
          {detento && (
            <>
              <div>
                <label htmlFor="nome">Nome:</label>
                <input
                  type="text"
                  value={nome}
                  onChange={(e) => setNome(e.target.value)}
                  required
                />
              </div>
              <div>
                <label htmlFor="dataNascimento">Data de Nascimento:</label>
                <input
                  type="date"
                  value={dataNascimento.split("T")[0]}
                  onChange={(e) => setDataNascimento(e.target.value)}
                  required
                />
              </div>
              <div>
                <label htmlFor="cpf">CPF:</label>
                <input
                  type="text"
                  value={cpf}
                  onChange={(x) => setCpf(x.target.value)}
                  required
                />
              </div>
              <div>
                <label htmlFor="sexo">Sexo:</label>
                <select
                  value={sexo}
                  onChange={(e) => setSexo(e.target.value as "M" | "F")}
                  required
                >
                  <option value="M">Masculino</option>
                  <option value="F">Feminino</option>
                </select>
              </div>
              <button type="submit" disabled={isLoading}>
                {isLoading ? "Salvando..." : "Editar"}
              </button>
            </>
          )}
        </form>
      </div>
    </div>
  );
}

export default EditarDetento;