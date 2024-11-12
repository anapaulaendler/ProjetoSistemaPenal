import React from "react";
import { useEffect, useState } from "react";
import { Atividade } from "../../../interfaces/Atividade";
import { Detento } from "../../../interfaces/Detento";

// 65725312334

function EditarDetento() {

  const [detento, setDetento] = useState<Detento>();
  const [id, setId] = useState<string>();
  const [erro, setErro] = useState<string>();
  const [valorForm, setValorForm] = useState({
    nome: '',
    dataNascimento: '',
    cpf: '',
    sexo: 'M' as 'M' | 'F',
    tempoPenaInicial: 0,
    penaRestante: 0,
    inicioPena: '',
    fimPena: '',
  });
  const [resposta, setResposta] = useState("");
  const [respostaClasse, setRespostaClasse] = useState("");
  const [nome, setNome] = useState<string>('');
  const [dataNascimento, setDataNascimento] = useState<string>('');
  const [cpf, setCpf] = useState<string>('');
  const [sexo, setSexo] = useState<'M' | 'F'>('M');
  const [tempoPenaInicial, setTempoPenaInicial] = useState<number>(0);
  const [penaRestante, setPenaRestante] = useState<number>(0);
  const [inicioPena, setInicioPena] = useState<string>('');
  const [fimPena, setFimPena] = useState<string>('');
  const [detentoId, setDetentoId] = useState<string>("");

  function encontrarDentento(e : any){
    
    fetch("http://localhost:5291/api/detento/buscar/cpf:" + e.target.value).then(resposta => {
      return resposta.json()
    }).then(detento => {
      if(detento == null){
        setRespostaClasse("resposta-erro")
        setResposta("detento não encontrado")
      } else {
        setDetento(detento)
        setDetentoId(detento.detentoId)
        setRespostaClasse("resposta-sucesso")
        setResposta("detento encontrado")
        setValorForm({
            nome: detento.nome,
            dataNascimento: detento.dataNascimento,
            cpf: detento.cpf,
            sexo: detento.sexo,
            tempoPenaInicial: detento.tempoPenaInicial,
            penaRestante: detento.penaRestante,
            inicioPena: detento.inicioPena,
            fimPena: detento.fimPena,
          });
        
      }
    }).catch(() => {
      setRespostaClasse("resposta-erro")
      setResposta("detento não encontrado")
    });
  }

  function handleSubmit(e : any) {
    e.preventDefault();

    fetch("http://localhost:5291/api/detento/alterar/" + detentoId, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(valorForm)
    })

    .then(response => {
        if (!response.ok) {
            throw new Error('Erro na requisição: ' + response.statusText);
        }
        return response.json();
    })

    .then(() => {
        setRespostaClasse("resposta-sucesso");
        setResposta("Detento atualizado com sucesso");
        setValorForm({
          nome: '',
          dataNascimento: '',
          cpf: '',
          sexo: 'M',
          tempoPenaInicial: 0,
          penaRestante: 0,
          inicioPena: '',
          fimPena: '',
        });
      })

      .catch((error) => {
        console.error('Erro:', error);
        setRespostaClasse("resposta-erro");
        setResposta("Erro ao atualizar detento");
      });
  }

  function handleChange(e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) {
    const { name, value } = e.target;
    setValorForm((prev) => ({
        ...prev,
        // o prev são as infos antigas, que estão guardadas
        [name]: value,
        // o name é o nome da var e o value é o valor digitado
    }));

        // o handleChange fica esperando mudanças pra atualizar :>
  }

return (
    <div id="form_cadastro_atividade">
      <h1>Alterar Detento</h1>
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="detentoId">CPF do Detento</label>
          <input type="text" onChange={encontrarDentento} required />
          <div className={respostaClasse}>{resposta}</div>
        </div>

        {detento && (
            // verifica se detento não é falsy
          <>
            <div>
              <label htmlFor="nome">Nome</label>
              <input
                type="text"
                name="nome"
                value={valorForm.nome}
                onChange={handleChange}
                required
              />
            </div>
            <div>
              <label htmlFor="dataNascimento">Data de Nascimento</label>
              <input
                type="text"
                name="dataNascimento"
                value={valorForm.dataNascimento}
                onChange={handleChange}
                required
              />
            </div>
            <div>
              <label htmlFor="cpf">CPF</label>
              <input
                type="text"
                name="cpf"
                value={valorForm.cpf}
                onChange={handleChange}
                required
              />
            </div>
            <div>
            <label htmlFor="sexo">Sexo</label>
            <select
                name="sexo"
                value={valorForm.sexo}
                onChange={handleChange}
                required
                >
                <option value="M">Masculino</option>
                <option value="F">Feminino</option>
            </select>
            </div>
            <button type="submit">Editar</button>
          </>
        )}
      </form>
    </div>
);
}

export default EditarDetento;
