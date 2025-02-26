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
    public partial class Teacherdashfrm : Form
    {

        static int latestmailID = 10000;

        public Teacherdashfrm()
        {
            InitializeComponent();
        }

        private void mailBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.mailBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);

        }

        private void Teacherdashfrm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Alerts' table. You can move, or remove it, as needed.
            this.alertsTableAdapter.Fill(this.database1DataSet.Alerts);
            // TODO: This line of code loads data into the 'database1DataSet.Files' table. You can move, or remove it, as needed.
            this.filesTableAdapter.Fill(this.database1DataSet.Files);
            // TODO: This line of code loads data into the 'database1DataSet.TutorSchedule' table. You can move, or remove it, as needed.
            this.tutorScheduleTableAdapter.Fill(this.database1DataSet.TutorSchedule);
            // TODO: This line of code loads data into the 'database1DataSet.Mail' table. You can move, or remove it, as needed.
            this.mailTableAdapter.Fill(this.database1DataSet.Mail);

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void chamgebtn_Click(object sender, EventArgs e)
        {
            string userid = IDtxt.Text;
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

        private void sendMailbtn_Click(object sender, EventArgs e)
        {
            string Sendermail = txtSenderMail.Text;
            string recivermail = txtReciverMail.Text;
            string sendmailto = txtSendMailto.Text;
            string mailsubject = txtSubject.Text;
            string mailbody = txtMailbody.Text;


            using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\202100115\\Downloads\\OOD_project\\OOD_project\\Database1.mdf;Integrated Security=True"))
            {
                con.Open();

                latestmailID++;

                string query = "Insert into Mail Values(@MailId, @SenderMail, @Recivermail, @Subject, @Body, @SendEmailTO)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@MailId", latestmailID);
                    cmd.Parameters.AddWithValue("@SenderMail", Sendermail);
                    cmd.Parameters.AddWithValue("@Recivermail", recivermail);
                    cmd.Parameters.AddWithValue("@Subject", mailsubject);
                    cmd.Parameters.AddWithValue("@Body", mailbody);
                    cmd.Parameters.AddWithValue("@SendEmailTO", sendmailto);



                    cmd.ExecuteNonQuery();

                    MessageBox.Show("The mail has been sent Sucessfully");

                }

            }
        }

        
        
       

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        //Recive Email tab
        // Event handler for the Reply button click in the Receive Mail tab
        private void Replybutton_Click(object sender, EventArgs e)
        {
            // Check if there is a selected mail in the DataGridView
            if (mailDataGridView.SelectedRows.Count > 0)
            {
                // Retrieve sender and receiver email from the selected mail row
                string senderEmail = mailDataGridView.SelectedRows[0].Cells["SenderMail"].Value.ToString();
                string receiverEmail = mailDataGridView.SelectedRows[0].Cells["Recivermail"].Value.ToString();

                // Switch to the Send Mail tab
                tabControl1.SelectedTab = tabPage5; // Send Email

                // Populate the send mail fields with the reply info
                txtSenderMail.Text = receiverEmail; // The receiver becomes the new sender
                txtReciverMail.Text = senderEmail; // The original sender is now the receiver
                txtSubject.Text = "Re: " + mailDataGridView.SelectedRows[0].Cells["Subject"].Value.ToString(); // Prefix subject with "Re:"

                // Optionally, you could also pre-fill the body with the original message
                txtMailbody.Text = "\n\n--- Original Message ---\n" + mailDataGridView.SelectedRows[0].Cells["Body"].Value.ToString();
            }
            else
            {
                MessageBox.Show("Please select an email to reply to.");
            }
        }

        private void mailDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {
            mailDataGridView.Update();
        }

        //REports Tab
        private void tabPage8_Click(object sender, EventArgs e)
        {
            filesDataGridView.Update();
        }

        //Reports Tab
        private void filesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //REport Tab
        private void bindingNavigator2_RefreshItems(object sender, EventArgs e)
        {

        }

        //Reports Tab
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

        private void tabPage4_Click(object sender, EventArgs e)
        {
            alertsDataGridView.Update();
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
