
namespace WinCC.AD.gRPCClientFormTest
{
    partial class Form1
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
            this.Login = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.uname = new System.Windows.Forms.TextBox();
            this.pass = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.server = new System.Windows.Forms.NumericUpDown();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.server)).BeginInit();
            this.SuspendLayout();
            // 
            // Login
            // 
            this.Login.Location = new System.Drawing.Point(236, 63);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(125, 39);
            this.Login.TabIndex = 0;
            this.Login.Text = "Login";
            this.Login.UseVisualStyleBackColor = true;
            this.Login.Click += new System.EventHandler(this.Login_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(236, 108);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 39);
            this.button1.TabIndex = 1;
            this.button1.Text = "Authentize";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Authentize_Click);
            // 
            // uname
            // 
            this.uname.Location = new System.Drawing.Point(56, 63);
            this.uname.Name = "uname";
            this.uname.Size = new System.Drawing.Size(145, 20);
            this.uname.TabIndex = 2;
            this.uname.Text = "lcernoch";
            // 
            // pass
            // 
            this.pass.Location = new System.Drawing.Point(56, 89);
            this.pass.Name = "pass";
            this.pass.Size = new System.Drawing.Size(145, 20);
            this.pass.TabIndex = 3;
            this.pass.Text = "Taurid1*";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(367, 63);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 39);
            this.button2.TabIndex = 4;
            this.button2.Text = "Logoff";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Logoff_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(367, 108);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(125, 39);
            this.button3.TabIndex = 5;
            this.button3.Text = "Autologoff";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Autologoff_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(236, 185);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(125, 39);
            this.button4.TabIndex = 6;
            this.button4.Text = "SetADServer";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.SetADServer_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(367, 185);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(125, 39);
            this.button5.TabIndex = 7;
            this.button5.Text = "GetADServer";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.GetADServer_Click);
            // 
            // server
            // 
            this.server.Location = new System.Drawing.Point(56, 185);
            this.server.Name = "server";
            this.server.Size = new System.Drawing.Size(145, 20);
            this.server.TabIndex = 9;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(367, 230);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(125, 39);
            this.button6.TabIndex = 10;
            this.button6.Text = "ServiceMode";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.ServiceMode_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(498, 63);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(125, 39);
            this.button7.TabIndex = 11;
            this.button7.Text = "ELogin";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.ELogin_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.server);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.uname);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Login);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.server)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Login;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox uname;
        private System.Windows.Forms.TextBox pass;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.NumericUpDown server;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
    }
}

