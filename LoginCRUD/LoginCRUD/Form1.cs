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

namespace LoginCRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }


        public string fname = "";
        public string lname = "";
        public string age = "";
        public string gender = "";
        public string  phnno= "";
        public int StudentId;


        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-0I3OV8I;Initial Catalog=Nitesh;Integrated Security=True");

        private void Form1_Load(object sender, EventArgs e)
        {
            GetRecords();
        }

        private void GetRecords()
        {

            SqlCommand cmd = new SqlCommand(" select * from Login", conn);
            DataTable dt=new DataTable();

            conn.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);

            conn.Close();

            StudentDataGridView.DataSource = dt;

        }

        private void getValues()
        {
             fname = txtFname.Text;
             lname = txtLname.Text;
             age = txtAge.Text;
            if (Male.Checked == true)
                gender = "Male";
            if (Female.Checked == true)
                gender = "Female";
           
            phnno = txtPhnno.Text;
        }


        private void Insert_Click(object sender, EventArgs e)
        {
            getValues();
            SqlCommand cmd = new SqlCommand("Insert into Login values(@FirstName,@LastName,@Age,@Gender,@PhoneNo)",conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@FirstName", fname);
            cmd.Parameters.AddWithValue("@LastName", lname);
            cmd.Parameters.AddWithValue("@Age", age);
            cmd.Parameters.AddWithValue("@Gender", gender);
            cmd.Parameters.AddWithValue("@PhoneNo", phnno);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("New Student details are succesfully saved in DB");
            GetRecords();

        }

        private void Reset_Click(object sender, EventArgs e)
        {
            StudentId = 0;
            txtFname.Clear();
            txtLname.Clear();
            txtAge.Clear();
            Male.Checked = false;
            Female.Checked = false;
            txtPhnno.Clear();
        }

        private void StudentDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            StudentId = Convert.ToInt32(StudentDataGridView.SelectedRows[0].Cells[0].Value);
            txtFname.Text = StudentDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtLname.Text = StudentDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtAge.Text = StudentDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            string genderValue = StudentDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            if(genderValue == "Male")
            {
                Male.Checked = true;
                Female.Checked = false;
            }

            if(genderValue == "Female")
            {
                Female.Checked= true;
                Male.Checked = false;
            }

            txtPhnno.Text = StudentDataGridView.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void Update_Click(object sender, EventArgs e)
        {

            if(StudentId>0)
            {

                getValues();
                SqlCommand cmd = new SqlCommand("UPDATE Login SET  Fname = @FirstName, Lname = @LastName, Age = @Age,Gender = @Gender, PhoneNo= @PhoneNo WHERE StudID = @StudentId", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@FirstName", fname);
                cmd.Parameters.AddWithValue("@LastName", lname);
                cmd.Parameters.AddWithValue("@Age", age);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@PhoneNo", phnno);
                cmd.Parameters.AddWithValue("@StudentId", this.StudentId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Student details are Updated Succesfully");
                GetRecords();

            }
            else
            {
                MessageBox.Show("Please Select an Student to update info");

            }

        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (StudentId > 0)
            {

                getValues();
                SqlCommand cmd = new SqlCommand("DELETE FROM  Login WHERE StudID = @StudentId", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@StudentId", this.StudentId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Student details are Deleted From DB");
                GetRecords();

            }
            else
            {
                MessageBox.Show("Please Select an Student to delete info");

            }
        }

    }
}
