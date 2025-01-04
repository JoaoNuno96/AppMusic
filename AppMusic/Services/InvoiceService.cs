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

        public Order Order;

        public string BaseDir = AppContext.BaseDirectory.Substring(0, 45);

        public InvoiceService() { }

        public InvoiceService(IPayment pay, Order or, PathDirectoryService pds)
        {
            this._paymentService = pay;
            this.Order = or;
            this._pathDirectoryService = pds;
        }

        public IPayment Pay
        {
            get
            {
                return _paymentService;
            }
        }

        //Metodo que retorna o layout de invoice.
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

        //Metodo que faz o processamento do documento da invoice, de acordo com o InvoiceProcess() => o layout;
        //Faz o processamento do numero de documento de acordo com o numero de arquivos no diretório com + 1;
        public void InvoiceDocument()
        {
            string dir = this._pathDirectoryService.Paths.InvoicePath;
            string source = this._pathDirectoryService.Paths.InvoicePath + @"\Invoice\Invoice" + Order.OrderId + ".txt";

            if (!dir.Any())
            {
                using (StreamWriter sw = File.CreateText(source))
                {

                    sw.WriteLine(this.InvoiceProcess());
                }
            }
            else
            {
                source = this._pathDirectoryService.Paths.InvoicePath + @"\Invoice\Invoice" + Order.OrderId + ".txt";
                using (StreamWriter sw = File.CreateText(source))
                {
                    sw.WriteLine(this.InvoiceProcess());
                }
            }

        }

        //Metodo para abrir Invoice de acordo com um numero especifico 
        public void OpenInvoice(int Number)
        {
            List<string> list = new List<string>();

            string source = source = this._pathDirectoryService.Paths.InvoicePath + @"\Invoice\Invoice" + Number + ".txt";

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

        //Metodo para listar o numero de invoices
        public void InvoicesListShow()
        {

            List<string> list = new List<string>();

            int index = Directory.GetFiles(this._pathDirectoryService.Paths.InvoicePath).Length;

            for (int i = 1; i >= 1 && i <= index; i++)
            {
                string source = this._pathDirectoryService.Paths.InvoicePath + @"\Invoice" + i + ".txt";

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
