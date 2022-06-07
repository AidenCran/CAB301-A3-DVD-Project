using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301Project
{
    class Program
    {
        MovieCollection communityLibrary = new MovieCollection();
        MemberCollection memberCollection = new MemberCollection(50);

        Menu menu = new Menu();

        static void Main(string[] args)
        {
            Program program = new Program();

            program.communityLibrary.Insert(new Movie("A"));
            program.communityLibrary.Insert(new Movie("B"));
            program.communityLibrary.Insert(new Movie("C"));

            program.Run();
        }

        /// <summary>
        /// Creates Main Menu
        /// </summary>
        void Run()
        {
            Console.WriteLine("======================================================");
            Console.WriteLine("Welcome to Community Library Movie DVD Management System");
            Console.WriteLine("======================================================");

            Console.WriteLine("====================Main Menu=======================");

            menu.Add("Staff Login", DisplayStaffMember);
            menu.Add("Member Login", DisplayMember);
            menu.Add("Exit", () => Environment.Exit(0));

            menu.Add("Display All Movies", DisplayAllDVDs);
            menu.Add("Display Movie Info", DisplayMovieInfo);
            DisplayMainMenu();
        }

        /// <summary>
        /// Displays Menu
        /// </summary>
        void DisplayMainMenu()
        {
            while (true)
                menu.Display();
        }

        public void DisplayStaffMember()
        {
            Menu submenu = new Menu();

            UserInterface.Message("====================Staff Menu=======================");

            submenu.Add("Add new DVDs of new movie to the system", menu.Display);
            submenu.Add("Remove DVDS of a movie from the system", menu.Display);
            submenu.Add("Register a new member with the system", menu.Display);
            submenu.Add("Remove a registered member from the system", menu.Display);
            submenu.Add("Display a member's contact phone number, given the member's name", menu.Display);
            submenu.Add("Display all members who are currently renting a particuler movie", menu.Display);
            submenu.Add("Return to the main menu", menu.Display);

            submenu.Display();
        }

        public void DisplayMember()
        {
            Menu submenu = new Menu();

            UserInterface.Message("====================Member Menu=======================");

            submenu.Add("Browse all the movies", menu.Display);
            submenu.Add("Display all the information about a movie, given the title of the movie", menu.Display);
            submenu.Add("Borrow a movie DVD", menu.Display);
            submenu.Add("Return a movie DVD", menu.Display);
            submenu.Add("List current borrowing movies", menu.Display);
            submenu.Add("Display the top 3 movies rented by the members", menu.Display);
            submenu.Add("Return to the main menu", menu.Display);

            submenu.Display();
        }

        #region To Be Removed

        //public void StaffController()
        //{
        //    DisplayStaffMember();

        //    string userInputText = Console.ReadLine();
        //    int userInput = GetInt(userInputText,0 ,6);


        //    switch (userInput)
        //    {
        //        case 1:
        //            AddMovie(communityLibrary);
        //            DisplayStaffMember();
        //            StaffController();
        //            break;
        //        case 2:
        //            RemoveMovie(communityLibrary);
        //            break;
        //        case 3:

        //        case 4:

        //        case 5:

        //        case 6:

        //        case 0:


        //        default:
        //            break;
        //    }

        //}

        #endregion

        #region StaffMember
        public void AddMovie(MovieCollection movieCollection)
        {
            //WARNING: no misinput checks
            Console.WriteLine("Please Enter Movie Title");
            string userTitle = Console.ReadLine();

            IMovie titleSearch = movieCollection.Search(userTitle);
            
            if (titleSearch != null)
            {
                Console.WriteLine($"DVD {titleSearch} exists!");
                Console.WriteLine($"Total Copies of DVD is {titleSearch.TotalCopies}, to add more input a number, if you want none added input 0");
                //need to add the add to total copies function here
                string userInputTC = Console.ReadLine();
                int userInputTCInt = GetInt(userInputTC);

                titleSearch.TotalCopies += userInputTCInt;

            }
            else
            {

                //Set the genre
                Console.WriteLine("Please set a Genre using the numbers indicated below");
                Console.WriteLine("1) Action");
                Console.WriteLine("2) Comedy");
                Console.WriteLine("3) History");
                Console.WriteLine("4) Drama");
                Console.WriteLine("5) Western");
                string userGenre = Console.ReadLine();
                MovieGenre userGenreTest;
                int userGenreInt = GetInt(userGenre,0 , 4);
                userGenreTest = FindGenre(userGenreInt);

                //Set the classification
                Console.WriteLine("Please set a Classification using the numbers indicated below");
                Console.WriteLine("1) G");
                Console.WriteLine("2) PG");
                Console.WriteLine("3) M");
                Console.WriteLine("4) M15Plus");
                string userClassification = Console.ReadLine();
                MovieClassification movieClass;
                int userClassificationInt = GetInt(userClassification,0 ,3);
                movieClass = FindClassification(userClassificationInt);

                //Set Duration
                Console.WriteLine("Please enter Duration of the DVD");
                string userDuration = Console.ReadLine();
                int userDurationInt = GetInt(userDuration);

                //Set Total Copies
                Console.WriteLine("Please enter Total Copies of DVD");
                string userTotalCopies = Console.ReadLine();
                int userTotalCopiesInt = GetInt(userTotalCopies);

                //Add the movie
                var movie = new Movie(userTitle, userGenreTest, movieClass, userDurationInt, userTotalCopiesInt);
                movieCollection.Insert(movie);


                //Print Result
                string movieResult = movie.ToString();
                Console.WriteLine($"{movieResult} has been added");
                
            }


        }

        public void RemoveMovie(MovieCollection movieCollection)
        {

            Console.WriteLine("Please enter in the title of the DVD you want to remove");

            string userInputString = Console.ReadLine();

            IMovie movie = movieCollection.Search(userInputString);


            //===========WARNING==================
            //May need to alter the total copies instead of available copies
            //===========WARNING==================
            if (movie != null && movie.AvailableCopies > 0)
            {
                movie.AvailableCopies -= 1;
            }
            else if (movie == null)
            {
                Console.WriteLine("Movie Does not exists");
                //StaffController();
            }

            if (movie.AvailableCopies == 0)
            {
                Console.WriteLine($"Movie {movie.Title} has been removed");
                movieCollection.Delete(movie);
            }
        }

        public void RegisterMember(MemberCollection memberCollection)
        {
            Console.WriteLine("Please Enter a Firstname");
            string userFirstName = Console.ReadLine();
            Console.WriteLine("Please Enter a Lastname");
            string userLastName = Console.ReadLine();
            Console.WriteLine("Please enter a correct Phone number");
            string userPhonenumber = Console.ReadLine();
            Console.WriteLine("Please enter a password");
            string userPassword = Console.ReadLine();

            //IMember test = mem;

            if (!IMember.IsValidContactNumber(userPhonenumber))
            {
                Console.WriteLine("ERROR:Given phonenumber is incorrect");
                RegisterMember(memberCollection);

            }

            if (!IMember.IsValidPin(userPassword))
            {
                Console.WriteLine("ERROR:Incorrect format for password");
                RegisterMember(memberCollection);
            }
            
            var member = new Member(userFirstName, userLastName, userPhonenumber, userPassword);
            memberCollection.Add(member);
            Console.WriteLine($"Member {userFirstName} {userLastName} has been added to database");

        }

        public void RemoveMember(MemberCollection memberCollection)
        {
            Console.WriteLine("Please Enter the first name of the member being removed");
            string UserInputFirstName = Console.ReadLine();
            Console.WriteLine("Please Enter the last name of the member being removed");
            string UserInputLastName = Console.ReadLine();

            Member member = new Member(UserInputFirstName, UserInputLastName);



        }
        #endregion

        #region RegisteredMember

        void DisplayAllDVDs()
        {
            Menu submenu = new Menu();
            StringBuilder output = new StringBuilder();

            Console.Clear();

            output.AppendLine("All Catalogued DVDs in Alphabetical Order\n");

            foreach (Movie movie in communityLibrary.ToArray())
            {
                output.AppendLine(movie.ToString());
            }

            Console.WriteLine(output.ToString());

            // Return To Menu
            menu.Display();
        }

        void DisplayMovieInfo()
        {
            Menu submenu = new Menu();
            Console.Clear();

            UserInterface.Message("Please Insert a Movie Title");

            string title = Console.ReadLine();
            IMovie movie = communityLibrary.Search(title);

            if (movie == null)
            {
                UserInterface.Error("Movie not found!");

                submenu.Add("Retry", DisplayMovieInfo);
                submenu.Add("Return", menu.Display);
                submenu.Display();
            }
            else
            {
                Console.WriteLine(movie.ToString());
                menu.Display();
            }
        }

        #endregion

        #region Utility
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


        private static int GetInt(string prompt, int min, int max)
        {
            if (min > max)
            {
                int t = min;
                min = max;
                max = t;
            }

            while (true)
            {
                int result = GetInt(prompt);

                if (min <= result && result <= max)
                {
                    return result;
                }
                else
                {
                    Error("Supplied value is out of range");
                }
            }
        }

        private static int GetInt(string prompt)
        {
            while (true)
            {
                string response = GetInput(prompt);

                int result;

                if (int.TryParse(response, out result))
                {
                    return result;
                }
                else
                {
                    Error("Supplied value is not an integer");
                }
            }
        }

        private static string GetInput(string prompt)
        {
            Console.WriteLine("{0}:", prompt);
            return Console.ReadLine();
        }

        private static void Error(string msg)
        {
            Console.WriteLine($"{msg}, please try again");
            Console.WriteLine();
        }
        #endregion
    }
}
