using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VortexLatticeClassLibrary.Utilities;

namespace VortexLatticeBlazorUI.Models
{
    public class SimulationParameterModel
    {
        [Required]
        [DisplayName("Airfoil file")]
        [RegularExpression(@"^.*\n+( *1\.0+ +.+\n)( *([ 01]*\.[0-9]+) +([ 0-]*\.[0-9]+) *.*\n)+( *1\.0+ +.+)$", ErrorMessage = "Airfoil file must be in Selig format.")]
        public string File { get; set; } = @"NACA 2412
  1.000000  0.001300
  0.950000  0.011400
  0.900000  0.020800
  0.800000  0.037500
  0.700000  0.051800
  0.600000  0.063600
  0.500000  0.072400
  0.400000  0.078000
  0.300000  0.078800
  0.250000  0.076700
  0.200000  0.072600
  0.150000  0.066100
  0.100000  0.056300
  0.075000  0.049600
  0.050000  0.041300
  0.025000  0.029900
  0.012500  0.021500
  0.000000  0.000000
  0.012500 -0.016500
  0.025000 -0.022700
  0.050000 -0.030100
  0.075000 -0.034600
  0.100000 -0.037500
  0.150000 -0.041000
  0.200000 -0.042300
  0.250000 -0.042200
  0.300000 -0.041200
  0.400000 -0.038000
  0.500000 -0.033400
  0.600000 -0.027600
  0.700000 -0.021400
  0.800000 -0.015000
  0.900000 -0.008200
  0.950000 -0.004800
  1.000000 -0.001300";
        [Required]
        [DisplayName("Wingspan")]
        [GreaterThanValidation(0, ErrorMessage = "Wingspan must be greater than 0.")]
        public double WingSpan { get; set; } = 7;
        [Required]
        [DisplayName("Chord")]
        [GreaterThanValidation(0, ErrorMessage = "Chord must be greater than 0.")]
        public double Chord { get; set; } = 1;
        [Required]
        [DisplayName("The number of tiles spanwise")]
        [GreaterThanValidation(0, ErrorMessage = "The number of tiles spanwise must be greater than 0.")]
        public int NumberOfTilesSpanwise { get; set; } = 10;
        [Required]
        [DisplayName("The number of tiles chordwise")]
        [GreaterThanValidation(0, ErrorMessage = "The number of tiles chordwise must be greater than 0.")]
        public int NumberOfTilesChordwise { get; set; } = 20;
        [Required]
        [DisplayName("The specified density")]
        [GreaterThanValidation(0, ErrorMessage = "The density of the air (rho) must be greater than 0.")]
        public double Rho { get; set; } = 1.225;
        [Required]
        [DisplayName("Magnitude of V_infinity")]
        public double MagnitudeOfVInfinity { get; set; } = 30;
        [Required]
        public double AOA { get; set; } = 0;
        [Required]
        public double AOY { get; set; } = 0;
    }
}
