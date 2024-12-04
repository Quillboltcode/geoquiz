namespace GeoWinform
{
    partial class AddPlayerForm
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtPlayerName = new TextBox();
            txtPassword = new TextBox();
            SaveBtn = new Button();
            BackBtn = new Button();
            chkIsGameMaster = new RadioButton();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(56, 71);
            label1.Name = "label1";
            label1.Size = new Size(49, 20);
            label1.TabIndex = 0;
            label1.Text = "Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(56, 182);
            label2.Name = "label2";
            label2.Size = new Size(70, 20);
            label2.TabIndex = 1;
            label2.Text = "Password";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(56, 291);
            label3.Name = "label3";
            label3.Size = new Size(103, 20);
            label3.TabIndex = 2;
            label3.Text = "IsGameMaster";
            // 
            // txtPlayerName
            // 
            txtPlayerName.Location = new Point(175, 75);
            txtPlayerName.Name = "txtPlayerName";
            txtPlayerName.Size = new Size(509, 27);
            txtPlayerName.TabIndex = 3;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(175, 173);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(509, 27);
            txtPassword.TabIndex = 4;
            // 
            // SaveBtn
            // 
            SaveBtn.Location = new Point(175, 388);
            SaveBtn.Name = "SaveBtn";
            SaveBtn.Size = new Size(94, 29);
            SaveBtn.TabIndex = 6;
            SaveBtn.Text = "Save";
            SaveBtn.UseVisualStyleBackColor = true;
            SaveBtn.Click += SaveBtn_Click;
            // 
            // BackBtn
            // 
            BackBtn.Location = new Point(590, 388);
            BackBtn.Name = "BackBtn";
            BackBtn.Size = new Size(94, 29);
            BackBtn.TabIndex = 7;
            BackBtn.Text = "Back";
            BackBtn.UseVisualStyleBackColor = true;
            BackBtn.Click += BackBtn_Click;
            // 
            // chkIsGameMaster
            // 
            chkIsGameMaster.AutoSize = true;
            chkIsGameMaster.Location = new Point(184, 291);
            chkIsGameMaster.Name = "chkIsGameMaster";
            chkIsGameMaster.Size = new Size(150, 24);
            chkIsGameMaster.TabIndex = 8;
            chkIsGameMaster.TabStop = true;
            chkIsGameMaster.Text = "Game Master True";
            chkIsGameMaster.UseVisualStyleBackColor = true;
            // 
            // AddPlayerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(chkIsGameMaster);
            Controls.Add(BackBtn);
            Controls.Add(SaveBtn);
            Controls.Add(txtPassword);
            Controls.Add(txtPlayerName);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "AddPlayerForm";
            Text = "AddPlayerForm";
            ResumeLayout(false);
            PerformLayout();
        }




        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtPlayerName;
        private TextBox txtPassword;
        private Button SaveBtn;
        private Button BackBtn;
        private RadioButton chkIsGameMaster;
    }
}