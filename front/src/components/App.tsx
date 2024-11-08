import React from "react";
import CadastrarDetento from "./pages/detento/CadastrarDetento";
import ListarDetentos from "./pages/detento/ListarDetentos";
import BuscarDetento from "./pages/detento/BuscarDetento";
import CadastrarAtividade from "./pages/atividade/CadastrarAtividade";
import EditarDetento from "./pages/detento/EditarDetento";

function App() {
  return (
    <div>
      <CadastrarAtividade/>
      <CadastrarDetento/>
      <ListarDetentos/>
      <EditarDetento/>
    </div>
  );
}

export default App;
