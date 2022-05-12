using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment4.Models
{
    public class Account
    {
	
        public string AccountId { get; set; }
		public string userName { get; set; }
        public List<Records> Records { get; set; }

		public double Balance
		{
			get
			{
				double total = 0;

				foreach (Records Record in this.Records)
				{
					if (Record.Type == Models.Records.RecordsType.Income)
						total += Record.Value;
					else
						total -= Record.Value;
				}

				return total;
			}
		}
		public Account() {
			this.AccountId = new Random().Next(10000, 99999).ToString();
			this.Records = new List<Records>();
		
		}

        public override string ToString()
        {
			return $"{AccountId} {userName}";
        }
		

    }
}
