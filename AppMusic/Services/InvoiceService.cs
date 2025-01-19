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
        private readonly PathDirectoryService _pathDirectoryService;
        public Order Order { get; set; }

        public InvoiceService() { }

        public InvoiceService(IPayment pay, Order or, PathDirectoryService pds)
        {
            this._paymentService = pay;
            this.Order = or;
            this._pathDirectoryService = pds;
        }

        //ACCESS PRIVATE PROPERTIES [SECURITY LABEL]
        public IPayment Pay
        {
            get
            {
                return _paymentService;
            }
        }

        //ACCESS PRIVATE PROPERTIES [SECURITY LABEL]
        public string InvoicePath
        {
            get
            {
                return this._pathDirectoryService.Paths.InvoicePath;
            }
        }

        //RETURNS INVOICE LAYOUT
        public string InvoiceProcess()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("------------------------------------------------------------------------------");
            sb.AppendLine("                          INVOICE #" + Order.OrderId);
            sb.AppendLine("|||||||||||| Buyer:");
            sb.AppendLine("|||||||||||| Name: " + Order.BuyerDetails.Name);
            sb.AppendLine("|||||||||||| Email: " + Order.BuyerDetails.Email);
            sb.AppendLine("|||||||||||| Phone Number: " + Order.BuyerDetails.PhoneNumber);
            sb.AppendLine("||||||||||||");
            sb.AppendLine("|||||||||||| Musics:");

            foreach (Music m in Order.OrderList)
            {
                sb.AppendLine($"|||||||||||| {m.Id}, Name: {m.Band} - {m.Name}, Price: {m.Price}");
            }

            var finalAmount = Order.OrderList.Where(x => x.Available == false).Select(x => x.Price).Sum();
            var first = _paymentService.Fee(finalAmount);
            var second = _paymentService.Tax(first);
            sb.AppendLine($"|||||||||||| Total Value: {second.ToString("F2", CultureInfo.InvariantCulture)}");
            sb.AppendLine("------------------------------------------------------------------------------");
            return sb.ToString();
        }

        //PROCESS THE INVOICE DOCUMENT, EACH NUMBER IS GENERATED ACCORDING TO DIRECTORY.FILES() SIZE
        public void InvoiceDocument()
        {
            string dir = this.InvoicePath;
            string source = this.InvoicePath + @"\Invoice\Invoice" + Order.OrderId + ".txt";

            if (!dir.Any())
            {
                using (StreamWriter sw = File.CreateText(source))
                {

                    sw.WriteLine(this.InvoiceProcess());
                }
            }
            else
            {
                source = this.InvoicePath + @"\Invoice\Invoice" + Order.OrderId + ".txt";
                using (StreamWriter sw = File.CreateText(source))
                {
                    sw.WriteLine(this.InvoiceProcess());
                }
            }

        }

        //METHOD TO OPEN EACH INVOICE
        public void OpenInvoice(int Number)
        {
            List<string> list = new List<string>();

            string source = source = this.InvoicePath + @"\Invoice\Invoice" + Number + ".txt";

            if (source.Any())
            {
                using (StreamReader sr = File.OpenText(source))
                {
                    while (!sr.EndOfStream)
                    {
                        list.Add(sr.ReadLine());
                    }
                }
            }

            foreach (string Line in list)
            {
                Console.WriteLine(Line);
            }
        }

        //LIST ALL INVOICES ON SCREEN
        public void InvoicesListShow()
        {

            List<string> list = new List<string>();

            int index = Directory.GetFiles(this.InvoicePath).Length;

            for (int i = 1; i >= 1 && i <= index; i++)
            {
                string source = this.InvoicePath + @"\Invoice" + i + ".txt";

                var lOne = File.ReadAllLines(source).Skip(1).Take(1).First().ToString().Substring(26);
                var lTwo = File.ReadAllLines(source).Skip(3).Take(1).First().ToString().Substring(13);
                list.Add(lOne);
                list.Add(lTwo);

            }

            foreach (string Line in list)
            {
                Console.WriteLine(Line);
            }
            
        }

    }
}
