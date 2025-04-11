namespace familiada.Models
{
    public class Pytanie
    {
        public string tresc
        {
            get;
            set;
        }
        public int ktore
        {
            get; set;
        }
        public bool czyfinal { get; set; }
        public List<Odpowiedz> Odpowiedzi { get; set; } = new();
    }
}
