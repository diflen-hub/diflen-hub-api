using domain.Entities;

namespace domain.Interfaces.Services
{
    public interface IPlaylistService
    {
        public Playlist ScrapVideos(string playlistUrl);
    }
}