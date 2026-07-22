namespace AppointmentBooking
{
    //public class AppointmentRequest
    //{
    //    public Patient Patient { get; set; }
    //    public Doctor Doctor { get; set; }
    //    public DateTime RequestedDate { get; set; }
    //}


    public class AppointmentRequest
    {
        public Patient Patient { get; }
        public Doctor Doctor { get; }
        public DateTime RequestedDate { get; }
        public AppointmentRequest(Patient patient, Doctor doctor, DateTime requestedDate)
        {
            // Assign values if not null and throw exception if null
            Patient = patient ?? throw new ArgumentNullException(nameof(patient));
            Doctor = doctor ?? throw new ArgumentNullException(nameof(doctor));
            // validate booking date
            if (requestedDate.Date < DateTime.Today)
                throw new ArgumentException("Requested appointment date cannot be in the past.");
            if (requestedDate.Date == DateTime.Today)
                throw new ArgumentException("Requested appointment for today, at least one day notice is required.");
            RequestedDate = requestedDate;
        }
    }
}