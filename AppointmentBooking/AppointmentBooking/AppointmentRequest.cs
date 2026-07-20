namespace AppointmentBooking
{
    public class AppointmentRequest
    {
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime RequestedDate { get; set; }
    }
}