using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MobisoftCsharp
{
    public partial class Selling : Form
    {
        public Selling()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Favour\OneDrive\Documents\DaleTechDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            con.Open();
            String query = "select * from SalesRecordTbl";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            SalesDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (SALEIDtb.Text == "" || SALEItemtb.Text == "" || SALETypeCb.SelectedItem.ToString() == "" || REMtb.Text == "" || SOLDtb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    String sql = "Insert into SalesRecordTbl values(" + SALEIDtb.Text + ", '" + SALEItemtb.Text.ToString() + "', '" + SALETypeCb.SelectedItem.ToString() + "', " + REMtb.Text + ", " + SOLDtb.Text + ", '"+SALEDatetb.Value.Date.ToString("yyyy/MM/dd")+"')";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item added successfully");
                    con.Close();
                    populate();
                    SALEIDtb.Text = "";
                    SALEItemtb.Text = "";
                    REMtb.Text = "";
                    SOLDtb.Text = "";
                    SOLDtb.Text = "";
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    con.Close();
                }
            }
        }

        private void Selling_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (SALEIDtb.Text == "")
            {
                MessageBox.Show("enter the item to be deleted");
            }
            else
            {
                try
                {
                    con.Open();
                    String query = "delete from SalesRecordTbl where Id=" + SALEIDtb.Text + "";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("entry deleted");
                    con.Close();
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    con.Close();
                }
            }
        }

        private void showProfits()
        {
            string Id = SALEIDtb.Text.ToString();
            int priceBought;
            int priceSold;
            double profits;
            con.Open();
            string query = "SELECT AmmountSold FROM SalesRecordTbl WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@Id", Id);
            object result = command.ExecuteScalar();
                priceSold = (int)result;
            string query2 = "SELECT APrice FROM AccessoriesTbl WHERE BID = @Id";
            SqlCommand command2 = new SqlCommand(query2, con);
            command.Parameters.AddWithValue("@Id", Id);
            object result2 = command.ExecuteScalar();
                priceBought = (int)result;
            if (priceBought > priceSold) 
            {
                profits = priceSold - priceBought;
                MessageBox.Show("you lost "+profits+" ");
            }
            else 
            {
                profits = priceSold - priceBought;
                MessageBox.Show("you gained " + profits + " ");


            }
            con.Close();

            
        }
    }
}
