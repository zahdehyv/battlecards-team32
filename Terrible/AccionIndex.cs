namespace YUGIOH
{
    public class AccionIndex
    {
        public Player Player;
        public int FieldIndex;

        public AccionIndex(Player player, int FieldIndex)
        {
            this.Player = player;
            this.FieldIndex = FieldIndex;
        }
    }
}