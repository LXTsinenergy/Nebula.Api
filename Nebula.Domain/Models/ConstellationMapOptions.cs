﻿using Nebula.Domain.Enums;

namespace Nebula.Domain.Models
{
    public class ConstellationMapOptions
    {
        public DateTime Date { get; set; }
        public City City { get; set; }
        public int StarSize { get; set; }
        public bool CoordinatesAreVisible { get; set; }
        public bool ConstellationBorderIsVisible { get; set; }
        public bool StarsAreColored { get; set; }
        public bool ConstellationsAreNamed { get; set; }
        public bool PlanetsAreNamed { get; set; }
        public ConstellationMapTheme ThemeVariant { get; set; }
        public Language Language { get; set; }  
    }
}
