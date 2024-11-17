import React from "react";
import CadastrarDetento from "./pages/detento/CadastrarDetento";
import ListarDetentos from "./pages/detento/ListarDetentos";
import BuscarDetento from "./pages/detento/BuscarDetento";
import CadastrarAtividade from "./pages/atividade/CadastrarAtividade";
import EditarDetento from "./pages/detento/EditarDetento";
import ListarAtividadesDetento from "./pages/atividade/ListarAtividadesDetento";
import BuscarDetentoCPF from "./pages/detento/BuscarDetentoCPF";
import CadastrarFuncionario from "./pages/funcionario/CadastrarFuncionario";
import EditarFuncionario from "./pages/funcionario/EditarFuncionario";
import ListarFuncionarios from "./pages/funcionario/ListarFuncionarios";
import BuscarFuncionario from "./pages/funcionario/BuscarFuncionario";

function App() {
  return (
    <div>
        <CadastrarFuncionario></CadastrarFuncionario>
        <EditarFuncionario/>
        <ListarFuncionarios></ListarFuncionarios>
        <BuscarFuncionario></BuscarFuncionario>
    </div>
  );
}

export default App;
