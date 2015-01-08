#region Copyright
// <copyright file="Program.cs" company="Sling">
// Copyright (c) 2015 All Rights Reserved
// </copyright>
// <author>Alan Doherty</author>
// <summary>Desktop client entry class/summary>
#endregion

using Sling.Desktop.Dialogs;
using Sling.Scripting;
using System;
using System.IO;
using System.Windows.Forms;

namespace Sling.Desktop {
    static class Program
    {
        #region Constants
        public const string Version = "1.0.0";
        #endregion

        #region Fields
        private static Engine engine;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the engine.
        /// </summary>
        /// <value>The engine.</value>
        public static Engine Engine {
            get {
                return engine;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            // initialize gui
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // try and load
            try {
                // detect platform
                PlatformID platformID = DetectPlatform();

                // create platform interface
                IPlatform platform = null;

                if (platformID == PlatformID.Win32NT)
                    platform = new Sling.Windows.Platform();
                else
                    MessageBox.Show("The current host platform is not supported by Sling", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // create engine
                engine = new Engine(platform);
                engine.Console.WriteLine("[sling] initialized engine on " + platformID.ToString());

                throw new Exception("Windows XP compatability - How bad was android again?");
            } catch (Exception ex) {
                Error(ex);
            }

            // run application
            //Application.Run(new DeviceCreate());
        }

        /// <summary>
        /// Detect the platform the program is running on.
        /// </summary>
        /// <returns>Platform.</returns>
        public static PlatformID DetectPlatform() {
            // detect platform
            PlatformID platform = Environment.OSVersion.Platform;

            // osx checks
            if (platform == PlatformID.Unix) {
                string[] directories = new string[] { "/Applications", "/System", "/Users", "/Volumes" };
                bool isunix = false;
                bool ismac = true;

                // if all directories exist, it's a mac
                // if just one is missing, it's unix
                foreach (string directory in directories) {
                    if (Directory.Exists(directory)) {
                        isunix = false;
                    } else {
                        isunix = true;
                        ismac = false;
                    }
                }

                // it's not unix and is mac
                if (!isunix && ismac)
                    return PlatformID.MacOSX;
            }

            return platform;
        }

        /// <summary>
        /// Display an error dialog and close the application;
        /// </summary>
        /// <param name="ex"></param>
        public static void Error(Exception ex) {
            // display error
            ErrorDialog dialog = new ErrorDialog(ex);
            dialog.ShowDialog();

            // exit
            Environment.Exit(0);
        }
        #endregion
    }
}
