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

                char firstCharacterChoice = char.Parse(Console.ReadLine());

                if (firstCharacterChoice == 'S' || firstCharacterChoice == 's')
                {
                    musicService.storeTableWrite();

                    Console.Write("Would you like to make an Order? (Y/N)");
                    char questionOrder = char.Parse(Console.ReadLine());
                    if (questionOrder == 'N' || questionOrder == 'n')
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

                            foreach (Music music in musicService.listOfMusics)
                            {
                                if (music.id == id)
                                {
                                    try
                                    {
                                        musicService.verifyMusicProcess(musicService.verifyMusic(music));
                                        music.available = false;
                                        orderItems.Add(music);
                                        repositoryService.rentItemDatabase(2);

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
                                        order.orderIdIncrement();
                                        order.addSongs(orderItems);
                                        invoiceService = new InvoiceService(pay, order);

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

                if (firstCharacterChoice == 'I' || firstCharacterChoice == 'i')
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
                    char openInvoiceChoice = char.Parse(Console.ReadLine());

                    if (openInvoiceChoice == 'N' || openInvoiceChoice == 'n')
                    {
                        continue;
                    }
                    else
                    {
                        Console.Write("Which one? (Invoice Nr) ");
                        int invoiceNumber = int.Parse(Console.ReadLine());

                        invoiceService.openInvoice(invoiceNumber);

                    }


                    if (firstCharacterChoice == 'E' || firstCharacterChoice == 'e')
                    {
                        execute = false;
                    }

                }

                Console.WriteLine("Application Shutting Down...");

            }
        }
    }

}
