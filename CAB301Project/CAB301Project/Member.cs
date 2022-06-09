//CAB301 assessment 1 - 2022
//The implementation of Member ADT
using System;
using System.Collections.Generic;

public class Member : IMember
{
    // Fields
    private string firstName;
    private string lastName;
    private string contactNumber;
    private string pin;


    // Properties
    public string FirstName { get { return firstName; } set { firstName = value; } }  // Get and set the first name of this member
    public string LastName { get { return lastName; } set { lastName = value; } }  // Get and set the last name of this member
    public string ContactNumber { get { return contactNumber; } set { contactNumber = value; } }  // Get and set the contact number of this member
    public string Pin { get { return pin; } set { pin = value; } }// Get and set a pin number

    public List<IMovie> Moives => _movies;

    List<IMovie> _movies = new List<IMovie>();

    
    // Constructor with member's first name and lastname
    public Member(string firstName, string lastName)
    {
        this.firstName = firstName;
        this.lastName = lastName;
    }

    // Constructor with member's full details
    public Member(string firstName, string lastName, string contactNumber, string pin)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.contactNumber = contactNumber;
        this.pin = pin;
    }



    // Define how to comapre two member objects
    // This member's full name is compared to another member's full name 
    // Pre-condition: nil
    // Post-condition: return -1 if this member's full name is less than another's full name in dictionary order
    //                 return 0, if this member's full name equals to another's full name in dictionary order
    //                 return +1, of this member's full name is greater than another's full name in dictionary order
    public int CompareTo(Member another)
    {
        if (this.LastName.CompareTo(another.LastName) < 0)
        {
            return -1;
        }
        else
            if (this.LastName.CompareTo(another.LastName) == 0)
        {
            return this.FirstName.CompareTo(another.FirstName);
        }
        else
            return 1;
    }



    // Return a string containing the first name, last name and contact number of this memeber
    // Pre-condition: nil
    // Post-condition: a  string containing the full name of this member is returned
    public string ToString()
    {
        return lastName + ", " + firstName;
    }
}