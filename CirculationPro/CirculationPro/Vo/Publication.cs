namespace CirculationPro.Vo
{
    public class Publication
    {
        public Publication(string data)
        {
            var temp = data.Split('|');

            if(temp.Length >= 3)
            {
                PublicationId = temp[0];
                PublicationName = temp[1];
                Url = temp[2];
            }
        }
        public string PublicationId { get; set; }
        public string PublicationName { get; set; }
        public string Url { get; set; }
    }
}
