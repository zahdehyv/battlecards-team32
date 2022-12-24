using PBT;
using Compiler;
using Spectre.Console;

namespace YUGIOH
{
    public class VirtualPlayer : Player
    {
        public VirtualPlayer(Deck aDeck, string _name) : base(aDeck, _name) { }

        public AccionIndex[] GetActions(Player oP, Board board)
        {
            int oPint = board.GetIntFromPlayer(oP);
            var cPint = board.GetIntFromPlayer(this);

            AccionIndex[] ans = new AccionIndex[this.Field.Count()];

            var Simulation = (Board)board.Clone();
            var SimulationCurrentPlayer = Simulation.GetPlayerFromInt(cPint);

            List<int> SortedList = new List<int>();
            List<int> NotSortedList = new List<int>();
            for (int i = 0; i < oP.Field.Length; i++)
            {
                NotSortedList.Add(i);
            }
            SortCardsBy("Speed", SortedList, NotSortedList);

            foreach (int i in SortedList)
            {
                if (Field[i] != null)
                {
                    if (cPint == 0)
                    {
                        AccionIndex CurrentAccion = GetCardAccion(Simulation, Simulation.P1, i);
                        ans[i] = CurrentAccion;
                        Simulation.P1.Field[i].ExecuteAction(CurrentAccion.Index, CurrentAccion.FieldIndex, Simulation.GetPlayerFromInt(CurrentAccion.Player), oPint, cPint, Simulation);
                    }
                    else
                    {
                        var CurrentAccion = GetCardAccion(Simulation, Simulation.P2, i);
                        ans[i] = CurrentAccion;
                        Simulation.P2.Field[i].ExecuteAction(CurrentAccion.Index, CurrentAccion.FieldIndex, Simulation.GetPlayerFromInt(CurrentAccion.Player), oPint, cPint, Simulation);
                    }
                }
            }
            return ans;
        }


        public AccionIndex GetCardAccion(Board board, Player CurrentPlayer, int CurrentCardIndex)//TERMINAR METHOD Y TENER EN CUENTA LOS VALORES NULL
        // La entrada es el tablero actual, el jugador actual y el indice de la carta actual
        {
            // Ciclar por cada carta del Player actual
            // En cada carta probar cada accion
            // Tras probar cada accion comparar los valores de cada campo
            // Ciclar de la misma forma por el player contrario

            if (CurrentPlayer.Field[CurrentCardIndex] != null)
            {
                var BestOutcome = Calc.MeanValues(5, 1, 1, 0, CurrentPlayer, board.GetOppossingPlayer(CurrentPlayer));

                int CurrentPlayerInt = board.GetIntFromPlayer(CurrentPlayer);
                int OpposingPlayerInt = board.GetIntFromPlayer(board.GetOppossingPlayer(CurrentPlayer));

                AccionIndex ans = new AccionIndex(OpposingPlayerInt, 0, 0);

                var dim0 = (CurrentPlayer.Field.Length) * 2;
                var dim1 = CurrentPlayer.Field[CurrentCardIndex].Actions.Count;

                for (int i = 0; i < dim0; i++)
                {
                    for (int j = 0; j < dim1; j++)
                    {
                        Board Simulation = (Board)board.Clone();

                        if (i < (int)dim0 / 2)
                        {
                            if (Simulation.P1.Field[i] != null)
                            {
                                var SimCurrentPlayer = Simulation.GetPlayerFromInt(CurrentPlayerInt);
                                SimCurrentPlayer.Field[CurrentCardIndex].ExecuteAction(j, i, Simulation.P1, OpposingPlayerInt, CurrentPlayerInt, Simulation);
                                var CurrentValue = Calc.MeanValues(5, 1, 1, 0, SimCurrentPlayer, Simulation.GetOppossingPlayer(SimCurrentPlayer));
                                if (CurrentValue > BestOutcome)
                                {
                                    BestOutcome = CurrentValue;
                                    ans.Player = 0;
                                    ans.FieldIndex = i;
                                    ans.Index = j;
                                }
                            }
                        }

                        else
                        {
                            if (Simulation.P2.Field[i - (int)dim0 / 2] != null)
                            {
                                var SimCurrentPlayer = Simulation.GetPlayerFromInt(CurrentPlayerInt);
                                SimCurrentPlayer.Field[CurrentCardIndex].ExecuteAction(j, i - (int)dim0 / 2, Simulation.P2, OpposingPlayerInt, CurrentPlayerInt, Simulation);
                                var CurrentValue = Calc.MeanValues(5, 1, 1, 0, SimCurrentPlayer, Simulation.GetOppossingPlayer(SimCurrentPlayer));
                                if (CurrentValue > BestOutcome)
                                {
                                    BestOutcome = CurrentValue;
                                    ans.Player = 1;
                                    ans.FieldIndex = i - (int)dim0 / 2;
                                    ans.Index = j;
                                }
                            }
                        }
                    }
                }
                return ans;
            }
            else
            {
                System.Console.WriteLine("Entro en el else");
                return new AccionIndex(-1, -1, -1);
            }


        }


