namespace GeoWinform
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
            label1 = new Label();
            textBoxName = new TextBox();
            GameMasterBtn = new Button();
            GamePlayerBtn = new Button();
            label2 = new Label();
            comboBoxCampus = new ComboBox();
            label3 = new Label();
            Passwordlbl = new Label();
            passwordtxtBox = new TextBox();
            ThemeComboBox = new ComboBox();
            label4 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(446, 47);
            label1.Name = "label1";
            label1.Size = new Size(49, 20);
            label1.TabIndex = 0;
            label1.Text = "Name";
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(504, 44);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(203, 27);
            textBoxName.TabIndex = 1;
            textBoxName.TextChanged += textBox1_TextChanged;
            // 
            // GameMasterBtn
            // 
            GameMasterBtn.Location = new Point(177, 227);
            GameMasterBtn.Name = "GameMasterBtn";
            GameMasterBtn.Size = new Size(160, 44);
            GameMasterBtn.TabIndex = 2;
            GameMasterBtn.Text = "Game master";
            GameMasterBtn.UseVisualStyleBackColor = true;
            GameMasterBtn.Click += GameMasterBtn_Click;
            // 
            // GamePlayerBtn
            // 
            GamePlayerBtn.Location = new Point(504, 227);
            GamePlayerBtn.Name = "GamePlayerBtn";
            GamePlayerBtn.Size = new Size(160, 46);
            GamePlayerBtn.TabIndex = 3;
            GamePlayerBtn.Text = "Game player";
            GamePlayerBtn.UseVisualStyleBackColor = true;
            GamePlayerBtn.Click += GamePlayerBtn_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("FOT-Yuruka Std UB", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(46, 45);
            label2.Name = "label2";
            label2.Size = new Size(80, 24);
            label2.TabIndex = 4;
            label2.Text = "Campus";
            // 
            // comboBoxCampus
            // 
            comboBoxCampus.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxCampus.FormattingEnabled = true;
            comboBoxCampus.Location = new Point(152, 44);
            comboBoxCampus.Name = "comboBoxCampus";
            comboBoxCampus.Size = new Size(212, 28);
            comboBoxCampus.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(50, 240);
            label3.Name = "label3";
            label3.Size = new Size(58, 20);
            label3.TabIndex = 6;
            label3.Text = "You are";
            // 
            // Passwordlbl
            // 
            Passwordlbl.AutoSize = true;
            Passwordlbl.Location = new Point(425, 104);
            Passwordlbl.Name = "Passwordlbl";
            Passwordlbl.Size = new Size(70, 20);
            Passwordlbl.TabIndex = 7;
            Passwordlbl.Text = "Password";
            // 
            // passwordtxtBox
            // 
            passwordtxtBox.Location = new Point(504, 101);
            passwordtxtBox.Name = "passwordtxtBox";
            passwordtxtBox.PasswordChar = '●';
            passwordtxtBox.PlaceholderText = "Password";
            passwordtxtBox.Size = new Size(203, 27);
            passwordtxtBox.TabIndex = 8;
            // 
            // ThemeComboBox
            // 
            ThemeComboBox.FormattingEnabled = true;
            ThemeComboBox.Location = new Point(504, 385);
            ThemeComboBox.Name = "ThemeComboBox";
            ThemeComboBox.Size = new Size(151, 28);
            ThemeComboBox.TabIndex = 9;
            ThemeComboBox.SelectedIndex = -1;
            ThemeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ThemeComboBox.DataSource = new BindingSource(ThemeManager.Themes.Keys,null);
            ThemeComboBox.SelectedIndexChanged += ThemeComboBox_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(344, 388);
            label4.Name = "label4";
            label4.Size = new Size(151, 20);
            label4.TabIndex = 10;
            label4.Text = "Change theme colour";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(ThemeComboBox);
            Controls.Add(passwordtxtBox);
            Controls.Add(Passwordlbl);
            Controls.Add(label3);
            Controls.Add(comboBoxCampus);
            Controls.Add(label2);
            Controls.Add(GamePlayerBtn);
            Controls.Add(GameMasterBtn);
            Controls.Add(textBoxName);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Geo Quiz";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBoxName;
        private Button GameMasterBtn;
        private Button GamePlayerBtn;
        private Label label2;
        private ComboBox comboBoxCampus;
        private Label label3;
        private Label Passwordlbl;
        private TextBox passwordtxtBox;
        private ComboBox ThemeComboBox;
        private Label label4;
    }
}
