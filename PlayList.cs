namespace Lab2;

// PLAYLIST

public class Playlist
{
    // АГРЕГАЦИЯ:
    // Playlist хранит существующие Track.
    // Track создаются не здесь.
    private readonly Track[] tracks;

    private int count;

    public Playlist(int capacity = 100)
    {
        if (capacity <= 0)
        {
            throw new ArgumentException(
                "Размер плейлиста должен быть больше нуля."
            );
        }

        tracks = new Track[capacity];
        count = 0;
    }

    public void Add(Track track)
    {
        if (track == null)
        {
            throw new ArgumentNullException(
                nameof(track)
            );
        }

        if (count >= tracks.Length)
        {
            throw new InvalidOperationException(
                "Плейлист заполнен."
            );
        }

        // Track пришёл извне.
        tracks[count] = track;
        count++;
    }

    public bool RemoveByNumber(int number)
    {
        for (int i = 0; i < count; i++)
        {
            if (tracks[i].Number == number)
            {
                for (int j = i; j < count - 1; j++)
                {
                    tracks[j] = tracks[j + 1];
                }

                tracks[count - 1] = null!;
                count--;

                return true;
            }
        }

        return false;
    }

    public Track[] GetTracks()
    {
        Track[] result = new Track[count];

        for (int i = 0; i < count; i++)
        {
            result[i] = tracks[i];
        }

        return result;
    }

    public int GetTotalDuration()
    {
        int total = 0;

        for (int i = 0; i < count; i++)
        {
            total += tracks[i].Duration;
        }

        return total;
    }

    public Track? GetMostPopular()
    {
        if (count == 0)
        {
            return null;
        }

        Track mostPopular = tracks[0];

        for (int i = 1; i < count; i++)
        {
            if (tracks[i].Plays > mostPopular.Plays)
            {
                mostPopular = tracks[i];
            }
        }

        return mostPopular;
    }

    public void SortByPlays()
    {
        for (int i = 0; i < count - 1; i++)
        {
            for (int j = 0; j < count - i - 1; j++)
            {
                if (tracks[j].Plays < tracks[j + 1].Plays)
                {
                    Track temp = tracks[j];

                    tracks[j] = tracks[j + 1];
                    tracks[j + 1] = temp;
                }
            }
        }
    }

    public int Count
    {
        get { return count; }
    }
}