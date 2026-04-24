namespace Medixa_AI.Infrastructure.Models
{
    public class TestOrder
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int LabTestId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
