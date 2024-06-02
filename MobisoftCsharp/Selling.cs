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
                    string name = SALEItemtb.Text;
                    con.Open();

                    string query3 = "SELECT Product_Id FROM ProductsTbl WHERE Name LIKE @name ";
                    SqlCommand command3 = new SqlCommand(query3, con);
                    command3.Parameters.AddWithValue("@name", name);
                    object result3 = command3.ExecuteScalar();
                    int id = (int)result3;

                    String sql = "Insert into SalesRecordTbl values(@productsId, " + SALEIDtb.Text + ", '" + SALEItemtb.Text.ToString() + "', '" + SALETypeCb.SelectedItem.ToString() + "', " + REMtb.Text + ", " + SOLDtb.Text + ", '"+SALEDatetb.Value.Date.ToString("yyyy/MM/dd")+"')";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@productsId", id);
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
            try
            {
                string Id = SALEIDtb.Text;
                int priceBought;
                int product_id;
                int priceSold;
                double profits;
                con.Open();
                string query = "SELECT Product_Id  FROM SalesRecordTbl WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@Id", Id);
                object result = command.ExecuteScalar();
                product_id = (int)result;
                string query2 = "SELECT AmmountSold FROM SalesRecordTbl WHERE Product_Id = @Id";
                SqlCommand command2 = new SqlCommand(query2, con);
                command2.Parameters.AddWithValue("@Id", product_id);
                object result2 = command2.ExecuteScalar();
                priceSold = (int)result2;


                if (SALETypeCb.SelectedItem.ToString() == "laptop" || SALETypeCb.SelectedItem.ToString() == "Laptop")
                {
                    string queryA = "SELECT MobPrice FROM LaptopTbl WHERE Product_Id = @Id";
                    SqlCommand commandA = new SqlCommand(queryA, con);
                    commandA.Parameters.AddWithValue("@Id", product_id);
                    object result3 = commandA.ExecuteScalar();
                    priceBought = (int)result3;
                    profits = priceSold - priceBought;
                    profitLabel.Text = profits.ToString();

                }
                else
                {
                    string query4 = "SELECT APrice FROM AccessoriesTbl WHERE Product_Id = @Id";
                    SqlCommand command4 = new SqlCommand(query4, con);
                    command4.Parameters.AddWithValue("@Id", product_id);
                    object result4 = command4.ExecuteScalar();
                    priceBought = (int)result4;
                    profits = priceSold - priceBought;
                    profitLabel.Text = profits.ToString();
                }


                con.Close();
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
            

            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            showProfits();
        }
    }
}
