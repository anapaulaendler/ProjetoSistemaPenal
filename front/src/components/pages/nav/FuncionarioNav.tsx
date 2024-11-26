import { Link } from "react-router-dom";

function FuncionarioNav(){
    return(
           <div className="sidebar">
             <nav>
               <ul>
                <li>
                   <Link to="/operacoesFuncionario/cadastrar">Cadastrar Funcionário</Link>
                </li>
                 <li>
                   <Link to="/operacoesFuncionario/buscar">Buscar Funcionário</Link>
                 </li>
                 <li>
                   <Link to="/operacoesFuncionario/listar">Listar Funcionários</Link>
                 </li>
               </ul>
             </nav>
           </div>
    )
}
export default FuncionarioNav;