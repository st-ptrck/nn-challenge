namespace NNChallenge.Utils
{
    public interface IImageLoader<TImage> where TImage : class
    {
        Task<TImage> LoadAsync(string url, CancellationToken token);
    }
}