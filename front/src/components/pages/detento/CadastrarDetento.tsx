import React from "react";
import { useEffect, useState } from "react";
import { Atividade } from "../../../interfaces/Atividade";
import { Detento } from "../../../interfaces/Detento";

function CadastrarDetento() {

    const [nome, setNome] = useState<string>('');
    const [dataNascimento, setDataNascimento] = useState<string>('');
    const [cpf, setCpf] = useState<string>('');
    const [sexo, setSexo] = useState<'M' | 'F'>('M');
    const [tempoPenaInicial, setTempoPenaInicial] = useState<number>(0);
    const [penaRestante, setPenaRestante] = useState<number>(0);
    const [inicioPena, setInicioPena] = useState<string>('');
    const [fimPena, setFimPena] = useState<string>('');
    const [atividades, setAtividades] = useState<Atividade[]>([]);

    function handleSubmit (e: any) {
        e.preventDefault();

        const novoDetento : Detento = {
            nome,
            dataNascimento,
            tempoPenaInicial: 0,
            penaRestante: 0,
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
            setTempoPenaInicial(0);
            setPenaRestante(0);
            setInicioPena('');
            setFimPena('');
            setAtividades([]);
        })

        .catch(error => {
            console.error('Erro:', error);
        });

    };

  return (
    <div>
  <h1>Cadastro de Detento</h1>

  <form onSubmit={handleSubmit}>
    <label>
      Nome:
      <input type="text" value={nome} onChange={e => setNome(e.target.value)} required />
    </label>
    
    <label>
      Data de Nascimento:
      <input type="date" value={dataNascimento} onChange={e => setDataNascimento(e.target.value)} required />
    </label>
    
    <label>
      CPF:
      <input type="text" value={cpf} onChange={e => setCpf(e.target.value)} required />
    </label>
    
    <label>
      Sexo:
      <select value={sexo} onChange={e => setSexo(e.target.value as 'M' | 'F')} required>
        <option value="M">Masculino</option>
        <option value="F">Feminino</option>
      </select>
    </label>
    
    <label>
      Tempo de Pena Inicial (anos):
      <input type="number" value={tempoPenaInicial} onChange={e => setTempoPenaInicial(Number(e.target.value))} required />
    </label>
    
    <label>
      Pena Restante (anos):
      <input type="number" value={penaRestante} onChange={e => setPenaRestante(Number(e.target.value))} required />
    </label>
    
    <label>
      Início da Pena:
      <input type="date" value={inicioPena} onChange={e => setInicioPena(e.target.value)} required />
    </label>
    
    <label>
      Fim da Pena:
      <input type="date" value={fimPena} onChange={e => setFimPena(e.target.value)} required />
    </label>
    
    <button type="submit">Cadastrar</button>
  </form>
</div>

  );
}

export default CadastrarDetento;
