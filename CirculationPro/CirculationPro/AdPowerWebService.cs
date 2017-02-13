using CirculationPro.Vo;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CirculationPro
{
    public class AdPowerWebService
    {
        public async Task<Customer[]> GetCustomersAsync(string address = "", string city = "", string state = "", string zip = "", string firstname = "", string lastname = "", string company = "", string telephone = "", string email = "", string maxrecords = "", string circName = "", string circPass = "")
        {
            using (var client = new HttpClient())
            {
                var body = $@"
                <soapenv:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:def=""http://DefaultNamespace"">
                   <soapenv:Header/>
                   <soapenv:Body>
                      <def:getCustomers soapenv:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
                         <address xsi:type=""soapenc:string"" xmlns:soapenc=""http://schemas.xmlsoap.org/soap/encoding/""{address}></address>
                         <city xsi:type=""soapenc:string"" xmlns:soapenc=""http://schemas.xmlsoap.org/soap/encoding/"">{city}</city>
                         <state xsi:type=""soapenc:string"" xmlns:soapenc=""http://schemas.xmlsoap.org/soap/encoding/"">{state}</state>
                         <zip xsi:type=""soapenc:string"" xmlns:soapenc=""http://schemas.xmlsoap.org/soap/encoding/"">{zip}</zip>
                         <firstname xsi:type=""soapenc:string"" xmlns:soapenc=""http://schemas.xmlsoap.org/soap/encoding/"">{firstname}</firstname>
                         <lastname xsi:type=""soapenc:string"" xmlns:soapenc=""http://schemas.xmlsoap.org/soap/encoding/"">{lastname}</lastname>
                         <company xsi:type=""soapenc:string"" xmlns:soapenc=""http://schemas.xmlsoap.org/soap/encoding/"">{company}</company>
                         <telephone xsi:type=""soapenc:string"" xmlns:soapenc=""http://schemas.xmlsoap.org/soap/encoding/"">{telephone}</telephone>
                         <email xsi:type=""soapenc:string"" xmlns:soapenc=""http://schemas.xmlsoap.org/soap/encoding/"">{email}</email>
                         <maxrecords xsi:type=""soapenc:string"" xmlns:soapenc=""http://schemas.xmlsoap.org/soap/encoding/"">{maxrecords}</maxrecords>
                         <circName xsi:type=""soapenc:string"" xmlns:soapenc=""http://schemas.xmlsoap.org/soap/encoding/"">{circPass}</circName>
                         <circPass xsi:type=""soapenc:string"" xmlns:soapenc=""http://schemas.xmlsoap.org/soap/encoding/"">{circName}</circPass>
                      </def:getCustomers>
                   </soapenv:Body>
                </soapenv:Envelope>";

                var response = await client.PostAsync("https://honoluluhipb.circulationpro.com/scripts/WebObjects.exe/PacBasinWebService.woa/1/ws/Law", new StringContent(body, Encoding.UTF8, "application/xml"));
                var content = await response.Content.ReadAsStringAsync();

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(content);
                XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
                xmlNamespaceManager.AddNamespace("ns1", "http://DefaultNamespace");
                XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//ns1:getCustomersResponse/getCustomersReturn/getCustomersReturn", xmlNamespaceManager);

                if (xmlNodeList[0].InnerText != "")
                {
                    throw new Exception(xmlNodeList[0].InnerText);
                }
                else
                {
                    Customer[] customers = new Customer[xmlNodeList.Count-1];
                    for (int i = 0; i < xmlNodeList.Count-1; i++)
                    {
                        customers[i] = new Customer(xmlNodeList[i+1].InnerText);
                    }
                    return customers;
                }
            }
        }

        public async Task<Customer> GetCustomerAsync(string nameId = "", string circName = "", string circPass = "")
        {
            using (var client = new HttpClient())
            {
                var body = $@"
                <soapenv:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:def=""http://DefaultNamespace"">
                    <soapenv:Header/>
                    <soapenv:Body>
                        <def:getCustomer soapenv:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
                            <nameID xsi:type=""soapenc:string"" xmlns:soapenc=""http://schemas.xmlsoap.org/soap/encoding/"">{nameId}</nameID>
                            <circName xsi:type=""soapenc:string"" xmlns:soapenc=""http://schemas.xmlsoap.org/soap/encoding/"">{circName}</circName>
                            <circPass xsi:type=""soapenc:string"" xmlns:soapenc=""http://schemas.xmlsoap.org/soap/encoding/"">{circPass}</circPass>
                        </def:getCustomer>
                    </soapenv:Body>
                </soapenv:Envelope>";

                var response = await client.PostAsync("https://honoluluhipb.circulationpro.com/scripts/WebObjects.exe/PacBasinWebService.woa/1/ws/Law", new StringContent(body, Encoding.UTF8, "application/xml"));
                var content = await response.Content.ReadAsStringAsync();

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(content);
                XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
                xmlNamespaceManager.AddNamespace("ns1", "http://DefaultNamespace");
                XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//ns1:getCustomerResponse/getCustomerReturn/getCustomerReturn", xmlNamespaceManager);

                if (xmlNodeList[0].InnerText != "")
                {
                    throw new Exception(xmlNodeList[0].InnerText);
                }
                else
                {
                    return new Customer(xmlNodeList[1].InnerText);
                }
            }
        }

        public async Task<Customer> GetCustomerAndBalanceAsync(string nameId = "", string circName = "", string circPass = "")
        {
            using (var client = new HttpClient())
            {
                var body = $@"
                <soapenv:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:def=""http://DefaultNamespace"">
                    <soapenv:Header/>
                    <soapenv:Body>
                        <def:getCustomerAndBalance soapenv:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
                            <nameID xsi:type=""soapenc:string"" xmlns:soapenc=""http://schemas.xmlsoap.org/soap/encoding/"">{nameId}</nameID>
                            <circName xsi:type=""soapenc:string"" xmlns:soapenc=""http://schemas.xmlsoap.org/soap/encoding/"">{circName}</circName>
                            <circPass xsi:type=""soapenc:string"" xmlns:soapenc=""http://schemas.xmlsoap.org/soap/encoding/"">{circPass}</circPass>
                        </def:getCustomerAndBalance>
                   </soapenv:Body>
                </soapenv:Envelope>";

                var response = await client.PostAsync("https://honoluluhipb.circulationpro.com/scripts/WebObjects.exe/PacBasinWebService.woa/1/ws/Law", new StringContent(body, Encoding.UTF8, "application/xml"));
                var content = await response.Content.ReadAsStringAsync();

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(content);
                XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
                xmlNamespaceManager.AddNamespace("ns1", "http://DefaultNamespace");
                XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//ns1:getCustomerAndBalanceResponse/getCustomerAndBalanceReturn/getCustomerAndBalanceReturn", xmlNamespaceManager);

                if (xmlNodeList[0].InnerText != "")
                {
                    throw new Exception(xmlNodeList[0].InnerText);
                }
                else
                {
                    return new Customer(xmlNodeList[1].InnerText);
                }
            }
        }

    }
}
