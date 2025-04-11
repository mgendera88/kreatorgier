namespace familiada.Models
{
	public class Zespol
	{
		public string Nazwa { get; set; }
		public DateTime? Przycisniety { get; set; }
		public int szanse {  get; set; }
		public int punkty { get; set; }
		public Zespol() 
		{
			punkty= 0;
			szanse= 3;
		}
	}
}
