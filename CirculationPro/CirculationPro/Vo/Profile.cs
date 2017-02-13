using System;

namespace CirculationPro.Vo
{
    public class Profile
    {
        public Profile(string data)
        {
            string[] temp = data.Split('|');

            if(temp.Length >= 21)
            {
                NameId = Int32.Parse(temp[0]);
                Salutation = temp[1];
                FirstName = temp[2];
                MiddleName = temp[3];
                LastName = temp[4];
                Suffix = temp[5];
                Company = temp[6];
                Password = temp[7];
                Other1 = temp[8];
                Other2 = temp[9];
                Address = temp[10];
                AddressId = Int32.Parse(temp[11]);
                City = temp[12];
                State = temp[13];
                Zip = temp[14];
                Phone = temp[15];
                Cell = temp[16];
                Fax = temp[17];
                Email = temp[18];
                Url = temp[19];
                ModifyUrl = temp[20];
            }
        }

        public int NameId { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Company { get; set; }
        public string Password { get; set; }
        public string Other1 { get; set; }
        public string Other2 { get; set; }
        public string Address { get; set; }
        public int AddressId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public string ModifyUrl { get; set; }
    }
}

