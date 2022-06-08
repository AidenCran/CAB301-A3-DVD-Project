using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301Project
{
    class Program
    {
        MovieCollection _communityLibrary = new MovieCollection();
        MemberCollection _memberCollection = new MemberCollection(50);

        Menu _menu = new Menu();

        Member _currentUser;

        static void Main(string[] args)
        {
            Program program = new Program();

            program._communityLibrary.Insert(new Movie("A"));
            program._communityLibrary.Insert(new Movie("B"));
            program._communityLibrary.Insert(new Movie("C"));

            program.Run();
        }

        /// <summary>
        /// Creates Main Menu
        /// </summary>
        void Run()
        {
            Console.WriteLine("========================================================");
            Console.WriteLine("Welcome to Community Library Movie DVD Management System");
            Console.WriteLine("========================================================\n");

            Console.WriteLine("======================= Main Menu ======================\n");

            _menu.Add("Staff Login", DisplayStaffMember);
            _menu.Add("Member Login", DisplayMember);
            _menu.Add("Exit", () => Environment.Exit(0));

            DisplayMainMenu();
        }

        /// <summary>
        /// Displays Menu
        /// </summary>
        void DisplayMainMenu()
        {
            while (true)
                _menu.Display();
        }

        public void DisplayStaffMember()
        {
            Menu submenu = new Menu();
            Console.Clear();
            UserInterface.Message("====================Staff Menu=======================");

            submenu.Add("Add new DVDs of new movie to the system", AddMovie);
            submenu.Add("Remove DVDS of a movie from the system", RemoveMovie);
            submenu.Add("Register a new member with the system", RegisterMember);
            submenu.Add("Remove a registered member from the system", RemoveMember);
            submenu.Add("Display a member's contact phone number, given the member's name", DisplayMembersPhoneNumber);
            submenu.Add("Display all members who are currently renting a particuler movie", DisplayMembersRentingMovie);
            submenu.Add("Return to the main menu", _menu.Display);

            submenu.Display();
        }

        public void DisplayMember()
        {
            Menu submenu = new Menu();
            Console.Clear();
            UserInterface.Message("====================Member Menu=======================");

            submenu.Add("Browse all the movies", DisplayAllDVDs);
            submenu.Add("Display all the information about a movie, given the title of the movie", DisplayMovieInfo);
            submenu.Add("Borrow a movie DVD", MemberBorrowMovie);
            submenu.Add("Return a movie DVD", MemberReturnMovie);
            submenu.Add("List current borrowing movies", MemberDisplayBorrowedMovies);
            // \/ \/ \/ \/ \/
            submenu.Add("Display the top 3 movies rented by the members", _menu.Display);
            submenu.Add("Return to the main menu", _menu.Display);

            submenu.Display();
        }

        #region StaffMember
        public void AddMovie()
        {
            Console.Clear();

            Menu submenu = new Menu();
            UserInterface.Message("Please Enter Movie Title");

            IMovie titleSearch = _communityLibrary.Search(Console.ReadLine());
            
            if (titleSearch != null)
            {
                UserInterface.Message($"DVD {titleSearch} exists!");
                UserInterface.Message($"Total Copies of DVD is {titleSearch.TotalCopies}.\n");
                
                //need to add the add to total copies function here
                titleSearch.TotalCopies += UserInterface.GetInteger("Please enter amount to add!");
            }
            else
            {
                MovieGenre selectedGenre = new MovieGenre();
                UserInterface.Message("Please Select a Genre");
                Menu genreMenu = new Menu();
                genreMenu.Add("Action", ()=> selectedGenre = MovieGenre.Action);
                genreMenu.Add("Comedy", ()=> selectedGenre = MovieGenre.Comedy);
                genreMenu.Add("History", ()=> selectedGenre = MovieGenre.History);
                genreMenu.Add("Drama", ()=> selectedGenre = MovieGenre.Drama);
                genreMenu.Add("Western", ()=> selectedGenre = MovieGenre.Western);
                genreMenu.Display();


                MovieClassification selectedClass = new MovieClassification();
                UserInterface.Message("Please Select a Classification");
                Menu classMenu = new Menu();
                classMenu.Add("G", ()=> selectedClass = MovieClassification.G);
                classMenu.Add("PG", ()=> selectedClass = MovieClassification.PG);
                classMenu.Add("M", ()=> selectedClass = MovieClassification.M);
                classMenu.Add("M15 Plus", ()=> selectedClass = MovieClassification.M15Plus);
                classMenu.Display();


                int userDurationInt = UserInterface.GetInteger("Please enter Duration of the DVD");
                int userTotalCopiesInt = UserInterface.GetInteger("Please enter Total Copies of DVD");

                //Add the movie
                var movie = new Movie(titleSearch.Title, selectedGenre, selectedClass, userDurationInt, userTotalCopiesInt);
                _communityLibrary.Insert(movie);


                //Print Result
                string movieResult = movie.ToString();
                UserInterface.SuccessfulAction($"\n{movieResult} has been added");   
            }
        }

        public void RemoveMovie()
        {
            Console.Clear();
            UserInterface.Message("Please enter in the title of the DVD you want to remove");

            IMovie movie = _communityLibrary.Search(Console.ReadLine());

            //===========WARNING==================
            //May need to alter the total copies instead of available copies
            //===========WARNING==================
            if (movie != null && movie.AvailableCopies > 0)
            {
                movie.AvailableCopies -= 1;

                // CHECK
                UserInterface.SuccessfulAction("Movie Deincremented");
            }
            else if (movie == null)
            {
                UserInterface.Error("Movie Does not exists");
            }
            if (movie.AvailableCopies == 0)
            {
                UserInterface.SuccessfulAction($"Movie {movie.Title} has been removed");
                _communityLibrary.Delete(movie);
            }
        }

        public void RegisterMember()
        {
            Console.Clear();

            Console.WriteLine("Please Enter a Firstname");
            string userFirstName = Console.ReadLine();
            Console.WriteLine("Please Enter a Lastname");
            string userLastName = Console.ReadLine();
            Console.WriteLine("Please enter a correct Phone number");
            string userPhonenumber = Console.ReadLine();
            Console.WriteLine("Please enter a password");
            string userPassword = Console.ReadLine();

            if (!IMember.IsValidContactNumber(userPhonenumber) || !IMember.IsValidPin(userPassword))
            {
                DisplayInvalidInput(RegisterMember);
            }
            
            var member = new Member(userFirstName, userLastName, userPhonenumber, userPassword);
            _memberCollection.Add(member);
            UserInterface.SuccessfulAction($"Member {userFirstName} {userLastName} has been added to database");
        }

        public void RemoveMember()
        {
            Console.WriteLine("Please Enter the first name of the member being removed");
            string UserInputFirstName = Console.ReadLine();
            Console.WriteLine("Please Enter the last name of the member being removed");
            string UserInputLastName = Console.ReadLine();

            IMember member = new Member(UserInputFirstName, UserInputLastName);
            Member UserMember;
            //memberCollection.Search(member);
            if (_memberCollection.Search(member))
            {

               //memberCollection.
               UserMember = _memberCollection.SearchMember(member);
               

            }
        }

        public void DisplayMembersPhoneNumber()
        {
            //Firstname Prompt
            string FirstNamePrompt = "Please enter the members firstname";
            string userFirstName = UserInterface.GetInput(FirstNamePrompt);
            
            //Lastname Prompt
            string LastNamePrompt = "Please enter the members Lastname";
            string userLastName = UserInterface.GetInput(LastNamePrompt);
            
            //make a object to search for
            IMember member = new Member(userFirstName, userLastName);
            
            //this new object equals found object
            Member userMember = _memberCollection.SearchMember(member);
            
            //Display PhoneNumber
            Console.WriteLine($"The members Contact number is {userMember.ContactNumber}");
        }

        public void DisplayMembersRentingMovie()
        {
            string movieTitlePrompt = "Please enter the title of the movie you wish to view";
            string movieTitle = UserInterface.GetInput(movieTitlePrompt);
            IMovie movie = _communityLibrary.Search(movieTitle);

            if (movie == null)
            {
                UserInterface.Error("That movie doesnt exist");
            }
            else
            {
                //this may not work. might only return one name at the top of borrowers
                //will need fixing
                movie.Borrowers.ToString();
            }
        }


        #endregion

        #region RegisteredMember

        /// <summary>
        /// Displays all catalogued movies in alphabetical order
        /// </summary>
        void DisplayAllDVDs()
        {
            Menu submenu = new Menu();
            StringBuilder output = new StringBuilder();

            Console.Clear();

            output.AppendLine("All Catalogued DVDs in Alphabetical Order\n");

            foreach (Movie movie in _communityLibrary.ToArray())
            {
                output.AppendLine(movie.ToString());
            }

            UserInterface.Message(output.ToString());
        }

        /// <summary>
        /// Given a valid input, displays information about a given movie
        /// </summary>
        void DisplayMovieInfo()
        {
            Menu submenu = new Menu();
            Console.Clear();

            UserInterface.Message("Please Insert a Movie Title");

            string title = Console.ReadLine();
            IMovie movie = _communityLibrary.Search(title);

            if (movie == null)  { DisplayInvalidInput(DisplayMovieInfo); }
            else                { UserInterface.Message("\n" + movie.ToString()); }
        }

        /// <summary>
        /// Allows a user to borrow a valid movie
        /// </summary>
        void MemberBorrowMovie()
        {
            Menu submenu = new Menu();
            Console.Clear();

            UserInterface.Message("Please Enter a Title to Borrow");

            DisplayAllDVDs();

            string title = Console.ReadLine();
            IMovie movie = _communityLibrary.Search(title);

            if (movie == null)  { DisplayInvalidInput(MemberBorrowMovie); }
            else                { movie.AddBorrower(_currentUser); }
        }

        /// <summary>
        /// Allows a user to return a valid movie
        /// </summary>
        void MemberReturnMovie()
        {
            Menu submenu = new Menu();
            Console.Clear();

            UserInterface.Message("Please Enter a Title to Return");

            DisplayAllDVDs();

            string title = Console.ReadLine();
            IMovie movie = _communityLibrary.Search(title);

            if (movie == null)  { DisplayInvalidInput(MemberReturnMovie); }
            else                { movie.RemoveBorrower(_currentUser); }
        }

        /// <summary>
        /// Displays all movies the selected user currently borrows
        /// </summary>
        void MemberDisplayBorrowedMovies()
        {
            StringBuilder output = new StringBuilder();

            foreach (var movie in _communityLibrary.ToArray())
            {
                if (movie.Borrowers.Search(_currentUser))
                {
                    output.AppendLine(movie.ToString());
                }
            }

            Console.WriteLine(output.ToString());
        }

        void DisplayTopThree()
        {

        }

        #endregion

        #region Utility

        void DisplayInvalidInput(Action action)
        {
            Menu submenu = new Menu();
            UserInterface.Error("Object Not Found!");

            submenu.Add("Retry", action);
            submenu.Add("Return", _menu.Display);
            submenu.Display();
        }

        #endregion
    }
}
