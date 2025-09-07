using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServiceApp.Domain
{
    public sealed class Issue
    {
        public string Location { get; init; } = string.Empty;
        public string Category { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string AttachmentPath { get; init; } = string.Empty;
    }
}
