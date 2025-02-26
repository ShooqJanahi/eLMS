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
    public partial class Loginfrm : Form
    {

        private string user;

        public string getsusers
        {
            get { return user; }    
            set { user = value; }
        }
        public Loginfrm()
        {
            InitializeComponent();
        }

        private void Loginbtn_Click(object sender, EventArgs e)
        {
            string enteruser = txtUsername.Text;
            string enterpass = txtpassword.Text;

            if (string.IsNullOrWhiteSpace(enterpass))
            {
                MessageBox.Show("Please enter a password.");
                return;
            }

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Shooq\\OneDrive\\Desktop\\OOD_project\\OOD_project\\OOD_project\\Database1.mdf;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "Select Role From Registration Where ID = @Username AND password = @Password";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", enteruser);
                    cmd.Parameters.AddWithValue("@Password", enterpass);

                    var roleObj = cmd.ExecuteScalar();
                    if (roleObj != null)
                    {
                        string role = roleObj.ToString();
                        switch (role)
                        {
                            case "Admin":
                                Adminfrm adminfrm = new Adminfrm();
                                adminfrm.Show();
                                break;
                            case "Student":
                                Studentdashfrm studentdashfrm = new Studentdashfrm(enteruser);
                                studentdashfrm.Show();
                                break;
                            case "Teacher":
                                Teacherdashfrm teacherdashfrm = new Teacherdashfrm();
                                teacherdashfrm.Show();
                                break;
                            default:
                                MessageBox.Show("Invalid role found.");
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Login failed. Please check your username and password.");
                    }
                }
            }
        }


        private void Registerbtn_Click(object sender, EventArgs e)
        {
            Registerfrm frm = new Registerfrm();
            frm.Show();
            this.Hide();
        }
    }
}
