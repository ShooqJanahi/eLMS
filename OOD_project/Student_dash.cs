using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
    public partial class Studentdashfrm : Form
    {

        private static int latestFeedbackID = 10;
        private static int latestmailID = 10;
        private string loggeduser ;
        string specificRecivermail;


        public Studentdashfrm(string username)
        {
            InitializeComponent();
            loggeduser = username;
           /* Console.WriteLine("The student will be displayed after this ");
            Console.WriteLine(loggeduser);*/

        }

        private void mailBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.mailBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);

        }

        private void Studentdashfrm_Load(object sender, EventArgs e)
        {
            //PopulateCourseIDComboBox(); // Call this method to populate the combo box
        

            // TODO: This line of code loads data into the 'database1DataSet.Files' table. You can move, or remove it, as needed.
            //this.filesTableAdapter.Fill(this.database1DataSet.Files);
            // TODO: This line of code loads data into the 'database1DataSet.Alerts' table. You can move, or remove it, as needed.
            this.alertsTableAdapter.Fill(this.database1DataSet.Alerts);
            // TODO: This line of code loads data into the 'database1DataSet.Registration' table. You can move, or remove it, as needed.
            //this.registrationTableAdapter.Fill(this.database1DataSet.Registration);
            // TODO: This line of code loads data into the 'database1DataSet.StudentSchedule' table. You can move, or remove it, as needed.
           /* this.studentScheduleTableAdapter.Fill(this.database1DataSet.StudentSchedule);*/
            // TODO: This line of code loads data into the 'database1DataSet.Mail' table. You can move, or remove it, as needed.
            /*            this.mailTableAdapter.Fill(this.database1DataSet.Mail.DefaultView.RowFilter = $"Recivermail = '{loggeduser}'");
            */
            string specificusername = loggeduser;

            using(SqlConnection con = DatabaseConnection.Instance.GetConnection())
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

                string query = "SELECT * FROM StudentSchedule WHERE StudentID = @loggeduser";

                using (SqlCommand sqlcmd = new SqlCommand(query, con))
                {
                    sqlcmd.Parameters.AddWithValue("@loggeduser", loggeduser);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlcmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        studentScheduleDataGridView.DataSource = dt;
                    }
                }

            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void chamgebtn_Click(object sender, EventArgs e)
        {
            string userid =  IDtxt.Text;
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

                                string updateQuery = "UPDATE UserRegistration SET Password = @newpass WHERE ID = @userid";

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
            
        }

        private void Sendbtn_Click(object sender, EventArgs e)
        {
            string sendermail = Sendertxt.Text;
            string course = CourseIDtxt.Text;
            string feedb = Bodytxt.Text;

            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
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

        private void buttonbtn_Click(object sender, EventArgs e)
        {
            string sendersmail =  sendersmailtxt.Text;
            string recivermail = reciversmailtxt.Text;
            string mailsubj = subjtxt.Text;
            string mailbody = mailbodytxt.Text;
            string sendmailto = txtSendmailto.Text;

            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
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
            
            //mailDataGridView

            /*foreach (DataGridViewRow row in mailDataGridView.Rows)
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

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Loginfrm loginfrm = new Loginfrm();
            loginfrm.ShowDialog();
            
        }

        private void mailDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void refreshbtn_Click(object sender, EventArgs e)
        {


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

            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

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

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshFilesDataGridView();
        }

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
        // Implement this method to retrieve the actual directory path from your database.
        private string GetDirectoryPathFromDatabase()
        {
            // This should contain the logic to connect to the database and retrieve the directory path.
            // The actual implementation will depend on how you're storing this information.
            // For example:
            string query = "SELECT DirectoryPath FROM Configuration WHERE Setting = 'FileStoragePath'";
            // Execute the query and return the directory path.
            // ...

            // Placeholder return, replace with actual directory path retrieved from the database.
            return @"C:\Path\To\Upload\Files";
        }

        private void Downloadbutton_Click(object sender, EventArgs e)
        {
            string fileID = FileIDtextBox.Text; // The ID of the file to download

            // Query to get the file name and format for the given file ID
            string query = "SELECT FileName, FileFormat FROM Files WHERE FileID = @FileID";

            using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FileID", fileID);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string fileName = reader["FileName"].ToString().Trim();
                            string fileFormat = reader["FileFormat"].ToString().Trim();

                            fileFormat = fileFormat.Trim('.');

                            // Use the method to get the full server file path.
                            string serverFilePath = GetServerFilePath(fileName, fileFormat);
                            Console.WriteLine("Thsi is the file path of the file which is being downlaoded");
                            Console.WriteLine(serverFilePath);

                            if (File.Exists(serverFilePath))
                            {
                                SaveFileDialog saveFileDialog = new SaveFileDialog
                                {
                                    FileName = fileName + "." + fileFormat,
                                    Filter = $"{fileFormat} files (*.{fileFormat})|*.{fileFormat}|All files (*.*)|*.*",
                                    Title = "Save File"
                                };

                                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    // Get the path where the user wants to save the file
                                    string destinationPath = saveFileDialog.FileName;

                                    // Copy the file to the user-selected location
                                    File.Copy(serverFilePath, destinationPath, true);

                                    MessageBox.Show($"File downloaded successfully to {destinationPath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                            }
                            else
                            {
                                MessageBox.Show($"File not found on server. Path checked: {serverFilePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("File ID does not exist in the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    conn.Close();
                }
            }
        }

        // Reports Tab
        // Ensure this method is updated to use the actual path where files are stored, as recorded in the database.
        private string GetServerFilePath(string fileName, string fileFormat)
        {
            return Path.Combine("C:\\Path\\To\\Upload\\Files", $"{fileName}.{fileFormat}");
        }

        private void FilePathtextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void CourseIDcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void studentScheduleDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        /*private void PopulateCourseIDComboBox()
        {
            
            string columnName = "CourseID"; 

            // Clear existing items
            //CourseIDcomboBox.Items.Clear();

            // Adding a check to ensure the column exists to prevent exceptions
            if (studentScheduleDataGridView.Columns[columnName] != null)
            {
                // Looping through the DataGridView and adding unique course IDs to the combo box
                HashSet<string> courseIds = new HashSet<string>();
                foreach (DataGridViewRow row in studentScheduleDataGridView.Rows)
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

        private void alertsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void mailDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DeleteEmailbutton_Click(object sender, EventArgs e)
        {

        }

        private void Replybutton_Click(object sender, EventArgs e)
        {

        }

        private void DeleteAlertbutton_Click(object sender, EventArgs e)
        {

        }

        private void NotificationtabPage_Click(object sender, EventArgs e)
        {

        }
    }
}
