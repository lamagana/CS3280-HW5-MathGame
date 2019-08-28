using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static HW5_MathGame.Enums;

namespace HW5_MathGame
{
    /// <summary>
    /// Simple entry class to create objects used to hold the different entries within the high scores list
    /// </summary>
    public class entry
    {
        public User user;
        public double score;
        public GameMode mode;
        public long time;

        public entry(User u, double s, GameMode m, long t)
        {
            user = u;
            score = s;
            mode = m;
            time = t;
        }
    }

    /// <summary>
    /// Class that handles logic of creating a high score list
    /// </summary>
    public class Scores
    {
        /// <summary>
        /// Holds a list of the current entires in the high scores table
        /// </summary>
        public List<entry> list { get; set; }


        public Scores()
        {
            list = new List<entry>();
            Add_Dummy_Data();
        }

        /// <summary>
        /// Simply adds dummy data to the high scores to show sorting functionality
        /// </summary>
        private void Add_Dummy_Data()
        {
            try
            {
                list.Add(new entry(new User("Hannah V", 10), 10, GameMode.Multiplication, 13546));
                list.Add(new entry(new User("Lawrence M", 10), 10, GameMode.Multiplication, 12221));
                list.Add(new entry(new User("Player 1", 5), 9, GameMode.Division, 21125));
                list.Add(new entry(new User("Player 2", 8), 9, GameMode.Addition, 8135));
                list.Add(new entry(new User("Player 3", 8), 9, GameMode.Addition, 8135));
                list.Add(new entry(new User("Player 4", 8), 7, GameMode.Addition, 5463));
                list.Add(new entry(new User("Shawn C", 10), 10, GameMode.Addition, 6245)); //Added last - should be first in list
                Refine_Scores();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Failed to Add Entry to High Scores Table.\n" + ex.ToString(),
                       "Error - New Entry", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Function to create a new entry and add it to the list
        /// </summary>
        /// <param name="user"></param>
        /// <param name="score"></param>
        /// <param name="mode"></param>
        /// <param name="time"></param>
        public void Add_Entry(User user, double score, GameMode mode, long time)
        {
            try
            {
                list.Add(new entry(user, score, mode, time));
                Refine_Scores();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Add Entry to High Scores Table.\n" + ex.ToString(),
                   "Error - New Entry", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Sorts the list and removes any entires that didn't make the top 10
        /// </summary>
        public void Refine_Scores()
        {
            try
            {

                List<entry> orderedList = list.OrderByDescending(x => x.score).ThenBy(y => y.time).ToList();
                list = orderedList;

                if (list.Count > 10)
                    list.RemoveRange(9, (list.Count - 10));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
