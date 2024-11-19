import BuscarFuncionario from "../funcionario/BuscarFuncionario";
import EditarFuncionario from "../funcionario/EditarFuncionario";
import ListarFuncionarios from "../funcionario/ListarFuncionarios";

function FuncionarioNav(){
    return(
        <div>
            <BuscarFuncionario/>
            <EditarFuncionario/>
            <ListarFuncionarios/>
            
        </div>
    )
}
export default FuncionarioNav;