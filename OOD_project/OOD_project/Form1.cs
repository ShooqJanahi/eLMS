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
    public partial class Adminfrm : Form
    {


        static int latestmailID = 1000;
        public Adminfrm()
        {
            InitializeComponent();

            // Register the RowsAdded event handler
            this.alertsDataGridView.RowsAdded += new DataGridViewRowsAddedEventHandler(this.alertsDataGridView_RowsAdded);
        }

        private void coursesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.coursesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);

        }

        private void Adminfrm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Branch' table. You can move, or remove it, as needed.
            this.branchTableAdapter.Fill(this.database1DataSet.Branch);
            // TODO: This line of code loads data into the 'database1DataSet.Mail' table. You can move, or remove it, as needed.
            this.mailTableAdapter.Fill(this.database1DataSet.Mail);
            // TODO: This line of code loads data into the 'database1DataSet.FeedBack' table. You can move, or remove it, as needed.
            this.feedBackTableAdapter.Fill(this.database1DataSet.FeedBack);
            // TODO: This line of code loads data into the 'database1DataSet.Alerts' table. You can move, or remove it, as needed.
            this.alertsTableAdapter.Fill(this.database1DataSet.Alerts);
            // TODO: This line of code loads data into the 'database1DataSet.Registration' table. You can move, or remove it, as needed.
            this.registrationTableAdapter.Fill(this.database1DataSet.Registration);
            // TODO: This line of code loads data into the 'database1DataSet.Staff' table. You can move, or remove it, as needed.
            this.staffTableAdapter.Fill(this.database1DataSet.Staff);
            // TODO: This line of code loads data into the 'database1DataSet.Student' table. You can move, or remove it, as needed.
            this.studentTableAdapter.Fill(this.database1DataSet.Student);
            // TODO: This line of code loads data into the 'database1DataSet.Courses' table. You can move, or remove it, as needed.
            this.coursesTableAdapter.Fill(this.database1DataSet.Courses);
            
            // Load the notifications
            LoadNotifications();


        }

        private void tabPage11_Click(object sender, EventArgs e)
        {
            staffDataGridView.Update();
        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void coursesBindingNavigator_RefreshItems(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            registrationDataGridView.Update();
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
            studentDataGridView.Update();
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

            registrationDataGridView.Update();
        }

        private void sendMailbtn_Click(object sender, EventArgs e)
        {
            string Sendermail = txtSenderMail.Text;
            string recivermail = txtReciverMail.Text;
            string sendmailto = txtSendMailto.Text;
            string mailsubject = txtSubject.Text;
            string mailbody = txtMailbody.Text;


            using (SqlConnection con = new SqlConnection("C:\\Users\\Shooq\\OneDrive\\Desktop\\OOD_project\\OOD_project\\OOD_project\\Database1.mdf"))
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

        private void labelAlertTitle_Click(object sender, EventArgs e)
        {

        }

        //Notification Tab
        private void alertsDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //Notification Tab
        private void bindingNavigator6_RefreshItems(object sender, EventArgs e)
        {

        }

        // This event handler is called every time a new row is added to the alertsDataGridView
        private void alertsDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            // Change the BackColor of the tab containing the alertsDataGridView
            tabPage5.BackColor = Color.Red;
        }

        //Notification tab
        private void tabPage10_Click(object sender, EventArgs e)
        {
            alertsDataGridView1.Update();

            // Reset the BackColor when the tab is selected/viewed
            tabPage10.BackColor = DefaultBackColor; // Or any other color you deem as default

            // Update the alertsDataGridView in case you need to refresh the data displayed
            alertsDataGridView.Update();
        }

        private void LoadNotifications()
        {
            using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Shooq\\OneDrive\\Desktop\\OOD_project\\OOD_project\\OOD_project\\Database1.mdf;Integrated Security=True"))
            {
                con.Open();
                string query = "SELECT * FROM Alerts"; // Replace with your actual table name and fields

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable AlertsTable = new DataTable();
                    adapter.Fill(AlertsTable);

                    // Assuming you have a DataGridView named notificationsDataGridView on your Notification tab
                    alertsDataGridView1.DataSource = AlertsTable;
                }
            }
        }

        // This could be a button click event handler for marking a notification as read
        private void markAsReadButton_Click(object sender, EventArgs e)
        {
            // Get the selected notification id from the DataGridView
            if (alertsDataGridView1.SelectedRows.Count > 0)
            {
                var id = alertsDataGridView1.SelectedRows[0].Cells["NotificationId"].Value; // Replace with your actual column name

                using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Shooq\\OneDrive\\Desktop\\OOD_project\\OOD_project\\OOD_project\\Database1.mdf;Integrated Security=True"))
                {
                    con.Open();
                    string query = "UPDATE Notifications SET IsRead = 1 WHERE NotificationId = @id"; // Replace with your actual table name and fields

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Refresh the DataGridView to show changes
                LoadNotifications();
            }
        }

        // This could be a button click event handler for deleting a notification
        private void deleteNotificationButton_Click(object sender, EventArgs e)
        {
            // Get the selected notification id from the DataGridView
            if (alertsDataGridView1.SelectedRows.Count > 0)
            {
                var id = alertsDataGridView1.SelectedRows[0].Cells["NotificationId"].Value; // Replace with your actual column name

                using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Shooq\\OneDrive\\Desktop\\OOD_project\\OOD_project\\OOD_project\\Database1.mdf;Integrated Security=True"))
                {
                    con.Open();
                    string query = "DELETE FROM Notifications WHERE NotificationId = @id"; // Replace with your actual table name and fields

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Refresh the DataGridView to show changes
                LoadNotifications();
            }
        }

        //Recive email tab
        // Event handler for the Reply button click in the Receive Mail tab
        private void ReplyButton_Click(object sender, EventArgs e)
        {
            // Check if there is a selected mail in the DataGridView
            if (mailDataGridView.SelectedRows.Count > 0)
            {
                // Retrieve sender and receiver email from the selected mail row
                string senderEmail = mailDataGridView.SelectedRows[0].Cells["SenderMail"].Value.ToString();
                string receiverEmail = mailDataGridView.SelectedRows[0].Cells["Recivermail"].Value.ToString();

                // Switch to the Send Mail tab
                tabControl1.SelectedTab = tabPage14; // Send Email

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

        //Recive email tab
        private void mailDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //Recive email tab
        private void bindingNavigator8_RefreshItems(object sender, EventArgs e)
        {

        }

        private void tabPage14_Click(object sender, EventArgs e)
        {

        }

        private void branchIDTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void branchDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void IDtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void coursesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void courseDecriptionTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
