namespace Lab2;

public abstract class MediaItem
{
    public string Title { get; protected set; }
    public string Author { get; protected set; }
    public int Plays { get; protected set; }

    protected MediaItem(string title, string author)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Название не может быть пустым.");

        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException("Автор не может быть пустым.");

        Title = title;
        Author = author;
        Plays = 0;
    }

    public abstract void Play();

    public void AddPlay()
    {
        Plays++;
    }

    public override string ToString()
    {
        return $"Название: {Title}, Автор: {Author}, прослушиваний: {Plays}";
    }
}


// PODCAST 

public class Podcast : MediaItem
{
    private readonly int episodeNumber;

    public Podcast(string title, string author, int episodeNumber)
        : base(title, author)
    {
        if (episodeNumber <= 0)
            throw new ArgumentException(
                "Номер выпуска должен быть больше нуля.");

        this.episodeNumber = episodeNumber;
    }

    public int EpisodeNumber => episodeNumber;

    public override void Play()
    {
        AddPlay();

        Console.WriteLine(
            $"Воспроизводится подкаст: {Title}, выпуск №{episodeNumber}");
    }

    public override string ToString()
    {
        return $"Подкаст: {Title} - {Author}, " +
               $"выпуск №{episodeNumber}, " +
               $"прослушиваний: {Plays}";
    }
}


// AUDIOBOOK 

public class Audiobook : MediaItem
{
    private int bookmark;

    public Audiobook(
        string title,
        string author,
        int bookmark = 0)
        : base(title, author)
    {
        if (bookmark < 0)
            throw new ArgumentException(
                "Закладка не может быть отрицательной.");

        this.bookmark = bookmark;
    }

    public int Bookmark => bookmark;

    public void SetBookmark(int position)
    {
        if (position < 0)
            throw new ArgumentException(
                "Позиция закладки не может быть отрицательной.");

        bookmark = position;
    }

    public override void Play()
    {
        AddPlay();

        Console.WriteLine(
            $"Воспроизводится аудиокнига: {Title}, " +
            $"продолжение с {bookmark} сек.");
    }

    public override string ToString()
    {
        return $"Аудиокнига: {Title} - {Author}, " +
               $"закладка: {bookmark} сек., " +
               $"прослушиваний: {Plays}";
    }
}


// SELECTION RULE 

public interface SelectionRule
{
    Track[] Select(Track[] tracks);
}


// Выбор по порядку
public class OrderSelectionRule : SelectionRule
{
    public Track[] Select(Track[] tracks)
    {
        if (tracks == null)
            throw new ArgumentNullException(nameof(tracks));

        return tracks.ToArray();
    }
}


// Выбор по популярности
public class PopularitySelectionRule : SelectionRule
{
    public Track[] Select(Track[] tracks)
    {
        if (tracks == null)
            throw new ArgumentNullException(nameof(tracks));

        Track[] result = tracks.ToArray();

        Array.Sort(
            result,
            (a, b) => b.Plays.CompareTo(a.Plays));

        return result;
    }
}


// Случайный выбор без повторов
public class RandomSelectionRule : SelectionRule
{
    private readonly Random random = new Random();

    public Track[] Select(Track[] tracks)
    {
        if (tracks == null)
            throw new ArgumentNullException(nameof(tracks));

        Track[] result = tracks.ToArray();

        for (int i = result.Length - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);

            Track temp = result[i];
            result[i] = result[j];
            result[j] = temp;
        }

        return result;
    }
}


//  COMPOSITION 

// Носитель
public class MediaCarrier
{
    public string Name { get; }

    public MediaCarrier(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Название носителя не может быть пустым.");

        Name = name;
    }

    public override string ToString()
    {
        return Name;
    }
}


// Уровень доступа
public class AccessLevel
{
    public string Name { get; }

    public AccessLevel(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Уровень доступа не может быть пустым.");

        Name = name;
    }

    public override string ToString()
    {
        return Name;
    }
}


// Возрастная маркировка
public class AgeRating
{
    public int Age { get; }

    public AgeRating(int age)
    {
        if (age < 0)
            throw new ArgumentException(
                "Возрастная маркировка не может быть отрицательной.");

        Age = age;
    }

    public override string ToString()
    {
        return $"{Age}+";
    }
}


// Элемент, использующий композицию
public class PremiumMediaItem : MediaItem
{
    private readonly MediaCarrier carrier;
    private readonly AccessLevel accessLevel;
    private readonly AgeRating ageRating;

    public PremiumMediaItem(
        string title,
        string author,
        MediaCarrier carrier,
        AccessLevel accessLevel,
        AgeRating ageRating)
        : base(title, author)
    {
        if (carrier == null)
            throw new ArgumentNullException(nameof(carrier));

        if (accessLevel == null)
            throw new ArgumentNullException(nameof(accessLevel));

        if (ageRating == null)
            throw new ArgumentNullException(nameof(ageRating));

        this.carrier = carrier;
        this.accessLevel = accessLevel;
        this.ageRating = ageRating;
    }

    public override void Play()
    {
        AddPlay();

        Console.WriteLine(
            $"Воспроизводится: {Title} - {Author}");
    }

    public override string ToString()
    {
        return $"Премиум-элемент: {Title} - {Author}, " +
               $"носитель: {carrier}, " +
               $"доступ: {accessLevel}, " +
               $"возраст: {ageRating}, " +
               $"прослушиваний: {Plays}";
    }
}