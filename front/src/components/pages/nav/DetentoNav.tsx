import { Link } from "react-router-dom";

function DetentoNav(){
    return(
           <div className="sidebar">
             <nav>
               <ul>
                <li>
                   <Link to="/operacoesDetento/cadastrar">Cadastrar Detento</Link>
                </li>
                <li>
                   <Link to="/operacoesDetento/cadastrar/atividade">Cadastrar Atividade em Detento</Link>
                </li>
                 <li>
                   <Link to="/operacoesDetento/buscar">Buscar Detento</Link>
                 </li>
                 <li>
                   <Link to="/operacoesDetento/listar">Listar Detentos</Link>
                 </li>
                 <li>
                   <Link to="/operacoesDetento/listar/inativos">Listar Detentos Inativos</Link>
                 </li>
               </ul>
             </nav>
           </div>
    )
}
export default DetentoNav;