using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TRP_Management.EF;

namespace TRP_Management.DTOs
{
    public class ProgramDTO
    {
        [Key]
        public int ProgramId { get; set; }

        [Required(ErrorMessage = "ProgramName is required.")]
        [StringLength(150, ErrorMessage = "ProgramName cannot exceed 150 characters.")]
        public string ProgramName { get; set; }

        [Required(ErrorMessage = "TRPScore is required.")]
        [Range(0.0, 10.0, ErrorMessage = "TRPScore must be a decimal value between 0.0 and 10.0.")]
        public decimal TRPScore { get; set; }

        [Required(ErrorMessage = "ChannelId is required.")]
        public int ChannelId { get; set; }

        [Required(ErrorMessage = "AirTime is required.")]
        public TimeSpan AirTime { get; set; }

        public virtual Channel Channel { get; set; }

      
    }
}