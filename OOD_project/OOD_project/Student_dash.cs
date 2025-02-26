using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOD_project
{
    public partial class Studentdashfrm : Form
    {

        private static int latestFeedbackID = 10;
        private static int latestmailID = 10;
        private string loggeduser ;



        public Studentdashfrm(string username)
        {
            InitializeComponent();
            populateEmails();
            loggeduser = username;
        }

        private void mailBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.mailBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);

            // Register the RowsAdded event handler
            this.alertsDataGridView.RowsAdded += new DataGridViewRowsAddedEventHandler(this.alertsDataGridView_RowsAdded);

        }

        private void populateEmails()
        {
            
        }

        private void Studentdashfrm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Files' table. You can move, or remove it, as needed.
            this.filesTableAdapter.Fill(this.database1DataSet.Files);
            // TODO: This line of code loads data into the 'database1DataSet.Alerts' table. You can move, or remove it, as needed.
            this.alertsTableAdapter.Fill(this.database1DataSet.Alerts);
            // TODO: This line of code loads data into the 'database1DataSet.Registration' table. You can move, or remove it, as needed.
            this.registrationTableAdapter.Fill(this.database1DataSet.Registration);
            // TODO: This line of code loads data into the 'database1DataSet.StudentSchedule' table. You can move, or remove it, as needed.
            this.studentScheduleTableAdapter.Fill(this.database1DataSet.StudentSchedule);
            // TODO: This line of code loads data into the 'database1DataSet.Mail' table. You can move, or remove it, as needed.
            /*            this.mailTableAdapter.Fill(this.database1DataSet.Mail);
            */

            string specificRecivermail = loggeduser;

            using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\202100115\\Downloads\\OOD_project\\OOD_project\\Database1.mdf;Integrated Security=True"))
            {
                con.Open();

                string query = "SELECT * FROM Mail WHERE Recivermail = @specificRecivermail";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add the parameter to the SqlCommand
                    cmd.Parameters.AddWithValue("@specificRecivermail", specificRecivermail);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        this.database1DataSet.Mail.Clear();
                        this.database1DataSet.Mail.Merge(dataTable);
                    }
                }
            }



            /* string specificRecivermail = loggeduser;

             using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\202100115\\Downloads\\OOD_project\\OOD_project\\Database1.mdf;Integrated Security=True"))
             {
                 con.Open();

                 string query = "SELECT * FROM Mail WHERE Recivermail = @specificRecivermail";

                 using (SqlCommand cmd = new SqlCommand(query, con))
                 {
                     cmd.Parameters.AddWithValue("@specificRecivermail", specificRecivermail);

                     using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                     {
                         DataTable dataTable = new DataTable();
                         adapter.Fill(dataTable);




                         this.mailDataGridView.DataSource = dataTable;
                     }
                 }
             }*/

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void chamgebtn_Click(object sender, EventArgs e)
        {
            string userid =  IDtxt.Text;
            string oldpass = currentpasstxt.Text;
            string newpass = newpasstxt.Text;

            using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\202100115\\Downloads\\OOD_project\\OOD_project\\Database1.mdf;Integrated Security=True"))
            {
                con.Open();

                string query = "Select Password From Registration where ID = @userid";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@userid", userid);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedPassword = reader["Password"].ToString();

                            if (storedPassword == oldpass)
                            {
                                reader.Close();

                                string updateQuery = "UPDATE Registration SET Password = @newpass WHERE ID = @userid";

                                using (SqlCommand updatecmd = new SqlCommand(updateQuery, con))
                                {
                                    updatecmd.Parameters.AddWithValue("@newpass", newpass);
                                    updatecmd.Parameters.AddWithValue("@userid", userid);

                                    updatecmd.ExecuteNonQuery();


                                    MessageBox.Show("The passowrd is updated");
                                }
                            }
                            else
                            {
                                MessageBox.Show("The current password is not matched");
                            }
                        }
                        else
                        {
                            MessageBox.Show("USer Id was not found");
                        }
                    }
                }
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            populateEmails();
        }

        private void Sendbtn_Click(object sender, EventArgs e)
        {
            string sendermail = Sendertxt.Text;
            string course = CourseIDtxt.Text;
            string feedb = Bodytxt.Text;

            using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\202100115\\Downloads\\OOD_project\\OOD_project\\Database1.mdf;Integrated Security=True"))
            {
                con.Open();

                latestFeedbackID++;

                string query = "Insert into Feedback Values(@feedbackID, @senderMail, @CourseID, @Feedback)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@feedbackID", latestFeedbackID);
                    cmd.Parameters.AddWithValue("@senderMail", sendermail);
                    cmd.Parameters.AddWithValue("@CourseID", course);
                    cmd.Parameters.AddWithValue("@Feedback", feedb);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Feedback is sent Thank you");

                }

            }


        }


        // This event handler is called every time a new row is added to the alertsDataGridView
        private void alertsDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            // Change the BackColor of the tab containing the alertsDataGridView
            tabPage5.BackColor = Color.Red;
        }


        private void buttonbtn_Click(object sender, EventArgs e)
        {
            string sendersmail =  sendersmailtxt.Text;
            string recivermail = reciversmailtxt.Text;
            string mailsubj = subjtxt.Text;
            string mailbody = mailbodytxt.Text;
            string sendmailto = txtSendmailto.Text;

            using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\202100115\\Downloads\\OOD_project\\OOD_project\\Database1.mdf;Integrated Security=True"))
            {
                con.Open();

                latestmailID++;

                string query = "Insert into Mail Values(@MailId, @SenderMail, @Recivermail, @Subject, @Body, @SendEmailTO)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@MailId", latestmailID);
                    cmd.Parameters.AddWithValue("@SenderMail", sendersmail);
                    cmd.Parameters.AddWithValue("@Recivermail", recivermail);
                    cmd.Parameters.AddWithValue("@Subject", mailsubj);
                    cmd.Parameters.AddWithValue("@Body", mailbody);
                    cmd.Parameters.AddWithValue("@SendEmailTO", sendmailto);



                    cmd.ExecuteNonQuery();

                    MessageBox.Show("The mail has been sent Sucessfully");

                }

            }


        }

        private void tabPage7_Click(object sender, EventArgs e)
        {
            /*string specificRecivermail = loggeduser;  

            using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\202100115\\Downloads\\OOD_project\\OOD_project\\Database1.mdf;Integrated Security=True"))
            {
                con.Open();

                string query = "SELECT * FROM Mail WHERE Recivermail = @specificRecivermail";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@specificRecivermail", specificRecivermail);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                       
                        *//*mailDataGridView.DataSource = dataTable;*//*

                        mailDataGridView.DataSource = dataTable;


                    }
                }
            }
            //mailDataGridView
            
                foreach (DataGridViewRow row in mailDataGridView.Rows)
                {
                    // Check if the row is not a header row and is not null
                    if (!row.IsNewRow && row.DataBoundItem != null)
                    {
                     if (row.Cells[0].Value.ToString().Trim() != specificRecivermail.Trim())
                    {
                        row.Visible = false;
                    }
                    }
                }*/
        }

        private void bodyTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        //notification tab
        private void alertsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        //notification tab
        private void tabPage5_Click(object sender, EventArgs e)
        {
            // Reset the BackColor when the tab is selected/viewed
            tabPage5.BackColor = DefaultBackColor; // Or any other color you deem as default

            // Update the alertsDataGridView in case you need to refresh the data displayed
            alertsDataGridView.Update();
        }


        private void studentScheduleDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                // Filter formats as needed or remove the filter to allow all types
                ofd.Filter = "All files|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = ofd.FileName;
                    string fileName = Path.GetFileName(filePath);
                    string fileLocation = "/path/to/your/uploaded/files/directory"; // Update this path
                    string fileSize = new FileInfo(filePath).Length.ToString();
                    string fileFormat = Path.GetExtension(filePath);
                    string dateModified = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    //int courseID = GetCourseID(); // You need to implement this based on how you get the CourseID

                    // Save the file to a server or database
                    string savedFilePath = SaveFileToSystem(filePath, fileLocation);

                    // Insert the file metadata into the database                                           //courseID
                    int fileID = InsertFileIntoDatabase(fileName, savedFilePath, fileSize, fileFormat, dateModified);

                    // Refresh the DataGridView to show the new file
                    filesDataGridView.Update();
                }
            }
        }

        private string SaveFileToSystem(string filePath, string fileLocation)
        {
            string targetFilePath = Path.Combine(fileLocation, Path.GetFileName(filePath));

            if (!Directory.Exists(fileLocation))
            {
                Directory.CreateDirectory(fileLocation);
            }

            File.Copy(filePath, targetFilePath, true);

            return targetFilePath; // Return the path where the file was saved
        }
        //int courseID
        private int InsertFileIntoDatabase(string fileName, string fileLocation, string fileSize, string fileFormat, string dateModified)
        {
            using (SqlConnection con = new SqlConnection("Your Connection String Here"))
            {
                con.Open();
                string query = "INSERT INTO Files (FileName, FileLocation, FileSize, FileFormat, DateModified, CourseID) OUTPUT INSERTED.FileID VALUES (@FileName, @FileLocation, @FileSize, @FileFormat, @DateModified, @CourseID)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@FileName", fileName);
                    cmd.Parameters.AddWithValue("@FileLocation", fileLocation);
                    cmd.Parameters.AddWithValue("@FileSize", fileSize);
                    cmd.Parameters.AddWithValue("@FileFormat", fileFormat);
                    cmd.Parameters.AddWithValue("@DateModified", dateModified);
                    // cmd.Parameters.AddWithValue("@CourseID", courseID);

                    // Execute the insert command and get the newly generated FileID
                    int fileID = (int)cmd.ExecuteScalar();
                    return fileID;
                }
            }
        }

        private void tabPage9_Click(object sender, EventArgs e)
        {
            filesDataGridView.Update();
        }

        private void filesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        /* fot the course ID
private int GetCourseID()
{
// Your logic to obtain the CourseID goes here
// This could involve prompting the user or getting it from the context in which the upload is happening
// return someCourseID; // Replace with actual course ID retrieval logic
}*/

    }
}
