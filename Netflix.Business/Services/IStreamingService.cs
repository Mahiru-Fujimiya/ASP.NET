namespace Netflix.Business.Services;

public interface IStreamingService
{
    string GetVideoStreamUrl(int episodeId); // Lấy URL streaming [cite: 498]
}