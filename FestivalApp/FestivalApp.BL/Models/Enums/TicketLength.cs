using System.ComponentModel;
using FestivalApp.BL.Mappers;

namespace FestivalApp.BL.Models.Enums
{
    [TypeConverter(typeof(EnumMapper))]
    public enum TicketLength
    {
        [Description("Day")] OneDay,
        [Description("Festival")] FestivalWide
    }
}