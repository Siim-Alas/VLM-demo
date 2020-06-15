using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VortexLatticeClassLibrary.Utilities;

namespace VortexLatticeBlazorUI.Models
{
    public class SimulationParameterModel
    {
        [Required]
        [RegularExpression(@"^.*\n+( *1\.0+ +.+\n)( *([ 01]*\.[0-9]+) +([ 0-]*\.[0-9]+) *.*\n)+( *1\.0+ +.+)$", ErrorMessage = "Airfoil file must be in Selig format.")]
        public string File { get; set; } = "";
        [Required]
        [GreaterThanValidation(0, ErrorMessage = "Wingspan must be greater than 0.")]
        public double WingSpan { get; set; } = 7;
        [Required]
        [GreaterThanValidation(0, ErrorMessage = "Chord must be greater than 0.")]
        public double Chord { get; set; } = 1;
        [Required]
        [GreaterThanValidation(0, ErrorMessage = "The number of tiles spanwise must be greater than 0.")]
        public int NumberOfTilesSpanwise { get; set; } = 10;
        [Required]
        [GreaterThanValidation(0, ErrorMessage = "The number of tiles chordwise must be greater than 0.")]
        public int NumberOfTilesChordwise { get; set; } = 20;
        [Required]
        [GreaterThanValidation(0, ErrorMessage = "The density of the air (rho) must be greater than 0.")]
        public double Rho { get; set; } = 1.225;
        [Required]
        public double MagnitudeOfVInfinity { get; set; } = 30;
        [Required]
        public double AOA { get; set; } = 0;
        [Required]
        public double AOY { get; set; } = 0;
    }
}
