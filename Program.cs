namespace Lab2;

class Program
{
    static Playlist playlist = new Playlist();

    static void Main()
    {
        bool running = true;

        while (running)
        {
            Console.Clear();

            Console.WriteLine("=================================");
            Console.WriteLine("        МУЗЫКАЛЬНЫЙ ПЛЕЙЛИСТ");
            Console.WriteLine("=================================");
            Console.WriteLine("1. Добавить трек");
            Console.WriteLine("2. Показать все треки");
            Console.WriteLine("3. Прослушать трек");
            Console.WriteLine("4. Удалить трек");
            Console.WriteLine("5. Самый популярный трек");
            Console.WriteLine("6. Общая длительность");
            Console.WriteLine("7. Сортировка по популярности");
            Console.WriteLine("8. Статистика");
            Console.WriteLine("9. Проверка защиты жанров");
            Console.WriteLine("10. Правила выбора SelectionRule");
            Console.WriteLine("11. Полиморфизм MediaItem");
            Console.WriteLine("12. Композиция");
            Console.WriteLine("0. Выход");
            Console.WriteLine("=================================");

            int choice = ReadInt("Выберите действие: ");

            Console.Clear();

            switch (choice)
            {
                case 1:
                    AddTrack();
                    break;

                case 2:
                    ShowTracks();
                    break;

                case 3:
                    PlayTrack();
                    break;

                case 4:
                    RemoveTrack();
                    break;

                case 5:
                    MostPopular();
                    break;

                case 6:
                    TotalDuration();
                    break;

                case 7:
                    SortByPlays();
                    break;

                case 8:
                    Statistics();
                    break;

                case 9:
                    GenresProtection();
                    break;

                case 10:
                    DemonstrateSelectionRules();
                    break;

                case 11:
                    DemonstrateMediaItems();
                    break;

                case 12:
                    DemonstrateComposition();
                    break;

                case 0:
                    running = false;
                    continue;

                default:
                    Console.WriteLine("Такого пункта меню нет.");
                    break;
            }

            Pause();
        }
    }


    // ==================== ДОБАВЛЕНИЕ ТРЕКА ====================

