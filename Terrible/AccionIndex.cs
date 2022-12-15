namespace YUGIOH
{
    public class AccionIndex : ICloneable
    {
        public int Player;
        public int FieldIndex;

        public AccionIndex(int player, int FieldIndex)
        {
            this.Player = player;
            this.FieldIndex = FieldIndex;
        }

        public AccionIndex(AccionIndex AI)
        {
            Player= AI.Player;
            FieldIndex = AI.FieldIndex;
        }

        public object Clone()
        {
            return new AccionIndex(this);
        }
    }
}