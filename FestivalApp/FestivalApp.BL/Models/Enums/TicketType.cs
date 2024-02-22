using System.ComponentModel;
using FestivalApp.BL.Mappers;

namespace FestivalApp.BL.Models.Enums
{
    [TypeConverter(typeof(EnumMapper))]
    public enum TicketType
    {
        [Description("Standard")] Basic,
        [Description("V.I.P.")] Vip
    }
}