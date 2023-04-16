namespace ManejadorPresupuesto2.Models
{
    using System;
    using System.Collections.Generic;

    public class Estado: ObjetoBase
    {        
        public string NombreEstado { get; set; }
        List<DetalleMovimientoGasto> DetalleMovimientoGastos { get; set; } = new List<DetalleMovimientoGasto>();
        List<DetalleMovimientoIngreso> DetalleMovimientoIngresos { get; set; } = new List<DetalleMovimientoIngreso>();
        List<EncabezadoMovimientoFinanciero> EncabezadoMovimientoFinancieros { get; set; } = new List<EncabezadoMovimientoFinanciero>();        

        public override string ToString()
        {
            return $"{NombreEstado},{Id}";
        }
    }
}