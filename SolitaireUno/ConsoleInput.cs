using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireUno
{
    /// <summary>
    /// Provides an implementation of IInputProvider for reading input from the console.
    /// </summary>
    public class ConsoleInput : IInputProvider
    {
        /// <summary>
        /// Reads a line of input from the console.
        /// </summary>
        /// <returns>The input string entered by the user.</returns>
        public string GetInput()
        {
            return Console.ReadLine(); // Read and return user input from the console
        }
    }
}
