using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VortexLatticeBlazorUI.Models
{
    public class SimulationParameterModel
    {
        [Required]
        public string File { get; set; } = "";
        [Required]
        public double WingSpan { get; set; } = 7;
        [Required]
        public double Chord { get; set; } = 1;
        [Required]
        public int NumberOfTilesSpanwise { get; set; } = 10;
        [Required]
        public int NumberOfTilesChordwise { get; set; } = 20;
        [Required]
        public double Rho { get; set; } = 1.225;
        [Required]
        public double MagnitudeOfVInfinity { get; set; } = 30;
        [Required]
        public double AOA { get; set; } = 0;
        [Required]
        public double AOY { get; set; } = 0;
    }
}
