namespace NNChallenge.iOS.Utils
{
    public class ImageLoaderCacheProxy : IImageLoader
    {
        private readonly IImageLoader _imageLoader;
        private readonly Dictionary<string, UIImage> _cache;

        public ImageLoaderCacheProxy(IImageLoader imageLoader)
        {
            _imageLoader = imageLoader;
            _cache = new Dictionary<string, UIImage>();
        }

        public async Task<UIImage> LoadAsync(string url, CancellationToken token)
        {
            if (!_cache.TryGetValue(url, out var image))
            {
                image = await _imageLoader.LoadAsync(url, token);
                _cache[url] = image;
            }

            return image;
        }
    }
}