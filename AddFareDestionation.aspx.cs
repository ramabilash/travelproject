using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using System.Data.SqlClient;
using System.Data;
using Businessobjects;

namespace PresentationLayer
{
    public partial class AddFareDestionation : System.Web.UI.Page
    {
        private void Bindsvno()
        {
            BusinessLogicLayer.BusinessLogicLayer objbll = new BusinessLogicLayer.BusinessLogicLayer();
            SqlDataReader dr = objbll.BindServiceno();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    string svno = dr[0].ToString();
                    Session["svno"] = svno;
                    string fplace = dr[1].ToString();
                    string tplace = dr[2].ToString();
                    string s = svno + "/" + fplace + "/" + tplace;
                    DropDownList1.Items.Add(s);
                }
            }
        }
        private void BindDpointsbasedonSvno()
        {
            Businessobjects.Businessobjects objbo = new Businessobjects.Businessobjects();
            string s = DropDownList1.SelectedItem.ToString();
            string[] s1 = s.Split('/');
            objbo.Serviceno = s1[0].ToString();
            BusinessLogicLayer.BusinessLogicLayer objbll = new BusinessLogicLayer.BusinessLogicLayer();
            DataSet ds = objbll.BindDpointsbasedonSvno(objbo);
            DropDownList2.DataSource = ds;
            DropDownList2.DataValueField = "dname";
            DropDownList2.DataBind();
            DropDownList3.DataSource = ds;
            DropDownList3.DataValueField = "dname";
            DropDownList3.DataBind();
        }
        private void FillDpoints1()
        {
            BusinessLogicLayer.BusinessLogicLayer objbll = new BusinessLogicLayer.BusinessLogicLayer();
            Businessobjects.Businessobjects objbo = new Businessobjects.Businessobjects();
            string s = DropDownList1.SelectedItem.ToString();
            string[] s1 = s.Split('/');
            objbo.Serviceno = s1[0];
            DataSet ds=objbll.ViewDpoints();
            DropDownList3.DataSource = ds;
            DropDownList3.DataValueField = "dname";
            DropDownList3.DataValueField = "did";
            DropDownList3.DataBind();
            ViewState["svno"] = objbo.Serviceno;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                Bindsvno();
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BusinessLogicLayer.BusinessLogicLayer objbll = new BusinessLogicLayer.BusinessLogicLayer();
            Businessobjects.Businessobjects objbo = new Businessobjects.Businessobjects();
            BindDpointsbasedonSvno();
            FillDpoints1();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BusinessLogicLayer.BusinessLogicLayer objbll = new BusinessLogicLayer.BusinessLogicLayer();
            Businessobjects.Businessobjects objbo = new Businessobjects.Businessobjects();
            objbo.Serviceno = ViewState["svno"].ToString();
            objbo.Fdid = DropDownList2.SelectedValue.ToString();
            objbo.Fdeptime = txtstarttime.Text;
            objbo.Tdid = DropDownList3.SelectedValue.ToString();
            objbo.Tdeptime = txtreachtime.Text;
            objbo.Fare = double.Parse(txtfare.Text);
            int i=objbll.AddFareDestination(objbo);
            if (i == 1)
            {
                Response.Write("Added");
            }
            else
            {
                Response.Write("Failed");
            }
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDpoints1();
            DropDownList3.Items.Remove(DropDownList2.SelectedItem.ToString());
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
              
        }
    }
}