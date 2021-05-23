using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Crypto.API
{ 
    public class CryptoViewModel
    {
        public string id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public double current_price { get; set; }

        [Required]
        public double high_24h { get; set; }

        [Required]
        public double low_24h { get; set; }

        [Required]
        public double price_change_24h { get; set; }

        [Required]
        public double price_change_percentage_24h { get; set; }

        [Required]
        public double market_cap_change_24h { get; set; }

        [Required]
        public double market_cap_change_percentage_24h { get; set; }

        [Required]
        public double ath_change_percentage { get; set; }
    }
}
