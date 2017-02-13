using Microsoft.VisualStudio.TestTools.UnitTesting;
using CirculationPro;
using System.Threading.Tasks;
using System;
using CirculationPro.Vo;

namespace CirculationProTests
{
    [TestClass]
    public class AdPowerWebServiceTest
    {
        private string CIRC_NAME = Environment.GetEnvironmentVariable("CIRC_NAME");
        private string CIRC_PASS = Environment.GetEnvironmentVariable("CIRC_PASS");

        [TestMethod]
        public async Task TestGetCustomersAsync()
        {

            AdPowerWebService adPowerWebService = new AdPowerWebService();

            var customers = await adPowerWebService.GetCustomersAsync(null, null, null, null, "", "tindle", null, null, "", null, CIRC_NAME, CIRC_PASS);

            Assert.IsTrue(customers.Length > 0, "Expecting result.Length > 0");

            for(int i=0; i<customers.Length; i++)
            {
                Assert.IsTrue(customers[i] is Customer, "Expecting customers[i] is Customer");
                Assert.IsTrue(customers[i].Profile.LastName == "TINDLE", " Expecting customers[i].Profile.LastName == 'TINDLE'");
            }

            Assert.IsTrue(customers[0].Profile.NameId == 1293350);
            Assert.IsTrue(customers[0].Profile.FirstName == "CHUCK");
            Assert.IsTrue(customers[0].Profile.MiddleName == "");
            Assert.IsTrue(customers[0].Profile.LastName == "TINDLE");
            Assert.IsTrue(customers[0].Profile.Suffix == "");
            Assert.IsTrue(customers[0].Profile.Company == "");
            Assert.IsTrue(customers[0].Profile.Password == "");
            Assert.IsTrue(customers[0].Profile.Other1 == "002543825");
            Assert.IsTrue(customers[0].Profile.Other2 == "");
            Assert.IsTrue(customers[0].Profile.Address == "1629 WAIKAHALULU LN APT A114");
            Assert.IsTrue(customers[0].Profile.AddressId == 1444676);
            Assert.IsTrue(customers[0].Profile.City == "HONOLULU");
            Assert.IsTrue(customers[0].Profile.AddressId == 1444676);
            Assert.IsTrue(customers[0].Profile.State == "HI");
            Assert.IsTrue(customers[0].Profile.Zip == "96817");
            Assert.IsTrue(customers[0].Profile.Phone == "");
            Assert.IsTrue(customers[0].Profile.Cell == "");
            Assert.IsTrue(customers[0].Profile.Fax == "");
            Assert.IsTrue(customers[0].Profile.Email == "");
            Assert.IsTrue(customers[0].Profile.Url == "");
            Assert.IsTrue(customers[0].Profile.ModifyUrl == "");


            var temp = new string[3][];
            temp[0] = "1338721|1000003|HONOLULU MAGAZINE|FULL RATE|COMPLEMENTARY|12/01/2017|0340929|Active|Gift|1||Postal|".Split('|');
            temp[1] = "1462772|1000005|NEWSLETTER SIGNUP|REGULAR|REGULAR|03/28/2013|0466970|Inactive|PIO|1||Postal|SMTWTFS".Split('|');
            temp[2] = "1289161|1000012|HAWAII MAGAZINE|REGULAR|COMPLIMENTARY|12/01/2017|0290397|Active|Gift|1||Postal|".Split('|');

            Assert.IsTrue(customers[0].Subscriptions.Length == 3);

            for (int i=0; i<temp.Length; i++)
            {
                Assert.IsTrue(customers[0].Subscriptions[i].SubId == temp[i][0]);
                Assert.IsTrue(customers[0].Subscriptions[i].PublicationId == temp[i][1]);
                Assert.IsTrue(customers[0].Subscriptions[i].PublicationName == temp[i][2]);
                Assert.IsTrue(customers[0].Subscriptions[i].SubscriptionRate == temp[i][3]);
                Assert.IsTrue(customers[0].Subscriptions[i].SubscriptionSubrate == temp[i][4]);
                Assert.IsTrue(customers[0].Subscriptions[i].Expiration == temp[i][5]);
                Assert.IsTrue(customers[0].Subscriptions[i].Account == temp[i][6]);
                Assert.IsTrue(customers[0].Subscriptions[i].Status == temp[i][7]);
                Assert.IsTrue(customers[0].Subscriptions[i].PayType == temp[i][8]);
                Assert.IsTrue(customers[0].Subscriptions[i].Copies == Int32.Parse(temp[i][9]));
                Assert.IsTrue(customers[0].Subscriptions[i].Url == temp[i][10]);
                Assert.IsTrue(customers[0].Subscriptions[i].DeliveryMethod == temp[i][11]);
            }

            var temp2 = new string[3][];
            temp[0] = "1000002|HAWAII BUSINESS|&pid=1000002".Split('|');
            temp[1] = "1000004|HAWAII BUYERS GUIDE|&pid=1000004".Split('|');
            temp[2] = "1000006|SHOPS WAIKIKI|&pid=1000006".Split('|');

            for(int i=0; i<temp2.Length; i++)
            {
                Assert.IsTrue(customers[0].Publications[i].PublicationId == temp[i][0]);
                Assert.IsTrue(customers[0].Publications[i].PublicationName == temp[i][1]);
                Assert.IsTrue(customers[0].Publications[i].Url == temp[i][2], customers[0].Publications[i].Url + ":" + temp[i][2]);
            }

        }

