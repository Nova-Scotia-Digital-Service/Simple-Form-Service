// using Microsoft.AspNetCore.Mvc;
// using SimpleFormsService.Services.Abstractions;
//
// namespace SimpleFormsService.Presentation.Controllers
// {
//     [ApiController]
//     [Route("template/")]
//     public class FormTemplateController : ControllerBase
//     {
//         private readonly IServiceManager _serviceManager;
//
//         public FormTemplateController(IServiceManager serviceManager) => _serviceManager = serviceManager;
//
//         [HttpGet("{id}")]
//         public async Task<IActionResult> GetFormTemplateById(string id, CancellationToken cancellationToken)
//         {
//             var formTemplateDto = await _serviceManager.FormTemplateService.GetFormTemplateByIdAsync(id, cancellationToken);
//             return Ok(formTemplateDto);
//         }
//     }
// }