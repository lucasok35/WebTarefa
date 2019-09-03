using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebTarefa
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CarregarLista();
        }

        private void CarregarLista()
        {
            TarefaDBEntities context = new TarefaDBEntities();
            List<TB_TAREFA> lstTarefa = context.TB_TAREFA.ToList<TB_TAREFA>();

            GVTarefa.DataSource = lstTarefa;
            GVTarefa.DataBind();
        }

        protected void GVTarefa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int IdItem = Convert.ToInt32(e.CommandArgument.ToString());
            TarefaDBEntities contextTarefa = new TarefaDBEntities();
            TB_TAREFA tarefa = new TB_TAREFA();

            tarefa = contextTarefa.TB_TAREFA.First(c => c.Id == IdItem);

            if (e.CommandName == "ALTERAR")
            {
                Response.Redirect("Tarefa.aspx?IdItem= " + IdItem);
            }
            else if(e.CommandName == "EXCLUIR")
            {
                contextTarefa.TB_TAREFA.Remove(tarefa);
                contextTarefa.SaveChanges();
                string msg = "Viagem Excluída com Sucesso!";
                string titulo = "Informação!";
                CarregarLista();
                DisplayAlert(msg, titulo, this);
            }
        }

        public void DisplayAlert(string titulo, string mensagem, System.Web.UI.Page page)
        {
            h1.InnerText = titulo;
            lblMsgPopup.InnerText = mensagem;
            ClientScript.RegisterStartupScript(typeof(Page), Guid.NewGuid().ToString(), "MostrarPopupMensagem();", true);
        }

    }
}