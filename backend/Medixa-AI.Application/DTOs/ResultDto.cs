namespace Medixa_AI.Application.DTOs
{
    public class ResultDto
    {
        public int ResultID { get; set; }
        public int OrderDetailID { get; set; }
        public int LabTestID { get; set; }
        public string? TestName { get; set; }
        public string? Value { get; set; }
        public string? Unit { get; set; }
        public string? ReferenceRange { get; set; }
        public string? Status { get; set; }
        public DateTime? ResultDate { get; set; }
        public int? VerifiedBy { get; set; }
    }

    public class CreateResultDto
    {
        public int OrderDetailID { get; set; }
        public string Value { get; set; } = string.Empty;
        public string? Unit { get; set; }
        public string? ReferenceRange { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class UpdateResultDto
    {
        public string Value { get; set; } = string.Empty;
        public string? Unit { get; set; }
        public string? ReferenceRange { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
