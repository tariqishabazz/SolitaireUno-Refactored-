using System;
using System.Collections.Generic;
using System.Text;

namespace SolitaireUno.Tests
{
    public class MockInput : IInputProvider
    {
        private Queue<string> _script;

        public MockInput(IEnumerable<string> moves)
        {
            _script = new Queue<string> (moves);
        }

        public string GetInput()
        {
            if(_script.Count > 0)
            {
                return _script.Dequeue();
            }
            return "pass";
        }
    }
}
