namespace PBT
{
    public static class PBTout
    {
        public static void PBTPrint(string toprint, long ms)
        {
            var start = DateTime.Now.Ticks;
            foreach (var item in toprint)
            {
                while ((DateTime.Now.Ticks - start) < ms*1000)
                {
                }
                System.Console.Write(item);
                start = DateTime.Now.Ticks;
            }
            System.Console.WriteLine();
        }
    }
}