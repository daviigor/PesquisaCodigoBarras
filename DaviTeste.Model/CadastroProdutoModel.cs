using System;
using System.Collections.Generic;
using System.Text;

namespace DaviTeste.Model
{
    public class CadastroProdutoModel
    {
        public int Id { get; set; }
        public String CodigoBarras { get; set; }
        public string Nome { get; set; }
        public int? Altura { get; set; }
        public int? Largura { get; set; }
        public int? Comprimento { get; set; }
        public decimal? PesoBruto { get; set; }
        public decimal? PesoLiquido { get; set; }
        public decimal? PrecoMedio { get; set; }
        public decimal? PrecoMinimo { get; set; }
        public decimal? PrecoMaximo { get; set; }
        public List<CadastroUnidadeModel> Unidades { get; set; }
        public String ImagemURL { get; set; }
        public String Marca { get; set; }
        public String ImagemURLMarca { get; set; }
        public String ImagemURLCodigoBarras { get; set; }
        public NCMModel NCM { get; set; }
    }

    public class NCMModel
    {
        public String Codigo { get; set; }
        public String Descricao { get; set; }
        public String DescricaoCompleta { get; set; }
    }

    public class CadastroUnidadeModel
    {
        public String CodigoBarras { get; set; }
        public String Embalagem { get; set; }
        public int? QtdeEmbalagem { get; set; }
        public String Lastro { get; set; }
        public String Camada { get; set; }

    }



}
