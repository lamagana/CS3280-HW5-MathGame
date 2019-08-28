using System;
using System.Windows;
using System.Windows.Controls;

namespace HW5_MathGame
{
    /// <summary>
    /// Interaction logic for Intro.xaml
    /// </summary>
    public partial class UCIntro : UserControl
    {

        #region Properties
        public string Username { get; set; }
        public byte Age { get; set; }
        #endregion

        #region Constructor
        public UCIntro()
        {
            InitializeComponent();
        }
        #endregion

        #region Click Events
        /// <summary>
        /// Grabs the information entered by the user in the textboxes, if the data is valid 
        /// (age is a number and name isn't whitepsace) then navigate the the MENU
        /// otherwise display the invalid labels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Sound.PlayJumpSound();
                byte age = 0;
                if (byte.TryParse(AgeTextBox.Text, out age) && !string.IsNullOrWhiteSpace(NameTextBox.Text))
                {
                    Username = NameTextBox.Text;
                    Age = age;

                    NameTextBox.Text = "";
                    AgeTextBox.Text = "";
                    InvalidNameLabel.Visibility = Visibility.Hidden;
                    InvalidAgeLabel.Visibility = Visibility.Hidden;
                    EventManager.Nav_Menu_Event(sender, e);
                } else
                {
                    InvalidNameLabel.Visibility = string.IsNullOrWhiteSpace(NameTextBox.Text) ? Visibility.Visible : Visibility.Hidden;
                    InvalidAgeLabel.Visibility = (age == 0) ? Visibility.Visible : Visibility.Hidden;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Navigate to MENU.\n" + ex.ToString(),
                    "Error - Navigation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        #endregion

    }
}
