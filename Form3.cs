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

namespace library_management_system
{
    public partial class Form3 : Form
    {
        String s;
        private BindingSource bindingSource1 = new BindingSource();
        private string username;

        public Form3(string u)
        {
            InitializeComponent();
            username = u;
            dataGridView1.Columns.Add("column", "Book");
            dataGridView1.Columns.Add("column", "Author");
            dataGridView1.Columns.Add("column", "Genre");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            s = comboBox1.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                textBox1.Text = "";
                return;
            }

            SqlConnection sqlConnection1 = new SqlConnection();
            sqlConnection1.ConnectionString = "Server=AWAN-PC;Database=lms;Trusted_Connection=true;";

            SqlCommand query = new SqlCommand();
            SqlDataReader reader;

            query.CommandType = CommandType.Text;
            query.Connection = sqlConnection1;

            String y = textBox1.Text;

            if (s == "genre")
            {
                query.CommandText = "SELECT artifactName, author, genre FROM artifact WHERE genre LIKE '" + y + "%'";
            }
            if (s == "name")
            {
                query.CommandText = "SELECT artifactName, author, genre FROM artifact WHERE artifactName LIKE '" + y + "%'";
            }
            if (s == "author")
            {
                query.CommandText = "SELECT artifactName, author, genre FROM artifact WHERE author LIKE '" + y + "%'";
            }



            try
            {

                sqlConnection1.Open();

                reader = query.ExecuteReader();
                this.dataGridView1.DataSource = null;
                this.dataGridView1.Rows.Clear();

                while (reader.Read()) {
                    object[] row = { reader[0], reader[1], reader[2] };
                    dataGridView1.Rows.Add(row);
                }

                reader.Close();
                
                //SqlDataAdapter adapter = new SqlDataAdapter(query.CommandText, sqlConnection1);

                //DataSet ds = new DataSet();
               // adapter.TableMappings.Add("artifact", "Books");
                //adapter.Fill(ds);
                
                //sqlConnection1.Close();
                //dataGridView1.DataSource = ds;
             //   dataGridView1.DataMember = "Books.artifactName";


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Issue f1 = new Issue(username);
            f1.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Return f1 = new Return(username);
            f1.ShowDialog();
            this.Close();
        }
    }
}

