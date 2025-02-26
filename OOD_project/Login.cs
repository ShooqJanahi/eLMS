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
    public partial class Loginfrm : Form
    {

        Interactor interactor = new Interactor();   

        

        public Loginfrm()
        {
            InitializeComponent();
        }

        private void Loginbtn_Click(object sender, EventArgs e)

        {
            string enteruser = txtUsername.Text;
            int enterpass;
            interactor.User = enteruser;
            Console.WriteLine("After thos the user will be printed");
            Console.WriteLine(interactor.User);
            if (!int.TryParse (txtpassword.Text, out enterpass))
            {
                MessageBox.Show("Invalid password format");
            }

            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "Select COUNT(*) From UserRegistration Where ID = @Username AND password = @Password AND Role = @Role AND userstatus = @Status";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", enteruser);
                    cmd.Parameters.AddWithValue("@Password", enterpass);
                    cmd.Parameters.AddWithValue("@Role", "Admin");
                    cmd.Parameters.AddWithValue("@status", "Accepted");
                    
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                         Adminfrm adminfrm = new Adminfrm();
                        adminfrm.Show();
                        this.Hide();
                        return;
                    }
                }
                
            }

            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "Select COUNT(*) From UserRegistration Where ID = @Username AND password = @Password AND Role = @Role AND userstatus = @Status";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", enteruser);
                    cmd.Parameters.AddWithValue("@Password", enterpass);
                    cmd.Parameters.AddWithValue("@Role", "Student");
                    cmd.Parameters.AddWithValue("@status", "Accepted");
                    //interactor.User = enteruser;
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        Studentdashfrm studentdashfrm = new Studentdashfrm(interactor.User);
                        studentdashfrm.Show();
                        this.Hide();
                        return;
                    }
                    
                   
                }
            }

            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "Select COUNT(*) From UserRegistration Where ID = @Username AND password = @Password AND Role = @Role AND userstatus = @Status";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", enteruser);
                    cmd.Parameters.AddWithValue("@Password", enterpass);
                    cmd.Parameters.AddWithValue("@Role", "Teacher");
                    cmd.Parameters.AddWithValue("@status", "Accepted");
                    
                    
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        Teacherdashfrm teacherdashfrm = new Teacherdashfrm(interactor.User);
                        teacherdashfrm.Show();
                        this.Hide();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("The username OR password entered is Wrong OR the Admin Still has not confirmed your regsitartion");
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

        private void Exitbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Loginfrm_Load(object sender, EventArgs e)
        {

        }
    }
}
