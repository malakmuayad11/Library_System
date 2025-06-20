using System;
using Library_Data;

namespace Library_Business
{
    public class clsAuthor
    {
        public int AuthorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public enum enMode { AddNew = 1, Update = 2 }
        private enMode _Mode;

        public clsAuthor()
        {
            this.AuthorID = -1;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            _Mode = enMode.AddNew;
        }

        public clsAuthor(int authorID, string firstName, string lastName)
        {
            AuthorID = authorID;
            FirstName = firstName;
            LastName = lastName;
            this._Mode = enMode.Update;
        }

        private bool _AddNewAuthor()
        {
            this.AuthorID = clsAuthorData.AddNewAuthor(this.FirstName, this.LastName);
            return this.AuthorID != -1;
        }

        public bool Save()
        {
            switch (this._Mode)
            {
                case enMode.AddNew:
                    if (_AddNewAuthor())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    break;
            }
            return false;
        }

        public static bool IsAuthorExists(string FirstName, string LastName) =>
            clsAuthorData.IsAuthorExists(FirstName, LastName);

        public static clsAuthor FindByBookID(int BookID)
        {
            int AuthorID = -1;
            string FirstName = string.Empty;
            string LastName = string.Empty;

            if (clsAuthorData.FindAuthorByBookID(BookID, ref AuthorID, ref FirstName, ref LastName))
                return new clsAuthor(AuthorID, FirstName, LastName);
            else
                return null;
        }

        public static clsAuthor Find(string FirstName, string LastName)
        {
            int AuthorID = -1;
            if (clsAuthorData.Find(FirstName, LastName, ref AuthorID))
                return new clsAuthor(AuthorID, FirstName, LastName);
            else
                return null;
        }
    }
}
