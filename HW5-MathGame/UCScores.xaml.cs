using System;
using System.Windows;
using System.Windows.Controls;
using static HW5_MathGame.Enums;

namespace HW5_MathGame
{
    #region Entry Class
    /// <summary>
    /// Entry Class - High Scores is a list of entry items
    /// </summary>

    #endregion

    /// <summary>
    /// Interaction logic for UCScores.xaml
    /// </summary>
    public partial class UCScores : UserControl
    {

        #region Attributes
        /// <summary>
        /// List of entry items to hold High Scores
        /// </summary>
        private Scores scores = new Scores();
        #endregion

        #region Constructor
        public UCScores()
        {
            InitializeComponent();
            try
            {
                Draw_Scores();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Add and Display New Entry to High Scores table.\n" + ex.ToString(),
                    "Error - New Entry", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        #endregion

        #region Click Events
        /// <summary>
        /// Navigates back to the MENU page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Menu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Sound.PlayJumpSound();
                EventManager.Nav_Menu_Event(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Navigate to: MENU.\n" + ex.ToString(),
                    "Error - Navigation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        #endregion

        #region Score Functions
        /// <summary>
        /// Passes the data to the Scores class in order to create a new entry within the table
        /// </summary>
        /// <param name="user"></param>
        /// <param name="score"></param>
        /// <param name="mode"></param>
        /// <param name="time"></param>
        public void Add_Entry(User user, double score, GameMode mode, long time)
        {
            try
            {
                scores.Add_Entry(user, score, mode, time);
                Draw_Scores();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Takes the data from the scores class and creates the high scores
        /// table
        /// </summary>
        private void Draw_Scores()
        {
            try
            {
                NamesTextBlock.Text = "";
                ScoresTextBlock.Text = "";
                TimeTextBlock.Text = "";
                TimeSpan ts = new TimeSpan();

                foreach (entry item in scores.list)
                {
                    NamesTextBlock.Text += (item.user.Name + "\n");

                    ScoresTextBlock.Text +=
                        (item.user.Age + "\t" +
                        ((item.score / 10) * 100) + "%\t\t" +
                        item.mode.ToString() + "\n");

                    ts = TimeSpan.FromMilliseconds(item.time);
                    TimeTextBlock.Text += ts.ToString(@"mm\:ss\:fff") + "\n";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

    }
}
