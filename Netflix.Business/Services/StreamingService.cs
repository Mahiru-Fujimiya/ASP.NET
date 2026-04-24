namespace Netflix.Business.Services;

public class StreamingService : IStreamingService
{
 public string GetVideoStreamUrl(int episodeId) => "https://cdn.movie.com/video.m3u8";
}