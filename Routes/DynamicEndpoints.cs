using modulum.Shared.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modulum.Client.Infrastructure.Routes
{
    public static class DynamicEndpoints
    {
        public static string CadastroDynamic = EndpointsDynamic.Raiz;
        public static string GetMenu = EndpointsDynamic.Raiz + EndpointsDynamic.Menu;
        public static string GetNewObject = EndpointsDynamic.Raiz + EndpointsDynamic.GetNewObjeto;
        public static string GetMapTable = EndpointsDynamic.Raiz + EndpointsDynamic.GetMapTable;
        public static string SelectDynamicById = EndpointsDynamic.Raiz + EndpointsDynamic.SelectDynamicById;
        public static string DeleteDynamicById = EndpointsDynamic.Raiz + EndpointsDynamic.DeleteDynamicById;
        public static string AlterMapTable = EndpointsDynamic.Raiz + EndpointsDynamic.AlterMapTable;
        public static string DeleteMapTable = EndpointsDynamic.Raiz + EndpointsDynamic.DeleteMapTable;
        public static string RenameNomeTabelaTela = EndpointsDynamic.Raiz + EndpointsDynamic.RenameNomeTabelaTela;
        public static string AlterRelacionamento = EndpointsDynamic.Raiz + EndpointsDynamic.AlterRelacionamento;
        public static string ConsultarRelacionamento = EndpointsDynamic.Raiz + EndpointsDynamic.ConsultarRelacionamento;
    }
}
