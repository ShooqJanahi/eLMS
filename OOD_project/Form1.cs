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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static OOD_project.DBManager;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace OOD_project
{
    public partial class Adminfrm : Form
    {
        String loggeduser;
        String specificRecivermail;

        static int latestmailID = 1000;
        public Adminfrm()
        {
            InitializeComponent();
            this.alertsDataGridView.RowsAdded += new DataGridViewRowsAddedEventHandler(this.alertsDataGridView_RowsAdded);

            FilePathtextBox.ReadOnly = true; // Set the textbox as read-only
        }

        private void coursesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.coursesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);

        }

        private void Adminfrm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.TutorSchedule' table. You can move, or remove it, as needed.
            this.tutorScheduleTableAdapter.Fill(this.database1DataSet.TutorSchedule);
            // TODO: This line of code loads data into the 'database1DataSet.StudentSchedule' table. You can move, or remove it, as needed.
            this.studentScheduleTableAdapter.Fill(this.database1DataSet.StudentSchedule);
            // TODO: This line of code loads data into the 'database1DataSet.UserRegistration' table. You can move, or remove it, as needed.
            this.userRegistrationTableAdapter.Fill(this.database1DataSet.UserRegistration);
            RefreshFilesDataGridView(); // Load initial data into filesDataGridView
            // TODO: This line of code loads data into the 'database1DataSet.Files' table. You can move, or remove it, as needed.
           // this.filesTableAdapter.Fill(this.database1DataSet.Files);
            // TODO: This line of code loads data into the 'database1DataSet.Branch' table. You can move, or remove it, as needed.
            this.branchTableAdapter.Fill(this.database1DataSet.Branch);
            // TODO: This line of code loads data into the 'database1DataSet.Mail' table. You can move, or remove it, as needed.
            //this.mailTableAdapter.Fill(this.database1DataSet.Mail);
            // TODO: This line of code loads data into the 'database1DataSet.FeedBack' table. You can move, or remove it, as needed.
            this.feedBackTableAdapter.Fill(this.database1DataSet.FeedBack);
            // TODO: This line of code loads data into the 'database1DataSet.Alerts' table. You can move, or remove it, as needed.
            this.alertsTableAdapter.Fill(this.database1DataSet.Alerts);
            // TODO: This line of code loads data into the 'database1DataSet.Registration' table. You can move, or remove it, as needed.
            //this.registrationTableAdapter.Fill(this.database1DataSet.Registration);
            // TODO: This line of code loads data into the 'database1DataSet.Staff' table. You can move, or remove it, as needed.
            this.staffTableAdapter.Fill(this.database1DataSet.Staff);
            // TODO: This line of code loads data into the 'database1DataSet.Student' table. You can move, or remove it, as needed.
            this.studentTableAdapter.Fill(this.database1DataSet.Student);
            // TODO: This line of code loads data into the 'database1DataSet.Courses' table. You can move, or remove it, as needed.
            this.coursesTableAdapter.Fill(this.database1DataSet.Courses);


            string specificusername = loggeduser; // This is a string representation of an integer, like "123"

            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "SELECT email FROM Registration WHERE ID = @specificusername";

                using (SqlCommand sqlcmd = new SqlCommand(query, con))
                {
                    // Convert the specificusername to an integer before adding it as a parameter
                    int userId;
                    if (int.TryParse(specificusername, out userId))
                    {
                        sqlcmd.Parameters.AddWithValue("@specificusername", userId);

                        object result = sqlcmd.ExecuteScalar();

                        if (result != null)
                        {
                            string specificReceiverEmail = result.ToString();
                            // You can now use specificReceiverEmail as needed within your application
                        }
                        else
                        {
                            Console.WriteLine("Email not found for the specified ID.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("The user ID is not in a correct format.");
                    }
                }
            }

            /*Console.WriteLine("The mail will be written after this line ");
            Console.WriteLine(specificRecivermail);*/

            // Ensure that 'specificRecivermail' contains the correct email address you are querying for.
            // It must be declared and assigned a value before this block of code.

            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "SELECT * FROM Mail WHERE Receivermail = @specificReceivermail"; // Make sure the field name is spelled correctly.

                using (SqlCommand sqlCmd = new SqlCommand(query, con))
                {
                    // Make sure that the 'specificRecivermail' variable contains the email address you want to look up.
                    // If 'specificRecivermail' is null, the parameter will not be supplied, causing the exception.
                    if (specificRecivermail != null)
                    {
                        sqlCmd.Parameters.AddWithValue("@specificReceivermail", specificRecivermail);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            mailDataGridView.DataSource = dt; // Make sure 'mailDataGridView' is the correct name of your DataGridView.
                        }
                    }
                    else
                    {
                        // Handle the case where 'specificRecivermail' is null.
                        // For example, you might want to inform the user or log an error.
                        Console.WriteLine("The 'specificReceivermail' variable is null.");
                    }
                }
                con.Close();
            }




        }

        // This event handler is called every time a new row is added to the alertsDataGridView
        private void alertsDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

            NotificationtabPage.BackColor = Color.Red;
        }

        //Notification tab
        private void tabPage10_Click(object sender, EventArgs e)
        {
            alertsDataGridView.Update();

            // Reset the BackColor when the tab is selected/viewed
            NotificationtabPage.BackColor = DefaultBackColor; // Or any other color you deem as default

            // Update the alertsDataGridView in case you need to refresh the data displayed
            alertsDataGridView.Update();
        }

        private void tabPage11_Click(object sender, EventArgs e)
        {
           
        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void coursesBindingNavigator_RefreshItems(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            userRegistrationDataGridView.Update();
        }

        private void registrationDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
            coursesDataGridView.Update();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {
            
        }

        private void tabPage12_Click(object sender, EventArgs e)
        {
            alertsDataGridView.Update();
        }

        private void tabPage6_Click(object sender, EventArgs e)
        {
            feedBackDataGridView.Update();
        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void tabPage8_Click(object sender, EventArgs e)
        {
            branchDataGridView.Update();
        }

        private void tabPage9_Click(object sender, EventArgs e)
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

            userRegistrationDataGridView.Update();
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

        private void txtMailbody_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSubject_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSendMailto_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtReciverMail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSenderMail_TextChanged(object sender, EventArgs e)
        {

        }

        private void Bodytxt_Click(object sender, EventArgs e)
        {

        }

        private void Subjtxt_Click(object sender, EventArgs e)
        {

        }

        private void ReciverMailtxt_Click(object sender, EventArgs e)
        {

        }

        private void SendMailtotxt_Click(object sender, EventArgs e)
        {

        }

        private void Sendermailtxt_Click(object sender, EventArgs e)
        {

        }

        private void Logoutbtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Loginfrm loginfrm = new Loginfrm();
            loginfrm.Show();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }

        private void AddbtnUser_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "INSERT INTO UserRegistration VALUES (@ID, @Fnaem, @Lname, @Phone, @password, @email, @department, @Major, @Role, @userstatus)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", iDTextBox2.Text);
                    cmd.Parameters.AddWithValue("@Fnaem", fnaemTextBox.Text);
                    cmd.Parameters.AddWithValue("@Lname", lnameTextBox.Text);
                    cmd.Parameters.AddWithValue("@Phone", int.Parse(phoneTextBox1.Text));
                    cmd.Parameters.AddWithValue("@password", int.Parse(passwordTextBox.Text));
                    cmd.Parameters.AddWithValue("@email", emailTextBox1.Text);
                    cmd.Parameters.AddWithValue("@department", departmentTextBox1.Text);
                    cmd.Parameters.AddWithValue("@Major", majorTextBox1.Text);
                    cmd.Parameters.AddWithValue("@Role", roleTextBox1.Text);
                    cmd.Parameters.AddWithValue("@userstatus", userstatusTextBox.Text);

                    cmd.ExecuteNonQuery();
                }


                con.Close();

                MessageBox.Show("The user has been added successfully");
            }
        }

        // Event handler for the Update button click
        // Event handler for the Update button click
        private void UpdatebtnUser_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "UPDATE UserRegistration SET Fnaem = @Fnaem, Lname = @Lname, Phone = @Phone, " +
                               "password = @password, email = @email, department = @department, " +
                               "Major = @Major, Role = @Role, userstatus = @userstatus WHERE ID = @ID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", iDTextBox2.Text);
                    cmd.Parameters.AddWithValue("@Fnaem", fnaemTextBox.Text);
                    cmd.Parameters.AddWithValue("@Lname", lnameTextBox.Text);
                    cmd.Parameters.AddWithValue("@Phone", int.Parse(phoneTextBox1.Text));
                    cmd.Parameters.AddWithValue("@password", int.Parse(passwordTextBox.Text));
                    cmd.Parameters.AddWithValue("@email", emailTextBox1.Text);
                    cmd.Parameters.AddWithValue("@department", departmentTextBox1.Text);
                    cmd.Parameters.AddWithValue("@Major", majorTextBox1.Text);
                    cmd.Parameters.AddWithValue("@Role", roleTextBox1.Text);
                    cmd.Parameters.AddWithValue("@userstatus", userstatusTextBox.Text);

                    cmd.ExecuteNonQuery();
                }

                con.Close();

                MessageBox.Show("The user has been updated successfully");
            }
        }


        private void DeletebtnUser_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "DELETE FROM UserRegistration WHERE ID = @ID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", iDTextBox2.Text);

                    cmd.ExecuteNonQuery();
                }

                con.Close();

                MessageBox.Show("The user has been deleted successfully");
            }
        }

        private void refreshbtnUsers_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("Select * from UserRegistration", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                userRegistrationDataGridView.DataSource = dt;
            }
        }
        private void AddbtnFeed_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "INSERT INTO FeedBack Values (@feedbackID, @senderMail, @CourseID, @Feedback)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@feedbackID", feedbackIDTextBox.Text);
                    cmd.Parameters.AddWithValue("@senderMail", senderMailTextBox.Text);
                    cmd.Parameters.AddWithValue("@CourseID", courseIDTextBox1.Text);
                    cmd.Parameters.AddWithValue("@Feedback", feedbackTextBox.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("The Feedback is addded");
            }
        }

        private void UpdatebtnFeed_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "UPDATE FeedBack SET senderMail = @senderMail, CourseID = @CourseID, Feedback = @Feedback WHERE feedbackID = @feedbackID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@feedbackID", feedbackIDTextBox.Text);
                    cmd.Parameters.AddWithValue("@senderMail", senderMailTextBox.Text);
                    cmd.Parameters.AddWithValue("@CourseID", courseIDTextBox1.Text);
                    cmd.Parameters.AddWithValue("@Feedback", feedbackTextBox.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("The Feedback is updated");
            }
        }

        private void DeletebtnFeed_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "DELETE FROM FeedBack WHERE feedbackID = @feedbackID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@feedbackID", feedbackIDTextBox.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("The Feedback is deleted");
            }
        }

        private void refreshbtnFeed_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("Select * from Feedback", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                feedBackDataGridView.DataSource = dt;
            }
        }

        private void AddbtnBranch_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "INSERT INTO Branch VALUES (@BranchID, @BranchName, @Location, @Collage, @NumberOfStudent)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@BranchID", branchIDTextBox.Text);
                    cmd.Parameters.AddWithValue("@BranchName", branchNameTextBox.Text);
                    cmd.Parameters.AddWithValue("@Location", locationTextBox1.Text);
                    cmd.Parameters.AddWithValue("@Collage", collageTextBox.Text);
                    cmd.Parameters.AddWithValue("@NumberOfStudent", numberOfStudentTextBox.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("The branch is added");
            }
        }

        private void UpdatebtnBaranch_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "UPDATE Branch SET BranchName = @BranchName, Location = @Location, Collage = @Collage, NumberOfStudent = @NumberOfStudent WHERE BranchID = @BranchID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@BranchID", branchIDTextBox.Text);
                    cmd.Parameters.AddWithValue("@BranchName", branchNameTextBox.Text);
                    cmd.Parameters.AddWithValue("@Location", locationTextBox1.Text);
                    cmd.Parameters.AddWithValue("@Collage", collageTextBox.Text);
                    cmd.Parameters.AddWithValue("@NumberOfStudent", numberOfStudentTextBox.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("The branch is updated");
            }
        }
        private void DeletebtnBranch_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "DELETE FROM Branch WHERE BranchID = @BranchID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@BranchID", branchIDTextBox.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("The branch is deleted");
            }
        }

        private void RefreshbtnBranch_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("Select * from Branch", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                branchDataGridView.DataSource = dt;
            }
        }
        private void AddbtnAlert_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "INSERT INTO Alerts VALUES (@AlertID, @Priority, @AlertTitle, @Message, @SendTo)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@AlertID", alertIDTextBox.Text);
                    cmd.Parameters.AddWithValue("@Priority", priorityTextBox.Text);
                    cmd.Parameters.AddWithValue("@AlertTitle", alertTitleTextBox.Text);
                    cmd.Parameters.AddWithValue("@Message", messageTextBox.Text);
                    cmd.Parameters.AddWithValue("@SendTo", sendToTextBox.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("The alert is added");
            }
        }

        private void UpdatebtnAlert_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "UPDATE Alerts SET Priority = @Priority, AlertTitle = @AlertTitle, Message = @Message, SendTo = @SendTo WHERE AlertID = @AlertID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@AlertID", alertIDTextBox.Text);
                    cmd.Parameters.AddWithValue("@Priority", priorityTextBox.Text);
                    cmd.Parameters.AddWithValue("@AlertTitle", alertTitleTextBox.Text);
                    cmd.Parameters.AddWithValue("@Message", messageTextBox.Text);
                    cmd.Parameters.AddWithValue("@SendTo", sendToTextBox.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("The alert has been updated successfully");
            }
        }
        private void DeletebtnAlert_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "DELETE FROM Alerts WHERE AlertID = @AlertID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@AlertID", alertIDTextBox.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("The alert has been deleted successfully");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("Select * from Alerts", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                alertsDataGridView.DataSource = dt;
            }
        }
        private void AddbtnCourse_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "INSERT INTO Courses (CourseID, Subj, CourseName, CourseDecription, Credit, StartTime, EndTime, Day, Location, Campus, TutorID, NumberOfRegisteredStudent) " +
                               "VALUES (@CourseID, @Subj, @CourseName, @CourseDecription, @Credit, @StartTime, @EndTime, @Day, @Location, @Campus, @TutorID, @NumberOfRegisteredStudent)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CourseID", courseIDTextBox.Text);
                    cmd.Parameters.AddWithValue("@Subj", subjTextBox.Text);
                    cmd.Parameters.AddWithValue("@CourseName", courseNameTextBox.Text);
                    cmd.Parameters.AddWithValue("@CourseDecription", courseDecriptionTextBox.Text);
                    cmd.Parameters.AddWithValue("@Credit", decimal.Parse(creditTextBox.Text));
                    cmd.Parameters.AddWithValue("@StartTime", startTimeTextBox.Text);
                    cmd.Parameters.AddWithValue("@EndTime", endTimeTextBox.Text);
                    cmd.Parameters.AddWithValue("@Day", dayTextBox.Text);
                    cmd.Parameters.AddWithValue("@Location", locationTextBox.Text);
                    cmd.Parameters.AddWithValue("@Campus", campusTextBox.Text);
                    cmd.Parameters.AddWithValue("@TutorID", tutorIDTextBox.Text);
                    cmd.Parameters.AddWithValue("@NumberOfRegisteredStudent", int.Parse(numberOfRegisteredStudentTextBox.Text));

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("The course has been added successfully");
            }
        }
        private void UpdatebtnCourse_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "UPDATE Courses SET Subj = @Subj, CourseName = @CourseName, CourseDecription = @CourseDecription, " +
                               "Credit = @Credit, StartTime = @StartTime, EndTime = @EndTime, Day = @Day, " +
                               "Location = @Location, Campus = @Campus, TutorID = @TutorID, " +
                               "NumberOfRegisteredStudent = @NumberOfRegisteredStudent WHERE CourseID = @CourseID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CourseID", courseIDTextBox.Text);
                    cmd.Parameters.AddWithValue("@Subj", subjTextBox.Text);
                    cmd.Parameters.AddWithValue("@CourseName", courseNameTextBox.Text);
                    cmd.Parameters.AddWithValue("@CourseDecription", courseDecriptionTextBox.Text);
                    cmd.Parameters.AddWithValue("@Credit", decimal.Parse(creditTextBox.Text));
                    cmd.Parameters.AddWithValue("@StartTime", startTimeTextBox.Text);
                    cmd.Parameters.AddWithValue("@EndTime", endTimeTextBox.Text);
                    cmd.Parameters.AddWithValue("@Day", dayTextBox.Text);
                    cmd.Parameters.AddWithValue("@Location", locationTextBox.Text);
                    cmd.Parameters.AddWithValue("@Campus", campusTextBox.Text);
                    cmd.Parameters.AddWithValue("@TutorID", tutorIDTextBox.Text);
                    cmd.Parameters.AddWithValue("@NumberOfRegisteredStudent", int.Parse(numberOfRegisteredStudentTextBox.Text));

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("The course has been updated successfully");
            }
        }
        private void DeletebtnCourse_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                // Check for associated files first
                string checkQuery = "SELECT COUNT(*) FROM Files WHERE CourseID = @CourseID";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                {
                    checkCmd.Parameters.AddWithValue("@CourseID", courseIDTextBox.Text);
                    int count = (int)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("There are files associated with this course. Please delete the files first.");
                        return;
                    }
                }

                // If no associated files, proceed to delete the course
                string query = "DELETE FROM Courses WHERE CourseID = @CourseID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CourseID", courseIDTextBox.Text);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("The course has been deleted successfully");
            }
        }


        private void researchbtnCourse_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("Select * from Courses", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                coursesDataGridView.DataSource = dt;
            }
        }

        //Report tab
        // Class-level variable to store the selected file path
        private string selectedFilePath;
        

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

        //creating the directory to save the file in
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













        //Report tab
        private void FileNametextBox_TextChanged(object sender, EventArgs e)
        {

        }
        //Report tab
        private void FileIDtextBox_TextChanged(object sender, EventArgs e)
        {

        }
        
       
        



        // Reports Tab
        // Ensure this method is updated to use the actual path where files are stored, as recorded in the database.
        private string GetServerFilePath(string fileName, string fileFormat)
        {
            // Retrieve the directory path from the database where the files are stored.
            // You need to implement this part based on your database schema.
            string directoryPath = GetDirectoryPathFromDatabase();

            // Append the dot before the file format if it's not there.
            string fullFileName = fileName + (fileFormat.StartsWith(".") ? "" : ".") + fileFormat;

            // Combine the directory path with the full file name.
            return Path.Combine(directoryPath, fullFileName);
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


        //Report tab
        private void filesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //Report tab
        private void tabPage13_Click(object sender, EventArgs e)
        {

        }

        
      
        
        
        //Report tab (upload)
        private void FilePathtextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void mailDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
         {

         }

        private void filesDataGridView_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshFilesDataGridView();
        }


        private void DeleteFilebutton_Click(object sender, EventArgs e)
        {
            int fileidno;
            if (!int.TryParse(deletefiletxt.Text, out fileidno))
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
        private string GetFilePathFromDatabase(int fileID)
        {
            string filePath = string.Empty;
            string query = "SELECT FilePath FROM Files WHERE FileID = @FileID";

            using (SqlConnection conn = DatabaseConnection.Instance.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FileID", fileID);

                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            filePath = Convert.ToString(result);
                        }
                        else
                        {
                            MessageBox.Show("No file path found for the specified ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while retrieving the file path: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            return filePath;
        }

        private void DownloadFileTitlelabel_Click(object sender, EventArgs e)
        {

        }

        private void UploadFileTitlelabel_Click(object sender, EventArgs e)
        {

        }

        private void FileIDlabel_Click(object sender, EventArgs e)
        {

        }

        private void FileNamelabel_Click(object sender, EventArgs e)
        {

        }

        private void RecivedMailTitlelabel_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void mailDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HighlightNotificationTab();
        }

        //for the Alerts
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HighlightNotificationTab();
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

        // The method to change the Notification tab color when a new email/alert is clicked
        private void HighlightNotificationTab()
        {
            //tabControl1.BackColor = Color.Red; // Change the background color of the tab control
        }

        // The method to reset the Notification tab color when it's clicked
        
       

        

        private void phone_TextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void userRegistrationDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void emailTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void departmentTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void majorTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void role_TextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddbtnStu_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                // First, validate the StudentID
                string studentValidationQuery = "SELECT COUNT(*) FROM Student WHERE StudentID = @StudentID";
                using (SqlCommand studentValidationCmd = new SqlCommand(studentValidationQuery, con))
                {
                    // Replace "studentIDTextBox.Text" with the actual control that contains the student ID.
                    studentValidationCmd.Parameters.AddWithValue("@StudentID", studentIDTextBox.Text);

                   /* int NumberOfRegisteredStudent = Convert.ToInt32(studentValidationCmd.ExecuteScalar());
                    if (NumberOfRegisteredStudent == 0)
                    {
                        MessageBox.Show("The Student ID does not exist in the Student table.");
                        return;
                    }*/
                }

                // Validate the CourseID as before
                string courseValidationQuery = "SELECT COUNT(*) FROM Courses WHERE CourseID = @CourseID";
                using (SqlCommand courseValidationCmd = new SqlCommand(courseValidationQuery, con))
                {
                    courseValidationCmd.Parameters.AddWithValue("@CourseID", courseIDTextBox3.Text);

                    int courseCount = Convert.ToInt32(courseValidationCmd.ExecuteScalar());
                    if (courseCount == 0)
                    {
                        MessageBox.Show("The Course ID does not exist in the Courses table.");
                        return;
                    }
                }

                // If both IDs exist, proceed with the insert
                string query = "INSERT INTO StudentSchedule (StudentID, CourseID, CourseName, Credit, StartTime, EndTime, Day, Location, Campus, TutorName) " +
                                "VALUES (@StudentID, @CourseID, @CourseName, @Credit, @StartTime, @EndTime, @Day, @Location, @Campus, @TutorName)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentIDTextBox.Text);
                    cmd.Parameters.AddWithValue("@CourseID", courseIDTextBox3.Text);
                    cmd.Parameters.AddWithValue("@CourseName", courseNameTextBox.Text); // Make sure this control exists and contains the course name
                    cmd.Parameters.AddWithValue("@Credit", creditTextBox.Text); // Assuming creditTextBox is the control for credit
                    cmd.Parameters.AddWithValue("@StartTime", startTimeTextBox.Text); // Assuming startTimeTextBox is the control for start time
                    cmd.Parameters.AddWithValue("@EndTime", endTimeTextBox.Text); // Assuming endTimeTextBox is the control for end time
                    cmd.Parameters.AddWithValue("@Day", dayTextBox.Text); // Assuming dayTextBox is the control for day
                    cmd.Parameters.AddWithValue("@Location", locationTextBox.Text); // Assuming locationTextBox is the control for location
                    cmd.Parameters.AddWithValue("@Campus", campusTextBox.Text); // Assuming campusTextBox is the control for campus
                    cmd.Parameters.AddWithValue("@TutorName", tutorNameTextBox.Text); // Assuming tutorNameTextBox is the control for tutor name

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("The student schedule has been added successfully.");
            }
        }





        private void UpdatebtnStu_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                // Check if the new CourseID exists in the Courses table
                string courseValidationQuery = "SELECT COUNT(*) FROM Courses WHERE CourseID = @CourseID";
                using (SqlCommand courseValidationCmd = new SqlCommand(courseValidationQuery, con))
                {
                    courseValidationCmd.Parameters.AddWithValue("@CourseID", courseIDTextBox3.Text);

                    int count = Convert.ToInt32(courseValidationCmd.ExecuteScalar());
                    if (count == 0)
                    {
                        MessageBox.Show("The Course ID does not exist in the Courses table.");
                        return;
                    }
                }

                // Proceed with the update if the CourseID is valid
                string query = "UPDATE StudentSchedule SET StudentID = @StudentID, CourseID = @CourseID, " +
                               "CourseName = @CourseName, Credit = @Credit, StartTime = @StartTime, " +
                               "EndTime = @EndTime, Day = @Day, Location = @Location, Campus = @Campus, " +
                               "TutorName = @TutorName WHERE ScheduleID = @OriginalScheduleID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Assuming originalScheduleID and other parameters are obtained from the form controls
                    int originalScheduleID = int.Parse(scheduleIDTextBox1.Text); // Replace with your actual control that contains the Schedule ID

                    cmd.Parameters.AddWithValue("@OriginalScheduleID", originalScheduleID);
                    cmd.Parameters.AddWithValue("@StudentID", studentIDTextBox.Text);
                    cmd.Parameters.AddWithValue("@CourseID", courseIDTextBox3.Text);
                    cmd.Parameters.AddWithValue("@CourseName", courseNameTextBox1.Text);
                    cmd.Parameters.AddWithValue("@Credit", decimal.Parse(creditTextBox1.Text));
                    cmd.Parameters.AddWithValue("@StartTime", startTimeTextBox1.Text);
                    cmd.Parameters.AddWithValue("@EndTime", endTimeTextBox1.Text);
                    cmd.Parameters.AddWithValue("@Day", dayTextBox1.Text);
                    cmd.Parameters.AddWithValue("@Location", locationTextBox2.Text);
                    cmd.Parameters.AddWithValue("@Campus", campusTextBox1.Text);
                    cmd.Parameters.AddWithValue("@TutorName", tutorNameTextBox.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("The student schedule has been updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("The student schedule could not be updated.");
                    }
                }
            }
        }


        private void DeletebtnStu_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "DELETE FROM StudentSchedule WHERE ScheduleID = @ScheduleID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Retrieve the ScheduleID to be deleted
                    int scheduleIDToDelete;
                    if (!int.TryParse(scheduleIDTextBox1.Text, out scheduleIDToDelete))
                    {
                        MessageBox.Show("Invalid Schedule ID");
                        return;
                    }

                    cmd.Parameters.AddWithValue("@ScheduleID", scheduleIDToDelete);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("The student schedule has been deleted successfully");
            }
        }

        private void AddbtnTutor_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                // Turn on IDENTITY_INSERT if you need to insert explicit values into an identity column
                SqlCommand identityInsertCmd = new SqlCommand("SET IDENTITY_INSERT TutorSchedule ON", con);
                identityInsertCmd.ExecuteNonQuery();

                string query = "INSERT INTO TutorSchedule (ScheduleID, TutorID, CourseID) VALUES (@ScheduleID, @TutorID, @CourseID)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ScheduleID", scheduleIDTextBox.Text);
                    cmd.Parameters.AddWithValue("@TutorID", tutorIDTextBox1.Text);
                    cmd.Parameters.AddWithValue("@CourseID", courseIDTextBox2.Text);

                    cmd.ExecuteNonQuery();
                }

                // Turn off IDENTITY_INSERT after the operation
                SqlCommand identityInsertOffCmd = new SqlCommand("SET IDENTITY_INSERT TutorSchedule OFF", con);
                identityInsertOffCmd.ExecuteNonQuery();

                MessageBox.Show("The tutor schedule has been added successfully");
            }
        }


        private void UpdatebtnTutor_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "UPDATE TutorSchedule SET TutorID = @TutorID, CourseID = @CourseID WHERE ScheduleID = @ScheduleID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Assuming you have TextBox controls for TutorID, CourseID, and ScheduleID
                    cmd.Parameters.AddWithValue("@ScheduleID", scheduleIDTextBox.Text);
                    cmd.Parameters.AddWithValue("@TutorID", tutorIDTextBox1.Text);
                    cmd.Parameters.AddWithValue("@CourseID", courseIDTextBox2.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("The tutor schedule has been updated successfully");
            }
        }

        private void DeletebtnTutor_Click(object sender, EventArgs e)
        {
            int scheduleID;
            if (!int.TryParse(scheduleIDTextBox.Text, out scheduleID))
            {
                MessageBox.Show("Invalid schedule ID");
                return;
            }

            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                con.Open();

                string query = "DELETE FROM TutorSchedule WHERE ScheduleID = @ScheduleID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ScheduleID", scheduleID);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("The tutor schedule has been deleted successfully");
            }
        }

        //Refresh Button
        // Refresh Button
        private void button2_Click(object sender, EventArgs e)
        {
            FillStudentScheduleData();
        }

        // Method to fill the StudentSchedule data from the database
        private void FillStudentScheduleData()
        {
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM StudentSchedule";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        DataTable dt = new DataTable();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                            studentScheduleDataGridView.DataSource = dt;
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show($"A database error occurred: {sqlEx.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FillTutorScheduleData();
        }

        // This is a placeholder method. You'll need to implement it to pull data from your database and bind it to your DataGridView.
        private void FillTutorScheduleData()
        {
            // Assuming DatabaseConnection.Instance.GetConnection() gives you an open SQL connection
            using (SqlConnection con = DatabaseConnection.Instance.GetConnection())
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM TutorSchedule"; // Replace with your actual SQL query
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        DataTable dt = new DataTable();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                            tutorScheduleDataGridView.DataSource = dt;
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show($"Database error: {sqlEx.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }

    }
}
