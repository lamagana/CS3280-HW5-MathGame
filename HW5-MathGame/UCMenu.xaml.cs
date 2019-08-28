using System;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static HW5_MathGame.Enums;

namespace HW5_MathGame
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class UCMenu : UserControl
    {
        /// <summary>
        /// Holds the mode that was selected by the user and will
        /// be passed to the Game
        /// </summary>
        public GameMode Mode { get; set; }

        public UCMenu()
        {
            InitializeComponent();
        }

        #region Click Events
        /// <summary>
        /// Determines which game mode was selected (Add, Sub, Multi, Div)
        /// and fires an event to MainWindow to display UCGame.xaml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GameMode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                if (btn != null)
                {
                    Sound.PlayJumpSound();
                    string mode = btn.Name;
                    GameMode gamemode;
                    Enum.TryParse(mode, out gamemode);
                    Mode = gamemode;

                    EventManager.Nav_Game_Event(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Navigate to: MENU.\n" + ex.ToString(),
                    "Error - Navigation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Fires an event to MainWindow to display UCScores.xaml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Scores_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Sound.PlayJumpSound();
                EventManager.Nav_Scores_Event(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Navigate to: SCORES.\n" + ex.ToString(),
                    "Error - Navigation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Fires an event to MainWindow to display UCIntro.xaml
        /// and resets user data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Exit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Sound.PlayJumpSound();
                EventManager.Exit_Event(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Navigate to: INTRO.\n" + ex.ToString(),
                    "Error - Navigation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        public void Exit_EditInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Sound.PlayJumpSound();
                EventManager.ExitEditInfo_Event(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Navigate to: INTRO.\n" + ex.ToString(),
                    "Error - Navigation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        #endregion

        /// <summary>
        /// Function used by MainApp to display the user's name and age
        /// on the welcome title bar.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="age"></param>
        public void Welcome_User(string name, int age)
        {
            try
            {
                WelcomeLabel.Content = "Welcome " + name + " (" + age + ")!";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
