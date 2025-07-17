namespace NNChallenge.iOS.Utils
{
    public interface IImageLoader
    {
        Task<UIImage> LoadAsync(string url, CancellationToken token);
    }
}