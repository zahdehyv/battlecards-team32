namespace AI
{
    class Trainer
    {
        Deck A;
        Deck B;
        int iterations;
        int turnos;
        double step;
        Board TrainingBoard;
        VirtualPlayer trainedA;
        VirtualPlayer trainedB;
        public Trainer(Deck _A, Deck _B, int _ite, int _turns)
        {
            A = _A;
            B = _B;
            iterations = _ite;
            if (_ite > 0)
                step = 1 / (double)_ite;
            else step = 0;
            turnos = _turns;
            trainedA = new VirtualPlayer((Deck)A.Clone(), "A", new (double, double)[1]);
            trainedB = new VirtualPlayer((Deck)B.Clone(), "B", new (double, double)[1]);
            TrainingBoard = new Board(trainedA, trainedB);
        }

        public double[] Train()
        {
            var weightss = WeightThrower(new double[8], 0, new List<double[]>());
            var currentVariation = new double[8];
            foreach (var item in weightss)
            {
                for (int i = 0; i < 4; i++)
                {
                    trainedA.Field[i] = null;
                    trainedB.Field[i] = null;
                }
                // System.Console.WriteLine(item[0]);
                trainedA.Deck = (Deck)A.Clone();
                trainedB.Deck = (Deck)B.Clone();
                trainedA.weights = new (double, double)[]{
                    (item[7],item[6]),
                    (item[5],item[4]),
                    (item[3],item[2]),
                    (item[1],item[0])
                };
                trainedB.weights = new (double, double)[]{
                    (item[7],item[6]),
                    (item[5],item[4]),
                    (item[3],item[2]),
                    (item[1],item[0])
                };
                var currTurns = TrainingBoard.FirstTurnTraining(trainedA, trainedB, turnos);
                if (currTurns <= turnos)
                {
               //     System.Console.WriteLine("Found");
                 //   System.Console.WriteLine(currTurns);
                    turnos = currTurns;
                    currentVariation = (double[])item.Clone();
                  //  foreach (var itecm in currentVariation)
                    //{
                      //  System.Console.Write(itecm + " ");
               //     }
                 //   System.Console.WriteLine();
                }
            }
            return currentVariation;
        }
        IEnumerable<double[]> WeightThrower(double[] variation, int ind, List<double[]> list)
        {
            if (ind >= variation.Length)
            {
                list.Add((double[])variation.Clone());
                return list;
            }
            for (int i = 0; i <= iterations; i++)
            {
                variation[ind] = step * i;
                WeightThrower(variation, ind + 1, list);
            }
            return list;
        }

    }
}