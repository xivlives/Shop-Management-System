using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MobisoftCsharp
{
    public partial class Accessories : Form
    {
        public Accessories()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Favour\OneDrive\Documents\DaleTechDb.mdf;Integrated Security=True;Connect Timeout=30");


        private void populate()
        {
            con.Open();
            String query = "select * from AccessoriesTbl";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            AccessoriesDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (AIDtb.Text == "" || Brandtb.Text == "" || Modeltb.Text == "" || Pricetb.Text == "" || Stocktb.Text == "" || AType.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    String sql = "Insert into AccessoriesTbl values(" + AIDtb.Text + ", '" + Brandtb.Text.ToString() + "', '" + Modeltb.Text.ToString() + "', " + Pricetb.Text + ", " + Stocktb.Text + ", '"+AType.Text.ToString()+"')";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item added successfully");
                    con.Close();
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    con.Close();
                }
            }
        }

        private void Accessories_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (AIDtb.Text == "" || Brandtb.Text == "" || Modeltb.Text == "" || Pricetb.Text == "" || Stocktb.Text == "" || AType.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    String sql = "update AccessoriesTbl set ABrand='" + Brandtb.Text + "', AModel='" + Modeltb.Text + "', APrice=" + Pricetb.Text + ", AStock=" + Stocktb.Text + ", Type=" + AType.Text + " where BID=" + AIDtb.Text + ";";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item Updated successfully");
                    con.Close();
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    con.Close();
                }
            }
        }

        private void AccessoriesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AIDtb.Text = AccessoriesDGV.SelectedRows[0].Cells[0].Value.ToString();
            Brandtb.Text = AccessoriesDGV.SelectedRows[0].Cells[1].Value.ToString();
            Modeltb.Text = AccessoriesDGV.SelectedRows[0].Cells[2].Value.ToString();
            Pricetb.Text = AccessoriesDGV.SelectedRows[0].Cells[3].Value.ToString();
            Stocktb.Text = AccessoriesDGV.SelectedRows[0].Cells[4].Value.ToString();
            AType.Text = AccessoriesDGV.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (AIDtb.Text == "")
            {
                MessageBox.Show("enter the item to be deleted");
            }
            else
            {
                try
                {
                    con.Open();
                    String query = "delete from AccessoriesTbl where BID=" + AIDtb.Text + "";
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

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            
              AIDtb.Text = "";
              Brandtb.Text = "";
              Modeltb.Text = "";
              Pricetb.Text = "";
              Stocktb.Text = "";
              AType.Text = "";
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
        }
    }
}
