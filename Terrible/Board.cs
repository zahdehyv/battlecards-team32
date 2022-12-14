namespace YUGIOH
{
    public class Board
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

        public void Play()
        {
            Round += 1;
            while (!End())
            {
                System.Console.WriteLine();
                System.Console.WriteLine("ROUND " + Round);
                System.Console.WriteLine();

                // ----------------------------------------
                // if (!P1.IsDeckEmpty()) { P1.Draw(0); }
                // PlayCard(0);
                // if (!P2.IsDeckEmpty()) { P2.Draw(0); }
                // PlayCard(1);
                // -----------------------------------------
                // Si el campo tiene espacio libre y el deck no esta vacio
                // Selecciona tantas cartas del deck como espacios libres o hasta que el deck este vacio

                P1.PlayCard();
                P2.PlayCard();

                var P1Accions = P1.GetActions(P2);//
                // System.Console.WriteLine("SEXOOOOOOOOOOOOO");
                var P2Accions = P2.GetActions(P1);

                List<(Player, int)> AccionOrder = new List<(Player, int)>();
                var AllCardsInField = GetAllCardsInFieldIndex();

                GetAccionOrder(AllCardsInField, AccionOrder, 0, (P1, -1));

                // ExecuteActions(AccionOrder,P1Accions,P2Accions);

                System.Console.WriteLine("EXECUTEACTIONS COMENTADO POR REPARACIONES. FIN DEL PROGRAMA");

                Round += 1;
                return;



            }
        }


        public void ExecuteActions(List<(Player, int)> AccionOrder, (Player, int)[] P1Accions, (Player, int)[] P2Accions)
        {
            foreach (var e in AccionOrder)
            {

                if (e.Item1 == P1)
                {
                    var CurrentCardAccion = P1Accions[e.Item2];
                    var CurrentCardAccionPlayer = CurrentCardAccion.Item1;
                    var CurrentCardAccionTatgetIndex = CurrentCardAccion.Item2;
                    var attacked_card = CurrentCardAccionPlayer.Field[CurrentCardAccionTatgetIndex];

                    if (attacked_card != null) { P1.Field[e.Item2].SimpleAttack(attacked_card); }
                }
                if (e.Item1 == P2)
                {
                    var CurrentCardAccion = P2Accions[e.Item2];
                    var CurrentCardAccionPlayer = CurrentCardAccion.Item1;
                    var CurrentCardAccionTatgetIndex = CurrentCardAccion.Item2;
                    var attacked_card = CurrentCardAccionPlayer.Field[CurrentCardAccionTatgetIndex];

                    // System.Console.WriteLine("Es null: " + (CurrentCardAccionPlayer.Field[ CurrentCardAccionTatgetIndex] == null));

                    if (attacked_card != null) { P2.Field[e.Item2].SimpleAttack(attacked_card); }
                }
            }
            UpdateBoard();
        }

        public Board SimulateBoard()
        {
            return new Board(P1.Copy(), P2.Copy());
        }
        public void ExecuteActions(AccionIndex[] Accions, Player cP)
        {
            for (int i = 0; i < Accions.Length; i++)
            {
                var t = Accions[i];
                var TargetPlayer = t.Player;
                var TargetIndex = t.FieldIndex;
                if (TargetPlayer.Field[TargetIndex] != null && cP.Field[i] != null)
                {
                    cP.Field[i].SimpleAttack(TargetPlayer.Field[TargetIndex]);
                }
            }
        }
        public Board SimulateAccions(AccionIndex[] Accions, Player cP)
        {
            var B = SimulateBoard();
            B.ExecuteActions(Accions, cP);
            return B;
        }

        // Returns the Player, Player.Field Card Index
        public List<(Player, int)> GetAllCardsInFieldIndex()
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

        // Returns the Player, Player.Field Card Index
        public void GetAccionOrder(List<(Player, int)> AllCards, List<(Player, int)> SortedList, int Biggest, (Player, int) FastestCard)
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
                        // SortedList.Add(Current);
                        // AllCards[i] = (cPlayer, -1);
                    }
                }
                SortedList.Add(FastestCard);
                AllCards.Remove(FastestCard);
                GetAccionOrder(AllCards, SortedList, 0, (P1, -1));
            }
            return;
        }

        // public (Player, int)[] GetActions(Player cP, Player oP)
        // {
        //     bool bp = cP == P2;

        //     (Player, int)[] ans = new (Player, int)[4];//Es 4 porque el terrno es de tamanno fijo 4

        //     for (int i = 0; i < 4; i++)
        //     {
        //         System.Console.WriteLine("Escoge una accion para la carta " + i);

        //         int PlayerAffectedint = GetSelection(2, "Player to be affected", bp);
        //         // int Accion = GetSelection(2,"Accion to take") AQUI EL TIPO DEBE ESCOGER LA ACCION A EJECUTAR

        //         Player PlayerAffected = P1;
        //         if (PlayerAffectedint == 1) { PlayerAffected = P2; }

        //         int CardIndex = GetSelection(4, "affected card", bp);

        //         ans[i] = (PlayerAffected, CardIndex);
        //     }

        //     return ans;
        // }


        public (int, int) GetBoardValue(Player cP, Player oP) { return (cP.GetFieldValue(), oP.GetFieldValue()); }
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

            // foreach(Card c in P1.Field)
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
            ShowBoard(bp, "Choose a " + w2s);

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
            // ShowBoard(bp, "Choose a " + w2s);
            // ShowField();

            int choice = Convert.ToInt32(Console.ReadLine());

            while (!IsIndexOk(choice, size))
            {
                System.Console.WriteLine("Indexa bien comunista >=(");
                choice = Convert.ToInt32(Console.ReadLine());
            }

            return choice - 1;
        }

        // public List<int> Options(Player p)
        // {
        //     List<int> options = new List<int>();
        //     for (int i = 1; i <= p.Hand.Count; i++)
        //     {
        //         options.Add(i);
        //     }
        //     return options;
        // }

        private bool IsIndexOk(int index, int size)
        {
            if (index > size || index <= 0)
                return false;
            return true;
        }

        // public bool End() { return (P1.Life <= 0 || P2.Life <= 0); }
        public bool End() { return (P1.IsDeckEmpty() && P1.IsFieldEmpty()) || (P2.IsDeckEmpty() && P2.IsFieldEmpty()); }



        public void ShowBoard(bool bp, string text)
        {
            Player cPlayer = P1;
            Player oPlayer = P2;
            if (bp) { cPlayer = P2; oPlayer = P1; }

            // System.Console.WriteLine("----------------------------------------------------------------------");
            // ShowHand(P1, b1);
            System.Console.WriteLine("--------------------------------------------");
            ShowField();
            System.Console.WriteLine("--------------------------------------------");

            if (cPlayer == P1) { System.Console.WriteLine("Player1"); }
            else { System.Console.WriteLine("Player2"); }

            // ShowHand(cPlayer);
            System.Console.WriteLine();

            ShowFieldData(cPlayer, oPlayer);
            // ShowHand(P2, b2);
            System.Console.WriteLine(text);
        }

        // public void ShowHand(Player player)
        // {
        //     System.Console.WriteLine();
        //     System.Console.WriteLine("Hand:");
        //     foreach (Card c in player.Hand)
        //     {
        //         string a = " | ";
        //         System.Console.Write(a + c.Name + a);
        //     }
        //     System.Console.WriteLine();
        //     System.Console.WriteLine();
        //     foreach (Card c in player.Hand)
        //     {
        //         // string a = " | ";
        //         // System.Console.Write(a + c.Name + a);
        //         c.WriteCard();
        //     }
        //     System.Console.WriteLine();
        // }

        public void ShowField()
        {
            ShowPlayerField(P1);
            System.Console.WriteLine("--------------------------------------------");
            ShowPlayerField(P2);
        }

        public void ShowFieldData(Player cPlayer, Player oPlayer)
        {
            System.Console.WriteLine("My Field:");
            for (int i = 0; i < cPlayer.Field.GetLength(0); i++)
            {
                for (int j = 0; j < cPlayer.Field.Count(); j++)
                {
                    if (cPlayer.Field[j] != null)
                    {
                        System.Console.Write("[" + i + ", " + j + "] ");
                        cPlayer.Field[j].WriteCard();
                    }
                }
            }
            System.Console.WriteLine();
            System.Console.WriteLine("Opponent Field:");
            for (int i = 0; i < oPlayer.Field.GetLength(0); i++)
            {
                for (int j = 0; j < oPlayer.Field.Count(); j++)
                {
                    if (oPlayer.Field[j] != null)
                    {
                        System.Console.Write("[" + i + ", " + j + "] ");
                        oPlayer.Field[j].WriteCard();
                    }
                }
            }
            System.Console.WriteLine();
        }

        public void ShowPlayerField(Player P)
        {
            var Field = P.Field;

            for (int i = 0; i < Field.GetLength(0); i++)
            {
                for (int j = 0; j < Field.Count(); j++)
                {
                    if (!(Field[j] == null))
                        System.Console.Write(" | " + Field[j].Name + " | ");
                    else
                        System.Console.Write(" | " + "Empty" + " | ");
                }
                System.Console.WriteLine();
            }
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

