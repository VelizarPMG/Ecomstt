using Ecomst.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata.Ecma335;

namespace Ecomst.Helpers
{
    public class Utils
    {
        public static IEnumerable<SelectListItem> ListToSelectListItem<T>(List<T> data, string propertyForText, string propertyForValue)
        {
            IEnumerable<SelectListItem> itemList = data.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.GetType().GetProperty(propertyForText).GetValue(a, null).ToString(),
                    Value = a.GetType().GetProperty(propertyForValue).GetValue(a, null).ToString(),
                };
            });
            return itemList;
        }
       
        public static string SaveFormFile(IFormFile file, string directory)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            using (var fileStream = new FileStream(Path.Combine(directory, fileName), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return fileName;
        }
        
        public static void DeleteFile(string path)
        {
            if (!String.IsNullOrEmpty(path) && System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }
    }
}
