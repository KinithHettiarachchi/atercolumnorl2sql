namespace AlterColumnORCL2SQL
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            txtOracle = new TextBox();
            btnConvert = new Button();
            txtMSSQL = new TextBox();
            btnTest = new Button();
            txtLog = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtConnectionString = new TextBox();
            label5 = new Label();
            txtConnectionORCL = new TextBox();
            btnGenerate = new Button();
            label6 = new Label();
            txtSchema = new TextBox();
            groupBox1 = new GroupBox();
            label7 = new Label();
            txtOracleLog = new TextBox();
            btnTestORCL = new Button();
            groupBox2 = new GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // txtOracle
            // 
            txtOracle.Location = new Point(14, 110);
            txtOracle.Name = "txtOracle";
            txtOracle.Size = new Size(567, 23);
            txtOracle.TabIndex = 3;
            txtOracle.Text = "D:\\DBDUMP\\ALTER_BYTE_TO_CHAR_ORACLE.sql";
            // 
            // btnConvert
            // 
            btnConvert.Location = new Point(14, 221);
            btnConvert.Name = "btnConvert";
            btnConvert.Size = new Size(126, 51);
            btnConvert.TabIndex = 10;
            btnConvert.Text = "Convert";
            btnConvert.UseVisualStyleBackColor = true;
            btnConvert.Click += button1_Click;
            // 
            // txtMSSQL
            // 
            txtMSSQL.Location = new Point(14, 109);
            txtMSSQL.Name = "txtMSSQL";
            txtMSSQL.Size = new Size(567, 23);
            txtMSSQL.TabIndex = 8;
            txtMSSQL.Text = "D:\\DBDUMP\\ALTER_BYTE_TO_CHAR_MSSQL.sql";
            // 
            // btnTest
            // 
            btnTest.Location = new Point(455, 222);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(126, 50);
            btnTest.TabIndex = 11;
            btnTest.Text = "Test";
            btnTest.UseVisualStyleBackColor = true;
            btnTest.Click += button1_Click_1;
            // 
            // txtLog
            // 
            txtLog.Location = new Point(14, 177);
            txtLog.Name = "txtLog";
            txtLog.Size = new Size(567, 23);
            txtLog.TabIndex = 9;
            txtLog.Text = "D:\\DBDUMP\\ALTER_BYTE_TO_CHAR_MSSQL_TEST_LOG.sql";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 92);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 5;
            label1.Tag = "";
            label1.Text = "Oracle  SQL File";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 91);
            label2.Name = "label2";
            label2.Size = new Size(82, 15);
            label2.TabIndex = 6;
            label2.Tag = "";
            label2.Text = "SQL Server file";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 159);
            label3.Name = "label3";
            label3.Size = new Size(92, 15);
            label3.TabIndex = 7;
            label3.Tag = "";
            label3.Text = "MSSQL Test Log";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 31);
            label4.Name = "label4";
            label4.Size = new Size(203, 15);
            label4.TabIndex = 9;
            label4.Tag = "";
            label4.Text = "MSSQL Connection String for Testing";
            // 
            // txtConnectionString
            // 
            txtConnectionString.Location = new Point(14, 49);
            txtConnectionString.Name = "txtConnectionString";
            txtConnectionString.Size = new Size(567, 23);
            txtConnectionString.TabIndex = 7;
            txtConnectionString.Text = "Server=KINITH;Database=TrunkJava21;User Id=sa;Password=Abcd_123;TrustServerCertificate=True;";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(14, 34);
            label5.Name = "label5";
            label5.Size = new Size(140, 15);
            label5.TabIndex = 11;
            label5.Tag = "";
            label5.Text = "Oracle Connection String";
            // 
            // txtConnectionORCL
            // 
            txtConnectionORCL.Location = new Point(14, 52);
            txtConnectionORCL.Name = "txtConnectionORCL";
            txtConnectionORCL.Size = new Size(411, 23);
            txtConnectionORCL.TabIndex = 1;
            txtConnectionORCL.Text = "User Id=system;Password=Abcd_123;Data Source=ORCL";
            // 
            // btnGenerate
            // 
            btnGenerate.Location = new Point(14, 214);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(126, 51);
            btnGenerate.TabIndex = 5;
            btnGenerate.Text = "Generate";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(455, 34);
            label6.Name = "label6";
            label6.Size = new Size(86, 15);
            label6.TabIndex = 13;
            label6.Tag = "";
            label6.Text = "Oracle Schema";
            // 
            // txtSchema
            // 
            txtSchema.Location = new Point(455, 52);
            txtSchema.Name = "txtSchema";
            txtSchema.Size = new Size(126, 23);
            txtSchema.TabIndex = 2;
            txtSchema.Text = "mline";
            txtSchema.TextChanged += txtSchema_TextChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(txtOracleLog);
            groupBox1.Controls.Add(btnTestORCL);
            groupBox1.Controls.Add(btnGenerate);
            groupBox1.Controls.Add(txtSchema);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(txtConnectionORCL);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txtOracle);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(597, 280);
            groupBox1.TabIndex = 15;
            groupBox1.TabStop = false;
            groupBox1.Text = "Generate SQLs on Oracle";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(15, 150);
            label7.Name = "label7";
            label7.Size = new Size(88, 15);
            label7.TabIndex = 17;
            label7.Tag = "";
            label7.Text = "Oracle Test Log";
            // 
            // txtOracleLog
            // 
            txtOracleLog.Location = new Point(15, 168);
            txtOracleLog.Name = "txtOracleLog";
            txtOracleLog.Size = new Size(567, 23);
            txtOracleLog.TabIndex = 4;
            txtOracleLog.Text = "D:\\DBDUMP\\ALTER_BYTE_TO_CHAR_ORACLE_TEST_LOG.sql\r\n";
            // 
            // btnTestORCL
            // 
            btnTestORCL.Location = new Point(455, 215);
            btnTestORCL.Name = "btnTestORCL";
            btnTestORCL.Size = new Size(126, 50);
            btnTestORCL.TabIndex = 6;
            btnTestORCL.Text = "Test";
            btnTestORCL.UseVisualStyleBackColor = true;
            btnTestORCL.Click += btnTestORCL_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(txtConnectionString);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(txtLog);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(txtMSSQL);
            groupBox2.Controls.Add(btnTest);
            groupBox2.Controls.Add(btnConvert);
            groupBox2.Location = new Point(12, 310);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(597, 284);
            groupBox2.TabIndex = 16;
            groupBox2.TabStop = false;
            groupBox2.Text = "Convert Oracle to MSSQL";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(624, 607);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "Alter Column BYTE to CHAR : ORACLE to SQL converter";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtOracle;
        private Button btnConvert;
        private TextBox txtMSSQL;
        private Button btnTest;
        private TextBox txtLog;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtConnectionString;
        private Label label5;
        private TextBox txtConnectionORCL;
        private Button btnGenerate;
        private Label label6;
        private TextBox txtSchema;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button btnTestORCL;
        private Label label7;
        private TextBox txtOracleLog;
    }
}
