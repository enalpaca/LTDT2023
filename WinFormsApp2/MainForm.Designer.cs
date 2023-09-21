namespace WinFormsApp2
{
    partial class MainForm
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
            panel1 = new Panel();
            button2 = new Button();
            labelPathFile = new Label();
            textBoxFilePath = new TextBox();
            button1 = new Button();
            listBox1 = new ListBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(button2);
            panel1.Controls.Add(labelPathFile);
            panel1.Controls.Add(textBoxFilePath);
            panel1.Controls.Add(button1);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(776, 103);
            panel1.TabIndex = 0;
            // 
            // button2
            // 
            button2.Location = new Point(9, 72);
            button2.Name = "button2";
            button2.Size = new Size(159, 23);
            button2.TabIndex = 3;
            button2.Text = "Phân tích đồ thị";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // labelPathFile
            // 
            labelPathFile.AutoSize = true;
            labelPathFile.Location = new Point(84, 11);
            labelPathFile.Name = "labelPathFile";
            labelPathFile.Size = new Size(69, 15);
            labelPathFile.TabIndex = 2;
            labelPathFile.Text = "Đường dẫn:";
            // 
            // textBoxFilePath
            // 
            textBoxFilePath.Enabled = false;
            textBoxFilePath.Location = new Point(159, 7);
            textBoxFilePath.Name = "textBoxFilePath";
            textBoxFilePath.Size = new Size(614, 23);
            textBoxFilePath.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(3, 7);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Tải tệp";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(12, 135);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(773, 304);
            listBox1.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(listBox1);
            Controls.Add(panel1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button button1;
        private Label labelPathFile;
        private TextBox textBoxFilePath;
        private Button button2;
        private ListBox listBox1;
    }
}