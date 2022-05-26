//CAB301 assessment 1 - 2022
//The specifications of MemberCollection ADT
using System;
using System.Collections.Generic;
using System.Text;





public interface IMemberCollection
{
    public int Capacity // get the capacity of this member collection 
    {
        get;
    }
    public int Number // get the number of members in this collection
    {
        get;
    }

    // Check if this member collection is full
    // Pre-condition: nil
    // Post-condition: return ture if this member collection is full; otherwise return false.
    public bool IsFull();


    // check if this member collection is empty
    // Pre-condition: nil
    // Post-condition: return ture if this member collection is empty; otherwise return false.
    public bool IsEmpty();


    // Add a new member to this member collection
    // Pre-condition: this member collection is not full
    // Post-condition: a new member is added to the member collection and the members are sorted in ascending order by their full names;
    // No duplicate has been added into this the member collection
    public void Add(IMember member);

    // Remove a given member out of this member collection
    // Pre-condition: nil
    // Post-condition: the given member has been removed from this member collection, if the given meber was in the member collection and the remaining members remain sorted by their full name.
    public void Delete(IMember aMember);


    // Search a given member in this member collection 
    // Pre-condition: nil
    // Post-condition: return true if this memeber is in the member collection; return false otherwise; member collection remains unchanged.
    public bool Search(IMember member);

    // Remove all the members in this member collection
    // Pre-condition: nil
    // Post-condition: no member in this member collection 
    public void Clear();

    // Return a string containing the information about all the members in this member collection.
    // The information includes last name, first name and contact number in this order
    // Pre-condition: nil
    // Post-condition: a string containing the information about all the members in this member collection is returned
    public string ToString();

   
}


