namespace MilsatIMS.Common
{
    public class Utils
    {
        public static string GetUserPicture(string storepath, string fileName)
        {
            var filePath = Path.Combine(storepath, fileName);
            if (!File.Exists(filePath))
            {
                return String.Empty;
            }
            byte[] contents = File.ReadAllBytes(filePath);
            string image = Convert.ToBase64String(contents);
            return image;
        }
    }
}
