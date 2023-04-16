using System;

namespace ManejadorPresupuesto2.Models
{ 
    public class ObjetoBase
    {
        public string Id { get; set; }
         public ObjetoBase()
        {
            Id = Guid.NewGuid().ToString();
        }


    }
}