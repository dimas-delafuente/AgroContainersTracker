using System;
using System.ComponentModel.DataAnnotations;

namespace AgroContainerTracker.Domain
{
    public class AddContainerRequest
    {
        [Required]
        [Range(1, 15, ErrorMessage ="No more containers available")]
        public int ContainerId { get; set; }

        [Required]
        public double Size { get; set; }
    }
}