        [TestMethod]
        public async Task TestGetCustomersExceptionAsync()
        {
            Exception exception;
            try
            {
                AdPowerWebService adPowerWebService = new AdPowerWebService();
                var result = await adPowerWebService.GetCustomersAsync(null, null, null, null, "", "tindle", null, null, "", null, "", "");
                exception = null;
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsNotNull(exception, "Excpeting exception to be raised");
        }

        [TestMethod]
        public async Task TestGetCustomerAsync()
        {
            AdPowerWebService adPowerWebService = new AdPowerWebService();

            Customer customer = await adPowerWebService.GetCustomerAsync("1460213", CIRC_NAME, CIRC_PASS);
            Assert.IsTrue(customer is Customer, "Expecting customer is Customer");
        }

        [TestMethod]
        public async Task TestGetCustomerExceptionAsync()
        {
            Exception exception;
            try
            {
                AdPowerWebService adPowerWebService = new AdPowerWebService();
                var result = await adPowerWebService.GetCustomerAsync("", "", "");
                exception = null;
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsNotNull(exception, "Expecting exception to be raised");
            Assert.IsTrue(exception.ToString().IndexOf("Invalid circulation log in.") != -1, "Expecting 'Invalid circulation log in.'");
        }

        [TestMethod]
        public async Task TestGetCustomerInvalidNameIdAsync()
        {
            Exception exception;
            try
            {
                AdPowerWebService adPowerWebService = new AdPowerWebService();
                var result = await adPowerWebService.GetCustomerAsync("", "CHUCK", "CHUCK");
                exception = null;
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsNotNull(exception, "Expecting exception to be raised");
            Assert.IsTrue(exception.ToString().IndexOf("Invalid name id.") != -1, "Expecting Invalid name id.");
        }



        [TestMethod]
        public async Task TestGetCustomerAndBalanceAsync()
        {
            AdPowerWebService adPowerWebService = new AdPowerWebService();

            var customer = await adPowerWebService.GetCustomerAndBalanceAsync("1460213", CIRC_NAME, CIRC_PASS);

            Assert.IsTrue(customer is Customer, "Expecting customer is Customer");
        }

        [TestMethod]
        public async Task TestGetCustomerAndBalanceInvalidAsync()
        {
            Exception exception;
            try
            {
                AdPowerWebService adPowerWebService = new AdPowerWebService();
                var result = await adPowerWebService.GetCustomerAndBalanceAsync("", "", "");
                exception = null;
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsNotNull(exception, "Expecting exception to be raised");
            Assert.IsTrue(exception.ToString().IndexOf("Invalid circulation log in.") != -1, "Expecting 'Invalid circulation log in.'");
        }

        [TestMethod]
        public async Task TestGetCustomerAndBalanceInvalidNameIdAsync()
        {
            Exception exception;
            try
            {
                AdPowerWebService adPowerWebService = new AdPowerWebService();
                var result = await adPowerWebService.GetCustomerAndBalanceAsync("abcdef", CIRC_NAME, CIRC_PASS);
                exception = null;
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsNotNull(exception, "Expecting exception to be raised");
            Assert.IsTrue(exception.ToString().IndexOf("Invalid name id.") != -1, "Expecting Invalid name id.");
        }

        [TestMethod]
        public async Task TestJohn()
        {
            AdPowerWebService adPowerWebService = new AdPowerWebService();
            var customers = await adPowerWebService.GetCustomersAsync(null, null, null, null, null, null, null, null, "johnniereb@optonline.net", null, CIRC_NAME, CIRC_PASS);
            Assert.IsTrue(customers.Length > 0, "Expecting result.Length > 0");
        }
    }
}
