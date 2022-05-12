using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment4.Models
{
    public class Records
    {
        private double value;
        public string RecordsId { get; set; }
      
        public RecordsType Type { get; set; }
        public double Value
        {
            get {
                return this.value;
            }
            set
            {
                if (value >= 0)
                {
                    this.value = value;
                }
                else
                {
                    throw new Exception("ERROR: Value cannot be negative!");
                }
            }
        }
        public string Description { get; set; }
        public DateTime ProcessedDate { get; set; }
        public Records()
        {
           //his.RecordsId = new Random().Next(10000, 99999).ToString();
            this.ProcessedDate = DateTime.Now;
        }

        public enum RecordsType { 
            Income,
            Expense
        }
        public override string ToString()
        {
            return $"{RecordsId} {Type} {Value} {Description} {ProcessedDate} ";
        }


    }
}
