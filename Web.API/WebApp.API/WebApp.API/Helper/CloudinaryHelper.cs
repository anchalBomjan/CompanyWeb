namespace WebApp.API.Helper
{
    public class CloudinaryHelper
    {
        public static string ExtractPublicId(string imageUrl)
        {
            // Parse the URL to extract the public ID
            var uri = new Uri(imageUrl);
            var segments = uri.Segments;

            // Combine the last two segments (e.g., folder_name/filename)
            var publicId = string.Join("", segments.Skip(segments.Length - 2));

            // Remove the file extension
            publicId = Path.GetFileNameWithoutExtension(publicId);

            return publicId;
        }
        }
}
