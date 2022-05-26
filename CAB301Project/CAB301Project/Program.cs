using System;
using System.Collections.Generic;

namespace CAB301Project
{
    class Program
    {
        static void Main(string[] args)
        {
            MovieCollection MovieCollection = new MovieCollection();

            var TestMovie1 = new Movie("Rocky", MovieGenre.Action, MovieClassification.M15Plus, 3, 5);
            var TestMovie2 = new Movie("Titanic", MovieGenre.Drama, MovieClassification.M, 2, 3);
            var TestMovie3 = new Movie("Red Dead", MovieGenre.Western, MovieClassification.M, 4, 5);
            var TestMovie4 = new Movie("War Movie", MovieGenre.History, MovieClassification.M15Plus, 2, 3);
            var TestMovie5 = new Movie("Abbey Road", MovieGenre.Comedy, MovieClassification.G, 6, 2);
            var TestMovie6 = new Movie("Abbey Road", MovieGenre.Comedy, MovieClassification.G, 6, 2);
            var TestMovie7 = new Movie("Private Ryan", MovieGenre.History, MovieClassification.M15Plus, 3, 5);
            var TestMovie8 = new Movie("Pirates", MovieGenre.Action, MovieClassification.G, 3, 8);
            var TestMovie9 = new Movie("Flappy birds", MovieGenre.Comedy, MovieClassification.G, 2, 1);
            var TestMovie10 = new Movie("yellow submarine", MovieGenre.History, MovieClassification.M, 2, 0);

            var Member1 = new Member("Chad", "Badwick");
            var Member2 = new Member("Jerome", "Woodford");
            var Member3 = new Member("barry", "allan");
            var Member4 = new Member("steve", "jobs");
            var Member5 = new Member("john", "hale");
            var Member6 = new Member("mr", "krabs");
            var Member7 = new Member("lucas", "adams");
            var Member8 = new Member("jack", "redwood");
            var Member9 = new Member("donald", "trump");
            var Member10 = new Member("Chad", "Badwick");

            Console.WriteLine("Inserting");
            Console.WriteLine("Inserting M1 " + MovieCollection.Insert(TestMovie1));
            Console.WriteLine("Inserting M2 " + MovieCollection.Insert(TestMovie2));
            Console.WriteLine("Inserting M3 " + MovieCollection.Insert(TestMovie3));
            Console.WriteLine("Inserting M4 " + MovieCollection.Insert(TestMovie4));
            Console.WriteLine("Inserting M5 " + MovieCollection.Insert(TestMovie5));
            Console.WriteLine("Inserting M6 (Dupe) " + MovieCollection.Insert(TestMovie6));
            Console.WriteLine("Inserting M7 " + MovieCollection.Insert(TestMovie7));
            Console.WriteLine("Inserting M8 " + MovieCollection.Insert(TestMovie8));
            Console.WriteLine("Inserting M9 " + MovieCollection.Insert(TestMovie9));
            Console.WriteLine("Inserting M10 " + MovieCollection.Insert(TestMovie10));

            Console.WriteLine();
            Console.WriteLine("Adding Borrowers");
            Console.WriteLine("Adding 1 " + TestMovie1.AddBorrower(Member1));
            Console.WriteLine("Adding 2 " + TestMovie1.AddBorrower(Member2));
            Console.WriteLine("Adding 3 " + TestMovie1.AddBorrower(Member3));
            Console.WriteLine("Adding 4 " + TestMovie1.AddBorrower(Member4));
            Console.WriteLine("Adding 5 " + TestMovie1.AddBorrower(Member5));
            Console.WriteLine("Adding 6 (Borrow # Maxxed Out) " + TestMovie1.AddBorrower(Member6));

            Console.WriteLine();
            Console.WriteLine("Removing Borrowers");
            Console.WriteLine("Removing 4 " + TestMovie1.RemoveBorrower(Member4));

            IMovie[] Array = MovieCollection.ToArray();
            MovieCollection.Search(TestMovie6);

            //string test1 = MovieCollection.ToString();
            string test1 = TestMovie4.ToString();
            Console.WriteLine($"{test1}");
            Console.WriteLine(MovieCollection.Delete(TestMovie2));
            Console.WriteLine(MovieCollection.Delete(TestMovie3));
            Console.WriteLine(MovieCollection.Delete(TestMovie5));

            var MovieA = new Movie("A");
            //var MovieB = new Movie("B");
            //var MovieC = new Movie("C");
            //var MovieD = new Movie("D");
            //var MovieE = new Movie("E");
            //var MovieF = new Movie("F");
            //var MovieG = new Movie("G");
            //var MovieH = new Movie("H");

            //MovieCollection mc = new MovieCollection();
            //mc.Insert(MovieH);
            //mc.Insert(MovieG);
            //mc.Insert(MovieF);
            //mc.Insert(MovieE);
            //mc.Insert(MovieD);
            //mc.Insert(MovieC);
            //mc.Insert(MovieB);
            //mc.Insert(MovieA);

            //var a = mc.Search("A");
            //var b = mc.Search("B");
            //var c = mc.Search("C");

            //var arr = mc.ToArray();

            //Console.WriteLine(mc.Delete(MovieC));
            //Console.WriteLine(mc.Delete(MovieD));

            //Console.WriteLine(mc.Delete(MovieC));
            //Console.WriteLine(mc.Delete(MovieD));

            //Console.WriteLine(mc.Delete(MovieA));

            //Program program = new Program();
            ////program.TestMemberAddSearchDelete();
            ////program.PostOrderSearch(mc);
        }

