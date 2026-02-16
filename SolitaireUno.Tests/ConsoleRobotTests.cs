using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Diagnostics;
using System.IO;

namespace SolitaireUno.Tests
{
    public class ConsoleRobotTests
    {
        
        public void Robot_Plays_Entire_Game()
        {
            // --- ARRANGE ---
            string gamePath = @"C:\Users\htdst\source\repos\SolitaireUno-Refactored-\bin\Debug\net9.0\SolitaireUno.exe";
            if (!File.Exists(gamePath)) gamePath = gamePath.Replace("Debug", "Release");

            ProcessStartInfo robotConfig = new ProcessStartInfo
            {
                FileName = gamePath,
                RedirectStandardInput = true,
                RedirectStandardOutput = true, // We must read output to know when game ends
                UseShellExecute = false,
                CreateNoWindow = true          // Keep it hidden for speed
            };

            // --- ACT ---
            using (Process gameProcess = new Process { StartInfo = robotConfig })
            {
                gameProcess.Start();
                StreamReader reader = gameProcess.StandardOutput;
                StreamWriter writer = gameProcess.StandardInput;

                // 1. Start the game
                writer.WriteLine("yes");

                // 2. The Game Loop
                // We will limit this to 100 turns to prevent infinite loops if the game gets stuck
                int maxTurns = 100;
                int currentTurn = 0;
                bool gameIsOver = false;

                while (currentTurn < maxTurns && !gameIsOver)
                {
                    // Read a chunk of text (we can't read line-by-line easily because we don't know how many lines)
                    // So we use a "Peek" strategy or just blindly send commands.
                    
                    // BETTER STRATEGY FOR CONSOLE APPS:
                    // Since reading is hard (blocking), we will blindly try to play.
                    // A real AI would parse the text, but a "Stress Test" just spams commands.
                    
                    // Try to play card 1
                    writer.WriteLine("1");
                    
                    // Try to pick up (if card 1 was invalid)
                    writer.WriteLine("p.u");
                    
                    // Try to pass (if pickup was invalid)
                    writer.WriteLine("p");

                    // Check if the process has exited (Game Over)
                    if (gameProcess.HasExited)
                    {
                        gameIsOver = true;
                    }

                    // Small delay to let the game process logic
                    System.Threading.Thread.Sleep(50);
                    currentTurn++;
                }

                // --- ASSERT ---
                // If the game exited, it means someone won!
                Assert.True(gameIsOver, "The game should have finished within 100 turns.");
            }
        }
    }
}
