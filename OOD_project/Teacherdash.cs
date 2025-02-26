using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static OOD_project.DBManager;

namespace OOD_project
{
    public partial class Teacherdashfrm : Form
    {
        private string loggeduser;
        string specificRecivermail;
        static int latestmailID = 10000;

        public Teacherdashfrm(string username)
        {
            InitializeComponent();
            loggeduser = username;
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
            //this.filesTableAdapter.Fill(this.database1DataSet.Files);
            // TODO: This line of code loads data into the 'database1DataSet.TutorSchedule' table. You can move, or remove it, as needed.
            this.tutorScheduleTableAdapter.Fill(this.database1DataSet.TutorSchedule);

            // TODO: This line of code loads data into the 'database1DataSet.Mail' table. You can move, or remove it, as needed.
            this.mailTableAdapter.Fill(this.database1DataSet.Mail);

            string specificusername = loggeduser;

            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "SELECT email FROM UserRegistration WHERE ID = @specificusername";

                using (SqlCommand sqlcmd = new SqlCommand(query, con))
                {

                    sqlcmd.Parameters.AddWithValue("@specificusername", specificusername);


                    object result = sqlcmd.ExecuteScalar();


                    if (result != null)
                    {
                        specificRecivermail = result.ToString();
                    }
                    else
                    {
                        Console.WriteLine("Email not found for the specified ID.");
                    }
                }
            }
            /*Console.WriteLine("The mail will be written after this line ");
            Console.WriteLine(specificRecivermail);*/

            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "SELECT * FROM Mail WHERE Recivermail = @specificRecivermail";

