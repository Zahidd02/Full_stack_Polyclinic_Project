using System.ComponentModel.DataAnnotations;

public class Patients
{
    [Required]
    public string PatientId { get; set; }
    [Required]
    public string PatientName { get; set; }
    [Required]
    [Range(1,99)]
    public byte Age { get; set; }
    [Required]
    public string Gender { get; set; }
    [Required]
    public string ContactNumber { get; set; }
}
