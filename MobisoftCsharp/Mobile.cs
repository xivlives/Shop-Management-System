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
    public partial class Mobile : Form
    {
        public Mobile()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Favour\OneDrive\Documents\DaleTechDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void Mobile_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void populate()
        {
            con.Open();
            String query = "select * from LaptopTbl";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            MobileDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Random randomNum = new Random();
            int ProductId = randomNum.Next(1000, 10000);
            string productName = BrandTb.Text + ModelTb.Text;
            string type = "Laptop";


            if (InsertLaptop(ProductId, productName, type))
            {
                MessageBox.Show("Laptop added successfully!");
                // Clear input fields after successful insert (optional)
                
            }
            else
            {
                MessageBox.Show("Error adding laptop. Please check details and try again.");
            }
        }

        private bool InsertLaptop(int productId, string productName, string type)
        {

            try
            {
                con.Open();

                // Insert into Products table
                string productQuery = "INSERT INTO ProductsTbl (Product_id, Name, Type) VALUES (@productId, @productName, @type)";
                SqlCommand productCommand = new SqlCommand(productQuery, con);
                productCommand.Parameters.AddWithValue("@productId", productId);
                productCommand.Parameters.AddWithValue("@productName", productName);
                productCommand.Parameters.AddWithValue("@type", type);

                int productRowsAffected = productCommand.ExecuteNonQuery();

                // Insert into Laptops table only if product insertion was successful
                if (productRowsAffected > 0)
                {
                    string sql = "INSERT INTO LaptopTbl (Product_Id, MobID, MobBrand, MobModel, MobPrice, MobStock, MobRAM, MobROM, MobProcs, MobVRAM, MobGen) VALUES ( @productId," + MobIDtb.Text + ", '" + BrandTb.Text.ToString() + "', '" + ModelTb.Text.ToString() + "', " + PriceTb.Text + ", " + StockTb.Text + ", " + RAMCb.SelectedItem.ToString() + ", " + ROMCb.SelectedItem.ToString() + ",'" + PROCSCb.SelectedItem.ToString() + "', " + VRAMCb.SelectedItem.ToString() + ", '" + GENCb.SelectedItem.ToString() + "') ";
                    SqlCommand laptopCommand = new SqlCommand(sql, con);
                    laptopCommand.Parameters.AddWithValue("@productId", productId);

                    int laptopRowsAffected = laptopCommand.ExecuteNonQuery();
                    con.Close();
                    populate();
                    return laptopRowsAffected > 0; // Successful insert into Laptops table


                }
                else
                {
                    return false; // Product insert failed
                }
            }catch (Exception ex)
                   {
                       MessageBox.Show(ex.Message);
                       return false;
                   }
            
        }

        private void MobileDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MobIDtb.Text = MobileDGV.SelectedRows[0].Cells[0].Value.ToString();
            BrandTb.Text = MobileDGV.SelectedRows[0].Cells[1].Value.ToString();
            ModelTb.Text = MobileDGV.SelectedRows[0].Cells[2].Value.ToString();
            PriceTb.Text = MobileDGV.SelectedRows[0].Cells[3].Value.ToString();
            StockTb.Text = MobileDGV.SelectedRows[0].Cells[4].Value.ToString();
            RAMCb.SelectedItem = MobileDGV.SelectedRows[0].Cells[5].Value.ToString();
            ROMCb.SelectedItem = MobileDGV.SelectedRows[0].Cells[6].Value.ToString();
            PROCSCb.SelectedItem = MobileDGV.SelectedRows[0].Cells[7].Value.ToString();
            VRAMCb.SelectedItem = MobileDGV.SelectedRows[0].Cells[8].Value.ToString();
            GENCb.SelectedItem = MobileDGV.SelectedRows[0].Cells[9].Value.ToString();   
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            MobIDtb.Text = "";
            BrandTb.Text = "";
            ModelTb.Text = "";
            PriceTb.Text = "";
            StockTb.Text = "";
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (MobIDtb.Text == "")
            {
                MessageBox.Show("enter the laptop to be deleted");
            }
            else
            {
                try
                {
                    con.Open();
                    String query = "delete from LaptopTbl where MobId=" + MobIDtb.Text + "";
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (MobIDtb.Text == "" || BrandTb.Text == "" || ModelTb.Text == "" || PriceTb.Text == "" || StockTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    String sql = "update LaptopTbl set MobBrand='" + BrandTb.Text + "', MobModel='" + ModelTb.Text + "', MobPrice=" + PriceTb.Text + ", MobStock=" + StockTb.Text + ", MobRAM=" + RAMCb.SelectedItem.ToString() + ", MobROM=" + ROMCb.SelectedItem.ToString() + ", MobProcs='"+PROCSCb.SelectedItem.ToString()+"', MobVRAM=" + VRAMCb.SelectedItem.ToString() + " MobGen='"+GENCb.SelectedItem.ToString()+"' where MobID=" + MobIDtb.Text + ";";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Laptop Updated successfully");
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

        private void label15_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
        }
    }
}
