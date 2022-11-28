namespace YUGIOH
{
    public class Effect
    {
        public string Stat;
        public int Cant;

        public Effect(string aStat, int aCant)
        {
            Stat = aStat;
            Cant = aCant;
        }

        public void Execute(Card card)
        {
            card.Stats[Stat] -= Cant;
        }
    }

    

    // class SimpleAttack:Effect
    // {
    //     public SimpleAttack()
    // }

    // class Damage:Effect
    // {

    // }
}