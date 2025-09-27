namespace facedetection.Services
{
    public class IFaceDetectionService
    {
        public interface IFaceDetectionService
        {
            FaceDetectionResult Validate(byte[] imageBytes);
        }

        public class FaceDetectionResult
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public double FaceCoverage { get; set; } = 0;
        }
    }
}
