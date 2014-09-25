namespace CurrencyService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Class that describes the values that a currency should have
    /// </summary>
    [Table("Currency")]
    public partial class Currency
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(3)]
        public string Code { get; set; }

        public double Value { get; set; }

        public Currency()
        {
        }

    }
}