        #region Binary Traversal

        void PostOrderSearch(MovieCollection mc)
        {
            //TraversePostOrder(mc.root);
            void TraversePostOrder(BTreeNode node)
            {
                if (node != null)
                {
                    TraversePostOrder(node.LChild);
                    Console.WriteLine(node.Movie.ToString() + " ");
                    TraversePostOrder(node.RChild);
                }
            }
        }

        #endregion

        #region Test Methods

        public void TestMemberAddSearchDelete()
        {
            Console.WriteLine("Added 6 Members");
            Console.WriteLine();
            MemberCollection memberCollection = new MemberCollection(6);
            var m1 = new Member("Aiden", "Cran");
            var m2 = new Member("Dio", "Maldez");
            memberCollection.Add(m1);
            memberCollection.Add(m2);
            memberCollection.Add(new Member("Sugma", "Balon"));
            memberCollection.Add(new Member("Zack", "Elon"));
            memberCollection.Add(new Member("Frank", "Boliver"));
            memberCollection.Add(new Member("Fred", "Bob"));

            Console.WriteLine(memberCollection.ToString());
            Console.ReadLine();


            Console.WriteLine("Delete 1 Member");
            Console.WriteLine();
            memberCollection.Delete(m1);
            Console.WriteLine(memberCollection.ToString());
            Console.ReadLine();

            Console.WriteLine("Searching For 1 Member");
            Console.WriteLine();

            Console.WriteLine(memberCollection.Search(m2).ToString());
            Console.ReadLine();

            Console.WriteLine("Delete 1 Member");
            Console.WriteLine();
            memberCollection.Delete(m2);
            Console.WriteLine(memberCollection.ToString());
            Console.ReadLine();

            Console.WriteLine("Searching For 1 Member");
            Console.WriteLine();

            Console.WriteLine(memberCollection.Search(m2).ToString());
            Console.ReadLine();

            var dupe = new Member("potato", "potato");

            Console.WriteLine("Attempting To Add Duplicate");
            Console.WriteLine();

            memberCollection.Add(dupe);
            memberCollection.Add(dupe);

            Console.WriteLine(memberCollection.ToString());
            Console.ReadLine();
        }

        public void TestContactNumAndPin()
        {
            List<string> phoneNumbers = new List<string>();
            phoneNumbers.Add("0123456789");
            phoneNumbers.Add("1222222222");
            phoneNumbers.Add("123");
            phoneNumbers.Add("0ABCDEFGHI");

            foreach (var item in phoneNumbers)
            {
                if (IMember.IsValidContactNumber(item))
                {
                    Console.WriteLine($"{item}: Is a VALID Number");
                }
                else
                {
                    Console.WriteLine($"{item}: Is an INVALID!! Number");
                }
            }

            List<string> pinNumbers = new List<string>();
            pinNumbers.Add("1234");
            pinNumbers.Add("ABCD");
            pinNumbers.Add("12345");
            pinNumbers.Add("ABCDE");
            pinNumbers.Add("1234567");
            pinNumbers.Add("ABCDEDE");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            foreach (var item in pinNumbers)
            {
                if (IMember.IsValidPin(item))
                {
                    Console.WriteLine($"{item}: Is a VALID Number");
                }
                else
                {
                    Console.WriteLine($"{item}: Is an INVALID!! Number");
                }
            }

            Console.ReadLine();
        }

        #endregion
    }
}
