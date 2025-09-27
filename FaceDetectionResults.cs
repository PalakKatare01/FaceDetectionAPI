namespace facedetection.Models
{
    public class FaceDetectionResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int FacesDetected { get; set; }
        public double BlurValue { get; set; }
        public double FaceCoverage { get; set; }
        public string? AnnotatedImagePath { get; set; }
        public string? Error { get; set; }
    }
}

