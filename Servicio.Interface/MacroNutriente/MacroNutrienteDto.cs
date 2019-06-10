namespace Servicio.Interface.MacroNutriente
{
    public class MacroNutrienteDto
    {
        public long Id { get; set; }

        public long AlimentoId { get; set; }

        public string AlimentoStr { get; set; }

        public decimal Proteina { get; set; }

        public decimal Grasa { get; set; }

        public decimal Energia { get; set; }

        public decimal HidratosCarbono { get; set; }

        public decimal Calorias { get; set; }

        public bool Eliminado { get; set; }
    }
}
