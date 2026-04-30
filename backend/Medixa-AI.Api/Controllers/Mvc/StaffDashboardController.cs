using Medixa_AI.Application.DTOs;
using Medixa_AI.Application.Interfaces;
using Medixa_AI.Domain.Enums;
using Medixa_AI.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OrderStatus = Medixa_AI.Domain.Enums.OrderStatus;

namespace Medixa_AI.Api.Controllers.Mvc
{
    public class StaffDashboardController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IOrderService _orderService;

        public StaffDashboardController(IEmployeeService employeeService, IOrderService orderService)
        {
            _employeeService = employeeService;
            _orderService = orderService;
        }

        // GET: /StaffDashboard
        public async Task<IActionResult> Index()
        {
            var allStaff = await _employeeService.GetAllAsync();
            var activeStaff = await _employeeService.GetActiveEmployeesAsync();
            var admins = await _employeeService.GetByRoleAsync(EmployeeRole.Admin);
            var technicians = await _employeeService.GetByRoleAsync(EmployeeRole.Technician);
            var receptionists = await _employeeService.GetByRoleAsync(EmployeeRole.Receptionist);
            var allOrders = await _orderService.GetAllAsync();

            var viewModel = new StaffDashboardViewModel
            {
                PendingOrders = allOrders.Count(o => o.Status == OrderStatus.Pending),
                InProgressOrders = 0, // TODO: Add InProgress status to OrderStatus enum
                CompletedToday = allOrders.Count(o => o.Status == OrderStatus.Completed && o.OrderDate.Date == DateTime.Today),
                TotalLabTests = allOrders.Sum(o => o.OrderDetails?.Count ?? 0),
                PendingOrdersList = allOrders
                    .Where(o => o.Status == OrderStatus.Pending)
                    .Select(o => new PendingOrder
                    {
                        OrderID = int.Parse(o.OrderID.ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber),
                        PatientName = $"Patient-{o.PatientID.ToString().Substring(0, 8)}",
                        OrderDate = o.OrderDate,
                        TestCount = o.OrderDetails?.Count ?? 0
                    }).ToList(),
                LabTestStats = new List<LabTestStat>
                {
                    new LabTestStat { TestName = "Blood Test", TotalPerformed = 150, AverageTurnaround = 24 },
                    new LabTestStat { TestName = "Urine Analysis", TotalPerformed = 200, AverageTurnaround = 12 },
                    new LabTestStat { TestName = "X-Ray", TotalPerformed = 80, AverageTurnaround = 48 }
                }
            };

            return View(viewModel);
        }

        // GET: /StaffDashboard/Active
        public async Task<IActionResult> Active()
        {
            var activeStaff = await _employeeService.GetActiveEmployeesAsync();
            return View(activeStaff);
        }

        // GET: /StaffDashboard/Role/{role}
        public async Task<IActionResult> ByRole(EmployeeRole role)
        {
            var staff = await _employeeService.GetByRoleAsync(role);
            return View(staff);
        }

        // GET: /StaffDashboard/Deactivate/{id}
        public async Task<IActionResult> Deactivate(Guid id)
        {
            // TODO: Replace with authenticated user role (JWT/Claims)
            var currentRole = EmployeeRole.Admin;
            var result = await _employeeService.DeactivateAsync(id, currentRole);
            return RedirectToAction(nameof(Index));
        }

        // GET: /StaffDashboard/Activate/{id}
        public async Task<IActionResult> Activate(Guid id)
        {
            // TODO: Replace with authenticated user role (JWT/Claims)
            var currentRole = EmployeeRole.Admin;
            var result = await _employeeService.ActivateAsync(id, currentRole);
            return RedirectToAction(nameof(Index));
        }
    }
}
