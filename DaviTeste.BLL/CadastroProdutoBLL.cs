using System;
using System.Collections.Generic;
using System.Text;
using DaviTeste.Utils.Utils;
using DaviTeste.Model;

namespace DaviTeste.BLL
{
    public class CadastroProdutoBLL
    {
        public Object BuscarDadosAPIProdutos(string codigoBarras)
        {
            //ClientRestHelper._CONFIG_CLIENT.END_POINT = "https://api.cosmos.bluesoft.com.br/gtins/7891991015493.json";
            var contentType = "application/json";

            ClientRestHelper._CONFIG_CLIENT = new ConfiguracaoClientRestModel();
            ClientRestHelper._CONFIG_CLIENT.CACHE_CONTROL = "no-cache";
            ClientRestHelper._CONFIG_CLIENT.CONTENT_TYPE = contentType;
            ClientRestHelper._CONFIG_CLIENT.END_POINT = getEndpoint(codigoBarras);
            ClientRestHelper._CONFIG_CLIENT.TOKEN = "5_R8es5PKjwSXvxK-ekDdA";
            ClientRestHelper._CONFIG_CLIENT.IS_LIST_DATA = false;

            String resultadoRequest = ClientRestHelper.GET();
            CadastroProdutoModel listaProdutos = RequestToObject(resultadoRequest);

            return null;
        }

        private CadastroProdutoModel RequestToObject(String resultadoRequest)
        {
            resultadoRequest = @"" + resultadoRequest;
            dynamic objCadProd = JsonHelper.DeserializeTeste<dynamic>(resultadoRequest);
            CadastroProdutoModel cadastroProduto = new CadastroProdutoModel();

            cadastroProduto.Nome = objCadProd.description;
            return cadastroProduto;
        }

        private string getEndpoint(string codigoBarras)
        {
            return "https://api.cosmos.bluesoft.com.br/gtins/" + codigoBarras.ToString() + ".json";
        }
    }
}
