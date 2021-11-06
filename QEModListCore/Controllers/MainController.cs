using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using QEModList.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QEModList.Core.Controllers
{
    [ApiController]
    [Route("")]
    public class MainController : ControllerBase
    {
        private AddonsRepository _repository;

        public MainController(AddonsRepository repository)
        {
            _repository = repository;
        }

        

        [HttpGet]
        [Route("/content.json",Order = 1)]
        public IActionResult GetContentJson()
        {
            var list = _repository.List;

            return Ok(list);
        }

        [HttpGet]
        [Route("/empty.pak", Order = 2)]
        public IActionResult GetEmptyPak()
        {
            return new FileContentResult(Convert.FromBase64String("UEFDSwwAAAAAAAAA"), "application/octet-stream");
        }

        [HttpGet]
        [Route("/{file}", Order = 3)]
        public async Task<IActionResult> GetFileAsync(string file, CancellationToken cancellationToken)
        {
            // Parse the source id
            const string Separator = "__";
            var idx = file.IndexOf(Separator);

            if (idx == -1)
                return BadRequest("Missing source id from filename");

            if (!int.TryParse(file.Substring(0, idx), out var sourceId))
                return BadRequest("Invalid source id");

            var filename = file.Substring(idx + Separator.Length);


            // Get mime type
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(file, out var contentType))
                contentType = "application/octet-stream";

            // Return file
            return new FileStreamResult(await _repository.Sources[sourceId].GetFileStreamAsync(filename, cancellationToken), contentType)
            {
                FileDownloadName = filename
            };

            
        }
    }
}
