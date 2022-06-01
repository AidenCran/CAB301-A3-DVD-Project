//CAB301 assessment 1 - 2022
//The implementation of MemberCollection ADT
using System;
using System.Linq;


class MemberCollection : IMemberCollection
{
    // Fields
    private int capacity;
    private int count;
    private Member[] members; //make sure members are sorted in dictionary order

    // Properties

    // get the capacity of this member colllection 
    // pre-condition: nil
    // post-condition: return the capacity of this member collection and this member collection remains unchanged
    public int Capacity { get { return capacity; } }

    // get the number of members in this member colllection 
    // pre-condition: nil
    // post-condition: return the number of members in this member collection and this member collection remains unchanged
    public int Number { get { return count; } }

   


    // Constructor - to create an object of member collection 
    // Pre-condition: capacity > 0
    // Post-condition: an object of this member collection class is created

    public MemberCollection(int capacity)
    {
        if (capacity > 0)
        {
            this.capacity = capacity;
            members = new Member[capacity];
            count = 0;
        }
    }

    // check if this member collection is full
    // Pre-condition: nil
    // Post-condition: return ture if this member collection is full; otherwise return false.
    public bool IsFull()
    {
        return count == capacity;
    }

    // check if this member collection is empty
    // Pre-condition: nil
    // Post-condition: return ture if this member collection is empty; otherwise return false.
    public bool IsEmpty()
    {
        return count == 0;
    }

    // Add a new member to this member collection
    // Pre-condition: this member collection is not full
    // Post-condition: a new member is added to the member collection and the members are sorted in ascending order by their full names;
    // No duplicate will be added into this the member collection
    public void Add(IMember member)
    {
        if (IsFull()) { Console.WriteLine("Collection Is Full"); return; }
        
        for (int i = 0; i < members.Length; i++)
        {
            var j = i;
            bool hasInserted = false;

            // Stop Duplicates
            if (members[j] != null && members[j].CompareTo((Member)member) == 0) { Console.WriteLine("Duplicate"); return; }

            if (members[j] == null) { members[j] = (Member)member; count++; hasInserted = true; }

            // Sorts Array Till Current Index
            while (j > 0 && members[j - 1].CompareTo(members[j]) == 1)
            {
                var swappedMember = members[j - 1];
                members[j - 1] = members[j];
                members[j] = swappedMember;
                j--;
            }

            if (hasInserted) { return; }
        }
    }

    // Remove a given member out of this member collection
    // Pre-condition: nil
    // Post-condition: the given member has been removed from this member collection, if the given meber was in the member collection
    public void Delete(IMember aMember)
    {
        for (int i = 0; i < members.Length; i++)
        {
            if (members[i] == null) { Console.WriteLine("Member Does Not Exist"); return; }

            // If Member Isn't Equal
            if (members[i].CompareTo((Member)aMember) != 0) { continue; }

            members[i] = null;
            count--;

            for (int j = i+1; j < members.Length; j++)
            {
                members[j-1] = members[j];
                members[j] = null;
            }
    
            return;
        }
    }

    // Search a given member in this member collection 
    // Pre-condition: nil
    // Post-condition: return true if this memeber is in the member collection; return false otherwise; member collection remains unchanged
    public bool Search(IMember member)
    {
        int left = 0;
        int right = count - 1;

        //while (left + 1 < right)
        //{
        //    int middle = (left + right) / 2;

        //    if (members[middle].CompareTo((Member)member) == 0) { Console.WriteLine("Member Found!"); return true; }
        //    if (members[middle].CompareTo((Member)member) == -1) { left = middle; }
        //    if (members[middle].CompareTo((Member)member) == 1) { right = middle; }
        //}

        while (left <= right)
        {
            int m = (left + right) / 2;
            if (members[m].CompareTo((Member)member) == 0) { return true; }
            if (members[m].CompareTo((Member)member) == -1) { left = m + 1; }
            if (members[m].CompareTo((Member)member) == 1) { right = m - 1; }
        }

        return false;
    }

    // Remove all the members in this member collection
    // Pre-condition: nil
    // Post-condition: no member in this member collection 
    public void Clear()
    {
        for (int i = 0; i < count; i++)
        {
            this.members[i] = null;
        }
        count = 0;
    }

    // Return a string containing the information about all the members in this member collection.
    // The information includes last name, first name and contact number in this order
    // Pre-condition: nil
    // Post-condition: a string containing the information about all the members in this member collection is returned
    public override string ToString()
    {
        string s = "";
        for (int i = 0; i < count; i++)
            s = s + members[i].ToString() + "\n";
        return s;
    }
	
	// Print a string containing the top 3 most borrowed movies in descending order.
	// Input: any array of movies. Does not have to be sorted.
	public void DisplayTopBorrows(IMovie[] array)
    {
		
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

		Console.WriteLine(first.Title + ": borrowed " + first.NoBorrowings + " times");
		Console.WriteLine(second.Title + ": borrowed " + second.NoBorrowings + " times");
		Console.WriteLine(third.Title + ": borrowed " + third.NoBorrowings + " times");
		Console.WriteLine();

	}
}