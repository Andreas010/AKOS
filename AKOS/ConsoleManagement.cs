using System;
using System.Threading;

namespace Andy.AKOS
{
    public class ConsoleManager
    {
        public enum SlowWriteMode { CharTime, TotalTime }

        public void FlushConsoleInputStream()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }

        public void WriteSlow(string text, int time, SlowWriteMode mode = SlowWriteMode.CharTime)
        {
            if (mode == SlowWriteMode.CharTime)
            {
                for (int i = 0; i < text.Length; i++)
                {
                    Console.Write(text[i]);
                    Thread.Sleep(time);
                }
            }
            else
            {
                float waitTime = (float)time / text.Length;

                for (int i = 0; i < text.Length; i++)
                {
                    Console.Write(text[i]);
                    Thread.Sleep((int)(waitTime * (i + 1)) / (i + 1));
                }
            }
        }

        public void MoveBuffer(int time)
        {
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.WriteLine();
                Thread.Sleep(time);
            }

            Console.SetCursorPosition(0, 0);
        }
    }
}
