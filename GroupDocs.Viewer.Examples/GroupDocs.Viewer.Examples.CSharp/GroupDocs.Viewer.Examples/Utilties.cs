﻿using GroupDocs.Viewer.Config;
using GroupDocs.Viewer.Converter.Options;
using GroupDocs.Viewer.Domain;
using GroupDocs.Viewer.Domain.Options;
using GroupDocs.Viewer.Handler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace GroupDocs.Viewer.Examples
{
    public static class Utilities
    {
        public const string StoragePath = "../../../../Data/Storage/";
        public const string OutputHtmlPath = "../../../../Data/Output/html/";
        public const string OutputImagePath = "../../../../Data/Output/images/";
        public const string OutputPath = "../../../../Data/Output/";
        public const string licensePath = "../../GroupDocs.total.lic";

        #region Configurations

        //ExStart:Configurations
        
        public static ViewerConfig GetConfigurations()
        {
            ViewerConfig config = new ViewerConfig();
            //set the storage path
            config.StoragePath = StoragePath;
            //Uncomment the below line for cache purpose
            //config.UseCache = true;
            return config;
        }




        //ExEnd:Configurations
        #endregion
        
        #region Transformations
        
        public static class PageTransformations
        {
           /// <summary>
           /// Rotate a Page before rendering
           /// </summary>
           /// <param name="options"></param>
           /// <param name="angle"></param>
           
            public static void RotatePages(ref ImageOptions options, int angle)
            {
               // config.UsePdf = true;
               
                // Set rotation angle
                options.RotationAngle = angle;
             }
           /// <summary>
           /// Reorder a page before rendering
           /// </summary>
           /// <param name="Handler">Base class of handlers</param>
           /// <param name="guid">File name</param>
           /// <param name="currentPageNumber">Existing number of page</param>
           /// <param name="newPageNumber">New number of page</param>
            public static void ReorderPage(ref ViewerHandler Handler, String guid, int currentPageNumber, int newPageNumber)
            {
                ReorderPageOptions options = new ReorderPageOptions(guid, currentPageNumber, newPageNumber);
                Handler.ReorderPage(options);
            }
            /// <summary>
            /// add a watermark text to all rendered images.
            /// </summary>
            /// <param name="options">HtmlOptions by reference</param>
            /// <param name="text">Watermark text</param>
            /// <param name="color">System.Drawing.Color</param>
            /// <param name="position"></param>
            /// <param name="width"></param>
            public static void AddWatermark(ref ImageOptions options, String text, Color color, WatermarkPosition position,int width)
            {
                
                Watermark watermark = new Watermark(text);
                watermark.Color = color;
                watermark.Position = position;
                watermark.Width = width;
                options.Watermark = watermark;
            }
            /// <summary>
            /// add a watermark text to all rendered Html pages.
            /// </summary>
            /// <param name="options">HtmlOptions by reference</param>
            /// <param name="text">Watermark text</param>
            /// <param name="color">System.Drawing.Color</param>
            /// <param name="position"></param>
            /// <param name="width"></param>
            public static void AddWatermark(ref HtmlOptions options, String text, Color color, WatermarkPosition position, int width)
            {

                Watermark watermark = new Watermark(text);
                watermark.Color = color;
                watermark.Position = position;
                watermark.Width = width;
                options.Watermark = watermark;
            }

        }
        
        #endregion

        #region ProductLicense
        //ExStart:ApplyLicense
        /// <summary>
        /// Set product's license
        /// </summary>
        public static void ApplyLicense()
        {
            //License lic = new License();
            //lic.SetLicense(licensePath);
        }
        //ExEnd:ApplyLicense
        #endregion

        #region OutputHandling
        /// <summary>
        /// Save file in html form
        /// </summary>
        /// <param name="filename">Save as provided string</param>
        /// <param name="content">Html contents in String form</param>
        public static void SaveAsHtml(String filename, String content)
        {
            try
            {
                String fname = Path.Combine(Path.GetFullPath(OutputHtmlPath), Path.GetFileNameWithoutExtension(filename) + ".html");
               
                // create a file at the disk
                System.IO.File.WriteAllText(fname, content);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Save the rendered images at disk
        /// </summary>
        /// <param name="imageName">Save as provided string</param>
        /// <param name="imageContent">stream of image contents</param>
        public static void SaveAsImage(String imageName, Stream imageContent)
        {
            try
            {
                // extract the image from stream
                Image img = Image.FromStream(imageContent);
                
                //save the image in the form of jpeg
                img.Save(Path.Combine(Path.GetFullPath(OutputImagePath), Path.GetFileNameWithoutExtension(imageName)) + ".Jpeg", ImageFormat.Jpeg);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Save file in any format
        /// </summary>
        /// <param name="imageName">Save as provided string</param>
        /// <param name="imageContent">stream of image contents</param>
        public static void SaveFile(String filename, Stream content)
        {
            try
            {
           

                FileStream fileStream = File.Create(Path.Combine(Path.GetFullPath(OutputPath), filename), (int)content.Length);

                // Initialize the bytes array with the stream length and then fill it with data
                byte[] bytesInStream = new byte[content.Length];
                content.Read(bytesInStream, 0, bytesInStream.Length);

                // Use write method to write to the file specified above
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
               
            }
            catch ( System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
       
        #endregion
    }
  
}
