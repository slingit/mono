using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sling.Desktop.Dialogs
{
    public partial class ErrorDialog : Form
    {
        #region Fields
        private Exception exception;
        #endregion

        #region Properties        
        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception {
            get {
                return exception;
            }
        }
        #endregion

        #region Methods        
        /// <summary>
        /// Generates the error report.
        /// </summary>
        public void GenerateReport() {
            try {
                // initialize
                StringWriter writer = new StringWriter();

                writer.WriteLine("Sling Error Report");
                writer.WriteLine("Version: " + Program.Version + writer.NewLine);

                writer.WriteLine("Exception Information");
                writer.WriteLine("HRESULT: 0x" + exception.HResult.ToString("X4"));
                writer.WriteLine(exception.ToString() + writer.NewLine);

                if (chkPlatform.Checked) {
                    // get os version
                    OperatingSystem ver = Environment.OSVersion;

                    writer.WriteLine("Platform Information");
                    writer.WriteLine("Version: " + ver.Version.ToString());
                    writer.WriteLine("Platform: " + ver.Platform.ToString());
                    writer.WriteLine("Service Pack: " + ver.ServicePack + writer.NewLine);
                }

                writer.WriteLine("Module Information");

                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                    writer.WriteLine(assembly.ToString());

                // update text
                this.txtError.Text = writer.ToString();
            } catch (Exception) {
                MessageBox.Show("A fatal error occured while trying to generate an error report.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorDialog"/> class with an exception.
        /// </summary>
        /// <param name="ex">The exception.</param>
        public ErrorDialog(Exception exception) {
            InitializeComponent();

            // load
            this.exception = exception;

            // generate report
            GenerateReport();
        }
        #endregion

        #region Events
        private void btnSend_Click(object sender, EventArgs e) {
            // TODO: Implement error reporting sending
        }

        private void chkPlatform_CheckedChanged(object sender, EventArgs e) {
            GenerateReport();
        }

        private void btnIgnore_Click(object sender, EventArgs e) {
            Close();
        }
        #endregion
    }
}
