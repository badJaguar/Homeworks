using System;

namespace Mediaplayer
{
    /// <summary>
    /// Shows that application works.
    /// </summary>
    public class AppStarter
    {
        private readonly Activator _activator;

        // TODO: Dependency inversion principal violated.
        public AppStarter()
        {
            _activator = new Activator();
        }

        /// <summary>
        /// Prints the message of activation and when app turned on.
        /// </summary>
        public void PrintMessage()
        {
            _activator.ActivatorMessage();
            Console.WriteLine("Player turned on.");
            Console.WriteLine();
        }
    }
}