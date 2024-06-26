using Godot;

namespace RMC.Core.Debug
{
    /// <summary>
    /// The Logger class provides a flexible logging utility for the Godot engine. 
    /// It allows for enabling/disabling logging, adding custom prefixes and suffixes to messages, 
    /// and printing various types of messages including standard messages, errors, empty lines, 
    /// dividers, and headers.
    /// </summary>
    /// <remarks>
    /// This class implements the ILogger interface and offers methods to log messages 
    /// with optional customization. It uses Godot's built-in GD.Print and GD.PrintErr methods 
    /// for output. The class can be configured to print messages with a specified prefix and suffix, 
    /// and to include dividers and headers for better log readability.
    /// </remarks>
    /// <example>
    /// Example usage:
    /// <code>
    /// Logger logger = new Logger(true)
    /// {
    ///     Prefix = "[INFO]",
    ///     Suffix = "[END]"
    /// };
    /// logger.Print("This is a log message.");
    /// logger.PrintErr("This is an error message.");
    /// logger.PrintEmptyLine();
    /// logger.PrintDivider();
    /// logger.PrintHeader("HEADER");
    /// </code>
    /// </example>

    public class Logger : ILogger
    {
        // Properties
        public bool IsEnabled { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }

        // Initialization
        public Logger (bool isEnabled)
        {
            IsEnabled = isEnabled;
        }

        // Methods
        public void Print(string message)
        {
            if (!IsEnabled)
            {
                return;
            }
            GD.Print($"{Prefix} {message} {Suffix}");
        }

        public void PrintErr(string message)
        {
            if (!IsEnabled)
            {
                return;
            }
            GD.PrintErr($"{Prefix} {message} {Suffix}");
        }

        public void PrintEmptyLine()
        {
            //Use GD.Print, not Print() to avoid prefix/suffix
            GD.Print("");
        }
        
        public void PrintDivider()
        {
            if (!IsEnabled)
            {
                return;
            }
            
            //Just a long line of dashes
            PrintHeader("");
        }


        public void PrintHeader(string message)
        {
            if (!IsEnabled)
            {
                return;
            }

            if (message.Length > 0)
            {
                message = " " + message + "  "; //padding
            }
            int totalLength = 36; 
            int padding = (totalLength - message.Length) / 2;
            string centeredMessage = new string('=', padding) + message + new string('=', padding);

            if (centeredMessage.Length < totalLength)
            {
                centeredMessage += "="; // Adjust if the message length is odd
            }

            //Use GD.Print, not Print() to avoid prefix/suffix
            GD.Print(centeredMessage);
        }
    }
}
