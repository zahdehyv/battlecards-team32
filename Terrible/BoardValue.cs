namespace YUGIOH
{

    public static class Calc
    {
        public static double MeanValues((double, double) LW, (double, double) AW, (double, double) DW, (double, double) SW, Player current, Player adversary)
        {
            // List<string> Stats = new List<string>{"Life","Attack","Defense","Speed"};
            double[] CurrentStats = new double[4];


            foreach (var ccard in current.Field)
            {
                if (ccard == null)
                    continue;

                for (int i = 0; i < 3; i++)
                    CurrentStats[i] += ccard.Stats.Values.ToArray()[i];

            }

            double[] AdversaryStats = new double[4];


            foreach (var acard in adversary.Field)
            {
                if (acard == null)
                    continue;

                for (int i = 0; i < 3; i++)
                    AdversaryStats[i] += acard.Stats.Values.ToArray()[i];
            }
            double LifeValue = (LW.Item1 * CurrentStats[0] - LW.Item2 * AdversaryStats[0]);
            double AttackValue = (AW.Item1 * CurrentStats[1] - AW.Item2 * AdversaryStats[1]);
            double DefenseValue = (DW.Item1 * CurrentStats[2] - DW.Item2 * AdversaryStats[2]);
            //double SpeedValue = (SW.Item1 * CurrentStats[3] - SW.Item2 * AdversaryStats[3]);
            var val = LifeValue + AttackValue + DefenseValue; // + SpeedValue;
            return val;
        }
    }
}