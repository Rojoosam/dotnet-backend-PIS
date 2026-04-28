namespace SIADAL.Models.DTOs.UpdateUserDTO
{
    public class UpdateUserDTO
    {
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public bool? is_active { get; set; }
    }
}
