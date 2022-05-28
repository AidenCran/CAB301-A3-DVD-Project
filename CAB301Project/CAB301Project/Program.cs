﻿using System;
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

        public void StaffController()
        {
            string userInputText = Console.ReadLine();
            int userInput = int.Parse(userInputText);


            switch (userInput)
            {
                case 1:

                case 2:

                case 3:

                case 4:

                case 5:

                case 6:

                case 0:


                default:
                    break;
            }

        }

        public void AddMovie(MovieCollection movieCollection)
        {
            //WARNING: no misinput checks
            Console.WriteLine("Please Enter Movie Title");
            string userTitle = Console.ReadLine();

            IMovie titleSearch = movieCollection.Search(userTitle);
            if (titleSearch != null)
            {
                Console.WriteLine($"DVD {titleSearch} exists!");
                Console.WriteLine($"Total Copies of DVD is {titleSearch.TotalCopies}, would you like to add more?");
                //need to add the add to total copies function here

            }

            Console.WriteLine("Please set a Genre using the numbers indicated below");
            Console.WriteLine("Action = 1");
            Console.WriteLine("Comedy = 2");
            Console.WriteLine("History = 3");
            Console.WriteLine("Drama = 4");
            Console.WriteLine("Western = 5");
            string userGenre = Console.ReadLine();
            MovieGenre userGenreTest;
            int userGenreInt = int.Parse(userGenre);
            userGenreTest = FindGenre(userGenreInt);
            

            Console.WriteLine("Please set a Classification using the numbers indicated below");
            Console.WriteLine("G = 1");
            Console.WriteLine("PG = 2");
            Console.WriteLine("M = 3");
            Console.WriteLine("M15Plus = 4");
            string userClassification = Console.ReadLine();
            MovieClassification movieClass;
            int userClassificationInt = int.Parse(userClassification);
            movieClass = FindClassification(userClassificationInt);


            Console.WriteLine("Please enter Duration of the DVD");
            string userDuration = Console.ReadLine();
            int userDurationInt = int.Parse(userDuration);

            Console.WriteLine("Please enter Total Copies of DVD");
            string userTotalCopies = Console.ReadLine();
            int userTotalCopiesInt = int.Parse(userTotalCopies);

            var movie = new Movie(userTitle, userGenreTest, movieClass, userDurationInt, userTotalCopiesInt);
            movieCollection.Insert(movie);

        }
        /// <summary>
        /// Input user Generated number to indicate what the classification is
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public MovieGenre FindGenre(int number)
        {
            MovieGenre userGenre;
            switch (number)
            {
                case 1:
                    userGenre = MovieGenre.Action;
                    return userGenre;
                case 2:
                    userGenre = MovieGenre.Comedy;
                    return userGenre;
                case 3:
                    userGenre = MovieGenre.History;
                    return userGenre;
                case 4:
                    userGenre = MovieGenre.Drama;
                    return userGenre;
                case 5:
                    userGenre = MovieGenre.Western;
                    return userGenre;

                default:
                    break;
            }

            return 0;

        }

        /// <summary>
        /// Input user Generated number to indicate what the classification is
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public MovieClassification FindClassification(int number)
        {
            MovieClassification userClassification;
            switch (number)
            {
                case 1:
                    userClassification = MovieClassification.G;
                    return userClassification;
                case 2:
                    userClassification = MovieClassification.PG;
                    return userClassification;
                case 3:
                    userClassification = MovieClassification.M;
                    return userClassification;
                case 4:
                    userClassification = MovieClassification.M15Plus;
                    return userClassification;


                default:
                    break;
            }

            return 0;

        }

    }
}
