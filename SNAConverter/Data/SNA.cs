using System;
using System.Collections.Generic;
using System.Text;

namespace SNAConverter.Data
{
    class SNA
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public string OperationType { get; set; }
        public Contragent Supplier { get; set; }
        public Licence Licence { get; set; }
        public string SupplierType { get; set; }
        public Contragent Recipient { get; set; }
        public string TransporterType { get; set; }
        public string VehicleInfo { get; set; }
        public double AlcoholForManufacture { get; set; }
        public double AlcoholForMedicine { get; set; }
        public double AlcoholForTechnicalNeed { get; set; }
        public double AlcoholTotalCount { get; set; }
        public double CognacTotalCount { get; set; }
        public decimal TotalCostEthanolAlcohol { get; set; }
        public decimal TotalCostWine { get; set; }
        public string DirectorName { get; set; }
        public string RecipientName { get; set; }

        public List<Alco> AlcoItems {get; set;}
        public List<Beer> BeerItems { get; set; }

    }
}
