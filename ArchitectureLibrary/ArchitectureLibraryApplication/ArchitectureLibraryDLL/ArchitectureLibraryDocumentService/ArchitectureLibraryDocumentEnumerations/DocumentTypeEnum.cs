using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryDocumentEnumerations
{
    public enum DocumentTypeEnum : int
    {
        [Description("Upload")]
        Upload = 100,
        [Description("Canvas")]
        Canvas = 200,
        [Description("Email")]
        Email = 300,
    }
}
