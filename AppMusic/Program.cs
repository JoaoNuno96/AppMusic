using System;
using System.Globalization;
using System.Collections.Generic;
using AppMusic.Entities;
using AppMusic.Services;
using AppMusic.Services.PaymentServices;
using AppMusic.Services.Exceptions;

namespace AppMusic
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("                       MUSIC APPLICATION PURCHASE");
            Console.WriteLine("------------------------------------------------------------------------------");

            Console.WriteLine("This a application which lets you rent songs");

            bool execute = true;
            List<Music> OrderItems = new List<Music>();
            var MS = new MusicService();
            MS.StoreRead();
            Order Ord;
            InvoiceService InvoiceS;
            RepositoryService RepositoryService = new RepositoryService();


            while (execute)
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("View Store:(S) ");
                Console.WriteLine("View InVoices: (I)");
                Console.WriteLine("Shut Down: (E)");

                char Char = char.Parse(Console.ReadLine());


                if (Char == 'S' || Char == 's')
                {
                    MS.StoreTableWrite();

                    Console.Write("Would you like to make an Order? (Y/N)");
                    char MaO = char.Parse(Console.ReadLine());
                    if (MaO == 'N' || MaO == 'n')
                    {
                        execute = false;
                    }
                    else
                    {
                        Console.Write("How many songs would you like to rent? ");
                        int NrSongs = int.Parse(Console.ReadLine());
                        Console.Write("Please select the music's id to rent: ");

                        for (int i = 1; i >= 1 && i <= NrSongs; i++)
                        {
                            int id = int.Parse(Console.ReadLine());

                            foreach (Music M in MS.ListOfMusics)
                            {
                                if (M.Id == id)
                                {
                                    M.Available = false;
                                    OrderItems.Add(M);
                                    RepositoryService.RentItemDatabase(2);
                                }
                            }
                        }
                        Console.WriteLine();

                        Console.WriteLine("In order to make the Invoice, please give us some data: ");
                        Console.Write("Name: ");
                        string Name = Console.ReadLine();
                        Console.Write("Email: ");
                        string Email = Console.ReadLine();
                        Console.Write("Phone Number: ");
                        string PhoneNumber = Console.ReadLine();

                        var Buyer = new Buyer(Name, Email, PhoneNumber);

                        Console.Write("Which method of payment would you like to use? Mbway(M) or Paypal(P): ");
                        char T = char.Parse(Console.ReadLine());

                        string PaymentMethod = (T == 'M') ? "Mbway" : "Paypal";
                        IPayment Pay = (T == 'M') ? new MbwayService() : new PaypalService();

                        Ord = new Order(MS, Buyer, PaymentMethod);
                        Ord.OrderIdIncrement();
                        Ord.AddSongs(OrderItems);
                        InvoiceS = new InvoiceService(Pay, Ord);

                        Console.WriteLine();
                        Console.WriteLine("We processed your invoice........ ");
                        Console.WriteLine();
                        Console.WriteLine(InvoiceS.InvoiceProcess());
                        Console.WriteLine();
                        InvoiceS.InvoiceDocument();
                        OrderItems.Clear();

                    }
                }
                if (Char == 'I' || Char == 'i')
                {
                    InvoiceS = new InvoiceService();
                    try
                    {
                        InvoiceS.InvoicesListShow();
                    }
                    catch (FileNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                 
                    Console.WriteLine();

                    Console.Write("Open Invoive? Y/N ");
                    char OI = char.Parse(Console.ReadLine());

                    if(OI == 'N' || OI == 'n')
                    {
                        continue;
                    }
                    else
                    {
                        Console.Write("Which one? (Invoice Nr) ");
                        int IN = int.Parse(Console.ReadLine());

                        InvoiceS.OpenInvoice(IN);

                    }

                }
                if(Char == 'E' || Char == 'e')
                {
                    execute = false;
                }

            }

            Console.WriteLine("Application Shutting Down...");

        }
    }
}
