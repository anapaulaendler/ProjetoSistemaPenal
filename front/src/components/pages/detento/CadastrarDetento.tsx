import React from "react";
import { useEffect, useState } from "react";
import { Atividade } from "../../../interfaces/Atividade";
import { Detento } from "../../../interfaces/Detento";
import "../../../css/detento/Detento.css"

function CadastrarDetento() {

    const [nome, setNome] = useState<string>('');
    const [dataNascimento, setDataNascimento] = useState<string>('');
    const [cpf, setCpf] = useState<string>('');
    const [sexo, setSexo] = useState<'M' | 'F'>('M');
    const [inicioPena, setInicioPena] = useState<string>('');
    const [fimPena, setFimPena] = useState<string>('');
    const [atividades, setAtividades] = useState<Atividade[]>([]);

    function handleSubmit (e: any) {
        e.preventDefault();

        const novoDetento : Detento = {
            nome,
            dataNascimento,
            inicioPena: "",
            fimPena: "",
            atividades: [],
            cpf: "",
            sexo: "M"
        };

        fetch("http://localhost:5291/api/detento/cadastrar", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(novoDetento)
        })

        .then(response => {
            if (!response.ok) {
                throw new Error('Erro na requisição: ' + response.statusText);
            }
            return response.json();
        })

        .then(data => {
            setNome('');
            setDataNascimento('');
            setCpf('');
            setSexo('M'); 
            setInicioPena('');
            setFimPena('');
            setAtividades([]);
        })

        .catch(error => {
            console.error('Erro:', error);
        });

    };

  return (
  <div id="form_cadastro_detento">
    <h1>Cadastro de Detento</h1>

    <form onSubmit={handleSubmit}>
      <div>
        <label>Nome:</label>
          <input type="text" value={nome} onChange={e => setNome(e.target.value)} required />
      </div>
      
      <div>
        <label>Data de Nascimento:</label>
          <input type="date" value={dataNascimento} onChange={e => setDataNascimento(e.target.value)} required />
      </div>

      <div>
        <label>CPF:</label>
          <input type="text" value={cpf} onChange={e => setCpf(e.target.value)} required />
      </div>
      
      <div>
        <label>Sexo:</label>
          <select value={sexo} onChange={e => setSexo(e.target.value as 'M' | 'F')} required>
              <option value="M">Masculino</option>
              <option value="F">Feminino</option>
          </select>
      </div>

      <div>
        <label>Início da Pena:</label>
          <input type="date" value={inicioPena} onChange={e => setInicioPena(e.target.value)} required />
      </div>
      
      <div>
        <label>Fim da Pena:</label>
          <input type="date" value={fimPena} onChange={e => setFimPena(e.target.value)} required />
      </div>

      <button type="submit">Cadastrar</button>
    </form>
  </div>

  );
}

export default CadastrarDetento;
