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
            
            // Default User - Testing
            //Member defaultUser = new Member("Aiden", "Cran", "0432873948", "12345");
            //program._memberCollection.Add(defaultUser);

            //Movie A = new Movie("A", MovieGenre.Action, MovieClassification.G, 10, 10);
            //Movie B = new Movie("B", MovieGenre.Action, MovieClassification.G, 10, 10);
            //Movie C = new Movie("C", MovieGenre.Action, MovieClassification.G, 10, 10);

            //Member MA = new Member("A", "A");
            //Member MB = new Member("B", "B");

            //A.AddBorrower(MA);
            //A.AddBorrower(MB);

            //B.AddBorrower(MA);
            //C.AddBorrower(MA);

            //program._communityLibrary.Insert(A);
            //program._communityLibrary.Insert(B);
            //program._communityLibrary.Insert(C);

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

            _menu.Add("Staff Login", StaffLogin);
            _menu.Add("Member Login", MemberLogin);
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

        void StaffLogin()
        {
            // In Task Specification, Staff Login is distinctly different to Member Login.
            // To accommodate this method is hardcoded.

            string username = UserInterface.GetInput("Please Enter Your Username");
            string password = UserInterface.GetPassword("Please Enter The Password");

            if (username == "staff" && password == "today123")
            {
                Console.Clear();
                UserInterface.SuccessfulAction("Welcome Staff!");
                DisplayStaffMember();
            }
            else
            {
                DisplayInvalidInput(StaffLogin, "Login Details Incorrect");
            }
        }

        public void MemberLogin()
        {
            Console.Clear();

            string firstName = UserInterface.GetInput("Please Enter Your First Name");
            string lastName = UserInterface.GetInput("Please Enter Your Last Name");

            string pin = UserInterface.GetPassword("Please Enter Your Pin");

            var member = _memberCollection.SearchMember(new Member(firstName, lastName));
            if (member != null)
            {
                if (member.Pin == pin)
                {
                    Console.Clear();
                    _currentUser = member;
                    UserInterface.SuccessfulAction($"Login Succesful, Welcome {firstName} {lastName}!");
                    DisplayMember();
                }
            }
            else
            {
                DisplayInvalidInput(MemberLogin, "Login Details Incorrect");
            }
        }

        public void DisplayMember()
        {
            Menu submenu = new Menu();
            UserInterface.Message("====================Member Menu=======================");

            submenu.Add("Browse all the movies", DisplayAllDVDs);
            submenu.Add("Display all the information about a movie, given the title of the movie", DisplayMovieInfo);
            submenu.Add("Borrow a movie DVD", MemberBorrowMovie);
            submenu.Add("Return a movie DVD", MemberReturnMovie);
            submenu.Add("List current borrowing movies", MemberDisplayBorrowedMovies);
            submenu.Add("Display the top 3 movies rented by the members", DisplayTopBorrows);
            submenu.Add("Return to the main menu", _menu.Display);

            submenu.Display();
        }

        #region StaffMember
        public void AddMovie()
        {
            Console.Clear();

            Menu submenu = new Menu();
            string title = UserInterface.GetInput("Please Enter Movie Title");
            IMovie titleSearch = _communityLibrary.Search(title);

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
                Menu genreMenu = new Menu();
                genreMenu.Add("Action", ()=> selectedGenre = MovieGenre.Action);
                genreMenu.Add("Comedy", ()=> selectedGenre = MovieGenre.Comedy);
                genreMenu.Add("History", ()=> selectedGenre = MovieGenre.History);
                genreMenu.Add("Drama", ()=> selectedGenre = MovieGenre.Drama);
                genreMenu.Add("Western", ()=> selectedGenre = MovieGenre.Western);
                genreMenu.Display();

                UserInterface.SuccessfulAction("Selected: " + selectedGenre.ToString());

                MovieClassification selectedClass = new MovieClassification();
                Menu classMenu = new Menu();
                classMenu.Add("G", ()=> selectedClass = MovieClassification.G);
                classMenu.Add("PG", ()=> selectedClass = MovieClassification.PG);
                classMenu.Add("M", ()=> selectedClass = MovieClassification.M);
                classMenu.Add("M15 Plus", ()=> selectedClass = MovieClassification.M15Plus);
                classMenu.Display();

                UserInterface.SuccessfulAction("Selected: " + selectedClass.ToString());

                int userDurationInt = UserInterface.GetInteger("Please enter Duration of the DVD");
                int userTotalCopiesInt = UserInterface.GetInteger("Please enter Total Copies of DVD");

                //Add the movie
                var movie = new Movie(title, selectedGenre, selectedClass, userDurationInt, userTotalCopiesInt);
                _communityLibrary.Insert(movie);


                //Print Result
                string movieResult = movie.ToString();
                UserInterface.SuccessfulAction($"\n{movieResult} has been added");   
            }

            DisplayStaffMember();
        }

        public void RemoveMovie()
        {
            Console.Clear();
            var result = UserInterface.GetInput("Please enter in the title of the DVD you want to remove");

            IMovie movie = _communityLibrary.Search(result);

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
                return;
            }
            
            if (movie.AvailableCopies == 0)
            {
                UserInterface.SuccessfulAction($"Movie {movie.Title} has been removed");
                _communityLibrary.Delete(movie);
            }

            DisplayStaffMember();
        }

        public void RegisterMember()
        {
            Console.Clear();

            string userFirstName = UserInterface.GetInput("Please Enter a Firstname");
            string userLastName = UserInterface.GetInput("Please Enter a Lastname");
            string userPhonenumber = UserInterface.GetInput("Please enter a correct Phone number");
            string userPassword = UserInterface.GetPassword("Please enter a password");

            if (!IMember.IsValidContactNumber(userPhonenumber) || !IMember.IsValidPin(userPassword))
            {
                DisplayInvalidInput(RegisterMember, "Phone Number OR Pin Invalid");
            }
            
            var member = new Member(userFirstName, userLastName, userPhonenumber, userPassword);
            
            _memberCollection.Add(member);
            UserInterface.SuccessfulAction($"Member {userFirstName} {userLastName} has been added to database");

            DisplayStaffMember();
        }

        public void RemoveMember()
        {
            string UserInputFirstName = UserInterface.GetInput("Please Enter Member's First Name");

            string UserInputLastName = UserInterface.GetInput("Please Enter Member's Last Name");

            IMember member = new Member(UserInputFirstName, UserInputLastName);

            Console.Clear();
            _memberCollection.Delete(member);

            DisplayStaffMember();
        }

        public void DisplayMembersPhoneNumber()
        {
            string userFirstName = UserInterface.GetInput("Please Enter The Member's First Name");
            string userLastName = UserInterface.GetInput("Please Enter The Member's Last Name");
            
            //make a object to search for
            IMember member = new Member(userFirstName, userLastName);
            
            //this new object equals found object
            Member userMember = _memberCollection.SearchMember(member);

            Console.Clear();

            if (userMember == null)
            {
                DisplayInvalidInput(DisplayMembersPhoneNumber, "Member Not Found!");
            }
            else
            {
                //Display PhoneNumber
                UserInterface.SuccessfulAction($"The members Contact number is {userMember.ContactNumber}");
            }

            DisplayStaffMember();
        }

        public void DisplayMembersRentingMovie()
        {
            string movieTitle = UserInterface.GetInput("Please enter the title of the movie you wish to view");
            IMovie movie = _communityLibrary.Search(movieTitle);

            if (movie == null)
            {
                DisplayInvalidInput(DisplayMembersRentingMovie, "That movie doesnt exist");
                return;
            }
            else
            {
                Console.Clear();
                var output = movie.Borrowers.ToString();
                if (string.IsNullOrEmpty(output))
                {
                    UserInterface.Error("No one is renting this movie");
                }
                else
                {
                    UserInterface.Message(output);
                }
            }

            DisplayStaffMember();
        }


        #endregion

        #region RegisteredMember

        /// <summary>
        /// Displays all catalogued movies in alphabetical order
        /// </summary>
        void DisplayAllDVDs()
        {
            Console.Clear();
            StringBuilder output = new StringBuilder();

            output.AppendLine("All Catalogued DVDs in Alphabetical Order\n");

            foreach (Movie movie in _communityLibrary.ToArray())
            {
                output.AppendLine(movie.ToString());
            }

            UserInterface.Message(output.ToString());
            DisplayMember();
        }

        /// <summary>
        /// Given a valid input, displays information about a given movie
        /// </summary>
        void DisplayMovieInfo()
        {
            Menu submenu = new Menu();
            Console.Clear();

            string title = UserInterface.GetInput("Please Insert a Movie Title");

            IMovie movie = _communityLibrary.Search(title);

            if (movie == null)  { DisplayInvalidInput(DisplayMovieInfo); }
            else                { UserInterface.Message("\n" + movie.ToString()); }

            DisplayMember(); 
        }

        /// <summary>
        /// Allows a user to borrow a valid movie
        /// </summary>
        void MemberBorrowMovie()
        {
            Menu submenu = new Menu();
            Console.Clear();

            StringBuilder output = new StringBuilder();

            foreach (var item in _communityLibrary.ToArray())
            {
                if (!item.Borrowers.Search(_currentUser))
                {
                    output.AppendLine(item.ToString());
                }
            }

            if (string.IsNullOrEmpty(output.ToString()))
            {
                UserInterface.Error("There are no unique movies avaliable to borrow!");
                DisplayMember();
                return;
            }

            UserInterface.Message("Please Enter a Title to Borrow");

            UserInterface.Message(output.ToString());

            string title = Console.ReadLine();
            IMovie movie = _communityLibrary.Search(title);

            if (movie == null) { DisplayInvalidInput(MemberBorrowMovie); }
            else if (!_currentUser.Moives.Contains(movie))
            {
                Console.Clear();
                UserInterface.SuccessfulAction("Movie Borrowed!");
                movie.AddBorrower(_currentUser);
            }
            else
            {
                Console.Clear();
                UserInterface.Error("You're already borrowing that movie");
            }
            DisplayMember();
        }

        /// <summary>
        /// Allows a user to return a valid movie
        /// </summary>
        void MemberReturnMovie()
        {
            Menu submenu = new Menu();
            Console.Clear();

            StringBuilder output = new StringBuilder();

            foreach (var item in _currentUser.Moives)
            {
                if (item.Borrowers.Search(_currentUser))
                {
                    output.AppendLine(item.ToString());
                }
            }

            if (string.IsNullOrEmpty(output.ToString()))
            {
                UserInterface.Error("You're not borrowing any movies!");
                DisplayMember();
                return;
            }

            UserInterface.Message("Please Enter a Title to Return");

            UserInterface.Message(output.ToString());

            string title = Console.ReadLine();
            IMovie movie = _communityLibrary.Search(title);

            if (movie == null)  { DisplayInvalidInput(MemberReturnMovie); }
            else if (_currentUser.Moives.Contains(movie))
            {
                Console.Clear();
                UserInterface.SuccessfulAction("Movie Returned!");
                movie.RemoveBorrower(_currentUser);
            }

            DisplayMember();
        }

        /// <summary>
        /// Displays all movies the selected user currently borrows
        /// </summary>
        void MemberDisplayBorrowedMovies()
        {
            StringBuilder output = new StringBuilder();

            foreach (var movie in _currentUser.Moives)
            {
                if (movie.Borrowers.Search(_currentUser))
                {
                    output.AppendLine(movie.ToString());
                }
            }

            Console.Clear();

            if (string.IsNullOrEmpty(output.ToString()))
            {
                UserInterface.Error("You're Currently not borrowing any movies!");
            }
            else
            {
                Console.WriteLine("Currently Borrowing:");
                Console.WriteLine(output.ToString());
            }

            DisplayMember();
        }

        
        // Print a string containing the top 3 most borrowed movies in descending order.
        // Input: any array of movies. Does not have to be sorted.
        public void DisplayTopBorrows()
        {
            var array = _communityLibrary.ToArray();
            IMovie first = new Movie("nil");
            first.NoBorrowings = 0;
            IMovie second = new Movie("nil");
            second.NoBorrowings = 0;
            IMovie third = new Movie("nil");
            third.NoBorrowings = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].NoBorrowings > first.NoBorrowings)
                {
                    third = second;
                    second = first;
                    first = array[i];
                }
                else if (array[i].NoBorrowings > second.NoBorrowings)
                {
                    third = second;
                    second = array[i];
                }
                else if (array[i].NoBorrowings > third.NoBorrowings)
                {
                    third = array[i];
                }
            }

            Console.Clear();

            Console.WriteLine(first.Title + ": borrowed " + first.NoBorrowings + " times");
            Console.WriteLine(second.Title + ": borrowed " + second.NoBorrowings + " times");
            Console.WriteLine(third.Title + ": borrowed " + third.NoBorrowings + " times");
            Console.WriteLine();

            DisplayMember();
        }

        #endregion

        #region Utility

        void DisplayInvalidInput(Action action, string prompt = "Object Not Found!")
        {
            Menu submenu = new Menu();
            UserInterface.Error("\n" + prompt);

            submenu.Add("Retry", action);
            submenu.Add("Return", _menu.Display);
            submenu.Display();
        }

        #endregion
    }
}
