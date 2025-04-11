using familiada.Models;
using System.Text.Json;
using System.IO;

namespace familiada.Services
{
    public class PytaniaService
    {
        private string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pytania.json");

        public List<Pytanie> OdczytajPytania()
        {
            if (!File.Exists(_filePath))
                return new List<Pytanie>();

            try
            {
                var json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Pytanie>>(json) ?? new List<Pytanie>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd JSON: " + ex.Message);
                return new List<Pytanie>();
            }
        }
    }
}
