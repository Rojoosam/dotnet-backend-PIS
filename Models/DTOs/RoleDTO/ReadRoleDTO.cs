using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.ReadRoleDTO
{
    public class ReadRoleDTO
    {
        public ulong id { get; set; }
        public string name { get; set; } = string.Empty;
        public DateTime? created_at { get; set; }
    }
}
