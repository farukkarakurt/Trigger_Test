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

namespace triggerTest
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=faruk\\sqlexpress;Initial Catalog=Test;Integrated Security=True;");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
            Sayac();
        }

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBLKITAPLAR", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("insert into TBLKITAPLAR (AD,YAZAR,SAYFA,YAYINEVI,TUR) values (@P1,@P2,@P3,@P4,@P5)", conn);
            cmd.Parameters.AddWithValue("@P1", txtbx_ad.Text);
            cmd.Parameters.AddWithValue("@P2", txt_yazar.Text);
            cmd.Parameters.AddWithValue("@P3", txt_sayfa.Text);
            cmd.Parameters.AddWithValue("@P4", txt_yayinevi.Text);
            cmd.Parameters.AddWithValue("@P5", txt_tur.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            sırala();
            MessageBox.Show("Yeni kayıt eklendi");
            
            listele();
            
            Sayac();
        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Delete from TBLKITAPLAR where ID=@P1",conn);
            cmd.Parameters.AddWithValue("@P1", txtbox_ID.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            sırala();
            MessageBox.Show("Kayıt silinmiştir ");
          
            listele();
           
            Sayac();
        }
        void Sayac()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from TBLSAYAC", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lbl_kitapsayisi.Text = dr[0].ToString();
            }
            conn.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtbox_ID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtbx_ad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txt_yazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txt_sayfa.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txt_yayinevi.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txt_tur.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }
        void sırala()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM TBLKITAPLAR ORDER BY ID ASC", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            conn.Close();
        }
    }
}
