import React from "react";
import CadastrarDetento from "./pages/detento/CadastrarDetento";
import ListarDetentos from "./pages/detento/ListarDetentos";
import BuscarDetento from "./pages/detento/BuscarDetento";
import CadastrarAtividade from "./pages/atividade/CadastrarAtividade";
import EditarDetento from "./pages/detento/EditarDetento";
import ListarAtividadesDetento from "./pages/atividade/ListarAtividadesDetento";
import BuscarDetentoCPF from "./pages/detento/BuscarDetentoCPF";

function App() {
  return (
    <div>
      <EditarDetento/>
      <BuscarDetentoCPF/>
    </div>
  );
}

export default App;
