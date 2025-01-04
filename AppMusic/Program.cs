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

            PathDirectoryService pds = new PathDirectoryService();

            bool execute = true;
            List<Music> orderItems = new List<Music>();
            var musicService = new MusicService(pds);
            musicService.StoreRead();
            Order order;
            InvoiceService invoiceService;
            RepositoryService repositoryService = new RepositoryService(pds);


            while (execute)
            {
                Console.WriteLine(repositoryService.Source);
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("View Store:(S) ");
                Console.WriteLine("View InVoices: (I)");
                Console.WriteLine("Shut Down: (E)");

                char firstCharacterChoice = char.Parse(Console.ReadLine());

                if (firstCharacterChoice == 'S' || firstCharacterChoice == 's')
                {
                    musicService.StoreTableWrite();

                    Console.Write("Would you like to make an Order? (Y/N)");
                    string questionOrder = Console.ReadLine();

                    if (string.Equals(questionOrder, "n", StringComparison.OrdinalIgnoreCase))
                    {
                        execute = false;
                    }
                    else
                    {
                        Console.Write("How many songs would you like to rent? ");
                        int numberOfSongs = int.Parse(Console.ReadLine());
                        Console.Write("Please select the music's id to rent: ");

                        for (int i = 1; i >= 1 && i <= numberOfSongs; i++)
                        {
                            int id = int.Parse(Console.ReadLine());

                            foreach (Music music in musicService.ListOfMusics)
                            {
                                if (music.Id == id)
                                {
                                    try
                                    {
                                        musicService.VerifyMusicProcess(musicService.VerifyMusic(music));
                                        music.Available = false;
                                        orderItems.Add(music);
                                        repositoryService.RentItemDatabase(music.Id);

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

                                        order = new Order(musicService, userBuyer, paymentMethod);
                                        order.OrderIdIncrement();
                                        order.AddSongs(orderItems);
                                        invoiceService = new InvoiceService(pay, order,pds);

                                        Console.WriteLine();
                                        Console.WriteLine("We processed your invoice........ ");
                                        Console.WriteLine();
                                        Console.WriteLine(invoiceService.InvoiceProcess());
                                        Console.WriteLine();
                                        invoiceService.InvoiceDocument();
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

                if (firstCharacterChoice == 'I' || firstCharacterChoice == 'i')
                {
                    invoiceService = new InvoiceService();
                    try
                    {
                        invoiceService.InvoicesListShow();
                    }
                    catch (FileNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    Console.WriteLine();

                    Console.Write("Open Invoive? Y/N ");
                    char openInvoiceChoice = char.Parse(Console.ReadLine());

                    if (openInvoiceChoice == 'N' || openInvoiceChoice == 'n')
                    {
                        continue;
                    }
                    else
                    {
                        Console.Write("Which one? (Invoice Nr) ");
                        int invoiceNumber = int.Parse(Console.ReadLine());

                        invoiceService.OpenInvoice(invoiceNumber);

                    }


                    if (firstCharacterChoice == 'E' || firstCharacterChoice == 'e')
                    {
                        execute = false;
                        //Environment.Exit(0);
                        break;
                        
                    }

                }

                Console.WriteLine("Application Shutting Down...");

            }
        }
    }

}
