using System;
using System.Text;
using System.Linq;
using System.IO;
using AppMusic.Entities;
using AppMusic.Services;
using AppMusic.Services.PaymentServices;
using System.Globalization;


namespace AppMusic.Services
{
    class InvoiceService
    {
        private readonly IPayment _paymentService;

        public Order Order;

        public string BaseDir = AppContext.BaseDirectory.Substring(0, 49);

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

            foreach (Music M in Order.MusicService.ListOfMusics)
            {
                if(M.Available == false)
                {
                    Sb.AppendLine($"|||||||||||| {M.Id}, Name: {M.Band} - {M.Name}, Price: {M.Price}");
                }
            }
            //return Sb.ToString();

            var finalAmount = Order.MusicService.ListOfMusics.Where(x => x.Available == false).Select(x => x.Price).Sum();
            var first = _paymentService.Fee(finalAmount);
            var second = _paymentService.Tax(first);
            Sb.AppendLine($"|||||||||||| Total Value: {second.ToString("F2",CultureInfo.InvariantCulture)}");
            Sb.AppendLine("------------------------------------------------------------------------------");
            return Sb.ToString();
        }


        public void InvoiceDocument()
        {
            int N = 1;
            string Dir = BaseDir + @"\Invoice\";
            string Source = BaseDir + @"\Invoice\inovice" + N + ".txt";

            if (!Dir.Any())
            {
                using (StreamWriter sw = File.CreateText(Source))
                {
                    sw.WriteLine(this.InvoiceProcess());
                    /*sw.WriteLine("INVOICE DOCUMENT:");
                    sw.WriteLine("Please do not share your data with anyone.");
                    sw.WriteLine("Order:#" + Order.OrderId);*/

                }
            }
            else
            {
                N++;
                using (StreamWriter sw = File.CreateText(Source))
                {
                    sw.WriteLine(this.InvoiceProcess());
                }

            }
        }

        public void OpenInvoice(int Number)
        {
            List<string> List = new List<string>();
            string Source = BaseDir + @"\Invoice\inovice" + Number + ".txt";

            if(Source.Any())
            {
                using (StreamReader sr = File.OpenText(Source))
                {
                    while (!sr.EndOfStream)
                    {
                        List.Add(sr.ReadLine());
                    }
                }
            }

            foreach(string Line in List)
            {
                Console.WriteLine(Line);
            }
        }
   
    }
}
