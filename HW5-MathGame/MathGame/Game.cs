using System;
using System.Diagnostics;
using System.Windows;
using static HW5_MathGame.Enums;

namespace HW5_MathGame
{
    /// <summary>
    /// Game class - Handles all the logic and timing of generation questions to the user,
    /// verifiying input, and duration of the game
    /// </summary>
    public class Game
    {

        #region Constants
        /// <summary>
        /// Constant that is used to determine how many questions the game should ask the user
        /// </summary>
        public const int TOTAL_QUESTIONS = 10;
        #endregion

        #region Attributes
        /// <summary>
        /// Used to create random numbers
        /// </summary>
        private Random rand = new Random();

        /// <summary>
        /// Stopwatch to keep track of the elapsed time in the game
        /// </summary>
        private Stopwatch _stopwatch;

        private int number1;
        private int number2;
        #endregion

        #region Properties
        /// <summary>
        /// Number of the user's correctly answered questions
        /// </summary>
        public int CorrectAnswers { get; set; }

        /// <summary>
        /// Numbers of the user's total answered questions
        /// </summary>
        public int AnsweredQuestions { get; set; }

        /// <summary>
        /// Holds the current game mode: +, -, *, /
        /// </summary>
        public GameMode CurrentMode { get; set; }

        /// <summary>
        /// Returns the elapsed milliseconds of the game
        /// </summary>
        public long Time { get { return _stopwatch.ElapsedMilliseconds; } }
        #endregion

        #region Constructor
        public Game()
        {
            AnsweredQuestions = 0;
            _stopwatch = new Stopwatch();
        }
        #endregion

        #region Timing Functions
        /// <summary>
        /// Starts the game timer
        /// </summary>
        public void StartTimer()
        {
            try
            {
                _stopwatch.Reset();
                _stopwatch.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Start Timer.\n" + ex.ToString(),
                    "Error - Start Game Timer", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Stops the timer
        /// </summary>
        public void StopTimer()
        {
            try
            {
                _stopwatch.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Start Timer.\n" + ex.ToString(),
                    "Error - Start Game Timer", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        #endregion

        #region  Game Functions
        /// <summary>
        /// Generate Question function based on 
        /// the current math operation
        /// returns a string with a math questions
        /// </summary>
        /// <returns></returns>
        public string GenerateQuestion()
        {
            try
            {
                bool isPossibleQuestion = false;
                do
                {
                    if (CurrentMode == GameMode.Division)
                    {
                        number1 = rand.Next(1, 20);
                        number2 = rand.Next(2, 20);
                        if (number1 > number2 && number1 % number2 == 0)
                            isPossibleQuestion = true;
                    }
                    else if (CurrentMode == GameMode.Subtraction)
                    {
                        number1 = rand.Next(1, 20);
                        number2 = rand.Next(1, 10);
                        if (number1 >= number2)
                            isPossibleQuestion = true;
                    }
                    else
                    {
                        number1 = rand.Next(1, 9);
                        number2 = rand.Next(1, 5);
                        isPossibleQuestion = true;
                    }
                } while (!isPossibleQuestion);

                string symbol = "";
                switch (CurrentMode)
                {
                    case GameMode.Addition:
                        symbol = "+";
                        break;
                    case GameMode.Subtraction:
                        symbol = "-";
                        break;
                    case GameMode.Division:
                        symbol = "÷";
                        break;
                    case GameMode.Multiplication:
                        symbol = "x";
                        break;
                }
                return number1 + " " + symbol + " " + number2 + " = ";

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Determines if the user's answer is correct for the
        /// current game mode. Also increments total number of questions
        /// answered.
        /// </summary>
        /// <param name="answer"></param>
        public bool IsCorrect(double answer)
        {
            try
            {
                bool result = false;
                switch (CurrentMode)
                {
                    case GameMode.Addition:
                        result = (number1 + number2 == answer);
                        break;
                    case GameMode.Subtraction:
                        result = (number1 - number2 == answer);
                        break;
                    case GameMode.Multiplication:
                        result = (number1 * number2 == answer);
                        break;
                    case GameMode.Division:
                        result = (number1 / number2 == answer);
                        break;
                }

                if (result)
                    CorrectAnswers++;

                AnsweredQuestions++;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
