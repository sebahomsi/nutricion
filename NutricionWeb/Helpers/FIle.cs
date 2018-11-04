using System.IO;
using System.Web;

namespace NutricionWeb.Helpers
{
    public static class File
    {
        public const string FolderDefault = "~/Content/Imagenes/Personas";

        public static string Upload(HttpPostedFileBase file, string folder)
        {
            var path = string.Empty;
            var pic = string.Empty;

            if (file != null)
            {
                pic = Path.GetFileName(file.FileName);
                path = Path.Combine(HttpContext.Current.Server.MapPath(folder), pic);
                file.SaveAs(path);
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }

            return $"{folder}/{pic}";
        }
    }
}