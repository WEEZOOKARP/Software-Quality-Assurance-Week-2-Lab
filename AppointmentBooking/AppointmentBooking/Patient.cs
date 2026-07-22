using System.Text.RegularExpressions;

namespace AppointmentBooking
{
    public class Patient
    {
        public string Id { get; }
        public string LegalName { get; }
        public string PreferredName { get; }
        public string DisplayName
        {
            get
            {   // Check for preferred name, if none is provided use legal name
                if (string.IsNullOrWhiteSpace(PreferredName))
                    return LegalName;
                return PreferredName;
            }
        }
        public Patient(string id, string legalName, string preferredName = "")
        {   // If legal name or id are empty throw error message
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Patient ID is required.");
            if (string.IsNullOrWhiteSpace(legalName))
                throw new ArgumentException("Legal name is required.");
            String tempString = id; // Couldn't be bothered with regex
            if (!tempString.StartsWith('P')) // Basic id format, as long as it starts with P you're fine.
                throw new ArgumentException("Patient ID is invalid, must start with: P.");
            // Valid inputs
            Id = id;
            LegalName = legalName;
            PreferredName = preferredName;
        }
    }
}