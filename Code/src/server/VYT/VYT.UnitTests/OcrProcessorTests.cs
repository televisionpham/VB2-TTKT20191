using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using VYT.ProcessingStation.Service;

namespace VYT.UnitTests
{
    [TestClass]
    public class OcrProcessorTests
    {
        [TestMethod]
        public void Can_ocr_images()
        {
            var image = @"D:\DKC\AnhMau\vbpq\CT20UB.tif";
            var processor = new OcrProcessor();
            processor.ProcessJob(image, "vie", "E:\\Temp\\out");
        }
    }
}
