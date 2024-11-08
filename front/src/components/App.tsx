import React from "react";
import CadastrarDetento from "./pages/detento/CadastrarDetento";
import ListarDetentos from "./pages/detento/ListarDetentos";
import BuscarDetento from "./pages/detento/BuscarDetento";
import CadastrarAtividade from "./pages/atividade/CadastrarAtividade";

function App() {
  return (
    <div>
      <CadastrarAtividade/>
      <CadastrarDetento/>
      <ListarDetentos/>
    </div>
  );
}

export default App;
