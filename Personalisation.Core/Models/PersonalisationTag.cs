namespace Personalisation.Core.Models
{
    public class PersonalisationTag
    {
        public PersonalisationTag(string tag)
        {
            Tag = tag;
            Score = 1;
        } 
        public string Tag { get; set; } 
        public int Score { get; set; }
    }
}