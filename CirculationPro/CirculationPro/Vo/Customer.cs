namespace CirculationPro.Vo
{
    public class Customer
    {
        public Customer(string data)
        {
            string[] sections = data.Split('[');

            Profile = new Profile(sections[0]);

            var temp = sections[1].TrimEnd(']').Split('^');
            Subscriptions = new Subscription[temp.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                Subscriptions[i] = new Subscription(temp[i]);
            }

            temp = sections[2].TrimEnd(']').Split('^');
            Publications = new Publication[temp.Length];
            for(int i=0; i<temp.Length; i++)
            {
                Publications[i] = new Publication(temp[i]);
            }
        }

        public Profile Profile { get; set; }

        public Subscription[] Subscriptions { get; set; }

        public Publication[] Publications { get; set; }
    }
}
