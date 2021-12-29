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

namespace finalfinalfinal
{
    public partial class Form1 : Form
    {

        DataTable smer = new DataTable();
        int kolona = 0;
        string cs = "Data source=DESKTOP-CKQNC93\PEKYPIRO; Initial catalog=final; Integrated security=true";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection veza = new SqlConnection(cs);
            SqlDataAdapter adapter = new SqlDataAdapter("select * from smer ", veza);
            adapter.Fill(smer);

            obnovi(kolona);

            if (kolona == 0)
            {
                button2.Enabled = false;
            }
            if (kolona == smer.Rows.Count - 1)
            {
                button3.Enabled = false;
            }
        }

        private void obnovi(int br)
        {
            textBox1.Text = smer.Rows[br]["id"].ToString();
            textBox2.Text = smer.Rows[br]["naziv"].ToString();
            textBox3.Text = smer.Rows[br]["trajanje"].ToString();
            textBox4.Text = smer.Rows[br]["max_ucenika"].ToString();
            textBox5.Text = smer.Rows[br]["prijemni_ispit"].ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            kolona = 0;
            obnovi(kolona);
            button2.Enabled = false;
            button3.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (kolona > 0)
            {
                kolona--;
                obnovi(kolona);
                button3.Enabled = true;
            }
            if (kolona == 0)
            {
                button2.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (kolona < smer.Rows.Count - 1)
            {
                kolona++;
                obnovi(kolona);
                button2.Enabled = true;
            }
            if (kolona == smer.Rows.Count - 1)
            {
                button3.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            kolona = smer.Rows.Count - 1;
            obnovi(kolona);
            button2.Enabled = true;
            button3.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection veza = new SqlConnection(cs);
            SqlCommand sql = new SqlCommand("insert into smer (id , naziv, trajanje, max_ucenika, prijemni_ispit) values (" + textBox1.Text + ", '" + textBox2.Text + "' ,'" + textBox3.Text + "', '" + textBox4.Text + "' , '" + textBox5.Text + "' ) ", veza);
            veza.Open();
            sql.ExecuteNonQuery();
            veza.Close();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from smer", veza);
            smer.Clear();
            adapter.Fill(smer);
            obnovi(kolona);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection veza = new SqlConnection(cs);
            SqlCommand naredba = new SqlCommand("Update smer Set naziv= '" + textBox2.Text + "', trajanje= '" + textBox3.Text + "' , max_ucenika= '" + textBox4.Text + "' , prijemni_ispit= '" + textBox5.Text + "'  where id= " + textBox1.Text, veza);
            veza.Open();
            naredba.ExecuteNonQuery();
            veza.Close();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from smer", veza);
            smer.Clear();
            adapter.Fill(smer);
            obnovi(kolona);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlConnection veza = new SqlConnection(cs);
            SqlCommand naredba = new SqlCommand("DELETE FROM smer WHERE id=" + textBox1.Text, veza);
            veza.Open();
            naredba.ExecuteNonQuery();
            veza.Close();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from smer", veza);
            smer.Clear();
            adapter.Fill(smer);
            if (kolona == smer.Rows.Count) kolona = kolona - 1;
            if (kolona == 0)
            {
                button2.Enabled = false;
            }
            if (smer.Rows.Count > 1)
            {
                button3.Enabled = true;
            }
            if (kolona == smer.Rows.Count - 1)
            {
                button3.Enabled = false;
            }

            obnovi(kolona);
        }
    }
}
