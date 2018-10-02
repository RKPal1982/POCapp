using System;
using System.IO;

namespace Solutions.OnlineSelling.Helpers
{
    public class Logger
    {
        #region Private Methods

        /// <summary>
        /// Writes the error or log message to the file
        /// </summary>
        /// <param name="passedErrorMessage">The messages to be written</param>
        private static void _logTheCause(string passedErrorMessage)
        {
            // the header to impose at each line
            string headerText =
                DateTime.Today.ToShortDateString() + " " +
                string.Format("{0:hh:mmtt}", DateTime.Now) + " > " +
                passedErrorMessage;

            // use the "using" to dispose the file stream handle properly
            using (FileStream requiredFileStream = new FileStream(
                _generateFileName(), FileMode.Append))
            {
                // disposable stream writer
                using (StreamWriter requiredWriter = new StreamWriter(
                    requiredFileStream))
                {
                    // aids writing to files with a safe context
                    lock (requiredWriter)
                    {
                        requiredWriter.WriteLine(headerText);
                        requiredWriter.Flush();
                        requiredWriter.Close();
                    }
                }
            }
        }


        /// <summary>
        /// Generates the required filename and directories
        /// </summary>
        /// <returns>The required file path</returns>
        private static string _generateFileName()
        {
            string requiredFileName = string.Empty;
            // the file name as yyyy-mm-dd_HH
            string requiredDateTime = string.Format("{0:yyyy-MM-dd_HHmm}", DateTime.Now) + ".log";

            // read the application log path as entered in app.config
            string requiredApplicationLogPath = null;

            requiredApplicationLogPath = System.Web.HttpContext.Current.Server.MapPath(
                System.Web.HttpContext.Current.Request.Url.AbsoluteUri);


            if (!Directory.Exists(requiredApplicationLogPath))
            {
                // create the Logs folder
                Directory.CreateDirectory(requiredApplicationLogPath);
            }

            // the required file name with path
            requiredFileName = requiredApplicationLogPath + "\\" + requiredDateTime;

            // return the generated file name with path
            return requiredFileName;
        }
        #endregion Private Methods

        #region Public Methods
        /// <summary>
        /// Logs the given exception message
        /// </summary>
        /// <param name="passedExceptionMessage">The required message</param>
        public static void LogException(Exception passedException, bool logException = true)
        {
            if (logException == false) return;

            try
            {
                String _errorString = "Exception occured at: \n Class: " +
                    System.Reflection.MethodBase.GetCurrentMethod().DeclaringType +
                    "\n Method: " + System.Reflection.MethodBase.GetCurrentMethod() +
                    "\n with the following message -- \n" + passedException.Message +
                    " -- \n" + passedException.StackTrace +
                     " -- \n" + passedException.HelpLink +
                     " -- \n At class: " + passedException.Source +
                     " -- \n At method: " + passedException.TargetSite;

                _logTheCause(_errorString);
            }
            catch
            {
                //to do nothing
            }
        }



        /// <summary>
        /// Logs the given message
        /// </summary>
        /// <param name="passedMethodName">The method name in which the exception occured</param>
        /// <param name="passedCustomMessage">The required message</param>
        public static void LogCustomMessage(string passedCustomMessage, bool logException = true)
        {
            if (logException == false) return;

            _logTheCause(passedCustomMessage);
        }
        #endregion

    }
}
