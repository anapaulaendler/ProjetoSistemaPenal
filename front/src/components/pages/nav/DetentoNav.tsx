import CadastrarAtividade from "../atividade/CadastrarAtividade";
import BuscarDetentoCPF from "../detento/BuscarDetentoCPF";
import EditarDetento from "../detento/EditarDetento";
import ListarDetentos from "../detento/ListarDetentos";

function DetentoNav(){
    return(
        <div>
            <BuscarDetentoCPF/>
            <CadastrarAtividade/>
            <EditarDetento/>
            <ListarDetentos/>
        </div>
    )
}
export default DetentoNav;