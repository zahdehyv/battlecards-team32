namespace YUGIOH
{
    public class VirtualPlayer : Player
    {
        public VirtualPlayer(Deck aDeck) : base(aDeck) {}

        public override AccionIndex[] GetActions(Player oP, int oPint, Board board)
        {
            AccionIndex[] ans = new AccionIndex[this.Field.Count()];
            AccionIndex[] permutation = new AccionIndex[this.Field.Count()];
            var Simulation = (Board)board.Clone();

            ans = Permutations(permutation, this.Field.Count(), 0, oP, board.GetBoardValue(this, oP), board, ans, Simulation);

            // // Lo que esta entre estos comentarios es una prueba
            // System.Console.WriteLine();
            // System.Console.WriteLine("ANS");
            // foreach (var item in ans)
            // {
            //     // if (item.Player == null) { System.Console.Write("Null Player, "); }
            //     { System.Console.Write(item.Player + ", "); }//else

            //     System.Console.WriteLine(item.FieldIndex);
            // }
            // // Lo que esta entre estos comentarios es una prueba



            return ans;
        }

        public override void PlayCard()
        {
            if (IsDeckEmpty() || IsFieldFull()) { return; }
            int s = GetMoreValuableCard(this.Deck.Cards);
            PlayCardFromDeck(s, GetFreeSpace());
            PlayCard();
            return;
        }

        public int GetMoreValuableCard(List<Card> a)
        {
            int ans = -1;

            for (int i = 0; i < a.Count(); i++)
            {
                if (a[i] != null)
                {
                    ans = i;
                    break;
                }

            }

            int ansVal = a[ans].GetCardValue();

            for (int i = ans; i < a.Count(); i++)
            {
                if (a[i] != null)
                {
                    var val = a[i].GetCardValue();
                    if (val > ansVal)
                    {
                        ans = i;
                        ansVal = val;
                    }
                }
            }

            if (ans == -1) System.Console.WriteLine("El array esta vacio en GetMore ValuableCard retorno -1");
            return ans;


        }



        public AccionIndex[] Permutations(AccionIndex[] permutation, int N, int i, Player oP, BoardValue BestOutcome, Board board, AccionIndex[] ans, Board Simulation)
        // UNFINISHED TRY USING CLASS ACCIONINDEX INSTEAD OF TUPLE(PLAYER, INT)
        {
            if (i == permutation.Length)
            {
                // Simulation.ShowFieldData();
                // Console.ReadLine();

                Simulation.ExecuteActions(permutation, Simulation.P2);

                // Simulation.ShowFieldData();
                // Console.ReadLine();

                var Value = Simulation.GetBoardValue(Simulation.P2, Simulation.P1);

                // System.Console.WriteLine("Antes del if Value " + Value.cpValue + " " + Value.opValue);
                // Console.ReadLine();

                if (Value.cpValue >= BestOutcome.cpValue && Value.opValue < BestOutcome.opValue)
                {
                    BestOutcome.cpValue = Value.cpValue;
                    BestOutcome.opValue = Value.opValue;
                    ans = (AccionIndex[])permutation.Clone();
                }

                // System.Console.WriteLine();
                // System.Console.WriteLine("Value " + Value.cpValue + " " + Value.opValue);
                // System.Console.WriteLine("BestOutcome " + BestOutcome.cpValue + " " + BestOutcome.opValue);
                // System.Console.WriteLine();

                return ans;
            }

            for (int n = 0; n < N; n++)
            {
                permutation[i] = new AccionIndex(0, n);
                ans = Permutations(permutation, N, i + 1, oP, BestOutcome, board, ans, Simulation);
                Simulation = (Board)board.Clone();

                // // Lo que esta entre estos comentarios es una prueba
                // System.Console.WriteLine();
                // System.Console.WriteLine("ANSWER");
                // foreach (var item in ans)
                // {
                //     if (item == null) { System.Console.WriteLine("Null"); }
                //     else
                //     {
                //         System.Console.Write(item.Player + ", ");
                //         System.Console.WriteLine(item.FieldIndex);
                //     }
                // }
                // // Lo que esta entre estos comentarios es una prueba            


            }
            return ans;
        }

    }

}





// // Lo que esta entre estos comentarios es una prueba
// Simulation.ShowFieldData(Simulation.P2, Simulation.P1);//Player)this.Clone(), (Player)oP.Clone()
// Console.ReadLine();
// board.ShowFieldData(this, oP);
// Console.ReadLine();
// System.Console.WriteLine();
// System.Console.WriteLine("PERMUTATION");
// foreach (var item in permutation)
// {
//     // if (item == null) { System.Console.Write("Null"); }
//     // else
//     {
//         System.Console.Write(item.Player + ", ");
//         System.Console.WriteLine(item.FieldIndex);
//     }
// }
// Console.ReadLine();
// // Lo que esta entre estos comentarios es una prueba

// System.Console.WriteLine("cpValue "+Value.cpValue);
// System.Console.WriteLine("cpBest "+ BestOutcome.cpValue);
// System.Console.WriteLine("Cp Value compare = " + (Value.cpValue >= BestOutcome.cpValue));
// System.Console.WriteLine("opValue "+Value.opValue);
// System.Console.WriteLine("opBest "+ BestOutcome.opValue);
// System.Console.WriteLine("Op Value compare = " + (Value.opValue < BestOutcome.opValue));
// Console.ReadLine();





// // Lo que esta entre estos comentarios es una prueba
// System.Console.WriteLine();
// System.Console.WriteLine("ANSWER");
// foreach (var item in ans)
// {
//     if (item == null) { System.Console.Write("Null"); }
//     else
//     {
//         System.Console.Write(item.Player + ", ");
//         System.Console.WriteLine(item.FieldIndex);
//     }
// }

// System.Console.WriteLine("Value " + Value.cpValue + " " + Value.opValue);
// System.Console.WriteLine("BestOutcome " + BestOutcome.cpValue + " " + BestOutcome.opValue);
// Console.ReadLine();
// // Lo que esta entre estos comentarios es una prueba            
