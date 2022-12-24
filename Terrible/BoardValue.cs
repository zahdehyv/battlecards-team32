namespace YUGIOH
{

    public static class Calc
    {
        public static double MeanValues(double LW, double AW, double DW, double SW, Player current, Player adversary)
        {
            // List<string> Stats = new List<string>{"Life","Attack","Defense","Speed"};
            double[] CurrentStats = new double[4];


            foreach (var ccard in current.Field)
            {
                if (ccard == null)
                    continue;

                int index = 0;
                foreach (var item in ccard.Stats.Values)
                {
                    if(item > 0)
                        CurrentStats[index] += item;
                    index++;
                }
            }

            double[] AdversaryStats = new double[4];


            foreach (var acard in adversary.Field)
            {
                if (acard == null)
                    continue;

                int Index =0;
                
                foreach (var item in acard.Stats.Values)
                {
                    if(item > 0)
                        AdversaryStats[Index] += item;
                    Index++;
                }
            }
            double LifeValue = LW * (CurrentStats[0] - AdversaryStats[0]);
            double AttackValue = AW * (CurrentStats[1] - AdversaryStats[1]);
            double DefenseValue = DW * (CurrentStats[2] - AdversaryStats[2]);
            double SpeedValue = SW * (CurrentStats[3] - AdversaryStats[3]);
            var val = LifeValue + AttackValue + DefenseValue + SpeedValue;
            return LifeValue + AttackValue + DefenseValue + SpeedValue;
        }
    }
}