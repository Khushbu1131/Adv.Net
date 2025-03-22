using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TRP_Management.EF;

namespace TRP_Management.DTOs
{
    public class ChannelDTO
    {
        public int ChannelId { get; set; }

        [Required(ErrorMessage = "Channel Name is required.")]
        [MaxLength(100, ErrorMessage = "Channel Name cannot exceed 100 characters.")]
        public string ChannelName { get; set; }

        [Required(ErrorMessage = "Established Year is required.")]
        [Range(1900, 2024, ErrorMessage = "Year must be between 1900 and the current year.")]
        public int EstablishedYear { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [MaxLength(50, ErrorMessage = "Country cannot exceed 50 characters.")]
        public string Country { get; set; }

        public virtual ICollection<ProgramDTO> Programs { get; set; } = new List<ProgramDTO>();
    }
}