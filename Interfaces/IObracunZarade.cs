using Glorpa.Models;
using Glorpa.Enums;

namespace Glorpa.Interfaces
{
    public interface IObracunZarade
    {
        double Izracunaj(Dostava dostava, TipDostave vozilo);
    }
}