import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Link } from "react-router-dom";

import "../css/nav/App.css"
import DetentoNav from "./pages/nav/DetentoNav";
import FuncionarioNav from "./pages/nav/FuncionarioNav";
function App() {
  return (
    <BrowserRouter>
      <div className="App">
        <nav>
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
          <Route path="/operacoesDetento" element={<DetentoNav/>}/>
          <Route path="/operacoesFuncionario" element={<FuncionarioNav/>}/>
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
