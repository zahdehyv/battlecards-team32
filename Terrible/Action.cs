namespace YUGIOH
{
    class Action
    {
        public void Damage(Card Emisor, Card Receptor)
        {

            Receptor.stats["Attack"] -= Emisor.stats["Defense"]; 
        }
    }    
}