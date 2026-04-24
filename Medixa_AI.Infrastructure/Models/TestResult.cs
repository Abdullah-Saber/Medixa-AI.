namespace Medixa_AI.Infrastructure.Models
{
    public class TestResult
    {
        public int Id { get; set; }
        public int TestOrderId { get; set; }
        public string Result { get; set; } = string.Empty;
        public string? Interpretation { get; set; }
        public DateTime ResultDate { get; set; }
        public string? TechnicianNotes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
