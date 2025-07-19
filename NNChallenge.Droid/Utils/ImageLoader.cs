using Android.Graphics;
using NNChallenge.Utils;

namespace NNChallenge.Droid.Utils
{
    public class ImageLoader : IImageLoader<Bitmap>
    {
        public async Task<Bitmap> LoadAsync(string url, CancellationToken token)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var bytes = await httpClient.GetByteArrayAsync(url, token);

                    var bitmap = await BitmapFactory.DecodeByteArrayAsync(bytes, 0, bytes.Length);
                    if (bitmap == null)
                    {
                        throw new Exception("img not loaded");
                    }

                    return bitmap;
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