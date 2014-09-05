using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CurrencyService
{
    public partial class manager : System.Web.UI.Page
    {
        List<Currency> currencyList;
        CurrencyServices currencyService;


        protected void Page_Load(object sender, EventArgs e)
        {
            currencyService = new CurrencyServices();
            currencyList = currencyService.GetCurrencyListing();
            if (!IsPostBack)
            {
                CurrencyListView.DataSource = currencyList;
                CurrencyListView.DataBind();

            }
        }

        protected void CurrencyListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                TextBox txtCode = (TextBox)e.Item.FindControl("txtUpCode");
                TextBox txtValue = (TextBox)e.Item.FindControl("txtUpValue");
                currencyService.UpdateCurrency(txtCode.Text, Double.Parse(txtValue.Text));
            }
        }

        protected void CurrencyListView_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            CurrencyListView.EditIndex = e.NewEditIndex;
            CurrencyListView.DataSource = currencyList;
            CurrencyListView.DataBind();
        }

        protected void CurrencyListView_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            Response.Redirect("manager.aspx");
        }

        protected void CurrencyListView_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            Response.Redirect("manager.aspx");
        }

    }
}