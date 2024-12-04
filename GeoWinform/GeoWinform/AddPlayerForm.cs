using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeoWinform
{
    public partial class AddPlayerForm : Form
    {
        private readonly GameCreator _gameCreator;
        public AddPlayerForm(GameCreator gamecreator)
        {
            InitializeComponent();
            ThemeManager.SetTheme(this);
            _gameCreator = gamecreator;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            string name = txtPlayerName.Text.Trim();
            string password = txtPassword.Text.Trim();
            bool isGameMaster = chkIsGameMaster.Checked;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _gameCreator.RegisterUser(name, password, isGameMaster); // Use GameCreator's method
                MessageBox.Show("Player added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
