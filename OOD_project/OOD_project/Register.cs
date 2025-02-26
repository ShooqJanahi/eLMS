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

namespace OOD_project
{
    public partial class Registerfrm : Form
    {
        public Registerfrm()
        {
            InitializeComponent();
        }

        private void registrationBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.registrationBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);

        }

        private void registrationBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.registrationBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);

        }

        private void Register_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Registration' table. You can move, or remove it, as needed.
            this.registrationTableAdapter.Fill(this.database1DataSet.Registration);
            roletxt.Hide();
            txtRole.Hide();

            rdAdmin.Checked = true;
            txtdep.Hide();
            txtMajor.Hide();
            deptxt.Hide();
            majortxt.Hide();

        }

        private void Cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();

            Loginfrm loginfrm = new Loginfrm();
            loginfrm.Show();
        }

        private void rdStudent_CheckedChanged(object sender, EventArgs e)
        {
            txtdep.Hide();
            deptxt.Hide();
            txtMajor.Show();
            majortxt.Show();
        }

        private void rdAdmin_CheckedChanged(object sender, EventArgs e)
        {
            txtdep.Hide();
            txtMajor.Hide();
            deptxt.Hide();
            majortxt.Hide();
        }

        private void rdTeacher_CheckedChanged(object sender, EventArgs e)
        {
            txtMajor.Hide();
            majortxt.Hide();
            deptxt.Show();
            txtdep.Show();
        }

        private void Registerbtn_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Shooq\\OneDrive\\Desktop\\OOD_project\\OOD_project\\OOD_project\\Database1.mdf;Integrated Security=True"))
            {
                con.Open();

                string query = "INSERT INTO Registration VALUES (@ID, @Fnaem, @Lname, @Phone, @Password, @Email, @Department, @Major, @Role)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", txtID.Text); // Assuming enteruser is the ID value
                    cmd.Parameters.AddWithValue("@Fnaem", txtFname.Text); // Example value for Fnaem
                    cmd.Parameters.AddWithValue("@Lname", txtLname.Text); // Example value for Lname
                    cmd.Parameters.AddWithValue("@Phone", txtPhone.Text); // Example value for Phone
                    cmd.Parameters.AddWithValue("@Password", txtpassword.Text); // Assuming enterpass is the password value
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text); // Example value for email
                    cmd.Parameters.AddWithValue("@Department", txtdep.Text); // Example value for department
                    cmd.Parameters.AddWithValue("@Major", txtMajor.Text); // Example value for Major
                    
                    if (rdAdmin.Checked)
                    {
                        cmd.Parameters.AddWithValue("@Role", "Admin");
                    } else if (rdTeacher.Checked)
                    {
                        cmd.Parameters.AddWithValue("@Role", "Teacher");
                    } else if (rdStudent.Checked)
                    {
                        cmd.Parameters.AddWithValue("@Role", "Student");
                    }

                    cmd.ExecuteNonQuery();
                }

                //con.Close();
                
            }

            MessageBox.Show("Registered");

        }
    }
}
