import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Link } from "react-router-dom";

import ListarDetentos from "./pages/detento/ListarDetentos";
import EditarDetento from "./pages/detento/EditarDetento";
import CadastrarAtividade from "./pages/atividade/CadastrarAtividade";
import BuscarDetentoCPF from "./pages/detento/BuscarDetentoCPF";
import CadastrarDetento from "./pages/detento/CadastrarDetento";
import ListarFuncionarios from "./pages/funcionario/ListarFuncionarios";
import CadastrarFuncionario from "./pages/funcionario/CadastrarFuncionario";
import BuscarFuncionario from "./pages/funcionario/BuscarFuncionario";
import EditarFuncionario from "./pages/funcionario/EditarFuncionario";
import ListarDetentosInativos from "./pages/detento/ListarDetentosInativos";
import ListarAtividadesDetento from "./pages/atividade/ListarAtividadesDetento";
function App() {
  return  (
    <BrowserRouter>
      <div className="App">
        <nav className="barra-superior">
          <ul>
            <li>
              <Link to="/operacoesDetento">Operações Detento</Link>
            </li>
            <li>
              <Link to="/operacoesFuncionario">Operações Funcionário</Link>
            </li>
          </ul>
        </nav>
        <Routes>
          <Route path="/operacoesDetento" element={<ListarDetentos />} />
          <Route path="/operacoesFuncionario" element={<ListarFuncionarios />} />
          {/* DETENTO */}
          <Route path="/operacoesDetento/cadastrar" element={<CadastrarDetento />} />
          <Route path="/operacoesDetento/cadastrar/atividade" element={<CadastrarAtividade />} />
          <Route path="/operacoesDetento/buscar" element={<BuscarDetentoCPF />} />
          <Route path="/operacoesDetento/listar" element={<ListarDetentos />} />
          <Route path="/operacoesDetento/editar/:id" element={<EditarDetento />} />
          <Route path="/operacoesDetento/listar/inativos" element={<ListarDetentosInativos />} />
          <Route path="/operacoesDetento/listar/atividades/:id" element={<ListarAtividadesDetento />} />
          {/* FUNCIONARIO */}
          <Route path="/operacoesFuncionario/cadastrar" element={<CadastrarFuncionario />} />
          <Route path="/operacoesFuncionario/buscar" element={<BuscarFuncionario />} />
          <Route path="/operacoesFuncionario/listar" element={<ListarFuncionarios />} />
          <Route path="/operacoesFuncionario/editar/:id" element={<EditarFuncionario />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;