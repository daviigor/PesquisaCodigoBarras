using System;
using System.Collections.Generic;
using System.Text;
using DaviTeste.Utils.Utils;
using DaviTeste.Model;

namespace Davi.Teste.BLL
{
    public class CadastroProdutoBLL
    {
        public CadastroProdutoModel BuscarDadosAPIProdutos(string codigoBarras)
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
            CadastroProdutoModel produtoRetornoAPI = RequestToObject(resultadoRequest);

            return produtoRetornoAPI;
        }

        private CadastroProdutoModel RequestToObject(String resultadoRequest)
        {
            resultadoRequest = @"" + resultadoRequest;
            dynamic objCadProd = JsonHelper.DeserializeTeste<dynamic>(resultadoRequest);
            CadastroProdutoModel cadastroProduto = new CadastroProdutoModel();

            cadastroProduto.Nome = objCadProd.description;
            cadastroProduto.CodigoBarras = objCadProd.gtin;
            cadastroProduto.ImagemURL = objCadProd.thumbnail;
            cadastroProduto.Largura = objCadProd.width;
            cadastroProduto.Altura = objCadProd.height;
            cadastroProduto.Comprimento = objCadProd.length;
            cadastroProduto.PesoLiquido = objCadProd.net_weight;
            cadastroProduto.PesoBruto = objCadProd.gross_weight;
            cadastroProduto.PrecoMedio = objCadProd.avg_price;
            cadastroProduto.PrecoMaximo = objCadProd.max_price;
            cadastroProduto.PrecoMinimo = objCadProd.min_price;

            cadastroProduto.Unidades = new List<CadastroUnidadeModel>();
            
            foreach (dynamic gtin in objCadProd.gtins) {
                CadastroUnidadeModel cadastroUnid = new CadastroUnidadeModel();
                cadastroUnid.CodigoBarras = gtin.gtin;
                cadastroUnid.Embalagem = gtin.commercial_unit.type_packaging;
                cadastroUnid.QtdeEmbalagem = gtin.commercial_unit.quantity_packaging;
                cadastroUnid.Lastro = gtin.commercial_unit.ballast;
                cadastroUnid.Camada = gtin.commercial_unit.layer;

                cadastroProduto.Unidades.Add(cadastroUnid);
            }

            cadastroProduto.Marca = objCadProd.brand.name;
            cadastroProduto.ImagemURLMarca = objCadProd.brand.picture;

            cadastroProduto.ImagemURLCodigoBarras = objCadProd.barcode_image;
            cadastroProduto.NCM = new NCMModel();
            cadastroProduto.NCM.Codigo = objCadProd.ncm.code;
            cadastroProduto.NCM.Descricao = objCadProd.ncm.description;
            cadastroProduto.NCM.DescricaoCompleta = objCadProd.ncm.full_description;

            return cadastroProduto;
        }

        private string getEndpoint(string codigoBarras)
        {
            return "https://api.cosmos.bluesoft.com.br/gtins/" + codigoBarras.ToString() + ".json";
        }
    }
}
