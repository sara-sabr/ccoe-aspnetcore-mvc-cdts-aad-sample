using System;

namespace Esdc.Ccoe.AspNetCore.Mvc.Cdts.Aad.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
