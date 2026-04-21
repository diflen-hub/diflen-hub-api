using domain.Entities;

namespace domain.tests.Entities;

public class PlaylistTests
{
    [Fact]
    public void Videos_DefaultsToEmptyList()
    {
        var playlist = new Playlist { Url = "https://example.com", Title = "My Playlist" };
        Assert.Empty(playlist.Videos);
    }

    [Fact]
    public void Videos_CanAddVideos()
    {
        var playlist = new Playlist
        {
            Url = "https://example.com",
            Title = "My Playlist",
            Videos =
            [
                new Playlist.Video { Title = "Intro", Url = "https://example.com/1" },
                new Playlist.Video { Title = "Part 2", Url = "https://example.com/2" }
            ]
        };

        Assert.Equal(2, playlist.Videos.Count);
    }

    [Fact]
    public void Video_Description_IsOptional()
    {
        var video = new Playlist.Video { Title = "Intro", Url = "https://example.com/1" };
        Assert.Null(video.Description);
    }

    [Fact]
    public void Playlist_Description_IsOptional()
    {
        var playlist = new Playlist { Url = "https://example.com", Title = "My Playlist" };
        Assert.Null(playlist.Description);
    }
}
