using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vendas.Models;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace vendas.Controller
{
    internal class ConProduto
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Aluno\\Downloads\\AppVendas\\vendas\\vendas\\DbVendas.mdf;Integrated Security=True");
        Produto produto = new Produto();    // só é possivel instanciar o model produto pelo uso do "using vendas.Models" nome do arquivo + pasta de models
        public List<Produto> ListaProduto()   // lista<produto> significa ser uma lista daquele model
        {
            List <Produto> li = new List<Produto> ();
            string sql = "SELECT * FROM Produto";
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open ();

            SqlCommand cmd = new SqlCommand(sql, con);   // preparando para mandar a solicitação para o banco
            SqlDataReader dr = cmd.ExecuteReader();       // abrindo data reader e executando
            while (dr.Read()) 
            {
                produto.Id = (int)dr["Id"];              //  informo qual tipo do campo e indico qual campo é
                produto.nome = dr["nome"].ToString();     //  quando é algo que comporta caracter somente no final informo para transformar em string
                produto.quantidade = (int)dr["quantidade"];
                produto.preco = (decimal)dr["preco"];
                li.Add(produto);
            }
            dr.Close ();
            con.Close();
            return li;
        }
        

        public void Inserir(string nome, int quantidade, string preco)
        {
            try
            {
                decimal PrecoFinal = Convert.ToDecimal(preco) /100;
                // insert esta inserindo os dados recebidos
                string sql = "INSERT INTO Produto(nome, quantidade, preco) VALUES ('" + nome + "', '" + quantidade + "', @preco)";
                                                                                                                        // @preco é um apelido
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open ();
                
                SqlCommand cmd = new SqlCommand (sql, con);
                cmd.Parameters.Add("@preco", SqlDbType.Decimal).Value = PrecoFinal;  // para validar o apelido "@nome do apelido", sqldbtype.tipo da variavel na tabela.value = variavel que chega
                cmd.ExecuteNonQuery ();
                con.Close ();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        public void Atualizar(int Id, string nome, int quantidade, string preco)
        {
            try
            {
                decimal PrecoFinal = Convert.ToDecimal (preco) / 100;
                string sql = "UPDATE Produto SET nome = '"+ nome +"', quantidade = '"+ quantidade +"', preco = @preco WHERE Id = '"+ Id +"' ";
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@preco", SqlDbType.Decimal).Value = PrecoFinal;  // para validar o apelido "@nome do apelido", sqldbtype.tipo da variavel na tabela.value = variavel que chega
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        public void Excluir(int Id)
        {
            try
            {
                string sql = "DELETE FROM Produto WHERE Id = '" + Id + "' ";
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();   // somente aqui executa o string sql (execute reader tbm executa)
                con.Close();

            }
            catch (Exception er)
            {
                MessageBox.Show (er.Message);
            }
        }

        public void Localizar(int Id)
        {
            string sql = "SELECT * FROM Produto WHERE Id = '" + Id + "' ";
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read()) 
            {
                produto.nome = dr["nome"].ToString();
                produto.quantidade = (int)dr["quantidade"];
                produto.preco = (decimal)dr["preco"];
            }
            dr.Close();
            con.Close();

        }

        public bool RegistroRepetido(string nome)
        {
            string sql = "SELECT * FROM Produto WHERE nome = '" + nome + "' ";
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

            SqlCommand cmd = new SqlCommand (sql, con);
            cmd.ExecuteNonQuery ();
            var result = cmd.ExecuteScalar (); // execute scalar traz a primeira linha e primeira coluna
            if (result != null)
            {
                return (int)result > 0;
            }
            con.Close ();
            return false;
        }
    }
}
