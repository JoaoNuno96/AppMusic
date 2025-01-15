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
        static PathDirectoryService Pds = new PathDirectoryService();
        static bool Execute = true;
        static List<Music> OrderItems = new List<Music>();
        static MusicService MusicService = new MusicService(Pds);
        static Order Order;
        static OrderService OrderService;
        static InvoiceService InvoiceService;
        static RepositoryService RepositoryService = new RepositoryService(Pds);
        static void Main(string[] args)
        {
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("                       MUSIC APPLICATION PURCHASE");
            Console.WriteLine("------------------------------------------------------------------------------");

            Console.WriteLine("This a application which lets you rent songs");

            while (Execute)
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("View Store:(S) ");
                Console.WriteLine("View InVoices: (I)");
                Console.WriteLine("Shut Down: (E)");
                Console.WriteLine(AppContext.BaseDirectory.ToString());
                char firstCharacterChoice = char.Parse(Console.ReadLine());

                if (StringComparer.OrdinalIgnoreCase.Equals(firstCharacterChoice, "s"))
                {
                    MusicService.StoreTableWrite();

                    Console.Write("Would you like to make an Order? (Y/N)");
                    string questionOrder = Console.ReadLine();

                    if (string.Equals(questionOrder, "n", StringComparison.OrdinalIgnoreCase))
                    {
                        Execute = false;
                    }
                    else
                    {
                        Console.Write("How many songs would you like to rent? ");
                        int numberOfSongs = int.Parse(Console.ReadLine());
                        Console.Write("Please select the music's id to rent: ");

                        for (int i = 1; i >= 1 && i <= numberOfSongs; i++)
                        {
                            int id = int.Parse(Console.ReadLine());

                            foreach (Music music in MusicService.ListOfMusics)
                            {
                                if (StringComparer.OrdinalIgnoreCase.Equals(music.Id,id))
                                {
                                    try
                                    {
                                        MusicService.VerifyMusicProcess(MusicService.VerifyMusic(music));
                                        music.Available = false;
                                        OrderItems.Add(music);
                                        RepositoryService.RentItemDatabase(music.Id);

                                        Console.WriteLine("In order to make the Invoice, please give us some data: ");
                                        Console.Write("Name: ");
                                        string userName = Console.ReadLine();
                                        Console.Write("Email: ");
                                        string userEmail = Console.ReadLine();
                                        Console.Write("Phone Number: ");
                                        string userPhoneNumber = Console.ReadLine();

                                        var userBuyer = new Buyer(userName, userEmail, userPhoneNumber);

                                        Console.Write("Which method of payment would you like to use? Mbway(M) or Paypal(P): ");
                                        char paymentChoice = char.Parse(Console.ReadLine());

                                        string paymentMethod = (paymentChoice == 'M') ? "Mbway" : "Paypal";
                                        IPayment pay = (paymentChoice == 'M') ? new MbwayService() : new PaypalService();


                                        Order = new Order(userBuyer, paymentMethod);
                                        OrderService = new OrderService(Pds, MusicService, RepositoryService, Order);

                                        OrderService.OrderIdIncrement();
                                        OrderService.AddSongs(OrderItems);
                                        InvoiceService = new InvoiceService(pay, Order, Pds);

                                        Console.WriteLine();
                                        Console.WriteLine("We processed your invoice........ ");
                                        Console.WriteLine();
                                        Console.WriteLine(InvoiceService.InvoiceProcess());
                                        Console.WriteLine();
                                        InvoiceService.InvoiceDocument();
                                        OrderItems.Clear();
                                    }
                                    catch (MusicNotAvailableException e)
                                    {
                                        Console.WriteLine("Erro: " + e.Message);
                                    }
                                }
                            }

                        }
                    }
                    Console.WriteLine();
                }

                if (StringComparer.OrdinalIgnoreCase.Equals(firstCharacterChoice, "i"))
                {
                    InvoiceService = new InvoiceService();
                    try
                    {
                        InvoiceService.InvoicesListShow();
                    }
                    catch (FileNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    Console.WriteLine();

                    Console.Write("Open Invoive? Y/N ");
                    char openInvoiceChoice = char.Parse(Console.ReadLine());

                    if (StringComparer.OrdinalIgnoreCase.Equals(openInvoiceChoice, "n"))
                    {
                        continue;
                    }
                    else
                    {
                        Console.Write("Which one? (Invoice Nr) ");
                        int invoiceNumber = int.Parse(Console.ReadLine());

                        InvoiceService.OpenInvoice(invoiceNumber);

                    }
                    
                    if (StringComparer.OrdinalIgnoreCase.Equals(firstCharacterChoice, "e"))
                    {
                        Execute = false;
                        //Environment.Exit(0);
                        break;

                    }

                }

                Console.WriteLine("Application Shutting Down...");

            }
        }
    }

}
