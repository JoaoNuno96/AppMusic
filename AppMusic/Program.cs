using System;
using System.Globalization;
using System.Collections.Generic;
using AppMusic.Entities;
using AppMusic.Services;
using AppMusic.Services.PaymentServices;
using AppMusic.Services.Exceptions;
using System.Runtime.CompilerServices;

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
            List<Music> orderItems = new List<Music>();
            var musicService = new MusicService();
            musicService.storeRead();
            Order order;
            InvoiceService invoiceService;
            RepositoryService repositoryService = new RepositoryService();


            while (execute)
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("View Store:(S) ");
                Console.WriteLine("View InVoices: (I)");
                Console.WriteLine("Shut Down: (E)");

                char Character = char.Parse(Console.ReadLine());


                if (Character == 'S' || Character == 's')
                {
                    musicService.storeTableWrite();

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

                            foreach (Music M in musicService.listOfMusics)
                            {
                                if (M.id == id)
                                {
                                    try
                                    {
                                        musicService.verifyMusicProcess(musicService.verifyMusic(M));
                                        M.available = false;
                                        orderItems.Add(M);
                                        repositoryService.rentItemDatabase(2);

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

                                        order = new Order(musicService, Buyer, PaymentMethod);
                                        order.orderIdIncrement();
                                        order.addSongs(orderItems);
                                        invoiceService = new InvoiceService(Pay, order);

                                        Console.WriteLine();
                                        Console.WriteLine("We processed your invoice........ ");
                                        Console.WriteLine();
                                        Console.WriteLine(invoiceService.invoiceProcess());
                                        Console.WriteLine();
                                        invoiceService.invoiceDocument();
                                        orderItems.Clear();

                                    }
                                    catch (MusicNotAvailableException e)
                                    {
                                        Console.WriteLine("Error: " + e.Message);
                                    }
                                }
                            }

                        }
                    }
                    Console.WriteLine();
                }

                if (Character == 'I' || Character == 'i')
                {
                    invoiceService = new InvoiceService();
                    try
                    {
                        invoiceService.invoicesListShow();
                    }
                    catch (FileNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    Console.WriteLine();

                    Console.Write("Open Invoive? Y/N ");
                    char OI = char.Parse(Console.ReadLine());

                    if (OI == 'N' || OI == 'n')
                    {
                        continue;
                    }
                    else
                    {
                        Console.Write("Which one? (Invoice Nr) ");
                        int IN = int.Parse(Console.ReadLine());

                        invoiceService.openInvoice(IN);

                    }


                    if (Character == 'E' || Character == 'e')
                    {
                        execute = false;
                    }

                }

                Console.WriteLine("Application Shutting Down...");

            }
        }
    }

}
