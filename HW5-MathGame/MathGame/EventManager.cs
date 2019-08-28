using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HW5_MathGame
{
    /// <summary>
    /// Events to hold all interaction between different pages using MainWindow
    /// as a "middle man" that handles all page swapping and communication between
    /// pages
    /// </summary>
    public class EventManager
    {

        #region Navigation Events
        /// <summary>
        /// Navigate to UCMenu.xaml
        /// </summary>
        public static event EventHandler Navigate_to_Menu;
        public static void Nav_Menu_Event(object sender, RoutedEventArgs e)
        {
            Navigate_to_Menu?.Invoke(sender, e);
        }

        /// <summary>
        /// Navigate to UCGame.xaml
        /// </summary>
        public static event EventHandler Navigate_to_Game;
        public static void Nav_Game_Event(object sender, RoutedEventArgs e)
        {
            Navigate_to_Game?.Invoke(sender, e);
        }

        /// <summary>
        /// Navigate to UCGame-End.xaml
        /// </summary>
        public static event EventHandler Navigate_to_GameEnd;
        public static void Nav_GameEnd_Event(object sender, RoutedEventArgs e)
        {
            Navigate_to_GameEnd?.Invoke(sender, e);
        }

        /// <summary>
        /// Navigate to UCScores.xaml
        /// </summary>
        public static event EventHandler Navigate_to_Scores;
        public static void Nav_Scores_Event(object sender, RoutedEventArgs e)
        {
            Navigate_to_Scores?.Invoke(sender, e);
        }

        /// <summary>
        /// Navigate to UCIntro.xaml
        /// </summary>
        public static event EventHandler Navigate_to_Intro;
        public static void Exit_Event(object sender, RoutedEventArgs e)
        {
            Navigate_to_Intro?.Invoke(sender, e);
        }

        /// <summary>
        /// Navigate to UCIntro.xaml w/ passing information back
        /// </summary>
        public static event EventHandler Navigate_to_IntroEditInfo;
        public static void ExitEditInfo_Event(object sender, RoutedEventArgs e)
        {
            Navigate_to_IntroEditInfo?.Invoke(sender, e);
        }
        #endregion

        #region UCScores Event
        /// <summary>
        /// Event to Add an entry to the HIGH SCORES table
        /// </summary>
        public static event EventHandler Add_Entry_High_Scores;
        public static void Add_Game_Score_Event(object sender, RoutedEventArgs e)
        {
            Add_Entry_High_Scores?.Invoke(sender, e);
        }
        #endregion
    }
}
