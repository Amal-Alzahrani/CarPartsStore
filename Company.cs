﻿namespace CarPartsStore.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
    }
}
