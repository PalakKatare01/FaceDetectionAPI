
using facedetection.Models;
using OpenCvSharp;
using System;
using System.Drawing;

   namespace facedetection.Services
{
    public class FaceDetectionService : IFaceDetectionService
    {
        private readonly CascadeClassifier _faceCascade;
        private readonly CascadeClassifier _eyeCascade;

        public FaceDetectionService()
        {
            string basePath = Path.Combine(AppContext.BaseDirectory, "Assets", "Haarcascades");
            _faceCascade = new CascadeClassifier(Path.Combine(basePath, "haarcascade_frontalface_default.xml"));
            _eyeCascade = new CascadeClassifier(Path.Combine(basePath, "haarcascade_eye.xml"));
        }

        public FaceDetectionResult Validate(byte[] imageBytes)
        {
            using var img = Cv2.ImDecode(imageBytes, ImreadModes.Color);
            using var gray = new Mat();
            Cv2.CvtColor(img, gray, ColorConversionCodes.BGR2GRAY);

            var faces = _faceCascade.DetectMultiScale(gray, 1.1, 4, HaarDetectionTypes.ScaleImage, new Size(30, 30));

            if (faces.Length == 0)
                return new FaceDetectionResult { Success = false, Message = "No face detected." };

            // Simple face coverage calculation
            var faceArea = faces[0].Width * faces[0].Height;
            var imgArea = img.Width * img.Height;
            double coverage = (double)faceArea / imgArea * 100;

            // Check eyes inside face
            var roi = new Mat(gray, faces[0]);
            var eyes = _eyeCascade.DetectMultiScale(roi, 1.1, 4, HaarDetectionTypes.ScaleImage, new Size(15, 15));

            if (eyes.Length < 2)
                return new FaceDetectionResult { Success = false, Message = "Eyes not properly detected.", FaceCoverage = coverage };

            return new FaceDetectionResult
            {
                Success = true,
                Message = "Face detected successfully.",
                FaceCoverage = coverage
            };
        }
    }



}


