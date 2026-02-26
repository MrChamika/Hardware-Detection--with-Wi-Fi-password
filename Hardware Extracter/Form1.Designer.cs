namespace Hardware_Extracter
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
            btnExit = new Button();
            label1 = new Label();
            panel1 = new Panel();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            label3 = new Label();
            label4 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnExit
            // 
            btnExit.Font = new Font("Stencil", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExit.Location = new Point(284, 452);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(183, 52);
            btnExit.TabIndex = 0;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.FlatStyle = FlatStyle.Flat;
            label1.Font = new Font("Unispace", 47.9999924F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(32, 9);
            label1.Name = "label1";
            label1.Size = new Size(735, 77);
            label1.TabIndex = 1;
            label1.Text = "Hardware Detection";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Control;
            panel1.Controls.Add(label1);
            panel1.ForeColor = SystemColors.ControlLight;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(810, 86);
            panel1.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Unispace", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.Location = new Point(23, 370);
            label2.Name = "label2";
            label2.Size = new Size(754, 46);
            label2.TabIndex = 3;
            label2.Text = "We're identifying the hardware and software components on your\ncomputer. No personal information is being collected.";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.ff;
            pictureBox1.Location = new Point(231, 106);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(290, 235);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(679, 505);
            label3.Name = "label3";
            label3.Size = new Size(109, 20);
            label3.TabIndex = 5;
            label3.Text = "Version 1.00.1";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(0, 509);
            label4.Name = "label4";
            label4.Size = new Size(135, 15);
            label4.TabIndex = 6;
            label4.Text = "Developed by : Chamika";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(800, 534);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(pictureBox1);
            Controls.Add(label2);
            Controls.Add(panel1);
            Controls.Add(btnExit);
            ForeColor = SystemColors.ActiveCaptionText;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Hardware Detection";
            TransparencyKey = Color.Yellow;
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnExit;
        private Label label1;
        private Panel panel1;
        private Label label2;
        private PictureBox pictureBox1;
        private Label label3;
        private Label label4;
    }
}
