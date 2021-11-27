using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Davi.Teste.BLL;
using DaviTeste.Model;

namespace DaviTeste.View
{
    public partial class frmCadastroProduto : Form
    {
        public frmCadastroProduto()
        {
            InitializeComponent();
        }

        private void frmCadastroProduto_Load(object sender, EventArgs e)
        {
            
        }

        private void btnBuscaExternaAPI_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (string.IsNullOrEmpty(txtBuscaCodBarrasAPI.Text))
            {
                exibirAlerta("Favor inserir um parâmetro para busca.");
            }

            var CadastroProdutoBLL = new CadastroProdutoBLL();
            CadastroProdutoModel dadosProduto = CadastroProdutoBLL.BuscarDadosAPIProdutos(txtBuscaCodBarrasAPI.Text);
            popularFormulario(dadosProduto);

            Cursor.Current = Cursors.Default;
        }

        private void popularFormulario(CadastroProdutoModel cadastroProduto)
        {
            txtNome.Text = cadastroProduto.Nome;
            txtCodBarras.Text = cadastroProduto.CodigoBarras;
            txtLargura.Text = cadastroProduto.Largura.ToString();
            txtAltura.Text = cadastroProduto.Altura.ToString();
            txtComprimento.Text = cadastroProduto.Comprimento.ToString();
            txtPesoLiquido.Text = cadastroProduto.PesoLiquido.ToString();
            txtPesoBruto.Text = cadastroProduto.PesoBruto.ToString();
            txtPrecoMedio.Text = cadastroProduto.PrecoMedio.ToString();
            txtPrecoMaximo.Text = cadastroProduto.PrecoMaximo.ToString();
            txtPrecoMinimo.Text = cadastroProduto.PrecoMinimo.ToString() ;
            txtMarca.Text = cadastroProduto.Marca;

            var bindingList = new BindingList<CadastroUnidadeModel>(cadastroProduto.Unidades);
            var dados = new BindingSource(bindingList, null);
            dgvUnidades.DataSource = dados;

            imgCodBarras.ImageLocation = cadastroProduto.ImagemURLCodigoBarras;
            imgProduto.ImageLocation = cadastroProduto.ImagemURL;
            imgMarca.ImageLocation = cadastroProduto.ImagemURLMarca;
            

            txtNCM.Text = cadastroProduto.NCM.Codigo;
            txtNCMDescricao.Text = cadastroProduto.NCM.Descricao;
            txtNCMDescricaoCompleta.Text = cadastroProduto.NCM.DescricaoCompleta;
        }

        private void exibirAlerta(string mensagem)
        {
            MessageBox.Show(mensagem, "Mensagem do sistema", MessageBoxButtons.OK);            
        }

        private void btnImagemProduto_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(imgProduto.ImageLocation);
        }

        private void btnImagemMarca_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(imgMarca.ImageLocation);
        }

        private void btnImagemCodigoBarras_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(imgCodBarras.ImageLocation);
        }

        private void btnBuscarProduto_Click(object sender, EventArgs e)
        {
            exibirAlerta("Será implementado na próxima versão");
        }

        private void btnImportarSalvarProduto_Click(object sender, EventArgs e)
        {
            exibirAlerta("Será implementado na próxima versão");
        }
    }
}
