namespace Frontend.Models
{
    public class DashBoard
    {
        public int TotalStudents { get; set; }
        public int TotalBook { get; set; }
        public int TotalTransaction { get; set; }
        public int TotalBooksBorrowed { get; set; }
        public int TotalBooksReturned { get; set; }
    }

        public class OverdueBorrower
    {
        public string Name { get; set; }
        public string BorrowedId { get; set; }
    }
    public class MyComponentModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
