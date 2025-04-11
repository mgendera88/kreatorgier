namespace familiada.Models
{
    public class AdminViewModel
    {
        public List<Zespol> Zespoly { get; set; } = new();
        public List<Pytanie> Pytania { get; set; } = new();
        public int runda=0;
    }
}
