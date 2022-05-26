// Phase 2
// An implementation of MovieCollection ADT
// 2022


using System;

//A class that models a node of a binary search tree
//An instance of this class is a node in a binary search tree 
public class BTreeNode
{
	private IMovie movie; // movie
	private BTreeNode lchild; // reference to its left child 
	private BTreeNode rchild; // reference to its right child

	public BTreeNode(IMovie movie)
	{
		this.movie = movie;
		lchild = null;
		rchild = null;
	}

	public IMovie Movie
	{
		get { return movie; }
		set { movie = value; }
	}

	public BTreeNode LChild
	{
		get { return lchild; }
		set { lchild = value; }
	}

	public BTreeNode RChild
	{
		get { return rchild; }
		set { rchild = value; }
	}
}

// invariant: no duplicates in this movie collection
public class MovieCollection : IMovieCollection
{
	private BTreeNode root; // movies are stored in a binary search tree and the root of the binary search tree is 'root' 
	private int count; // the number of (different) movies currently stored in this movie collection 

	// get the number of movies in this movie colllection 
	// pre-condition: nil
	// post-condition: return the number of movies in this movie collection and this movie collection remains unchanged
	public int Number { get { return count; } }

	// constructor - create an object of MovieCollection object
	public MovieCollection()
	{
		root = null;
		count = 0;
	}

	// Check if this movie collection is empty
	// Pre-condition: nil
	// Post-condition: return true if this movie collection is empty; otherwise, return false.
	public bool IsEmpty()
	{
        if (root == null) { return true; }
        return false;
    }

	// Insert a movie into this movie collection
	// Pre-condition: nil
	// Post-condition: the movie has been added into this movie collection and return true, if the movie is not in this movie collection; otherwise, the movie has not been added into this movie collection and return false.
	public bool Insert(IMovie movie)
	{
		if (IsEmpty())
		{
			// If Empty, Instantiate Tree
			root = new BTreeNode(movie);
			count++;
			return true;
		}

		// Check If Already Exists
		if (Search(movie)) { return false; }

		// Recursive Call
		return Insert(movie, root);

		// Local Insert Method (Added Pointer)
		bool Insert(IMovie item, BTreeNode pointer)
        {
			if (pointer.Movie.CompareTo(item) == -1)
			{
				if (pointer.LChild == null)
				{
					pointer.LChild = new BTreeNode(item);
					count++;
					return true;
				}

				// Traverse L-Tree
				return Insert(item, pointer.LChild);
			}

			if (pointer.RChild == null)
			{
				pointer.RChild = new BTreeNode(item);
				count++;
				return true;
			}

			// Traverse R-Tree
			return Insert(item, pointer.RChild);
		}
	}



	// Delete a movie from this movie collection
	// Pre-condition: nil
	// Post-condition: the movie is removed out of this movie collection and return true, if it is in this movie collection; return false, if it is not in this movie collection
	public bool Delete(IMovie movie)
	{
		// Check If It Exists
		if (!Search(movie)) { return false; }

		BTreeNode pointer = root;
		BTreeNode parent = null;

		// Locating Item
		while (pointer != null && pointer.Movie.CompareTo(movie) != 0)
		{
			parent = pointer;
			if (pointer.Movie.CompareTo(movie) == -1) 
			{ 
				pointer = pointer.LChild; 
			}
			else 
			{ 
				pointer = pointer.RChild; 
			}
        }

		if (pointer != null)
        {
			// CASE 1: Pointer has 2 children
			if (pointer.LChild != null && pointer.RChild != null)
            {
				// Special Case
				if (pointer.LChild.RChild == null)
                {
					pointer.Movie = pointer.LChild.Movie;
					pointer.LChild = pointer.LChild.LChild;
                }
                else
                {
					// Find Right-Most Node of the Pointer's Left Subtree
					BTreeNode p = pointer.LChild;
					BTreeNode pp = pointer;

                    while (p.RChild != null)
                    {
						pp = p;
						p = p.RChild;
                    }

					pointer.Movie = p.Movie;
					pp.RChild = p.LChild;
                }
            }
            else
            {
				BTreeNode c;
				if(pointer.LChild != null) 
				{ 
					c = pointer.LChild; 
				}
                else 
				{ 
					c = pointer.RChild; 
				}

				if (pointer == root) 
				{ 
					root = c;
				}
				else
                {
					if (pointer == parent.LChild) 
					{ 
						parent.LChild = c; 
					}
					else 
					{ 
						parent.RChild = c; 
					}
                }
            }

			count--;
			return true;
        }
		return false;
	}

	// Search for a movie in this movie collection
	// pre: nil
	// post: return true if the movie is in this movie collection;
	//	     otherwise, return false.
	public bool Search(IMovie movie)
	{
		if (IsEmpty()) { return false; }

		return Search(movie, root);

		// Local Search - Intended for Recursion
		bool Search(IMovie item, BTreeNode pointer)
        {
			if (pointer == null) { return false; }

			// Check Item
			if (pointer.Movie.CompareTo(item) == 0) { return true; }

			// Search Left
			if (pointer.Movie.CompareTo(item) == -1) { return Search(item, pointer.LChild); }

			// Search Right
			if (pointer.Movie.CompareTo(item) == 1) { return Search(item, pointer.RChild); }

			return false;
		}
	}

	// Search for a movie by its title in this movie collection  
	// pre: nil
	// post: return the reference of the movie object if the movie is in this movie collection;
	//	     otherwise, return null.
	public IMovie Search(string movietitle)
	{
		if (IsEmpty()) { return null; }

		return Search(movietitle, root);

		// Local Search - Intended for Recursion
		IMovie Search(string item, BTreeNode pointer)
		{
			if (pointer == null) { return null; }

			// Check Item
			if (pointer.Movie.Title.CompareTo(item) == 0) { return pointer.Movie; }

			// Search Left
			if (pointer.Movie.Title.CompareTo(item) == -1) { return Search(item, pointer.LChild); }

			// Search Right
			if (pointer.Movie.Title.CompareTo(item) == 1) { return Search(item, pointer.RChild); }

			return null;
		}
	}



	// Store all the movies in this movie collection in an array in the dictionary order by their titles
	// Pre-condition: nil
	// Post-condition: return an array of movies that are stored in dictionary order by their titles
	public IMovie[] ToArray()
	{
		IMovie[] movieArray = new IMovie[count];
		int index = 0;

		InOrderTraversal(root);

		// Reversed InOrder Traversal
		// Normal Traversal Left - Root - Right
		// This Traversal  Right - Root - Left
		void InOrderTraversal(BTreeNode pointer)
		{
			if (pointer != null)
			{
				InOrderTraversal(pointer.RChild);
				movieArray[index] = pointer.Movie;
				index++;
				InOrderTraversal(pointer.LChild);
			}
		}

		return movieArray;
	}



	// Clear this movie collection
	// Pre-condotion: nil
	// Post-condition: all the movies have been removed from this movie collection 
	public void Clear()
	{
		root = null;
		count = 0;
	}
}