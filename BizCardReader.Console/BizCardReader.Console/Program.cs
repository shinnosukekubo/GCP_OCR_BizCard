using System;
using Google.Cloud.Language.V1;
using System.Windows.Forms;
using Google.Cloud.Vision.V1;
using System.Diagnostics;

namespace BizCardReader.Console
{
    class Program
    {
        static void Main(string[] args)
        {            
            // Instantiates a client
            var client = ImageAnnotatorClient.Create();
            // Load the image file into memory
            var image = Image.FromFile(@"C:\Users\shinn\Downloads\IMG_0662.JPG");
            // Performs label detection on the image file
            var response = client.DetectText(image);
            foreach (var annotation in response)
            {
                if (annotation.Description != null)
                    Debug.WriteLine(annotation.Description);
            }
            var text = response[0].Description;
            AnalyzeEntities(text);
        }

        public static void AnalyzeEntities(string text)
        {
            // The text to analyze.
            var client = LanguageServiceClient.Create();
            var response = client.AnalyzeEntities(new Document()
            {
                Content = text,
                Type = Document.Types.Type.PlainText
            });
            foreach (var entity in response.Entities)
            {
                Debug.WriteLine($"{entity.Type}：{entity.Name}");
            }
        }
    }
}
