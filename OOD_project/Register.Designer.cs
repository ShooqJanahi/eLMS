namespace OOD_project
{
    partial class Registerfrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.database1DataSet = new OOD_project.Database1DataSet();
            this.registrationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.registrationTableAdapter = new OOD_project.Database1DataSetTableAdapters.RegistrationTableAdapter();
            this.tableAdapterManager = new OOD_project.Database1DataSetTableAdapters.TableAdapterManager();
            this.Registertxt = new System.Windows.Forms.Label();
            this.rdAdmin = new System.Windows.Forms.RadioButton();
            this.rdStudent = new System.Windows.Forms.RadioButton();
            this.rdTeacher = new System.Windows.Forms.RadioButton();
            this.Registerbtn = new System.Windows.Forms.Button();
            this.Cancelbtn = new System.Windows.Forms.Button();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtFname = new System.Windows.Forms.TextBox();
            this.txtLname = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtdep = new System.Windows.Forms.TextBox();
            this.txtMajor = new System.Windows.Forms.TextBox();
            this.txtRole = new System.Windows.Forms.TextBox();
            this.IDtxt = new System.Windows.Forms.Label();
            this.Fnametxt = new System.Windows.Forms.Label();
            this.lnametxt = new System.Windows.Forms.Label();
            this.Phonetxt = new System.Windows.Forms.Label();
            this.deptxt = new System.Windows.Forms.Label();
            this.emailtxt = new System.Windows.Forms.Label();
            this.majortxt = new System.Windows.Forms.Label();
            this.passtxt = new System.Windows.Forms.Label();
            this.roletxt = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.database1DataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.registrationBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // database1DataSet
            // 
            this.database1DataSet.DataSetName = "Database1DataSet";
            this.database1DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // registrationBindingSource
            // 
            this.registrationBindingSource.DataMember = "Registration";
            this.registrationBindingSource.DataSource = this.database1DataSet;
            // 
            // registrationTableAdapter
            // 
            this.registrationTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.AlertsTableAdapter = null;
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.BranchTableAdapter = null;
            this.tableAdapterManager.CoursesTableAdapter = null;
            this.tableAdapterManager.FeedBackTableAdapter = null;
            this.tableAdapterManager.FilesTableAdapter = null;
            this.tableAdapterManager.MailTableAdapter = null;
            this.tableAdapterManager.RegistrationTableAdapter = this.registrationTableAdapter;
            this.tableAdapterManager.StaffTableAdapter = null;
            this.tableAdapterManager.StudentScheduleTableAdapter = null;
            this.tableAdapterManager.StudentTableAdapter = null;
            this.tableAdapterManager.TutorScheduleTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = OOD_project.Database1DataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // Registertxt
            // 
            this.Registertxt.AutoSize = true;
            this.Registertxt.Location = new System.Drawing.Point(295, 33);
            this.Registertxt.Name = "Registertxt";
            this.Registertxt.Size = new System.Drawing.Size(49, 13);
            this.Registertxt.TabIndex = 19;
            this.Registertxt.Text = "Register ";
            // 
            // rdAdmin
            // 
            this.rdAdmin.AutoSize = true;
            this.rdAdmin.Location = new System.Drawing.Point(120, 75);
            this.rdAdmin.Name = "rdAdmin";
            this.rdAdmin.Size = new System.Drawing.Size(54, 17);
            this.rdAdmin.TabIndex = 20;
            this.rdAdmin.TabStop = true;
            this.rdAdmin.Text = "Admin";
            this.rdAdmin.UseVisualStyleBackColor = true;
            this.rdAdmin.CheckedChanged += new System.EventHandler(this.rdAdmin_CheckedChanged);
            // 
            // rdStudent
            // 
            this.rdStudent.AutoSize = true;
            this.rdStudent.Location = new System.Drawing.Point(341, 75);
            this.rdStudent.Name = "rdStudent";
            this.rdStudent.Size = new System.Drawing.Size(62, 17);
            this.rdStudent.TabIndex = 21;
            this.rdStudent.TabStop = true;
            this.rdStudent.Text = "Student";
            this.rdStudent.UseVisualStyleBackColor = true;
            this.rdStudent.CheckedChanged += new System.EventHandler(this.rdStudent_CheckedChanged);
            // 
            // rdTeacher
            // 
            this.rdTeacher.AutoSize = true;
            this.rdTeacher.Location = new System.Drawing.Point(543, 75);
            this.rdTeacher.Name = "rdTeacher";
            this.rdTeacher.Size = new System.Drawing.Size(65, 17);
            this.rdTeacher.TabIndex = 22;
            this.rdTeacher.TabStop = true;
            this.rdTeacher.Text = "Teacher";
            this.rdTeacher.UseVisualStyleBackColor = true;
            this.rdTeacher.CheckedChanged += new System.EventHandler(this.rdTeacher_CheckedChanged);
            // 
            // Registerbtn
            // 
            this.Registerbtn.Location = new System.Drawing.Point(328, 322);
            this.Registerbtn.Name = "Registerbtn";
            this.Registerbtn.Size = new System.Drawing.Size(75, 23);
            this.Registerbtn.TabIndex = 23;
            this.Registerbtn.Text = "Register";
            this.Registerbtn.UseVisualStyleBackColor = true;
            this.Registerbtn.Click += new System.EventHandler(this.Registerbtn_Click);
            // 
            // Cancelbtn
            // 
            this.Cancelbtn.Location = new System.Drawing.Point(713, 415);
            this.Cancelbtn.Name = "Cancelbtn";
            this.Cancelbtn.Size = new System.Drawing.Size(75, 23);
            this.Cancelbtn.TabIndex = 24;
            this.Cancelbtn.Text = "Cancel";
            this.Cancelbtn.UseVisualStyleBackColor = true;
            this.Cancelbtn.Click += new System.EventHandler(this.Cancelbtn_Click);
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(232, 135);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(100, 20);
            this.txtID.TabIndex = 25;
            // 
            // txtFname
            // 
            this.txtFname.Location = new System.Drawing.Point(232, 162);
            this.txtFname.Name = "txtFname";
            this.txtFname.Size = new System.Drawing.Size(100, 20);
            this.txtFname.TabIndex = 26;
            // 
            // txtLname
            // 
            this.txtLname.Location = new System.Drawing.Point(232, 191);
            this.txtLname.Name = "txtLname";
            this.txtLname.Size = new System.Drawing.Size(100, 20);
            this.txtLname.TabIndex = 27;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(232, 217);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(100, 20);
            this.txtPhone.TabIndex = 28;
            // 
            // txtpassword
            // 
            this.txtpassword.Location = new System.Drawing.Point(510, 153);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.Size = new System.Drawing.Size(100, 20);
            this.txtpassword.TabIndex = 29;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(510, 127);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(100, 20);
            this.txtEmail.TabIndex = 30;
            // 
            // txtdep
            // 
            this.txtdep.Location = new System.Drawing.Point(232, 244);
            this.txtdep.Name = "txtdep";
            this.txtdep.Size = new System.Drawing.Size(100, 20);
            this.txtdep.TabIndex = 31;
            // 
            // txtMajor
            // 
            this.txtMajor.Location = new System.Drawing.Point(510, 179);
            this.txtMajor.Name = "txtMajor";
            this.txtMajor.Size = new System.Drawing.Size(100, 20);
            this.txtMajor.TabIndex = 32;
            // 
            // txtRole
            // 
            this.txtRole.Location = new System.Drawing.Point(510, 213);
            this.txtRole.Name = "txtRole";
            this.txtRole.Size = new System.Drawing.Size(100, 20);
            this.txtRole.TabIndex = 33;
            // 
            // IDtxt
            // 
            this.IDtxt.AutoSize = true;
            this.IDtxt.Location = new System.Drawing.Point(165, 141);
            this.IDtxt.Name = "IDtxt";
            this.IDtxt.Size = new System.Drawing.Size(24, 13);
            this.IDtxt.TabIndex = 35;
            this.IDtxt.Text = "ID: ";
            // 
            // Fnametxt
            // 
            this.Fnametxt.AutoSize = true;
            this.Fnametxt.Location = new System.Drawing.Point(132, 165);
            this.Fnametxt.Name = "Fnametxt";
            this.Fnametxt.Size = new System.Drawing.Size(57, 13);
            this.Fnametxt.TabIndex = 36;
            this.Fnametxt.Text = "First Name";
            // 
            // lnametxt
            // 
            this.lnametxt.AutoSize = true;
            this.lnametxt.Location = new System.Drawing.Point(131, 191);
            this.lnametxt.Name = "lnametxt";
            this.lnametxt.Size = new System.Drawing.Size(58, 13);
            this.lnametxt.TabIndex = 37;
            this.lnametxt.Text = "Last Name";
            // 
            // Phonetxt
            // 
            this.Phonetxt.AutoSize = true;
            this.Phonetxt.Location = new System.Drawing.Point(147, 217);
            this.Phonetxt.Name = "Phonetxt";
            this.Phonetxt.Size = new System.Drawing.Size(38, 13);
            this.Phonetxt.TabIndex = 38;
            this.Phonetxt.Text = "Phone";
            // 
            // deptxt
            // 
            this.deptxt.AutoSize = true;
            this.deptxt.Location = new System.Drawing.Point(127, 244);
            this.deptxt.Name = "deptxt";
            this.deptxt.Size = new System.Drawing.Size(62, 13);
            this.deptxt.TabIndex = 39;
            this.deptxt.Text = "Department";
            // 
            // emailtxt
            // 
            this.emailtxt.AutoSize = true;
            this.emailtxt.Location = new System.Drawing.Point(421, 130);
            this.emailtxt.Name = "emailtxt";
            this.emailtxt.Size = new System.Drawing.Size(31, 13);
            this.emailtxt.TabIndex = 40;
            this.emailtxt.Text = "email";
            // 
            // majortxt
            // 
            this.majortxt.AutoSize = true;
            this.majortxt.Location = new System.Drawing.Point(419, 191);
            this.majortxt.Name = "majortxt";
            this.majortxt.Size = new System.Drawing.Size(33, 13);
            this.majortxt.TabIndex = 42;
            this.majortxt.Text = "Major";
            // 
            // passtxt
            // 
            this.passtxt.AutoSize = true;
            this.passtxt.Location = new System.Drawing.Point(400, 162);
            this.passtxt.Name = "passtxt";
            this.passtxt.Size = new System.Drawing.Size(52, 13);
            this.passtxt.TabIndex = 41;
            this.passtxt.Text = "password";
            // 
            // roletxt
            // 
            this.roletxt.AutoSize = true;
            this.roletxt.Location = new System.Drawing.Point(416, 223);
            this.roletxt.Name = "roletxt";
            this.roletxt.Size = new System.Drawing.Size(29, 13);
            this.roletxt.TabIndex = 43;
            this.roletxt.Text = "Role";
            // 
            // Registerfrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.roletxt);
            this.Controls.Add(this.majortxt);
            this.Controls.Add(this.passtxt);
            this.Controls.Add(this.emailtxt);
            this.Controls.Add(this.deptxt);
            this.Controls.Add(this.Phonetxt);
            this.Controls.Add(this.lnametxt);
            this.Controls.Add(this.Fnametxt);
            this.Controls.Add(this.IDtxt);
            this.Controls.Add(this.txtRole);
            this.Controls.Add(this.txtMajor);
            this.Controls.Add(this.txtdep);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtpassword);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtLname);
            this.Controls.Add(this.txtFname);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.Cancelbtn);
            this.Controls.Add(this.Registerbtn);
            this.Controls.Add(this.rdTeacher);
            this.Controls.Add(this.rdStudent);
            this.Controls.Add(this.rdAdmin);
            this.Controls.Add(this.Registertxt);
            this.Name = "Registerfrm";
            this.Text = "Register";
            this.Load += new System.EventHandler(this.Register_Load);
            ((System.ComponentModel.ISupportInitialize)(this.database1DataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.registrationBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Database1DataSet database1DataSet;
        private System.Windows.Forms.BindingSource registrationBindingSource;
        private Database1DataSetTableAdapters.RegistrationTableAdapter registrationTableAdapter;
        private Database1DataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.Label Registertxt;
        private System.Windows.Forms.RadioButton rdAdmin;
        private System.Windows.Forms.RadioButton rdStudent;
        private System.Windows.Forms.RadioButton rdTeacher;
        private System.Windows.Forms.Button Registerbtn;
        private System.Windows.Forms.Button Cancelbtn;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtFname;
        private System.Windows.Forms.TextBox txtLname;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtpassword;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtdep;
        private System.Windows.Forms.TextBox txtMajor;
        private System.Windows.Forms.TextBox txtRole;
        private System.Windows.Forms.Label IDtxt;
        private System.Windows.Forms.Label Fnametxt;
        private System.Windows.Forms.Label lnametxt;
        private System.Windows.Forms.Label Phonetxt;
        private System.Windows.Forms.Label deptxt;
        private System.Windows.Forms.Label emailtxt;
        private System.Windows.Forms.Label majortxt;
        private System.Windows.Forms.Label passtxt;
        private System.Windows.Forms.Label roletxt;
    }
}