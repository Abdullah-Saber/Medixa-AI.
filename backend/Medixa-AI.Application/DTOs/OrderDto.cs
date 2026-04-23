namespace Medixa_AI.Application.DTOs
{
    public class OrderDto
    {
        public int OrderID { get; set; }
        public Guid PatientID { get; set; }
        public string? PatientName { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public List<OrderDetailDto> Details { get; set; } = new();
    }

    public class OrderDetailDto
    {
        public int OrderDetailID { get; set; }
        public int LabTestID { get; set; }
        public string? TestName { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateOrderDto
    {
        public Guid PatientID { get; set; }
        public List<int> LabTestIDs { get; set; } = new();
    }

    public class UpdateOrderDto
    {
        public string Status { get; set; } = string.Empty;
    }
}
