using System;
using System.Media;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;


namespace HW5_MathGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Attributes
        private static UCIntro ucIntro = new UCIntro();
        private static UCMenu ucMenu = new UCMenu();
        private static UCGame ucGame = new UCGame();
        private static UCGame_End ucGame_End = new UCGame_End();
        private static UCScores ucScores = new UCScores();

        private static User currentPlayer = new User();
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            #region Event Subscribers
            EventManager.Navigate_to_Menu += new EventHandler(Nav_Menu_Event);
            EventManager.Navigate_to_Game += new EventHandler(Nav_Game_Event);
            EventManager.Navigate_to_GameEnd += new EventHandler(Nav_GameEnd_Event);
            EventManager.Navigate_to_Scores += new EventHandler(Nav_Scores_Event);
            EventManager.Navigate_to_Intro += new EventHandler(Nav_Intro_Event);
            EventManager.Navigate_to_IntroEditInfo += new EventHandler(Nav_IntroEditInfo_Event);

            EventManager.Add_Entry_High_Scores += new EventHandler(Add_Entry_Event);
            #endregion

            Sound.PlayThemeMusic();
            ViewFrame.Navigate(ucIntro);
        }

        private void Add_Entry_Event(object sender, EventArgs e)
        {
            try
            {
                ucScores.Add_Entry(currentPlayer, ucGame.CurrentGame.CorrectAnswers, ucGame.CurrentGame.CurrentMode, ucGame.CurrentGame.Time);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Add Entry to High Scores Table.\n" + ex.ToString(),
                    "Error - New Entry", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Event to navigate to the MENU User Control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Nav_Menu_Event(object sender, EventArgs e)
        {
            try
            {
                currentPlayer.Name = ucIntro.Username;
                currentPlayer.Age = ucIntro.Age;

                ucMenu.Welcome_User(currentPlayer.Name, currentPlayer.Age);
                ViewFrame.Navigate(ucMenu);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Navigate to: MENU.\n" + ex.ToString(),
                    "Error - Navigation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Event to navigate to the GAME UC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Nav_Game_Event(object sender, EventArgs e)
        {
            try
            {
                ucGame.PrepareGame(ucMenu.Mode);
                ViewFrame.Navigate(ucGame);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Navigate to: GAME.\n" + ex.ToString(),
                    "Error - Navigation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Event to navigate to the GAME-End UC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Nav_GameEnd_Event(object sender, EventArgs e)
        {
            try
            {
                ucGame_End.PrepareScreen(currentPlayer.Name, ucGame.CurrentGame.CorrectAnswers, ucGame.CurrentGame.Time);
                ViewFrame.Navigate(ucGame_End);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Navigate to: GAME_END.\n" + ex.ToString(),
                    "Error - Navigation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Event to navigate to the HIGH SCORES UC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Nav_Scores_Event(object sender, EventArgs e)
        {
            try
            {
                ViewFrame.Navigate(ucScores);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Navigate to: HIGH_SCORES.\n" + ex.ToString(),
                        "Error - Navigation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Event to navigate back to the INTRO UC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Nav_Intro_Event(object sender, EventArgs e)
        {
            try
            {
                ViewFrame.Navigate(ucIntro);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Navigate to: INTRO.\n" + ex.ToString(),
                        "Error - Navigation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Event to navigate back to the INTRO UC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Nav_IntroEditInfo_Event(object sender, EventArgs e)
        {
            try
            { 
                ViewFrame.Navigate(ucIntro);
                ucIntro.NameTextBox.Text = currentPlayer.Name;
                ucIntro.AgeTextBox.Text = currentPlayer.Age.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Navigate to: INTRO.\n" + ex.ToString(),
                       "Error - Navigation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        #endregion

    }
}
