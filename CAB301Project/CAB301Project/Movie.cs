//CAB301 assessment 2 - 2022
//An implementation of Movie ADT
using System;
using System.Collections.Generic;



public class Movie : IMovie
{
    private string title;  // the titleof this movie
    private MovieGenre genre;  // the genre of this movie
    private MovieClassification classification; // the classification of this movie
    private int duration; // the duration of this movie in minutes
    private int availablecopies; // the number of copies that are currently available in the library
    private int totalcopies; // the total number of copies of this movie
    private int noborrows; // the number of times this movie has been borrowed so far
    private IMemberCollection borrowers;  // a collection of members that are currently borrowing a copy of this movie


    // a constructor 
    public Movie(string t, MovieGenre g, MovieClassification c, int d, int n)
    {
        title = t;
        genre = g;
        classification = c;
        duration = d;
        availablecopies = n;
        totalcopies = n;
        noborrows = 0;
        borrowers = new MemberCollection(10);
    }

    // another constructor
    public Movie(string t)
    {
        title = t;
    }

    // get and set the tile of this movie
    public string Title { get { return title; } set { title = value; } }

    //get and set the genre of this movie
    public MovieGenre Genre { get { return genre; } set { genre = value; } }

    //get and set the classification of this movie
    public MovieClassification Classification { get { return classification; } set { classification = value; } }

    //get and set the duration of this movie
    public int Duration { get { return duration; } set { duration = value; } }

    //get and set the number of DVDs of this movie currently available in the library
    public int AvailableCopies { get { return availablecopies; } set { availablecopies = value; } }

    //get and set the total number of DVDs of this movie in the library
    public int TotalCopies { get { return totalcopies; } set { totalcopies = value; } }

    //get and set the number of times that this movie has been borrowed so far
    public int NoBorrowings { get { return noborrows; } set { noborrows = value; } }

    //get all the members who are currently holding this movie
    public IMemberCollection Borrowers { get { return borrowers; } set { borrowers = value; } }


    //Add a member to the borrowers list of this movie
    //Pre-condition: number of available copies is greater than or equals to 1 
    //Post-condition:   if the member is not in the borrowers list, add the member to the borrower list,
    //                  number of available copies decreases by one, number of borrowed times increases by one, and return true;
    //                  if the member is in the borrowers list, do not add the member to the borrowers list and return false.  
    public bool AddBorrower(IMember member)
    {
        if (availablecopies < 1) { return false; }
        if (borrowers.Search(member)) { return false; }

        // Member Is Not Already Borrowing
        borrowers.Add(member);
        availablecopies--;
        noborrows++;

        member.Moives.Add(this);

        return true;
    }

    //Remove a member from the borrower list of this movie
    //Pre-condition:    nil 
    //Post-condition:   if the member is in the borrowers list, the member is removed from the borrowers list,
    //                  number of available copies increases by one, and return true;
    //                  otherwise, return false.
    public bool RemoveBorrower(IMember member)
    {
        if (!borrowers.Search(member)) { return false; }

        // Else Borrower Is In The List
        borrowers.Delete(member);
        availablecopies++;

        member.Moives.Remove(this);

        return true;
    }

    //Define how to comapre two Movie objects
    //This movie's title is compared to another movie's title 
    //Pre-condition: nil
    //Post-condition:  return -1, if this movie's title is less than another movie's title by dictionary order
    //                 return 0, if this movie's title equals to another movie's title by dictionary order
    //                 return +1, if this movie's title is greater than another movie's title by dictionary order
    public int CompareTo(IMovie another)
    {
        if (title.CompareTo(another.Title) < 0) { return -1; }
        if (title.CompareTo(another.Title) == 0) { return 0; }
        return 1;
    }

    //Return a string containing the title, genre, classification, duration, and the number of copies of this movie currently in the  library 
    //Pre-condition: nil
    //Post-condition: A string containing the title, genre, classification, duration, and the number of available copies of this movie has been returned
    public string ToString()
    {
        return $"{title}, {genre}, {classification}, {duration}, {availablecopies} Copies Available";
    }
}

