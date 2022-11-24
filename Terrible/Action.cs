namespace YUGIOH
{
    class Action
    {
        public void Damage(Character Emisor, Character Receptor)
        {

            Receptor.strength -= Emisor.strength; 
        }
    }    
}