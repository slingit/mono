#region Copyright
// <copyright file="Platform.cs" company="Sling">
// Copyright (c) 2015 All Rights Reserved
// </copyright>
// <author>Alan Doherty</author>
// <summary>Windows platform interface</summary>
#endregion

using Sling.Networking;
using Sling.Scripting;
using System;

namespace Sling.Windows
{
    public class Platform : IPlatform
    {
        #region Properties        
        /// <summary>
        /// Gets the platform name.
        /// </summary>
        /// <value>The name.</value>
        public string Name {
            get {
                return "Windows";
            }
        }
        #endregion

        #region Methods        
        /// <summary>
        /// Creates a Lua interface.
        /// </summary>
        /// <returns>Lua interface.</returns>
        public ILua CreateLua() {
            return new Lua();
        }

        /// <summary>
        /// Creates a Socket interface.
        /// </summary>
        /// <returns>Socket interface</returns>
        public ISocket CreateSocket() {
            return new Socket();
        }

        /// <summary>
        /// Creates the console interface.
        /// </summary>
        /// <returnsConsole interface.></returns>
        public IConsole CreateConsole() {
            return new Console();
        }
        #endregion
    }
}
