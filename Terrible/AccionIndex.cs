namespace YUGIOH
{
    public class AccionIndex : ICloneable
    {
        public int Player;//Index Of the player to be affected
        public int FieldIndex;//Index of the card to be affected
        public int Index;//El index de la accion como tal

        public AccionIndex(int player, int FieldIndex, int Index)
        {
            this.Player = player;
            this.FieldIndex = FieldIndex;
            this.Index = Index;
        }

        public AccionIndex(AccionIndex AI)
        {
            Player= AI.Player;
            FieldIndex = AI.FieldIndex;
            Index = AI.Index;
        }

        public object Clone()
        {
            return new AccionIndex(this);
        }
    }
}