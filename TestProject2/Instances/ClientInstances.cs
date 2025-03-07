using TestProject2.Models;

namespace TestProject2.Instances
{
    public class ClientInstances
    {
        public static Client ValidClient = new Client
        {
            ClientType = "Adult",
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 1),
            PreferedName = "JD",
            ClientStatus = "Active",
            WaitList = false,
            Location = "",
            Phone = new List<ClientContactDetails>
            {
                new ClientContactDetails
                {
                    Contact = "123-456-7890",
                    Type = "Mobile",
                    Permission = "Text OK"
                },
                new ClientContactDetails
                {
                    Contact = "123-456-7891",
                    Type = "Home",
                    Permission = "Do not use"
                }
            },
            UpcommingAppointments = false,
            IncompleteDocuments = false,
            Cancellations = true
        };
    }
}
