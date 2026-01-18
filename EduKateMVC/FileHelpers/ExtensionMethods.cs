using System.Threading.Tasks;

namespace EduKateMVC.FileHelpers
{
    public static class ExtensionMethods
    {
        public static bool CheckSize(this IFormFile file, int mb) {

            return file.Length > mb * 2 * 1024 * 1024;
        }
        public static bool CheckType(this IFormFile file, string type) {

            return file.ContentType.Contains(type);
        }
        public static async Task<string> FileUploadAsync(this IFormFile file,string folderPath) {
            string uniqueName = Guid.NewGuid().ToString() + file.FileName;

            string path = Path.Combine(folderPath,uniqueName);

            using FileStream stream = new FileStream(path,FileMode.Create);

            await file.CopyToAsync(stream);
            return uniqueName;
        
        }
        public static void Delete(string file) {
            if (File.Exists(file)) {
                File.Delete(file);
            }
        
        }
    }
}
