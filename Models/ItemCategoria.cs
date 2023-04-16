using System.Collections.Generic;

namespace ManejadorPresupuesto2.Models
{    
    public class ItemCategoria: ObjetoBase
    {        
        public string NombreItem { get; set; }
        public string CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        List<DetalleMovimientoGasto> DetalleMovimientoGastos { get; set; } = new List<DetalleMovimientoGasto>();
        List<DetalleMovimientoIngreso> DetalleMovimientoIngresos { get; set; } = new List<DetalleMovimientoIngreso>();        
    }
}