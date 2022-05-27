using System;
using System.Collections.Generic;

namespace CAB301Project
{
    class Program
    {
        static void Main(string[] args)
        {
            MovieCollection movieCollection = new MovieCollection();


            




        }

        public static void Display()
        {
            Console.WriteLine("====================================================");
            Console.WriteLine("Welcome to Community Library Movie DVD Management System");
            Console.WriteLine("====================================================");

            Console.WriteLine("====================Main Menu=======================");

            Console.WriteLine("1) Staff Login");
            Console.WriteLine("2) Member Login");
            Console.WriteLine("0) Exit");

            

        }

        public static void DisplayStaffMember()
        {
            Console.WriteLine("====================Staff Menu=======================");
            
            
            Console.WriteLine("1) Add new DVDs of new movie to the system");
            Console.WriteLine("2) Remove DVDS of a movie from the system");
            Console.WriteLine("3) Register a new member with the system");
            Console.WriteLine("4) Remove a registered member from the system");
            Console.WriteLine("5) Display a member's contact phone number, given the member's name");
            Console.WriteLine("6) Display all members who are currently renting a particuler movie");
            Console.WriteLine("0) Return to the main menu");


        }

        public static void DisplayMember()
        {
            Console.WriteLine("====================Member Menu=======================");


            Console.WriteLine("1) Browse all the movies");
            Console.WriteLine("2) Display all the information about a movie, given the title of the movie");
            Console.WriteLine("3) Borrow a movie DVD");
            Console.WriteLine("4) Return a movie DVD");
            Console.WriteLine("5) List current borrowing movies");
            Console.WriteLine("6) Display the top 3 movies rented by the members");
            Console.WriteLine("0) Return to the main menu");
        }

        public void LoginController()
        {
            string userInputext = Console.ReadLine();
            int userinput = int.Parse(userInputext);
           
            
            
            switch (userinput)
            {
                case 1:

                case 2:

                case 0:

                default:
                    break;
            }
        }

    }
}