        public void SortCardsBy(string stat, List<int> SortedList, List<int> NotSortedList)
        {
            if (NotSortedList.Count == 0)
            {
                return;
            }


            int ToAdd = 0;
            if (this.Field[NotSortedList[0]] != null)
            {
                int Biggest = this.Field[NotSortedList[0]].Stats[stat];
                for (int i = 1; i < NotSortedList.Count; i++)
                {
                    if (this.Field[NotSortedList[i]] != null)
                    {
                        var CurrentNumber = this.Field[NotSortedList[i]].Stats[stat];
                        if (CurrentNumber > Biggest)
                        {
                            Biggest = CurrentNumber;
                            ToAdd = i;
                        }
                    }
                }
            }
            SortedList.Add(NotSortedList[ToAdd]);
            NotSortedList.RemoveAt(ToAdd);
            SortCardsBy(stat, SortedList, NotSortedList);
            return;
        }

        public override string PLAY(Player adversary, Board board)
        {
            board.UpdateBoard();
            PBTout.PrintField(adversary, this);
            PlayCard(adversary);
            var actions = GetActions(adversary, board);


            for (int i = 0; i < Field.Length; i++)
            {
                if (Field[i] == null) continue;
                Card ATTACKER = Field[i];
                Accion ACCION = ATTACKER.Actions[actions[i].Index];
                Player PLAYERATTACKED = board.GetPlayerFromInt(actions[i].Player);
                Card ATTACKED = PLAYERATTACKED.Field[actions[i].FieldIndex];

                if (ATTACKER != null && ATTACKED != null)
                {
                    PBTout.PBTPrint($"{ATTACKER.Name} ha usado {ACCION.Name} en {ATTACKED.Name}", 200, "white");
                    ACCION.DoAct(ATTACKER, ATTACKED, this, adversary);
                    Console.ReadKey(true);
                    board.UpdateBoard();
                    PBTout.PrintField(adversary, this);
                    if (board.End())
                    {
                        Console.Clear();
                        return board.GetWinner();
                    }
                }
            }
            System.Console.WriteLine();
            System.Console.WriteLine();
            AnsiConsole.Markup($"[red]TERMINAR TURNO de {Name}[/]");
            Console.ReadKey(true);
            return adversary.PLAY(this, board);
        }


        public override void PlayCard(Player adversary)
        {
            if (IsDeckEmpty() || IsFieldFull()) { return; }
            PBTout.ShowSummonable(Deck);

            var card = GetMoreValuableCard(Deck.Cards);

            PlayCardFromDeck(card, GetFreeSpace());
            PBTout.PBTPrint($"{Name} ha invocado a {card.Name}", 200, "white");
            Console.ReadKey(true);
            PBTout.PrintField(adversary, this);
            PlayCard(adversary);
        }

        public Card GetMoreValuableCard(List<Card> a)
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
            return a[ans];


        }


    }

}



