namespace ManejadorPresupuesto2.Models
{
    using System;
    public class ErrorViewModel: ObjetoBase
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
