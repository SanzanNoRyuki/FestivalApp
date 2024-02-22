using System.ComponentModel;
using FestivalApp.BL.Mappers;

namespace FestivalApp.BL.Models.Enums
{
    [TypeConverter(typeof(EnumMapper))]
    public enum Currency
    {
        [Description("USD")] Usd,
        [Description("EUR")] Euro,
        [Description("CZK")] Czk
    }
}