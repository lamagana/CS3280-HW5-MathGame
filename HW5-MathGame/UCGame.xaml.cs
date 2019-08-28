using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using static HW5_MathGame.Enums;

namespace HW5_MathGame
{
    /// <summary>
    /// Interaction UI functionality for UCGame.xaml
    /// </summary>
    public partial class UCGame : UserControl
    {

        #region Attributes
        /// <summary>
        /// Holds a game object which is instaniated on the Start_Game_Click
        /// </summary>
        public Game CurrentGame;

        /// <summary>
        /// Holds the game mode that was selected
        /// </summary>
        private GameMode mode;

        /// <summary>
        /// Creates a dispatcher which is used to update the stopwatch time to the user
        /// </summary>
        private DispatcherTimer Timer;
        #endregion

        #region Constructor
        public UCGame()
        {
            InitializeComponent();
            Timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 25), DispatcherPriority.Background, timer_tick, Dispatcher.CurrentDispatcher);
            Timer.IsEnabled = false;
        }

        /// <summary>
        /// Occurs on each tick of of the dispatch timer (25 milliseconds) used to display the running
        /// time to the user as the game is playing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_tick(object sender, EventArgs e)
        {
            TimeSpan ts = new TimeSpan();
            ts = TimeSpan.FromMilliseconds(CurrentGame.Time);
            TimerContent.Text = ts.ToString(@"mm\:ss\:ff");
        }
        #endregion

        #region Click/Key Events
        /// <summary>
        /// Navigate to the MENU page
        /// </summary>
        /// <param name="sender"></param>s
        /// <param name="e"></param>
        public void Menu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ResetGame();
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
        /// Navigates to the HIGH SCORES page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void High_Scores_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ResetGame();
                Sound.PlayJumpSound();
                EventManager.Nav_Scores_Event(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Navigate to: MENU.\n" + ex.ToString(),
                    "Error - Navigation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }


        /// <summary>
        /// Starts the Animation321Go and sets the game up to begin starting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Game_Start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Sound.PlayJumpSound();

                StartGameBtn.Visibility = Visibility.Hidden;

                CurrentGame = new Game();
                CurrentGame.CurrentMode = mode;

                Animation321Go();
                await Task.Delay(TimeSpan.FromSeconds(4));

                if (CurrentGame != null)
                {
                    GameGrid.Visibility = Visibility.Visible;
                    TimerLabel.Visibility = Visibility.Visible;

                    CurrentGame.StartTimer();
                    Timer.IsEnabled = true;
                    QuestionTextBlock.Text = CurrentGame.GenerateQuestion();
                }

                InputTextBox.Text = "";
                InputTextBox.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Start the Games.\n" + ex.ToString(),
                    "Error - Start Game", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Events when a user selects "Submit" to submit an answer to the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Submit_User_Input();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Submit User Input.\n" + ex.ToString(),
                    "Error - Game Entry", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Event when a user clicks Enter to submit an answer to the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Enter_Click(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return && InputTextBox.Text != "")
                {
                    Submit_User_Input();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Submit User Input.\n" + ex.ToString(),
                    "Error - Game Entry", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        #endregion

        #region Game Functions
        /// <summary>
        /// Adds the game mode to the title of the screen
        /// </summary>
        /// <param name="mode"></param>
        public void PrepareGame(GameMode mode)
        {
            try
            {
                this.mode = mode;
                Label.Content = mode.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// function which grabs the user's input and determines if it is
        /// correct and if it needs to create a new question. If the limit has
        /// been reached -> Ends the game
        /// </summary>
        public void Submit_User_Input()
        {
            try
            {
                Sound.PlayCoinSound();
                double answer;
                double.TryParse(InputTextBox.Text, out answer);
                bool result = CurrentGame.IsCorrect(answer);

                if (CurrentGame.AnsweredQuestions < 10)
                {
                    QuestionTextBlock.Text = CurrentGame.GenerateQuestion();
                    AnswerResponse.Text = (result) ? "Correct!" : "Incorrect!";
                    InputTextBox.Text = "";
                    InputTextBox.Focus();
                }
                else
                {
                    QuestionTextBlock.Text = "";
                    AnswerResponse.Text = "";
                    CurrentGame.StopTimer();
                    Timer.IsEnabled = false;
                    EndGame();
                }

                ProgressBar.Value += 10;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// End Game - Hides the game,
        /// Displays "Fin!" and sets the contents of their game score
        /// to the EndGameGrid and displays it after "Fin!"
        /// </summary>
        public async void EndGame()
        {
            try
            {
                GameGrid.Visibility = Visibility.Hidden;
                TimerLabel.Visibility = Visibility.Hidden;

                DisplayEndGameLabel();
                await Task.Delay(TimeSpan.FromSeconds(1.5));

                Sound.PlayGameOverSound();
                EventManager.Add_Game_Score_Event(null, null);
                EventManager.Nav_GameEnd_Event(null, null);
                ResetGame();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Function to reset the game
        /// </summary>
        public void ResetGame()
        {
            try
            {
                CurrentGame = null;
                StartGameBtn.Visibility = Visibility.Visible;
                GameGrid.Visibility = Visibility.Hidden;
                TimerLabel.Visibility = Visibility.Hidden;
                ProgressBar.Value = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Animation Functions
        /// <summary>
        /// 3 2 1 Go! Animation
        /// </summary>
        public async void Animation321Go()
        {
            try
            {
                string[] elements = { "3", "2", "1", "Go!" };
                string[] elementsImgs = { "Mario1", "Mario2", "Mario3", "" };
                await Dispatcher.Invoke(async () =>
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Image ImageControl = (Image)FindName(elementsImgs[i]);
                        SecondsTextBlock.Text = elements[i];

                        SecondsTextBlock.Visibility = Visibility.Visible;
                        if (ImageControl != null)
                            ImageControl.Visibility = Visibility.Visible;
                        await Task.Delay(new TimeSpan(0, 0, 0, 0, 750));

                        SecondsTextBlock.Visibility = Visibility.Hidden;
                        if (ImageControl != null)
                            ImageControl.Visibility = Visibility.Hidden;
                        await Task.Delay(new TimeSpan(0, 0, 0, 0, 250));
                    }
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Displays "Fin!" for 1.5 seconds
        /// </summary>
        public void DisplayEndGameLabel()
        {
            try
            {
                Dispatcher.Invoke(async () =>
                {
                    EndGameTextBlock.Visibility = Visibility.Visible;
                    await Task.Delay(TimeSpan.FromSeconds(1.5));
                    EndGameTextBlock.Visibility = Visibility.Hidden;
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}