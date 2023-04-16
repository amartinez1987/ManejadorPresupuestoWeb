using System.Collections.Generic;

namespace ManejadorPresupuesto2.Models
{ 
    public class Categoria: ObjetoBase
    {        
        public string NombreCategoria { get; set; }
        List<ItemCategoria> ItemsCategoria { get; set; } = new List<ItemCategoria>();
    }
}