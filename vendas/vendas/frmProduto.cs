using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using vendas.Models;
using vendas.Controller;

namespace vendas
{
    public partial class frmProduto : Form
    {
        public frmProduto()
        {
            InitializeComponent();
        }

        private void frmProduto_Load(object sender, EventArgs e)
        {
            ConProduto conProduto = new ConProduto();
            List<Produto> produtos = conProduto.ListaProduto();
            dgvProduto.DataSource = produtos;
            btnAtualizar.Enabled = false;
            btnExcluir.Enabled = false;
            this.ActiveControl = txtNome;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNome.Text == "" || txtQuantidade.Text == "" || txtPreco.Text == "")
                {
                    MessageBox.Show("Por favor, preencha todos os campos!", "Campos Obrigatórios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    ConProduto conProduto = new ConProduto();
                    if (conProduto.RegistroRepetido(txtNome.Text) == true)
                    {
                        MessageBox.Show("Produto já existe em nossa base de dados1", "Produto Repetido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtNome.Text = string.Empty;
                        txtQuantidade.Text = string.Empty;
                        txtPreco.Text = string.Empty;
                        this.ActiveControl = txtNome;
                        return;
                    }
                    else
                    {
                        int quantidade = Convert.ToInt32(txtQuantidade.Text.Trim());
                        if (quantidade == 0)
                        {
                            MessageBox.Show("Por favor, a qauntidade tem que ser maior que zero(0)!", "Quantidade", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.ActiveControl = txtNome;
                            return;
                        }
                        else
                        {
                            
                            conProduto.Inserir(txtNome.Text, quantidade,txtPreco.Text);
                            MessageBox.Show("Produto inserido com sucesso!", "Inserção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtNome.Text = string.Empty;
                            txtPreco.Text = string.Empty;
                            txtPreco.Text = string.Empty;
                            this.ActiveControl = txtNome;
                            List<Produto> produtos = conProduto.ListaProduto();
                            dgvProduto.DataSource = produtos;
                        }
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ConProduto conProduto = new ConProduto();
            var Id = Convert.ToInt32(txtId.Text);
            conProduto.Localizar(Id);
        }
    }
}
