using System.Numerics;
using AppointmentBooking;

[TestClass]
public class AppointmentBookingServiceTests
{
    [TestMethod]
    public void BookAppointment_WhenDoctorHasAvailableSlots_ReturnsSuccess()
    {
        var doctor = new Doctor("D001", "Dr Mark", 2);
        var patient = new Patient("P001", "Diana William");
        var request = new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(1));
        var service = new AppointmentBookingService();
        BookingResult result = service.BookAppointment(request);
        Assert.IsTrue(result.Success);
    }
    [TestMethod]
    public void BookAppointment_WhenDoctorHasNoAvailableSlots_ReturnsFailure()
    {
        var doctor = new Doctor("D001", "Dr Mark", 0);
        var patient = new Patient("P001", "Diana William");
        var request = new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(1));
        var service = new AppointmentBookingService();
        BookingResult result = service.BookAppointment(request);
        Assert.IsFalse(result.Success);
    }
    [TestMethod]
    public void BookAppointment_WhenSuccessful_DecreasesAvailableSlots()
    {
        var doctor = new Doctor("D001", "Dr Mark", 2);
        var patient = new Patient("P001", "Diana William");
        var request = new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(1));
        var service = new AppointmentBookingService();
        service.BookAppointment(request);
        Assert.AreEqual(1, doctor.AvailableSlots);
    }
    [TestMethod]
    public void BookAppointment_WhenFailed_DoesNotDecreaseAvailableSlots()
    {
        var doctor = new Doctor("D001", "Dr Mark", 0);
        var patient = new Patient("P001", "Diana William");
        var request = new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(1));
        var service = new AppointmentBookingService();
        service.BookAppointment(request);
        Assert.AreEqual(0, doctor.AvailableSlots);
    }

    [TestMethod]
    public void Doctor_WhenIdIsEmpty_ThrowsException()
    {
        Assert.ThrowsException<ArgumentException>(() => new Doctor("", "Dr Mark", 2));
    }
    [TestMethod]
    public void Doctor_WhenAvailableSlotsIsNegative_ThrowsException()
    {
        Assert.ThrowsException<ArgumentException>(() => new Doctor("D001", "Dr Mark", -1));
    }
    [TestMethod]
    public void Patient_WhenIdIsEmpty_ThrowsException()
    {
        Assert.ThrowsException<ArgumentException>(() => new Patient("", "Diana William"));
    }
    [TestMethod]
    public void Patient_WhenPreferredNameExists_DisplayNameUsesPreferredName()
    {
        var patient = new Patient("P001", "Diana William", "Aroha");
        Assert.AreEqual("Aroha", patient.DisplayName);
    }
    [TestMethod]
    public void Patient_WhenPreferredNameMissing_DisplayNameUsesLegalName()
    {
        var patient = new Patient("P001", "Diana William");
        Assert.AreEqual("Diana William", patient.DisplayName);
    }
    [TestMethod]
    public void AppointmentRequest_WhenRequestedDateIsInPast_ThrowsException()
    {
        var doctor = new Doctor("D001", "Dr Mark", 2);
        var patient = new Patient("P001", "Diana William");
        Assert.ThrowsException<ArgumentException>(() =>
        new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(-1)));
    }
    [TestMethod]
    public void BookAppointment_WhenSuccessful_ReturnsHelpfulMessage()
    {
        var doctor = new Doctor("D001", "Dr Mark", 2);
        var patient = new Patient("P001", "Diana William", "Aroha");
        var request = new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(1));
        var service = new AppointmentBookingService();
        BookingResult result = service.BookAppointment(request);
        StringAssert.Contains(result.Message, "Appointment booked successfully");
        StringAssert.Contains(result.Message, "Aroha");
    }
    [TestMethod]
    public void BookAppointment_WhenNoSlots_ReturnsHelpfulMessage()
    {
        var doctor = new Doctor("D001", "Dr Mark", 0);
        var patient = new Patient("P001", "Diana William");
        var request = new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(1));
        var service = new AppointmentBookingService();
        BookingResult result = service.BookAppointment(request);
        StringAssert.Contains(result.Message, "no available slots");
    }

    // New tests, couldn't use copilot, kept crashing :/
    // Tried several fixes but none worked, even gemini failed while debugging
    // due to copilot crashes I couldn't complete
    // Step 15: Ask Copilot for Test Ideas
    // Step 16: Ask Copilot for a Quality Review

    [TestMethod]
    public void BookAppointment_DateIsInPast()
    {
        var doctor = new Doctor("D001", "Dr Mark", 2);
        var patient = new Patient("P001", "Diana William", "Aroha");
        Assert.ThrowsException<ArgumentException>(() =>
           new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(-1))
        );
    }

    [TestMethod]
    public void BookAppointment_DateIsToday()
    {
        var doctor = new Doctor("D001", "Dr Mark", 2);
        var patient = new Patient("P001", "Diana William", "Aroha");
        Assert.ThrowsException<ArgumentException>(() =>
           new AppointmentRequest(patient, doctor, DateTime.Today)
        );
    }

    [TestMethod]
    public void BookAppointment_Invalidinputs()
    {
        var doctor = new Doctor("D001", "Egg", 60);
        Assert.ThrowsException<ArgumentException>(() =>
            new Patient("asdf", "", "")
        );
    }

    [TestMethod]
    public void BookAppointment_InvalidSlots()
    {
        Assert.ThrowsException<ArgumentException>(() =>
            new Doctor("D001", "Egg", -60)
        );
    }

    [TestMethod]
    public void BookAppointment_IncludesPatientPreferredName()
    {
        var doctor = new Doctor("D001", "Dr Test", 2);
        var patient = new Patient("P001", "Diana", "TestName");
        var request = new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(1));
        BookingResult result = new AppointmentBookingService().BookAppointment(request);
        StringAssert.Contains(result.Message, "TestName");
    }

    [TestMethod]
    public void BookAppointment_IncludesDoctorName()
    {
        var doctor = new Doctor("D001", "Dr Test", 2);
        var patient = new Patient("P001", "Diana", "TestName");
        var request = new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(1));
        BookingResult result = new AppointmentBookingService().BookAppointment(request);
        StringAssert.Contains(result.Message, "Dr Test");
    }

    [TestMethod]
    public void BookAppointment_PatientIdDoesntStartWithP()
    {
        var doctor = new Doctor("D001", "Dr Test", 2);
        Assert.ThrowsException<ArgumentException>(() =>
            new Patient("D001", "Diana", "TestName")
        );
    }
}