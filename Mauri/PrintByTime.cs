using Spectre.Console;

namespace PBT
{
    public static class PBTout
    {
        public static void PBTPrint(string toprint, long ms,string color)
        {
            var start = DateTime.Now.Ticks;
            foreach (var item in toprint)
            {
                while ((DateTime.Now.Ticks - start) < ms*1000){}
               AnsiConsole.Markup($"[{color}]{item}[/]"); //System.Console.Write();
                start = DateTime.Now.Ticks;
            }
            System.Console.WriteLine();
        }
    }
}