    static void AddTrack()
    {
        Console.WriteLine("=== Добавление трека ===");

        int number = ReadInt("Номер трека: ");

        Console.Write("Название: ");
        string title = Console.ReadLine() ?? "";

        Console.Write("Исполнитель: ");
        string artist = Console.ReadLine() ?? "";

        int duration = ReadInt(
            "Длительность в секундах: ");

        Console.Write(
            "Жанры через запятую: ");

        string genresInput =
            Console.ReadLine() ?? "";

        string[] genres = genresInput
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(g => g.Trim())
            .ToArray();

        if (genres.Length == 0)
        {
            genres = new[] { "Не указан" };
        }

        try
        {
            Track track = new Track(
                number,
                title,
                artist,
                duration,
                genres);

            playlist.Add(track);

            Console.WriteLine(
                "Трек успешно добавлен.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"Ошибка: {ex.Message}");
        }
    }


    // ==================== ПОКАЗ ТРЕКОВ ====================

    static void ShowTracks()
    {
        Console.WriteLine("=== Все треки ===");

        Track[] tracks = playlist.GetTracks();

        if (tracks.Length == 0)
        {
            Console.WriteLine("Плейлист пуст.");
            return;
        }

        foreach (Track track in tracks)
        {
            Console.WriteLine(track);
        }
    }


    // ==================== ПРОСЛУШИВАНИЕ ====================

    static void PlayTrack()
    {
        Console.WriteLine("=== Прослушивание трека ===");

        int number = ReadInt(
            "Введите номер трека: ");

        Track[] tracks = playlist.GetTracks();

        foreach (Track track in tracks)
        {
            if (track.Number == number)
            {
                track.Play();

                Console.WriteLine(
                    "Трек прослушан.");

                return;
            }
        }

        Console.WriteLine(
            "Трек не найден.");
    }


    // ==================== УДАЛЕНИЕ ====================

    static void RemoveTrack()
    {
        Console.WriteLine("=== Удаление трека ===");

        int number = ReadInt(
            "Введите номер трека: ");

        if (playlist.RemoveByNumber(number))
        {
            Console.WriteLine(
                "Трек удалён.");
        }
        else
        {
            Console.WriteLine(
                "Трек не найден.");
        }
    }


    // ==================== ПОПУЛЯРНЫЙ ====================

    static void MostPopular()
    {
        Console.WriteLine(
            "=== Самый популярный трек ===");

        Track? track =
            playlist.GetMostPopular();

        if (track == null)
        {
            Console.WriteLine(
                "Плейлист пуст.");
            return;
        }

        Console.WriteLine(track);
    }


    // ==================== ДЛИТЕЛЬНОСТЬ ====================

    static void TotalDuration()
    {
        Console.WriteLine(
            "=== Общая длительность ===");

        int total =
            playlist.GetTotalDuration();

        int minutes = total / 60;
        int seconds = total % 60;

        Console.WriteLine(
            $"Общая длительность: " +
            $"{minutes} мин. {seconds} сек.");
    }


    // ==================== СОРТИРОВКА ====================

    static void SortByPlays()
    {
        Console.WriteLine(
            "=== Сортировка по популярности ===");

        playlist.SortByPlays();

        Console.WriteLine(
            "Треки отсортированы.");

        ShowTracks();
    }


    // ==================== СТАТИСТИКА ====================

    static void Statistics()
    {
        Console.WriteLine(
            "=== Статистика ===");

        Console.WriteLine(
            $"Количество треков: {playlist.Count}");

        Console.WriteLine(
            $"Всего создано треков: " +
            $"{Track.GetTracksCount()}");

        Track? popular =
            playlist.GetMostPopular();

        if (popular != null)
        {
            Console.WriteLine(
                $"Самый популярный: {popular.Title}");

            Console.WriteLine(
                $"Прослушиваний: {popular.Plays}");
        }
    }


    // ==================== ЗАЩИТА ЖАНРОВ ====================

    static void GenresProtection()
    {
        Console.WriteLine(
            "=== Проверка защиты массива жанров ===");

        Track[] tracks =
            playlist.GetTracks();

        if (tracks.Length == 0)
        {
            Console.WriteLine(
                "Плейлист пуст.");
            return;
        }

        Track track = tracks[0];

        string[] genres =
            track.GetGenres();

        Console.WriteLine(
            "Жанры до изменения:");

        Console.WriteLine(
            string.Join(", ", track.GetGenres()));

        if (genres.Length > 0)
        {
            genres[0] = "ИЗМЕНЕНО СНАРУЖИ";
        }

        Console.WriteLine(
            "Жанры внутри объекта:");

        Console.WriteLine(
            string.Join(", ", track.GetGenres()));

        Console.WriteLine(
            "Внутренний массив не изменился.");
    }


    // ==================== SELECTION RULE ====================

    static void DemonstrateSelectionRules()
    {
        Console.WriteLine(
            "=== SelectionRule ===");

        Track[] tracks =
            playlist.GetTracks();

        if (tracks.Length == 0)
        {
            Console.WriteLine(
                "Плейлист пуст.");
            return;
        }

        Console.WriteLine(
            "1. По порядку");

        Console.WriteLine(
            "2. По популярности");

        Console.WriteLine(
            "3. Случайный выбор без повторов");

        int choice =
            ReadInt("Выберите правило: ");

        SelectionRule? rule = null;

        switch (choice)
        {
            case 1:
                rule = new OrderSelectionRule();
                break;

            case 2:
                rule = new PopularitySelectionRule();
                break;

            case 3:
                rule = new RandomSelectionRule();
                break;

            default:
                Console.WriteLine(
                    "Неверный выбор.");
                return;
        }

        Track[] result =
            rule.Select(tracks);

        Console.WriteLine(
            "\nРезультат:");

        foreach (Track track in result)
        {
            Console.WriteLine(track);
        }
    }


    // ==================== MEDIA ITEM ====================

    static void DemonstrateMediaItems()
    {
        Console.WriteLine(
            "=== Полиморфизм MediaItem ===");

        MediaItem[] items =
        {
            new Track(
                100,
                "Тестовый трек",
                "Исполнитель",
                200,
                new[] { "Rock" }),

            new Podcast(
                "Наука сегодня",
                "Автор подкаста",
                5),

            new Audiobook(
                "Война и мир",
                "Лев Толстой",
                120)
        };

        foreach (MediaItem item in items)
        {
            item.Play();

            Console.WriteLine(item);
            Console.WriteLine();
        }
    }


    // ==================== КОМПОЗИЦИЯ ====================

    static void DemonstrateComposition()
    {
        Console.WriteLine(
            "=== Композиция ===");

        MediaCarrier carrier =
            new MediaCarrier("Цифровой файл");

        AccessLevel access =
            new AccessLevel("Premium");

        AgeRating rating =
            new AgeRating(16);

        PremiumMediaItem item =
            new PremiumMediaItem(
                "Премиум-контент",
                "Автор",
                carrier,
                access,
                rating);

        item.Play();

        Console.WriteLine(item);

        Console.WriteLine();
        Console.WriteLine(
            "Объект содержит отдельные компоненты:");
        Console.WriteLine(
            $"- Носитель: {carrier}");
        Console.WriteLine(
            $"- Уровень доступа: {access}");
        Console.WriteLine(
            $"- Возрастная маркировка: {rating}");
    }


    // ==================== ПОИСК ====================

    static void FindTrack()
    {
        Console.WriteLine(
            "=== Поиск трека ===");

        Console.Write(
            "Введите название: ");

        string search =
            Console.ReadLine() ?? "";

        Track[] tracks =
            playlist.GetTracks();

        bool found = false;

        foreach (Track track in tracks)
        {
            if (track.Title
                .Contains(
                    search,
                    StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(track);
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine(
                "Треки не найдены.");
        }
    }


    // ==================== ВВОД ЧИСЛА ====================

    static int ReadInt(string message)
    {
        while (true)
        {
            Console.Write(message);

            string input =
                Console.ReadLine() ?? "";

            if (int.TryParse(input, out int value))
            {
                return value;
            }

            Console.WriteLine(
                "Введите целое число.");
        }
    }


    // ==================== ПАУЗА ====================

    static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine(
            "Нажмите Enter для продолжения...");

        Console.ReadLine();
    }
}