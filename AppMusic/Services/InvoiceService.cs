using System;
using System.Text;
using System.Linq;
using System.IO;
using AppMusic.Entities;
using AppMusic.Services;
using AppMusic.Services.PaymentServices;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AppMusic.Services.Exceptions;


namespace AppMusic.Services
{
    class InvoiceService
    {
        private readonly IPayment _paymentService;

        public Order Order;

        public string BaseDir = AppContext.BaseDirectory.Substring(0, 49);

        public InvoiceService() { }

        public InvoiceService(IPayment pay, Order or)
        {
            _paymentService = pay;
            Order = or;
        }

        public string InvoiceProcess()
        {
            StringBuilder Sb = new StringBuilder();
            Sb.AppendLine("------------------------------------------------------------------------------");
            Sb.AppendLine("                          INVOICE #" + Order.OrderId);
            Sb.AppendLine("|||||||||||| Buyer:");
            Sb.AppendLine("|||||||||||| Name: " + Order.BuyerDetails.Name);
            Sb.AppendLine("|||||||||||| Email: " + Order.BuyerDetails.Email);
            Sb.AppendLine("|||||||||||| Phone Number: " + Order.BuyerDetails.PhoneNumber);
            Sb.AppendLine("||||||||||||");
            Sb.AppendLine("|||||||||||| Musics:");

            foreach (Music M in Order.OrderList)
            {
                Sb.AppendLine($"|||||||||||| {M.Id}, Name: {M.Band} - {M.Name}, Price: {M.Price}");
            }

            var finalAmount = Order.OrderList.Where(x => x.Available == false).Select(x => x.Price).Sum();
            var first = _paymentService.Fee(finalAmount);
            var second = _paymentService.Tax(first);
            Sb.AppendLine($"|||||||||||| Total Value: {second.ToString("F2", CultureInfo.InvariantCulture)}");
            Sb.AppendLine("------------------------------------------------------------------------------");
            return Sb.ToString();
        }


        public void InvoiceDocument()
        {
            string Dir = BaseDir + @"\Invoice\";
            string Source = BaseDir + @"\Invoice\Invoice" + Order.OrderId + ".txt";

            if (!Dir.Any())
            {
                using (StreamWriter sw = File.CreateText(Source))
                {

                    sw.WriteLine(this.InvoiceProcess());
                }
            }
            else
            {
                Source = BaseDir + @"\Invoice\Invoice" + Order.OrderId + ".txt";
                using (StreamWriter sw = File.CreateText(Source))
                {
                    sw.WriteLine(this.InvoiceProcess());
                }
            }

        }

        public void OpenInvoice(int Number)
        {
            List<string> List = new List<string>();
            string Source = BaseDir + @"\Invoice\Invoice" + Number + ".txt";

            if (Source.Any())
            {
                using (StreamReader sr = File.OpenText(Source))
                {
                    while (!sr.EndOfStream)
                    {
                        List.Add(sr.ReadLine());
                    }
                }
            }

            foreach (string Line in List)
            {
                Console.WriteLine(Line);
            }
        }

        public void InvoicesListShow()
        {

            List<string> List = new List<string>();

            var BaseDir = AppContext.BaseDirectory.Substring(0, 49) + @"\Invoice\";
            int index = Directory.GetFiles(BaseDir).Length;

            for (int i = 1; i >= 1 && i <= index; i++)
            {
                string Source = BaseDir + @"\Invoice" + i + ".txt";

                var lOne = File.ReadAllLines(Source).Skip(1).Take(1).First().ToString().Substring(26);
                var lTwo = File.ReadAllLines(Source).Skip(3).Take(1).First().ToString().Substring(13);
                List.Add(lOne);
                List.Add(lTwo);

            }

            foreach (string Line in List)
            {
                Console.WriteLine(Line);
            }
            
        }

    }
}
