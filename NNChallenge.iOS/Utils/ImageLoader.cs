using NNChallenge.Utils;

namespace NNChallenge.iOS.Utils
{
    public class ImageLoader : IImageLoader<UIImage>
    {
        public async Task<UIImage> LoadAsync(string url, CancellationToken token)
        {
            try
            {
                using (var nsUrl = new NSUrl(url))
                using (var nsSession = NSUrlSession.SharedSession)
                {
                    var request = await nsSession.CreateDataTaskAsync(nsUrl);
                    if (request.Data == null)
                    {
                        throw new Exception("img not loaded");
                    }

                    var image = UIImage.LoadFromData(request.Data);
                    if (image == null)
                    {
                        throw new Exception("img not loaded");
                    }

                    return image;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}