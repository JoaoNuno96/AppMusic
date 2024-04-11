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
            var MS = new MusicService();
            MS.StoreRead();
            Order Ord;
            InvoiceService InvoiceS;
            
            
            while (execute)
            {
                Console.WriteLine("What would you like to do?");
                Console.Write("View Store:(S) ");

                char Char = char.Parse(Console.ReadLine());
                if (Char == 'S' || Char == 's')
                {
                    MS.StoreTableWrite();
                }

                Console.WriteLine();
                Console.Write("Whould you like to make an order?(y/n) ");
                char I = char.Parse(Console.ReadLine());

                if(I == 'N' || I == 'n'){
                    execute = false;
                }
                else
                {
                    Console.Write("How many songs would you like to rent? ");
                    int NrSongs = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.WriteLine("Which of the Songs Id would you like to rent: ");
                   
                    for (int i = 1; i>=1 && i<= NrSongs;i++)
                    {
                        int id = int.Parse(Console.ReadLine());

                        foreach (Music M in MS.ListOfMusics)
                        {
                            if(M.Id == id)
                            {
                                M.Available = false;
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

                    var Buyer = new Buyer(Name,Email,PhoneNumber);

                    Console.Write("Which method of payment would you like to use? Mbway(M) or Paypal(P): ");
                    char T = char.Parse(Console.ReadLine());

                    string PaymentMethod = (T == 'M') ? "Mbway" : "Paypal";
                    IPayment Pay = (T == 'M') ? new MbwayService() : new PaypalService();

                    Ord = new Order(MS, Buyer, PaymentMethod);

                    InvoiceS = new InvoiceService(Pay, Ord);
                    Console.WriteLine();
                    Console.WriteLine("We processed your invoice........ ");
                    Console.WriteLine();
                    Console.WriteLine(InvoiceS.InvoiceProcess());
                    Console.WriteLine();
                    InvoiceS.InvoiceDocument();

                }

            }
        }
    }
}