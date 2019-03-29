namespace Servicio.Interface.MacroNutriente
{
    public class MacroNutrienteDto
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public long AlimentoId { get; set; }

        public string AlimentoStr { get; set; }

        public int Proteina { get; set; }

        public int Grasa { get; set; }

        public int Energia { get; set; }

        public int HidratosCarbono { get; set; }

        public int Calorias { get; set; }

        public bool Eliminado { get; set; }
    }
}
