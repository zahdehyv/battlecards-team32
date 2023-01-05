namespace YUGIOH
{
    public class Board : ICloneable
    {
        public Player P1;
        public Player P2;
        public string TextBox;
        public int Round;

        public Board(Player P1, Player P2)
        {
            this.P1 = P1;
            this.P2 = P2;
            Round = 0;
            TextBox = "START";
        }

        public Board(Board board)
        {
            P1 = (Player)board.P1.Clone();
            P2 = (Player)board.P2.Clone();
            TextBox = "";
            Round = 0;
        }

        public Object Clone()
        {
            return new Board(this);
        }

        public void FirstTurn(Player current, Player adversary)
        {
            //Invvocacion inicial
            PBTout.PrintField(adversary, current);
            current.PlayCard(adversary);
            PBTout.PrintField(current, adversary);
            adversary.PlayCard(current);
            UpdateBoard();
            if (End())
            {
                Console.Clear();
                AnsiConsole.Write(new FigletText(GetWinner())
            .Centered()
            .Color(Color.White));
                AnsiConsole.Write(new FigletText("WINS")
            .Centered()
            .Color(Color.Green));
                Console.ReadKey(true);
                Game.MainMenu();
            }
            AnsiConsole.Write(new FigletText(current.PLAY(adversary, this))
        .Centered()
        .Color(Color.White));
            AnsiConsole.Write(new FigletText("WINS")
            .Centered()
            .Color(Color.Green));
            Console.ReadKey(true);
            Game.MainMenu();
        }
        public int FirstTurnTraining(VirtualPlayer current, VirtualPlayer adversary, int Maxturn)
        {
            //Invvocacion inicial
            current.PlayCardT(adversary);
            adversary.PlayCardT(current);
            UpdateBoard();
            return current.TRAIN(adversary, this, 0, Maxturn);
        }

        public string GetWinner()
        {
            if (P1.IsDeckEmpty() && P1.IsFieldEmpty())
                return P2.Name;
            else if (P2.IsDeckEmpty() && P2.IsFieldEmpty())
                return P1.Name;
            else return "TIE";
        }

        public Player GetPlayerFromInt(int p)
        // Recibe un entero y devuelve el jugador al que esta haciendo referencia (0 se utiliza para player1 y cualquier otro numero para player2)
        {
            if (p == 0) return P1;
            return P2;
        }

        public void ExecuteActions(AccionIndex[] Accions, Player cP)//This Method is for VirtualPlayer only
        {

            for (int i = 0; i < Accions.Length; i++)
            {
                var CurrentAccionIndex = Accions[i];

                var TargetPlayerIndex = CurrentAccionIndex.Player;
                var TargetPlayer = GetPlayerFromInt(TargetPlayerIndex);

                var TargetIndex = CurrentAccionIndex.FieldIndex;

                var AccionToExecute = CurrentAccionIndex.Index;

                if (TargetPlayer.Field[TargetIndex] != null && cP.Field[i] != null)
                {
                    cP.ExecuteAction(i, AccionToExecute, TargetIndex, TargetPlayer);
                }
            }
        }

        // Returns the Player, Player.Field Card Index
        public List<(Player, int)> GetAllCardsInFieldIndex()
        // Este metodo devuelve todas las cartas del campo desordenadas
        {
            List<(Player, int)> Cards = new List<(Player, int)>();

            for (int i = 0; i < P1.Field.Count(); i++)
            {
                if (P1.Field[i] != null)
                {
                    Cards.Add((P1, i));
                }

                if (P2.Field[i] != null)
                {
                    Cards.Add((P2, i));
                }
            }
            return Cards;
        }

        public void GetAccionOrder(List<(Player, int)> AllCards, List<(Player, int)> SortedList, int Biggest, (Player, int) FastestCard)
        // Este metodo modifica la lista sortedlist para devolver todas las cartas del campo en una lista ordenadas en funcion de su velocidad
        // la lista contien una tupla con un item player(el jugador en cuyo campo esta la carta) y un entero que corresponde a la posicion de dicha carta
        {
            if (AllCards.Count != 0)
            {
                for (int i = 0; i < AllCards.Count; i++)
                {
                    var Current = AllCards[i];
                    var cPlayer = Current.Item1;
                    var Index = Current.Item2;
                    var cSpeed = 0;

                    if (cPlayer.Field[Index].Stats["Speed"] >= 0)
                        cSpeed = cPlayer.Field[Index].Stats["Speed"];

                    if (cSpeed > Biggest)
                    {
                        Biggest = cSpeed;
                        FastestCard = Current;
                    }
                }

                SortedList.Add(FastestCard);
                AllCards.Remove(FastestCard);
                GetAccionOrder(AllCards, SortedList, 0, (P1, -1));
            }
            return;
        }


        public Player GetOppossingPlayer(Player CurrentPlayer)
        {
            if (CurrentPlayer == P1)
                return P2;
            else
                return P1;
        }

        public int GetIntFromPlayer(Player aPlayer)
        {
            if (aPlayer == P1)
                return 0;
            else
                return 1;
        }

        public void UpdateBoard()
        {

            for (int j = 0; j < P1.Field.Count(); j++)
                if (P1.Field[j] != null)
                    if (P1.Field[j].IsDead())
                    {

                        PBTout.PBTPrint($"{P1.Field[j].Name} ha morido", 200, "white");
                        Console.ReadKey(true);
                        P1.Field[j] = null;
                    }


            for (int j = 0; j < P2.Field.Count(); j++)
                if (P2.Field[j] != null)
                    if (P2.Field[j].IsDead())
                    {
                        PBTout.PBTPrint($"{P2.Field[j].Name} ha morido", 200, "white");
                        Console.ReadKey(true);
                        P2.Field[j] = null;
                    }


        }


        public bool IsFieldEmpty() { return P1.IsFieldEmpty() && P2.IsFieldEmpty(); }
        public bool IsMaskComplete(bool[,] mask)
        {
            foreach (bool b in mask)
                if (!b) { return b; }
            return true;
        }

        public bool[] GetFieldBooleanMask(Player player)
        {
            var Mask = new bool[player.Field.Count()];

            for (int i = 0; i < player.Field.Count(); i++)
            {
                Mask[i] = player.Field[i] == null;
            }
            return Mask;
        }
        public (Player, Player) GetPlayers(bool bp)
        {
            var cPlayer = P1;
            var oPlayer = P2;
            if (bp) { cPlayer = P2; oPlayer = P1; }

            return (cPlayer, oPlayer);
        }

        public bool GetBooleanPlayer(int player) { return Convert.ToBoolean(player); }

        public bool IsEmpty(Player P, int iDim) { return (P.Field[iDim] == null); }

        private bool IsIndexOk(int index, int size)
        {
            if (index > size || index <= 0)
                return false;
            return true;
        }
        public bool End() { return (P1.IsDeckEmpty() && P1.IsFieldEmpty()) || (P2.IsDeckEmpty() && P2.IsFieldEmpty()); }

        public void ShowFieldData()
        {
            System.Console.WriteLine("Player1:");
            for (int j = 0; j < P1.Field.Count(); j++)
            {
                if (P1.Field[j] != null)
                {
                    System.Console.Write(j + " ");
                    P1.Field[j].WriteCard();
                }
            }

            System.Console.WriteLine();
            System.Console.WriteLine("Player2:");

            for (int j = 0; j < P2.Field.Count(); j++)
            {
                if (P2.Field[j] != null)
                {
                    System.Console.Write(j + " ");
                    P2.Field[j].WriteCard();
                }
            }
        }

        public void ShowFieldData(Player cPlayer, Player oPlayer)
        {
            System.Console.WriteLine("My Field:");
            for (int j = 0; j < cPlayer.Field.Count(); j++)
            {
                if (cPlayer.Field[j] != null)
                {
                    System.Console.Write(j + " ");
                    cPlayer.Field[j].WriteCard();
                }
            }

            System.Console.WriteLine();
            System.Console.WriteLine("Opponent Field:");
            for (int j = 0; j < oPlayer.Field.Count(); j++)
            {
                if (oPlayer.Field[j] != null)
                {
                    System.Console.Write(j + " ");
                    oPlayer.Field[j].WriteCard();
                }
            }

            System.Console.WriteLine();
        }
    }
}
