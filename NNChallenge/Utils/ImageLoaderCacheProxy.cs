namespace NNChallenge.Utils
{
    public class ImageLoaderCacheProxy<TImage> : IImageLoader<TImage> where TImage : class
    {
        private readonly IImageLoader<TImage> _imageLoader;
        private readonly Dictionary<string, TImage> _cache;

        public ImageLoaderCacheProxy(IImageLoader<TImage> imageLoader)
        {
            _imageLoader = imageLoader;
            _cache = new Dictionary<string, TImage>();
        }

        public async Task<TImage> LoadAsync(string url, CancellationToken token)
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