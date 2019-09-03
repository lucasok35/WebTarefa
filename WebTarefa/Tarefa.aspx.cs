using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebTarefa
{
    public partial class Tarefa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarDadosPagina();
            }
        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            string DescricaoTarefa = txtDescricao.Text;
            DateTime DtAtiv = Convert.ToDateTime(txtDataAtividade.Text);
            DateTime DtEnt = Convert.ToDateTime(txtDataEntrega.Text);
            DateTime DtLimEnt = Convert.ToDateTime(txtDataLimEntrega.Text);

            TB_TAREFA t = new TB_TAREFA() { Descricao = DescricaoTarefa, DtAtividade = DtAtiv, DtEntrega = DtEnt,
            DtLimEntrega = DtEnt};
            TarefaDBEntities contextTarefa = new TarefaDBEntities();

            string valor = Request.QueryString["IdItem"];

            if (String.IsNullOrEmpty(valor))
            {
                contextTarefa.TB_TAREFA.Add(t);
                lblmsg.Text = "Registro Inserido!";
                Clear();
            }
            else
            {
                int id = Convert.ToInt32(valor);
                TB_TAREFA tarefa = contextTarefa.TB_TAREFA.First(c => c.Id == id);
                tarefa.Descricao = t.Descricao;
                tarefa.DtAtividade = t.DtAtividade;
                tarefa.DtEntrega = t.DtEntrega;
                tarefa.DtLimEntrega = t.DtLimEntrega;
                lblmsg.Text = "Registro Alterado!";
            }

            contextTarefa.SaveChanges();
        }

        private void Clear()
        {
            txtDescricao.Text = "";
            txtDataAtividade.Text = "";
            txtDataEntrega.Text = "";
            txtDataLimEntrega.Text = "";
        }

        private void CarregarDadosPagina()
        {
            string valor = Request.QueryString["IdItem"];
            int IdItem = 0;
            TB_TAREFA tarefa = new TB_TAREFA();
            TarefaDBEntities contextTarefa = new TarefaDBEntities();

            if (!String.IsNullOrEmpty(valor))
            {
                IdItem = Convert.ToInt32(valor);
                tarefa = contextTarefa.TB_TAREFA.First(c => c.Id == IdItem);

                txtDescricao.Text = tarefa.Descricao;
                txtDataAtividade.Text = tarefa.DtAtividade.ToString();
                txtDataEntrega.Text = tarefa.DtEntrega.ToString();
                txtDataLimEntrega.Text = tarefa.DtLimEntrega.ToString();
            
            }
        }

    }
}