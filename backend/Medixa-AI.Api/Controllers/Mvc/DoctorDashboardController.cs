using Medixa_AI.Application.DTOs;
using Medixa_AI.Application.Interfaces;
using Medixa_AI.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OrderStatus = Medixa_AI.Domain.Enums.OrderStatus;

namespace Medixa_AI.Api.Controllers.Mvc
{
    public class DoctorDashboardController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IOrderService _orderService;
        private readonly IResultService _resultService;
        private readonly IPatientService _patientService;

        public DoctorDashboardController(
            IDoctorService doctorService,
            IOrderService orderService,
            IResultService resultService,
            IPatientService patientService)
        {
            _doctorService = doctorService;
            _orderService = orderService;
            _resultService = resultService;
            _patientService = patientService;
        }

        // GET: /DoctorDashboard
        public async Task<IActionResult> Index()
        {
            var allDoctors = await _doctorService.GetAllAsync();
            var allOrders = await _orderService.GetAllAsync();
            var allResults = await _resultService.GetAllAsync();
            var allPatients = await _patientService.GetAllAsync();

            var viewModel = new DoctorDashboardViewModel
            {
                TotalPatients = allPatients.Count(),
                PendingAppointments = 0, // TODO: Implement when AppointmentService is ready
                TodayResults = allResults.Count(r => r.ResultDate.Date == DateTime.Today),
                ActiveOrders = 0, // TODO: Add InProgress status to OrderStatus enum
                RecentPatients = allPatients
                    .Take(5)
                    .Select(p => new RecentPatient
                    {
                        PatientID = p.PatientID,
                        FullName = p.FullName,
                        LastVisit = p.RegistrationDate
                    }).ToList(),
                UpcomingAppointments = new List<UpcomingAppointment>
                {
                    new UpcomingAppointment
                    {
                        AppointmentID = 1,
                        PatientName = "John Doe",
                        AppointmentDate = DateTime.Now.AddHours(2),
                        Reason = "Routine Checkup"
                    },
                    new UpcomingAppointment
                    {
                        AppointmentID = 2,
                        PatientName = "Jane Smith",
                        AppointmentDate = DateTime.Now.AddHours(4),
                        Reason = "Follow-up"
                    }
                }
            };

            return View(viewModel);
        }

        // GET: /DoctorDashboard/Doctors
        public async Task<IActionResult> Doctors()
        {
            var doctors = await _doctorService.GetAllAsync();
            return View(doctors);
        }

        // GET: /DoctorDashboard/Orders
        public async Task<IActionResult> Orders()
        {
            var orders = await _orderService.GetAllAsync();
            return View(orders);
        }

        // GET: /DoctorDashboard/Results
        public async Task<IActionResult> Results()
        {
            var results = await _resultService.GetAllAsync();
            return View(results);
        }
    }
}
