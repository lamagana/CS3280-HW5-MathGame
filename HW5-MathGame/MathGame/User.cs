using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW5_MathGame
{
    /// <summary>
    /// Adds ability to create an instance of a user which holds
    /// their Name and Age
    /// </summary>
    public class User
    {
        #region Attributes
        /// <summary>
        /// User's Name
        /// </summary>
        private string _name;

        /// <summary>
        /// User's Age
        /// </summary>
        private byte _age;
        #endregion

        #region Properties
        /// <summary>
        /// User's Name
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// User's Age
        /// </summary>
        public byte Age
        {
            get { return _age; }
            set { _age = value; }
        }
        #endregion

        #region Constructor
        public User() { }

        public User(string name, byte age)
        {
            Name = name;
            Age = age;
        }
        #endregion

    }
}
