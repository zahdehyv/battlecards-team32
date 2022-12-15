using Compiler;

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

        public void Play()
        {
            Round += 1;
            while (!End())
            {
                System.Console.WriteLine();
                System.Console.WriteLine("ROUND " + Round);
                System.Console.WriteLine();

                P1.PlayCard();
                P2.PlayCard();

                var P1Accions = P1.GetActions(P2, 1, this);
                var P2Accions = P2.GetActions(P1, 0, this);

                // System.Console.WriteLine("Player1 Accions");
                // foreach (var element in P1Accions)
                // {
                //     System.Console.WriteLine(element.Player + ", " + element.FieldIndex);
                // }

                // System.Console.WriteLine("Player2 Accions");
                // foreach (var element in P2Accions)
                // {
                //     System.Console.WriteLine(element.Player + ", " + element.FieldIndex);
                // }
                // Console.ReadLine();

                List<(Player, int)> AccionOrder = new List<(Player, int)>();
                var AllCardsInField = GetAllCardsInFieldIndex();

                GetAccionOrder(AllCardsInField, AccionOrder, 0, (P1, -1));

                ExecuteActions(AccionOrder, P1Accions, P2Accions);

                Round += 1;
            }
            return;
        }

        public void ExecuteActions(List<(Player, int)> AccionOrder, AccionIndex[] P1Accions, AccionIndex[] P2Accions)
        // Este metodo recibe la lista de orden de ejecucion de las cartas y las acciones de cada carta de cada jugador
        // Primero va por cada elemento de la lista de orden de ejecucion y realiz un proceso igual en funcion de el jugador que le corresponda
        // al estar el array de acciones ordenado de la misma forma en que estan las cartas en los campos el index de la carta ejecutora
        // Coincide con el de la posicion en el array de acciones en el accionIndex object esta el player al que esta dirigido el ataque
        // y el indice de la carta atacada
        // TODO:Tomar en cuenta la accion especifica de la lista de acciones
        {
            foreach (var e in AccionOrder)
            // Primero comprobar cual es el player que le toca
            {

                if (e.Item1 == P1)
                {
                    var AccionForCurrentCard = P1Accions[e.Item2];//Coger el accionIndex de la carta que le toca

                    var TargetPlayer = GetPlayerFromInt(AccionForCurrentCard.Player);//Coger el jugador al que esta dirigida la accion

                    var TargetIndex = AccionForCurrentCard.FieldIndex;//Coger el index de la carta a la que esta dirigida la accion

                    var attacked_card = TargetPlayer.Field[TargetIndex];//Aqui selecciono a carta sobre la que se realizara la accion

                    if (attacked_card != null && P1.Field[e.Item2] != null)
                    {
                        P1.Field[e.Item2].SimpleAttack(attacked_card); //Aca en vez de Simpple attack tiene que llamar 
                                                                       // al accion escogido por el player
                        System.Console.WriteLine("La carta ");
                        P1.Field[e.Item2].WriteCard();
                        System.Console.WriteLine("De Player1");//
                        System.Console.WriteLine("Ejecuto una accion sobre la carta");
                        attacked_card.WriteCard();
                        System.Console.WriteLine("De " + TargetPlayer.WritePlayer(this));
                        Console.ReadLine();
                        ShowFieldData();
                        Console.ReadLine();
                    }
                    UpdateBoard();
                }
                else if (e.Item1 == P2)
                {
                    var AccionForCurrentCard = P2Accions[e.Item2];

                    var TargetPlayer = GetPlayerFromInt(AccionForCurrentCard.Player);

                    var TargetIndex = AccionForCurrentCard.FieldIndex;

                    var attacked_card = TargetPlayer.Field[TargetIndex];

                    if (attacked_card != null && P2.Field[e.Item2] != null )
                    {
                        P2.Field[e.Item2].SimpleAttack(attacked_card); //Aca en vez de Simpple attack tiene que llamar 
                                                                       // al accion escogido por el player
                        System.Console.WriteLine("La carta ");
                        P2.Field[e.Item2].WriteCard();
                        System.Console.WriteLine("De Player2");
                        System.Console.WriteLine("Ejecuto una accion sobre la carta");
                        attacked_card.WriteCard();
                        System.Console.WriteLine("De " + TargetPlayer.WritePlayer(this));
                        Console.ReadLine();
                        ShowFieldData();
                        Console.ReadLine();
                    }
                    UpdateBoard();
                }
            }
            System.Console.WriteLine("ESTA ES LA PRUEBA");
            ShowFieldData();//THIS IS A TEST
            Console.ReadLine();
            UpdateBoard();
        }


        public Player GetPlayerFromInt(int p)
        // Recibe un entero y devuelve el jugador al que esta haciendo referencia (0 se utiliza para player1 y cualquier otro numero para player2)
        {
            if (p == 0) return P1;
            return P2;
        }

        public void ExecuteActions(AccionIndex[] Accions, Player cP)
        {

            for (int i = 0; i < Accions.Length; i++)
            {
                var t = Accions[i];
                var TargetPlayer = P1;
                if (t.Player != 0) { TargetPlayer = P2; }
                var TargetIndex = t.FieldIndex;
                if (TargetPlayer.Field[TargetIndex] != null && cP.Field[i] != null)
                {
                    cP.Field[i].SimpleAttack(TargetPlayer.Field[TargetIndex]);
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

        public BoardValue GetBoardValue(Player cP, Player oP)
        {
            return (new BoardValue(cP.GetFieldValue(), oP.GetFieldValue()));
        }

        public void UpdateBoard()
        {
            for (int i = 0; i < P1.Field.GetLength(0); i++)
            {
                for (int j = 0; j < P1.Field.Count(); j++)
                {
                    if (P1.Field[j] != null)
                    {
                        if (P1.Field[j].IsDead())
                        {
                            P1.Field[j] = null;
                        }
                    }
                }
            }
            for (int i = 0; i < P2.Field.GetLength(0); i++)
            {
                for (int j = 0; j < P2.Field.Count(); j++)
                {
                    if (P2.Field[j] != null)
                    {
                        if (P2.Field[j].IsDead())
                        {
                            P2.Field[j] = null;
                        }
                    }
                }
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

        public int GetSelection(int size, string w2s, bool bp)
        {
            System.Console.WriteLine("Choose a " + w2s); ;

            int choice = Convert.ToInt32(Console.ReadLine());

            while (!IsIndexOk(choice, size))
            {
                System.Console.WriteLine("Indexa bien comunista >=(");
                choice = Convert.ToInt32(Console.ReadLine());
            }

            return choice - 1;
        }

        public int GetSelection(int size, string w2s)
        {

            int choice = Convert.ToInt32(Console.ReadLine());

            while (!IsIndexOk(choice, size))
            {
                System.Console.WriteLine("Indexa bien comunista >=(");
                choice = Convert.ToInt32(Console.ReadLine());
            }

            return choice - 1;
        }

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

// public void PlayRound()
// {
//     ShowBoard(true, false, "Player1 Turn");
//     P1.ShowHand();
//     System.Console.WriteLine("Select a card pressing a number");
//     string s = System.Console.ReadLine();
//     int selection = Convert.ToInt32(s);
//     // if(Options(P1).Contains(selection))
//     System.Console.WriteLine("Donde lo pongo");
//     System.Console.WriteLine("Fila");
//     int dim0 = Convert.ToInt32(System.Console.ReadLine());
//     System.Console.WriteLine("Columna");
//     int dim1 = Convert.ToInt32(System.Console.ReadLine());
//     P1.PlayCard(selection,dimdim1);
//     ShowBoard(true,false,"");


// }


// public void PlayCard(int player)
// {
//     bool bp = GetBooleanPlayer(player);

//     var cPlayer = P1;
//     if (bp) { cPlayer = P2; }

//     ShowBoard(bp, "Write Something if you want to play a card");

//     if (Console.ReadLine() != "")
//     {


//         // //Esta pincha no se porque la puse
//         // foreach (Card c in cPlayer.Field)
//         // {
//         //     if (c == null)
//         //         break;
//         //     return;
//         // }

//         int selected_card = GetSelection(cPlayer.Hand.Count(), "card", bp);
//         int selected_fil = 0;//GetSelection(cPlayer.Field.GetLength(0), "fila", bp);
//         int selected_col = GetSelection(cPlayer.Field.Count(), "columna", bp);

//         while (!IsEmpty(cPlayer, selected_fil, selected_col))
//         {
//             System.Console.WriteLine("Ese lugar esta ocupado");
//             selected_card = GetSelection(cPlayer.Hand.Count(), "card", bp);
//             selected_fil = GetSelection(cPlayer.Field.GetLength(0), "fila", bp);
//             selected_col = GetSelection(cPlayer.Field.Count(), "columna", bp);
//         }

//         string text = "Carta " + cPlayer.Hand[selected_card].Name + " jugada a posicion: " + "[" + selected_fil + ", " + selected_col + "]";

//         cPlayer.PlayCard(selected_card, selected_fil, selected_col);
//         ShowBoard(bp, text);
//     }

// }

// Este metodo devuelve un array de tuplas , cada tupla coincide con una carta en el campo del jugador
// el primer item es el player sobre el qe va a tenr accion el efecto, y el segundo es un int con el index de la carta 
// sobre la que va tener efecto


// public void ActionPhase(int player)
// {
//     var bp = GetBooleanPlayer(player);
//     var Players = GetPlayers(bp);

//     var cPlayer = Players.Item1;
//     var oPlayer = Players.Item2;

//     var bool_mask = GetFieldBooleanMask(cPlayer);

//     while (!IsMaskComplete(bool_mask))
//     {
//         ShowBoard(bp, "Write something if you are going to take action Write nothing if you dont");

//         if (Console.ReadLine() == "") return;

//         // int selected_card = GetSelection(cPlayer.Hand.Count(), "attacking card", bp);
//         int attackin_fil = 0;//GetSelection(cPlayer.Field.GetLength(0), "attacking card fila", bp);
//         int attackin_col = GetSelection(cPlayer.Field.Count(), "attacking card columna", bp);

//         while (cPlayer.Field[attackin_fil, attackin_col] == null || bool_mask[attackin_fil, attackin_col])
//         {
//             // attackin_fil = 0;//GetSelection(cPlayer.Field.GetLength(0), "attacking card fila", bp);
//             attackin_col = GetSelection(cPlayer.Field.Count(), "attacking card columna", bp);
//         }

//         Card attackin_card = cPlayer.Field[attackin_fil, attackin_col];
//         attackin_card.WriteCard();

//         // if (oPlayer.IsFieldEmpty()) oPlayer.Life -= cPlayer.Field[attackin_fil, attackin_col].Damage();

//         int attacked_fil = 0;//GetSelection(cPlayer.Field.GetLength(0), "attacked card fila", !bp);
//         int attacked_col = GetSelection(oPlayer.Field.Count(), "attacked card columna", bp);

//         while (oPlayer.Field[attacked_fil, attacked_col] == null)
//         {
//             // attacked_fil = 0;//GetSelection(cPlayer.Field.GetLength(0), "attacked card fila", bp);
//             attacked_col = GetSelection(cPlayer.Field.Count(), "attacked card columna", bp);
//         }

//         Card attacked_card = oPlayer.Field[attacked_fil, attacked_col];

//         attackin_card.SimpleAttack(attacked_card);
//         bool_mask[attacked_fil, attackin_col] = true;

//         // attacked_card.TakeDamage(attackin_card.Damage());
//         // Action.SimpleAttack(attackin_card,attacked_card,cPlayer);

//         attacked_card.WriteCard();

//         UpdateBoard();
//     }
// }

