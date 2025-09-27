using Microsoft.AspNetCore.Mvc;
using facedetection.Models;
using facedetection.Services;

    namespace facedetection.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FaceDetectionController : ControllerBase
    {
        private readonly IFaceDetectionService _faceService;
        private readonly ILogger<FaceDetectionController> _logger;

        public FaceDetectionController(IFaceDetectionService faceService, ILogger<FaceDetectionController> logger)
        {
            _faceService = faceService;
            _logger = logger;
        }

        [HttpPost("validate")]
        public IActionResult ValidateFace([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { success = false, message = "No image uploaded" });

            try
            {
                using var ms = new MemoryStream();
                file.CopyTo(ms);
                var imageBytes = ms.ToArray();

                var result = _faceService.Validate(imageBytes);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Face detection failed");
                return StatusCode(500, new { success = false, message = "Internal server error", error = ex.Message });
            }
        }
    }
}


