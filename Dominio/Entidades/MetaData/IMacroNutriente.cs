using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades.MetaData
{
    public interface IMacroNutriente
    {
        decimal Proteina { get; set; }

        decimal Grasa { get; set; }

        decimal Energia { get; set; }

        decimal HidratosCarbono { get; set; }

        decimal Calorias { get; set; }

        bool Eliminado { get; set; }
    }
}
