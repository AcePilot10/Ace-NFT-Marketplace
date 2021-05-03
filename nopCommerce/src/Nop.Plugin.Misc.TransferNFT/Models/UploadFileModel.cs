using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.TransferNFT.Models
{
    public class UploadFileModel
    {
        [BindProperty]
        public IFormFile Upload { get; set; }
    }
}
