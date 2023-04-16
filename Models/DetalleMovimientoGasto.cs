namespace ManejadorPresupuesto2.Models
{
    using System;
    public class DetalleMovimientoGasto: ObjetoBase
    {
                
        public string ItemCategoriaId { get; set; }
        public ItemCategoria ItemCategoria { get; set; }
        public string EstadoId { get; set; }
        public Estado Estado { get; set; }
        public string EncabezadoMovimientoFinancieroId { get; set; }
        public EncabezadoMovimientoFinanciero EncabezadoMovimientoFinanciero { get; set; }
        public decimal ValorMovimiento { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }

        public override string ToString()
        {
            return $"{EncabezadoMovimientoFinanciero.Titulo}-{ItemCategoria.NombreItem},{Id}";
        }
    }
}