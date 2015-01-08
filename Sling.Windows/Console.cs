#region Copyright
// <copyright file="Console.cs" company="Sling">
// Copyright (c) 2015 All Rights Reserved
// </copyright>
// <author>Alan Doherty</author>
// <summary>Windows Console interface/summary>
#endregion

using System;

namespace Sling.Windows
{
    public class Console : IConsole
    {
        #region Methods
        /// <summary>
        /// Write some text to the console.
        /// </summary>
        /// <param name="str">The string.</param>
        public void Write(string str) {
            System.Console.Write(str);
        }

        /// <summary>
        /// Write a new line to the console.
        /// </summary>
        public void WriteLine() {
            System.Console.WriteLine();
        }

        /// <summary>
        /// Write a line of text to the console.
        /// </summary>
        /// <param name="str">The string.</param>
        public void WriteLine(string str) {
            System.Console.WriteLine(str);
        }
        #endregion
    }
}