                using (SqlCommand sqlcmd = new SqlCommand(query, con))
                {
                    sqlcmd.Parameters.AddWithValue("@specificRecivermail", specificRecivermail);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlcmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        mailDataGridView.DataSource = dt;
                    }
                }
                con.Close();

            }

            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "SELECT * FROM TutorSchedule WHERE TutorID = @loggeduser";

                using (SqlCommand sqlcmd = new SqlCommand(query, con))
                {
                    sqlcmd.Parameters.AddWithValue("@loggeduser", loggeduser);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlcmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        tutorScheduleDataGridView.DataSource = dt;
                    }
                }

            }

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void chamgebtn_Click(object sender, EventArgs e)
        {
            string userid = IDtxt.Text;
            string oldpass = currentpasstxt.Text;
            string newpass = newpasstxt.Text;


            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "Select Password From UserRegistration where ID = @userid";

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


            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
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

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Loginfrm loginfrm = new Loginfrm();
            loginfrm.Show();
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void CourseIDcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /*private void PopulateCourseIDComboBox()
        {

            string columnName = "CourseID";

            // Clear existing items
            //CourseIDcomboBox.Items.Clear();

            // Adding a check to ensure the column exists to prevent exceptions
            if (tutorScheduleDataGridView.Columns[columnName] != null)
            {
                // Looping through the DataGridView and adding unique course IDs to the combo box
                HashSet<string> courseIds = new HashSet<string>();
                foreach (DataGridViewRow row in tutorScheduleDataGridView.Rows)
                {
                    if (!row.IsNewRow) // This check prevents trying to add the placeholder 'new' row
                    {
                        string id = row.Cells[columnName].Value?.ToString(); // Use ?.ToString() to handle null values
                        if (!string.IsNullOrEmpty(id))
                        {
                            courseIds.Add(id);
                        }
                    }
                }

                // Add the unique IDs to the combo box
                foreach (var id in courseIds)
                {
                    //CourseIDcomboBox.Items.Add(id);
                }
            }
            else
            {
                MessageBox.Show($"Column '{columnName}' does not exist in the DataGridView.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }*/
        private void RefreshFilesDataGridView()
        {
            string query = "SELECT * FROM Files";

            using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    filesDataGridView.DataSource = dt;
                }
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshFilesDataGridView();
        }
        private string selectedFilePath;
        private void CreateDirectoryWithPermissions(string path)
        {
            DirectoryInfo di = Directory.CreateDirectory(path);

            // Get the directory's access control list (ACL)
            DirectorySecurity ds = di.GetAccessControl();

            // Add the FileSystemAccessRule to grant read/write access to all users
            ds.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                            FileSystemRights.Modify |
                            FileSystemRights.Synchronize,
                            InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                            PropagationFlags.None,
                            AccessControlType.Allow));

            // Set the new access settings.
            di.SetAccessControl(ds);
        }
        private void Uploadbutton_Click(object sender, EventArgs e)
        {
            // Ensure a file has been selected
            if (string.IsNullOrEmpty(selectedFilePath))
            {
                MessageBox.Show("Please select a file to upload.", "No File Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Read the file into a byte array
            byte[] fileData;
            try
            {
                fileData = File.ReadAllBytes(selectedFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while reading the file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Set the target path where files should be saved
            string targetPath = @"C:\Path\To\Upload\Files";

            // Check if the directory exists, if not, create it with appropriate permissions
            if (!Directory.Exists(targetPath))
            {
                CreateDirectoryWithPermissions(targetPath);
            }

            // Get the file name from the textbox and combine it with the target path
            string fileName = FileNametextBox.Text.Trim();
            string fileFormat = Path.GetExtension(selectedFilePath).Trim('.');


            string targetFilePath = Path.Combine(targetPath, $"{fileName}.{fileFormat}");

            // Save the file to the target path
            try
            {
                File.WriteAllBytes(targetFilePath, fileData);
                MessageBox.Show("File uploaded successfully.", "Upload Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("You do not have permission to write to the specified directory: " + ex.Message, "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while saving the file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Insert the file metadata into the database
            // Assuming 'DatabaseConnection' is a class that provides a connection to your database
            string insertQuery = "INSERT INTO Files (FileName, FileSize, FileFormat, DateModified) VALUES (@FileName, @FileSize, @FileFormat, @DateModified)";

            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@FileName", fileName);
                    cmd.Parameters.AddWithValue("@FileSize", fileData.Length);
                    cmd.Parameters.AddWithValue("@FileFormat", Path.GetExtension(selectedFilePath));
                    cmd.Parameters.AddWithValue("@DateModified", File.GetLastWriteTime(selectedFilePath));

                    try
                    {
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("File metadata inserted successfully.", "Insert Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("File metadata insertion failed.", "Insert Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while inserting the file metadata to the database: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            // Refresh the DataGridView to show the new file
            RefreshFilesDataGridView();
        }

        private void DeleteFilebutton_Click(object sender, EventArgs e)
        {
            int fileidno;
            if (!int.TryParse(DeleteIDtxt.Text, out fileidno))
            {
                MessageBox.Show("Invalid file");
                return;
            }

            using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
            {
                conn.Open();

                string fileNameQuery = "SELECT FileName, FileFormat FROM Files WHERE FileID = @FileID";

                using (SqlCommand fileNameCmd = new SqlCommand(fileNameQuery, conn))
                {

                    fileNameCmd.Parameters.AddWithValue("@FileID", fileidno);

                    using (SqlDataReader reader = fileNameCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string fileName = reader["FileName"].ToString().Trim();
                            string fileFormat = reader["FileFormat"].ToString().Trim().Trim('.');
                            //fileFormat.Trim('.');
                            reader.Close();
                            string targetpath = "C:\\Path\\To\\Upload\\Files";

                            string filePath = Path.Combine(targetpath, $"{fileName}.{fileFormat}");
                           
                            if (File.Exists(filePath))
                            {
                                File.Delete(filePath);

                                string deleteQuery = "DELETE FROM Files WHERE FileID = @FileID";

                                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                                {
                                    cmd.Parameters.AddWithValue("@FileID", fileidno);
                                    cmd.ExecuteNonQuery();
                                }

                                MessageBox.Show("The file is deleted");
                            }
                            else
                            {
                                MessageBox.Show($"File not found at path: {filePath}");
                            }
                        }
                        else
                        {
                            MessageBox.Show("File not found.");
                        }
                    }
                }
            }
        }

        private void Browsebutton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*"; // Allow all file formats
            openFileDialog.Title = "Select a file to upload";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFilePath = openFileDialog.FileName;
                FilePathtextBox.Text = selectedFilePath; // Set the full file path in the textbox
            }
        }
    }
}
