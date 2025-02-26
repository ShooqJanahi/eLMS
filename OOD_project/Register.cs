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
using static OOD_project.DBManager;

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
            //this.registrationTableAdapter.Fill(this.database1DataSet.Registration);
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
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "INSERT INTO UserRegistration VALUES (@ID, @Fnaem, @Lname, @Phone, @Password, @Email, @Department, @Major, @Role, @userstatus)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", txtID.Text); 
                    cmd.Parameters.AddWithValue("@Fnaem", txtFname.Text); 
                    cmd.Parameters.AddWithValue("@Lname", txtLname.Text); 
                    cmd.Parameters.AddWithValue("@Phone", txtPhone.Text); 
                    cmd.Parameters.AddWithValue("@Password", txtpassword.Text); 
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Department", txtdep.Text); 
                    cmd.Parameters.AddWithValue("@Major", txtMajor.Text); 
                    cmd.Parameters.AddWithValue("@userstatus", "Waiting");
                    
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

                con.Close();

            }

            MessageBox.Show("Registered");

            if (rdStudent.Checked)
            {
                using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
                {
                    con.Open();

                    string query = "INSERT INTO Student ([ID], [First Name], [Last Name], [Phone Number], [Email], [Major]) " +
                                   "VALUES (@ID, @FirstName, @LastName, @PhoneNumber, @Email, @Major)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", txtID.Text);
                        cmd.Parameters.AddWithValue("@FirstName", txtFname.Text);
                        cmd.Parameters.AddWithValue("@LastName", txtLname.Text);
                        cmd.Parameters.AddWithValue("@PhoneNumber", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Major", txtMajor.Text);

                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }

            }
            else
            {
                using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
                {
                    con.Open();

                    string query = "INSERT INTO Staff ([ID], [FirstName], [LastName], [Email], [PhoneNumber], [Department], [Role]) " +
                                   "VALUES (@ID, @FirstName, @LastName, @Email, @PhoneNumber, @Department, @Role)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", txtID.Text);
                        cmd.Parameters.AddWithValue("@FirstName", txtFname.Text);
                        cmd.Parameters.AddWithValue("@LastName", txtLname.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@PhoneNumber", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@Department", txtdep.Text);

                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }
            }
        }
    }
}
