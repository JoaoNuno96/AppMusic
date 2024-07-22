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

        public Order order;

        public string baseDir = AppContext.BaseDirectory.Substring(0, 49);

        public InvoiceService() { }

        public InvoiceService(IPayment pay, Order or)
        {
            _paymentService = pay;
            order = or;
        }

        //Metodo que retorna o layout de invoice.
        public string invoiceProcess()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("------------------------------------------------------------------------------");
            sb.AppendLine("                          INVOICE #" + order.orderId);
            sb.AppendLine("|||||||||||| Buyer:");
            sb.AppendLine("|||||||||||| Name: " + order.buyerDetails.name);
            sb.AppendLine("|||||||||||| Email: " + order.buyerDetails.email);
            sb.AppendLine("|||||||||||| Phone Number: " + order.buyerDetails.phoneNumber);
            sb.AppendLine("||||||||||||");
            sb.AppendLine("|||||||||||| Musics:");

            foreach (Music m in order.orderList)
            {
                sb.AppendLine($"|||||||||||| {m.id}, Name: {m.band} - {m.name}, Price: {m.price}");
            }

            var finalAmount = order.orderList.Where(x => x.available == false).Select(x => x.price).Sum();
            var first = _paymentService.fee(finalAmount);
            var second = _paymentService.tax(first);
            sb.AppendLine($"|||||||||||| Total Value: {second.ToString("F2", CultureInfo.InvariantCulture)}");
            sb.AppendLine("------------------------------------------------------------------------------");
            return sb.ToString();
        }

        //Metodo que faz o processamento do documento da invoice, de acordo com o InvoiceProcess() => o layout;
        //Faz o processamento do numero de documento de acordo com o numero de arquivos no diretório com + 1;
        public void invoiceDocument()
        {
            string dir = baseDir + @"\Invoice\";
            string source = baseDir + @"\Invoice\Invoice" + order.orderId + ".txt";

            if (!dir.Any())
            {
                using (StreamWriter sw = File.CreateText(source))
                {

                    sw.WriteLine(this.invoiceProcess());
                }
            }
            else
            {
                source = baseDir + @"\Invoice\Invoice" + order.orderId + ".txt";
                using (StreamWriter sw = File.CreateText(source))
                {
                    sw.WriteLine(this.invoiceProcess());
                }
            }

        }

        //Metodo para abrir Invoice de acordo com um numero especifico 
        public void openInvoice(int Number)
        {
            List<string> list = new List<string>();
            string source = baseDir + @"\Invoice\Invoice" + Number + ".txt";

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
        public void invoicesListShow()
        {

            List<string> list = new List<string>();

            var baseDir = AppContext.BaseDirectory.Substring(0, 49) + @"\Invoice\";
            int index = Directory.GetFiles(baseDir).Length;

            for (int i = 1; i >= 1 && i <= index; i++)
            {
                string source = baseDir + @"\Invoice" + i + ".txt";

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
