using System;

namespace CirculationPro.Vo
{
    public class Subscription
    {
        public Subscription(string data)
        {
            string[] temp = data.Split('|');

            if(temp.Length >= 12)
            {
                SubId = temp[0];
                PublicationId = temp[1];
                PublicationName = temp[2];
                SubscriptionRate = temp[3];
                SubscriptionSubrate = temp[4];
                Expiration = temp[5];
                Account = temp[6];
                Status = temp[7];
                PayType = temp[8];
                Copies = Int32.Parse(temp[9]);
                Url = temp[10];
                DeliveryMethod = temp[11];
            }
        }

        public string SubId { get; set; }
        public string PublicationId { get; set; }
        public string PublicationName { get; set; }
        public string SubscriptionRate { get; set; }
        public string SubscriptionSubrate { get; set; }
        public string Expiration { get; set; }
        public string Account { get; set; }
        public string Status { get; set; }
        public string PayType { get; set; }
        public int Copies { get; set; }
        public string Url { get; set; }
        public string DeliveryMethod { get; set; }

    }
}
