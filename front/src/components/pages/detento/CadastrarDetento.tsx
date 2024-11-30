import React from "react";
import { useEffect, useState } from "react";
import { Atividade } from "../../../interfaces/Atividade";
import { Detento } from "../../../interfaces/Detento";
import DetentoNav from "../nav/DetentoNav";
import axios from "axios";

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
            nome: nome,
            dataNascimento: dataNascimento,
            inicioPena: inicioPena,
            fimPena: fimPena,
            atividades: [],
            cpf: formatCPF(cpf),
            sexo: sexo
        };

        axios.post("http://localhost:5291/api/detento/cadastrar", novoDetento)
        .then(() => {
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
    function formatCPF(cpf: string): string {
      // Remove qualquer caractere que não seja um número
      const numericCPF = cpf.replace(/\D/g, "");
      
      // Verifica se o CPF tem 11 dígitos
      if (numericCPF.length !== 11) {
          throw new Error("CPF inválido. Certifique-se de que ele contém 11 dígitos.");
      }
      
      // Aplica a formatação XXX.XXX.XXX-XX
      return numericCPF.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4");
  }
  return (
  <div className="main-content">
    <DetentoNav/>
    <div id="form">
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
  </div>
  );
}

export default CadastrarDetento;
