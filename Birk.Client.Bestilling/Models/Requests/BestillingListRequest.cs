namespace Birk.Client.Bestilling.Models.Requests
{
    public class BestillingListRequest
    {
        public int Pk { get; set; }
        public string AnsvarligKommune { get; set; }
        public string Bydel { get; set; }
        public string Resyme { get; set; }

        public BestillingListRequest(int pk, string ansvarligKommune, string bydel, string resyme)
        {
            Pk = pk;
            AnsvarligKommune = ansvarligKommune;
            Bydel = bydel;
            Resyme = resyme;
        }
    }
}
