namespace TestProject2.Models
{
    public class Client
    {
        public string ClientType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PreferedName { get; set; }
        public string ReferedBy { get; set; }
        public string ClientStatus { get; set; }
        public bool WaitList { get; set; }
        public string Location { get; set; }
        public List<ClientContactDetails> Email { get; set; }
        public List<ClientContactDetails> Phone { get; set; }
        public bool UpcommingAppointments { get; set; }
        public bool IncompleteDocuments { get; set; }
        public bool Cancellations { get; set; }
    }
}
