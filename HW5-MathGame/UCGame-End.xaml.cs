using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HW5_MathGame
{
    /// <summary>
    /// Interaction logic for UCGame_End.xaml
    /// </summary>
    public partial class UCGame_End : UserControl
    {
        #region Constructor
        public UCGame_End()
        {
            InitializeComponent();
        }
        #endregion

        #region Click Events
        /// <summary>
        /// Navigation to MENU
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Click(object sender, RoutedEventArgs e)
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

        /// <summary>
        /// Navigation to SCORES
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Scores_Click(object sender, RoutedEventArgs e)
        {
            Sound.PlayJumpSound();
            EventManager.Nav_Scores_Event(sender, e);
        }
        #endregion

        #region Prepare Final Screen
        /// <summary>
        /// Called from MainWindow and is used to prepare the Final Screen
        /// which presents the user's score and time to them.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="correctAnswers"></param>
        /// <param name="duration"></param>
        public void PrepareScreen(string name, int correctAnswers, long duration)
        {
            try
            {
                NameLabel.Content = name + "!!!";

                TimeSpan ts = new TimeSpan();
                ts = TimeSpan.FromMilliseconds(duration);

                double percentage = (((double)correctAnswers / 10.0) * 100.0);
                ScoreLabel.Content = percentage + "%";

                switch (correctAnswers)
                {
                    case 10:
                        MessageLabel.Content = "Perfect! ";
                        break;
                    case 9:
                    case 8:
                        MessageLabel.Content = "Great job! ";
                        break;
                    case 7:
                    case 6:
                    case 5:
                        MessageLabel.Content = "Good job! ";
                        break;
                    default:
                        MessageLabel.Content = "Maybe next time! ";
                        break;
                }

                MessageLabel.Content += ts.ToString(@"mm\:ss\:ff");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
