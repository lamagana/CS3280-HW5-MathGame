using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HW5_MathGame
{
    /// <summary>
    /// Static class that is used throughout the program to play sound
    /// </summary>
    public static class Sound
    {
        /// <summary>
        /// Toggle to enable/disable the mario theme song
        /// </summary>
        private const bool IS_MUSIC_ON = true;

        /// <summary>
        /// Mario Theme Song - Played as the program is running
        /// </summary>
        private static MediaPlayer ThemeSong = new MediaPlayer();

        /// <summary>
        /// Jump Sound - Played whenever a button is clicked
        /// </summary>
        private static MediaPlayer JumpSound = new MediaPlayer();

        /// <summary>
        /// Coin Sound - Played whenever a user submits an answer to the game
        /// </summary>
        private static MediaPlayer CoinSound = new MediaPlayer();

        /// <summary>
        /// GameOver Sound - Played when the current game has been finished
        /// </summary>
        private static MediaPlayer GameOverSound = new MediaPlayer();

        /// <summary>
        /// Constructor - Opens all the song files 
        /// </summary>
        static Sound()
        {
            try
            {
                ThemeSong.Open(new Uri("../../Sounds/themesong.wav", UriKind.RelativeOrAbsolute));
                JumpSound.Open(new Uri("../../Sounds/jump.wav", UriKind.RelativeOrAbsolute));
                CoinSound.Open(new Uri("../../Sounds/coin.wav", UriKind.RelativeOrAbsolute));
                GameOverSound.Open(new Uri("../../Sounds/gameover.wav", UriKind.RelativeOrAbsolute));
                ThemeSong.MediaEnded += new EventHandler(Reset_Music);

                ThemeSong.Volume = (IS_MUSIC_ON) ? 100 : 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Open Sound File.\n" + ex.ToString(),
                    "Error - New Entry", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Triggers when the themesong ends because it has finished playing, simply
        /// resets the position of the song back to the beginning and resumes playing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Reset_Music(object sender, EventArgs e)
        {
            ThemeSong.Position = TimeSpan.Zero;
            PlayThemeMusic();
        }


        /*
         * I also decided it made more sense to handle any sound exceptions within the method itself
         * because if the sound fails to play, I do not want it to prevent any necessary game functionality
         * to not occur because of an exception. For example: Unable to play a sound -> Navigation is no longer gets
         * triggered because of exceptions
         */

        /// <summary>
        /// Plays the Mario Theme Song
        /// </summary>
        public static void PlayThemeMusic()
        {
            try
            {
                ThemeSong.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to play sound.\n" + ex.ToString(),
                    "Error - Sound", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Plays the Jump sound for buttons
        /// </summary>
        public static void PlayJumpSound()
        {
            try
            {
                JumpSound.Position = TimeSpan.Zero;
                JumpSound.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to play sound.\n" + ex.ToString(),
                    "Error - Sound", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Plays the Coin sound for game answer submissions
        /// </summary>
        public static void PlayCoinSound()
        {
            try
            {
                CoinSound.Position = TimeSpan.Zero;
                CoinSound.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to play sound.\n" + ex.ToString(),
                    "Error - Sound", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Plays the Game Over sound whenever a user finishes a game
        /// </summary>
        public static void PlayGameOverSound()
        {
            try
            {
                ThemeSong.Volume = 0;
                GameOverSound.Position = TimeSpan.Zero;
                GameOverSound.Play();
                Thread.Sleep(3500);

                ThemeSong.Volume = (IS_MUSIC_ON) ? 100 : 0;

                ThemeSong.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to play sound.\n" + ex.ToString(),
                    "Error - Sound", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }


    }
}
