namespace AppointmentBooking
{
    public class Doctor
    {
        public string Id { get; }
        public string FullName { get; }
        public int AvailableSlots { get; private set; }

        public Doctor(string id, string fullName, int availableSlots)
        {
            // basic validation of provided values
            if (string.IsNullOrWhiteSpace(id))
                // throw message if id is empty
                throw new ArgumentException("Doctor ID is required.");
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Doctor name is required.");
            if (availableSlots < 0)
                throw new ArgumentException("Available slots cannot be negative.");
            // Assuming all values are valid continue
            Id = id;
            FullName = fullName;
            AvailableSlots = availableSlots;
        }
        public bool HasAvailableSlot()
        {
            return AvailableSlots > 0;
        }
        public void ReserveSlot()
        {
            //if no valid slot exists (if available slots > 0)
            if (!HasAvailableSlot())
                throw new InvalidOperationException("No appointment slots are available.");
            AvailableSlots--; // There are available spaces, decrement
        }
    }
}