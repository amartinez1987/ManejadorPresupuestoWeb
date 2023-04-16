namespace ManejadorPresupuesto2.Models
{
    using System;
    using System.Collections.Generic;

    public class EncabezadoMovimientoFinanciero: ObjetoBase
    {        
        public DateTime FechaMovimiento { get; set; }
        public string Titulo { get; set; }
        public string EstadoId { get; set; }
        public Estado Estado { get; set; }
        public List<DetalleMovimientoGasto> DetalleMovimientoGastos { get; set; } = new List<DetalleMovimientoGasto>();
        public List<DetalleMovimientoIngreso> DetalleMovimientoIngresos { get; set; } = new List<DetalleMovimientoIngreso>();

        public override string ToString()
        {
            return $"{Titulo},{Id}";
        }
    }
}