namespace Lab2;

// КЛАСС DURATION

public class Duration
{
    public const int MIN_SECONDS = 1;
    public const int MAX_SECONDS = 3600;

    public int Seconds { get; }

    public Duration(int seconds)
    {
        if (seconds < MIN_SECONDS || seconds > MAX_SECONDS)
        {
            throw new ArgumentException(
                "Длительность должна быть от 1 до 3600 секунд."
            );
        }

        Seconds = seconds;
    }

    public override string ToString()
    {
        int minutes = Seconds / 60;
        int seconds = Seconds % 60;

        return $"{minutes} мин. {seconds} сек.";
    }
}


// TRACK

public class Track : MediaItem
{
    private static int tracksCount = 0;

    private readonly int number;
    private readonly string artist;

    // КОМПОЗИЦИЯ:
    // Duration создаётся внутри Track.
    private readonly Duration duration;

    // Защитное копирование массива жанров.
    private readonly string[] genres;

    public Track(
        int number,
        string title,
        string artist,
        int duration,
        string[] genres)
        : base(title, artist)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException(
                "Название трека не может быть пустым."
            );
        }

        if (string.IsNullOrWhiteSpace(artist))
        {
            throw new ArgumentException(
                "Исполнитель не может быть пустым."
            );
        }

        if (genres == null)
        {
            throw new ArgumentNullException(
                nameof(genres)
            );
        }

        this.number = number;
        this.artist = artist;

        // Duration создаётся самим Track.
        this.duration = new Duration(duration);

        // Защитная копия.
        this.genres = genres.ToArray();

        tracksCount++;
    }

    public Track(
        int number,
        string title,
        string artist,
        int duration)
        : this(
            number,
            title,
            artist,
            duration,
            new[] { "Не указан" })
    {
    }

    public Track(
        int number,
        string title,
        string artist)
        : this(
            number,
            title,
            artist,
            180,
            new[] { "Не указан" })
    {
    }

    public int Number
    {
        get { return number; }
    }

    public string Artist
    {
        get { return artist; }
    }

    public int Duration
    {
        get { return duration.Seconds; }
    }

    public string[] GetGenres()
    {
        return genres.ToArray();
    }

    public static int GetTracksCount()
    {
        return tracksCount;
    }

    public override void Play()
    {
        AddPlay();

        Console.WriteLine(
            $"Воспроизводится трек: {Title} - {Artist}"
        );
    }

    public void Play(int times)
    {
        if (times <= 0)
        {
            throw new ArgumentException(
                "Количество прослушиваний должно быть больше нуля."
            );
        }

        Plays += times;
    }

    public override string ToString()
    {
        string genreText = string.Join(", ", genres);

        return
            $"№{number}: {Title} - {Artist}, " +
            $"длительность: {duration}, " +
            $"прослушиваний: {Plays}, " +
            $"жанры: {genreText}";
    }
}