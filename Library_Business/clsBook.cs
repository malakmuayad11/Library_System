using System;
using System.Data;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using Library_Data;

namespace Library_Business
{
    public class clsBook
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }
        public enum enCondition { Good = 1, Damaged = 2 }
        public enCondition Condition { get; set; }
        public DateTime PublicationDate { get; set; }

        public enum enAvailabilityStatus { Available = 1, Borrowed = 2, Reserved = 3 }
        public enAvailabilityStatus AvailabilityStatus { get; set; }

        public enum enLanguage { Arabic = 1, English = 2 }
        public enLanguage Language { get; set; }

        public enum enMode { AddNew = 1, Update = 2 }
        private enMode _Mode;

        public clsBook()
        {
            this.BookID = -1;
            this.Title = string.Empty;
            this.Genre = string.Empty;
            this.ISBN = string.Empty;
            this.Condition = enCondition.Good;
            this.PublicationDate = DateTime.MinValue;
            this.AvailabilityStatus = enAvailabilityStatus.Available;
            this.Language = enLanguage.Arabic;
            this._Mode = enMode.AddNew;
        }

        private clsBook(int BookID, string Title, string Genre, string ISBN, enCondition Condition,
            DateTime PublicationDate, enAvailabilityStatus AvailabilityStatus, enLanguage Language)
        {
            this.BookID = BookID;
            this.Title = Title;
            this.Genre = Genre;
            this.ISBN = ISBN;
            this.Condition = Condition;
            this.PublicationDate = PublicationDate;
            this.AvailabilityStatus = AvailabilityStatus;
            this.Language = Language;
            this._Mode = enMode.Update;
        }

        private bool _AddAuthorToBook(string AuthorFirstName, string AuthorLastName)
        {
            if (!clsAuthor.IsAuthorExists(AuthorFirstName, AuthorLastName))
            {
                clsAuthor author = new clsAuthor();
                author.FirstName = AuthorFirstName;
                author.LastName = AuthorLastName;
                if (!author.Save())
                    return false;
            }
            return clsBookData.AddAuthorToBook(clsAuthor.Find(AuthorFirstName, AuthorLastName)
                .AuthorID, this.BookID);
        }

        private bool _AddNewBook(string AuthorFirstName, string AuthorLastName)
        {
            this.BookID = clsBookData.AddNewBook(this.Title, this.Genre, this.ISBN, (byte)this.Condition,
                this.PublicationDate, (byte)this.Language);
            if (this.BookID == -1) return false;
            if (!_AddAuthorToBook(AuthorFirstName, AuthorLastName)) return false;
            return true;
        }

        public bool Save(string AuthorFirstName, string AuthorLastName)
        {
            switch (this._Mode)
            {
                case enMode.AddNew:
                    
                    if (_AddNewBook(AuthorFirstName, AuthorLastName))
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    break;
            }
            return false;
        }

        public bool SetCondition(enCondition Condition)=>
            clsBookData.UpdateCondition(this.BookID, (byte)Condition);
        

        public bool SetAvailabilityStatus(enAvailabilityStatus Status) =>
            clsBookData.UpdateAvailabilityStatus(BookID, (byte)Status);

        public static DataTable GetAllBooks() => clsBookData.GetAllBooks();

        public async static Task<DataTable> GetAllBooksAsync() =>
            await clsBookData.GetAllBooksAsync();

        public static bool DoesISBNExist(string ISBN) => clsBookData.DoesISBNExist(ISBN);

        public static clsBook Find(int BookID)
        {
            string Title = string.Empty;
            string Genre = string.Empty;
            string ISBN = string.Empty;
            enCondition condition = enCondition.Damaged;
            byte Condition = (byte)condition;
            DateTime PublicationDate = DateTime.MinValue;
            enAvailabilityStatus status = enAvailabilityStatus.Available;
            byte AvailabilityStatus = (byte)status;
            enLanguage language = enLanguage.Arabic;
            byte Language = (byte)language;

            if (clsBookData.Find(BookID, ref Title, ref Genre, ref ISBN, ref Condition, ref PublicationDate,
                ref AvailabilityStatus, ref Language))
                return new clsBook(BookID, Title, Genre, ISBN, (enCondition)Condition, PublicationDate,
                    (enAvailabilityStatus)AvailabilityStatus, (enLanguage)Language);
            else
                return null;
        }

        public static clsBook Find(string Title)
        {
            int BookID = -1;
            string Genre = string.Empty;
            string ISBN = string.Empty;
            enCondition condition = enCondition.Damaged;
            byte Condition = (byte)condition;
            DateTime PublicationDate = DateTime.MinValue;
            enAvailabilityStatus status = enAvailabilityStatus.Available;
            byte AvailabilityStatus = (byte)status;
            enLanguage language = enLanguage.Arabic;
            byte Language = (byte)language;

            if (clsBookData.Find(Title, ref BookID, ref Genre, ref ISBN, ref Condition, ref PublicationDate,
                ref AvailabilityStatus, ref Language))
                return new clsBook(BookID, Title, Genre, ISBN, (enCondition)Condition, PublicationDate,
                    (enAvailabilityStatus)AvailabilityStatus, (enLanguage)Language);
            else
                return null;
        }

        public static bool DeleteBook(int BookID) => clsBookData.DeleteBook(BookID);

        public static int GetAuthorID(int BookID) => clsBookData.GetAuthorID(BookID);
    }
}
