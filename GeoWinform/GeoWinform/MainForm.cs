namespace GeoWinform
{

    public partial class MainForm : Form
    {

        private readonly GameCreator _gameCreator;
        private readonly GamePlayer _gameplayer;


        public MainForm(string connectionString)
        {
            var campus = new[] { "Ha Noi", "Da Nang", "TP HCM" };
            _gameCreator = new GameCreator(connectionString);
            _gameplayer = new GamePlayer(connectionString);
            InitializeComponent();
            ThemeManager.SetTheme(this);
            comboBoxCampus.ValueMember = "id";
            comboBoxCampus.DisplayMember = "name";
            comboBoxCampus.DataSource = campus;

        }

        private bool ValidateMainForm()
        {
            if (String.IsNullOrEmpty(textBoxName.Text) || String.IsNullOrEmpty(comboBoxCampus.Text) ||
            String.IsNullOrEmpty(passwordtxtBox.Text))
            {
                return false;
            }
            return true;
        }

        private bool ValidatePassword() => _gameplayer.Login(textBoxName.Text, passwordtxtBox.Text);

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void GameMasterBtn_Click(object sender, EventArgs e)
        {
            // Validate required fields
            if (!ValidateMainForm())
            {
                MessageBox.Show("Please fill in all required fields.",
                                "Input Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Retrieve input values
            string username = textBoxName.Text.Trim();
            string password = passwordtxtBox.Text.Trim();

            // Attempt to log in
            if (!_gameplayer.Login(username, password))
            {
                MessageBox.Show("User not found. Please contact administration for more information.",
                                "Login Failed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Check if the user is a Game Master
            if (!_gameplayer.IsGameMaster(username))
            {
                MessageBox.Show("Unauthorized access. You are not allowed to access this page.",
                                "Access Denied",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Open the Game Creation Form if all checks pass
            var gameCreationForm = new Gamecreation(_gameCreator, this);
            this.Hide();
            gameCreationForm.Show();
        }


        private void GamePlayerBtn_Click(object sender, EventArgs e)
        {
            // Validate that all required fields are filled
            if (!ValidateMainForm())
            {
                MessageBox.Show("Please fill in all required fields.",
                                "Input Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }



            // Retrieve user inputs
            string username = textBoxName.Text.Trim();
            string password = passwordtxtBox.Text.Trim();

            // Validate the password
            if (!ValidatePassword())
            {
                MessageBox.Show("Incorrect password. Please try again.",
                                "Input Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Retrieve all quiz questions for the game
            List<Question> quizQuestions = _gameCreator.GetAllQuestions();

            // Open the Game Player Form
            using (var gamePlayerForm = new GameplayerForm(username, _gameplayer, quizQuestions))
            {
                this.Hide();
                gamePlayerForm.ShowDialog();
            }
            this.Show();
        }

        private void ThemeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTheme = ThemeComboBox.SelectedItem.ToString();
            ThemeManager.CurrentTheme = selectedTheme; // Update the global theme
            ThemeManager.ApplyThemeToAllForms(); // Apply to currently open forms
        }
    }
}
