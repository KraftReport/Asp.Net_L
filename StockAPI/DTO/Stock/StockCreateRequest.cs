﻿using System.ComponentModel.DataAnnotations;

namespace StockAPI.DTO.Stock
{
    public class StockCreateRequest
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol is not allowed to be more than 10 characters")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(10,ErrorMessage ="Company name is not allowed to be more than 10 characters")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1,1000000000)]
        public decimal Purchase {  get; set; }
        [Required]
        [Range(0.001,100)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Industry is not allowed to be more than 10 characters")]
        public string Industry { get; set; }
        [Range(1,5000000000)]
        public long MarketCap { get; set; }
    }
}
