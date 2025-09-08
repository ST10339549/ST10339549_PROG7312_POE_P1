using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServiceApp.Domain
{
    public sealed class Issue
    {
        // Used by repository lookups and shown to the user
        public string TrackingNumber { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AttachmentPath { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
