namespace Birk.Client.Bestilling.Models.Dtos
{
    public class SimplifiedBarnDto
    {
        public int Pk { get; set; }

        public string? EndretAv { get; set; }
        public DateTime? EndretDato { get; set; }
        public int PersonFk { get; set; }
    }
}
