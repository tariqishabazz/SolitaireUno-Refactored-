using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireUno
{
    /// <summary>
    /// Provides an implementation of IOutputProvider for writing output to the console.
    /// </summary>
    public class ConsoleOutput : IOutputProvider
    {
        /// <summary>
        /// Writes a string to the console without a newline.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public void Write(string message)
        {
            Console.Write(message); // Write message to the console (no newline)
        }

        /// <summary>
        /// Writes a string to the console followed by a newline.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public void WriteLine(string message)
        {
            Console.WriteLine(message); // Write message to the console (with newline)
        }
    }
}
