namespace lab1;

public class Program
{
    public static void Main()
    {
        Playlist playlist = new Playlist();

        bool running = true;

        while (running)
        {
            Console.Clear();

            Console.WriteLine("========================================");
            Console.WriteLine("       ЛАБОРАТОРНАЯ РАБОТА №1");
            Console.WriteLine("       ВАРИАНТ 15 - ТРЕК И ПЛЕЙЛИСТ");
            Console.WriteLine("========================================");
            Console.WriteLine();

            Console.WriteLine("1. Добавить трек");
            Console.WriteLine("2. Показать все треки");
            Console.WriteLine("3. Прослушать трек");
            Console.WriteLine("4. Удалить трек");
            Console.WriteLine("5. Показать самый популярный трек");
            Console.WriteLine("6. Показать суммарную длительность");
            Console.WriteLine("7. Отсортировать по прослушиваниям");
            Console.WriteLine("8. Показать статистику");
            Console.WriteLine("9. Проверить защитное копирование жанров");
            Console.WriteLine("0. Выход");

            Console.WriteLine();
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine() ?? "";

            Console.Clear();

            switch (choice)
            {
                case "1":
                    AddTrack(playlist);
                    break;

                case "2":
                    ShowTracks(playlist);
                    break;

                case "3":
                    PlayTrack(playlist);
                    break;

                case "4":
                    RemoveTrack(playlist);
                    break;

                case "5":
                    ShowMostPopular(playlist);
                    break;

                case "6":
                    ShowTotalDuration(playlist);
                    break;

                case "7":
                    SortTracks(playlist);
                    break;

                case "8":
                    ShowStatistics(playlist);
                    break;

                case "9":
                    TestGenresProtection(playlist);
                    break;

                case "0":
                    running = false;
                    Console.WriteLine("Программа завершена.");
                    break;

                default:
                    Console.WriteLine("Такого пункта меню нет.");
                    Pause();
                    break;
            }
        }
    }


    // ============================================================
    // 1. ДОБАВЛЕНИЕ ТРЕКА
    // ============================================================

    private static void AddTrack(Playlist playlist)
    {
        Console.WriteLine("========== ДОБАВЛЕНИЕ ТРЕКА ==========");
        Console.WriteLine();

        Console.Write("Введите номер трека: ");
        int number = ReadInt();

        Console.Write("Введите название: ");
        string title = Console.ReadLine() ?? "";

        Console.Write("Введите исполнителя: ");
        string artist = Console.ReadLine() ?? "";

        Console.Write("Введите длительность в секундах: ");
        int duration = ReadInt();

        Console.Write("Введите жанры через запятую: ");
        string genresInput = Console.ReadLine() ?? "";

        string[] genres = genresInput.Split(
            ',',
            StringSplitOptions.RemoveEmptyEntries
        );

        // Убираем пробелы в начале и конце каждого жанра
        for (int i = 0; i < genres.Length; i++)
        {
            genres[i] = genres[i].Trim();
        }

        try
        {
            Track track = new Track(
                number,
                title,
                artist,
                duration,
                genres
            );

            playlist.Add(track);

            Console.WriteLine();
            Console.WriteLine("Трек успешно добавлен!");
            Console.WriteLine();
            Console.WriteLine(track);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine();
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine();
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        Pause();
    }


    // ============================================================
    // 2. ПОКАЗАТЬ ВСЕ ТРЕКИ
    // ============================================================

    private static void ShowTracks(Playlist playlist)
    {
        Console.WriteLine("========== ВСЕ ТРЕКИ ==========");
        Console.WriteLine();

        Track[] tracks = playlist.GetTracks();

        if (tracks.Length == 0)
        {
            Console.WriteLine("Плейлист пуст.");
        }
        else
        {
            foreach (Track track in tracks)
            {
                Console.WriteLine(track);
            }
        }

        Pause();
    }


    // ============================================================
    // 3. ПРОСЛУШАТЬ ТРЕК
    // ============================================================

    private static void PlayTrack(Playlist playlist)
    {
        Console.WriteLine("========== ПРОСЛУШИВАНИЕ ==========");
        Console.WriteLine();

        Track[] tracks = playlist.GetTracks();

        if (tracks.Length == 0)
        {
            Console.WriteLine("Плейлист пуст.");
            Pause();
            return;
        }

        Console.Write("Введите номер трека: ");
        int number = ReadInt();

        Track? selectedTrack = FindTrack(playlist, number);

        if (selectedTrack == null)
        {
            Console.WriteLine("Трек с таким номером не найден.");
            Pause();
            return;
        }

        Console.Write("Сколько раз прослушать: ");
        int times = ReadInt();

        try
        {
            selectedTrack.Play(times);

            Console.WriteLine();
            Console.WriteLine("Прослушивание добавлено!");
            Console.WriteLine();
            Console.WriteLine(selectedTrack);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine();
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        Pause();
    }


    // ============================================================
    // 4. УДАЛЕНИЕ ТРЕКА
    // ============================================================

    private static void RemoveTrack(Playlist playlist)
    {
        Console.WriteLine("========== УДАЛЕНИЕ ТРЕКА ==========");
        Console.WriteLine();

        Console.Write("Введите номер трека: ");
        int number = ReadInt();

        bool removed = playlist.RemoveByNumber(number);

        if (removed)
        {
            Console.WriteLine("Трек успешно удалён.");
        }
        else
        {
            Console.WriteLine("Трек с таким номером не найден.");
        }

        Pause();
    }


    // ============================================================
    // 5. САМЫЙ ПОПУЛЯРНЫЙ ТРЕК
    // ============================================================

    private static void ShowMostPopular(Playlist playlist)
    {
        Console.WriteLine("========== САМЫЙ ПОПУЛЯРНЫЙ ==========");
        Console.WriteLine();

        Track? track = playlist.GetMostPopular();

        if (track == null)
        {
            Console.WriteLine("Плейлист пуст.");
        }
        else
        {
            Console.WriteLine(track);
        }

        Pause();
    }


    // ============================================================
    // 6. СУММАРНАЯ ДЛИТЕЛЬНОСТЬ
    // ============================================================

    private static void ShowTotalDuration(Playlist playlist)
    {
        Console.WriteLine("========== СУММАРНАЯ ДЛИТЕЛЬНОСТЬ ==========");
        Console.WriteLine();

        int total = playlist.GetTotalDuration();

        Console.WriteLine($"Общая длительность: {total} секунд.");

        // Дополнительно показываем минуты
        int minutes = total / 60;
        int seconds = total % 60;

        Console.WriteLine(
            $"Это примерно: {minutes} мин. {seconds} сек."
        );

        Pause();
    }


    // ============================================================
    // 7. СОРТИРОВКА
    // ============================================================

    private static void SortTracks(Playlist playlist)
    {
        Console.WriteLine("========== СОРТИРОВКА ==========");
        Console.WriteLine();

        playlist.SortByPlays();

        Console.WriteLine(
            "Треки отсортированы по количеству прослушиваний."
        );

        Console.WriteLine();

        Track[] tracks = playlist.GetTracks();

        if (tracks.Length == 0)
        {
            Console.WriteLine("Плейлист пуст.");
        }
        else
        {
            foreach (Track track in tracks)
            {
                Console.WriteLine(track);
            }
        }

        Pause();
    }


    // ============================================================
    // 8. СТАТИСТИКА
    // ============================================================

    private static void ShowStatistics(Playlist playlist)
    {
        Console.WriteLine("========== СТАТИСТИКА ==========");
        Console.WriteLine();

        Track[] tracks = playlist.GetTracks();

        Console.WriteLine(
            $"Количество треков в плейлисте: {tracks.Length}"
        );

        Console.WriteLine(
            $"Всего создано объектов Track: {Track.GetTracksCount()}"
        );

        Console.WriteLine(
            $"Суммарная длительность: " +
            $"{playlist.GetTotalDuration()} сек."
        );

        Track? popular = playlist.GetMostPopular();

        if (popular != null)
        {
            Console.WriteLine(
                $"Самый популярный: {popular.Title} " +
                $"({popular.Plays} прослушиваний)"
            );
        }
        else
        {
            Console.WriteLine("Самый популярный: нет данных");
        }

        Pause();
    }


    // ============================================================
    // 9. ПРОВЕРКА ЗАЩИТНОГО КОПИРОВАНИЯ
    // ============================================================

    private static void TestGenresProtection(Playlist playlist)
    {
        Console.WriteLine("========== ЗАЩИТА ЖАНРОВ ==========");
        Console.WriteLine();

        Track[] tracks = playlist.GetTracks();

        if (tracks.Length == 0)
        {
            Console.WriteLine("Сначала добавьте хотя бы один трек.");
            Pause();
            return;
        }

        Console.WriteLine("Выберите номер трека для проверки:");
        Console.Write("Номер: ");

        int number = ReadInt();

        Track? track = FindTrack(playlist, number);

        if (track == null)
        {
            Console.WriteLine("Трек не найден.");
            Pause();
            return;
        }

        string[] genresBefore = track.GetGenres();

        Console.WriteLine();
        Console.WriteLine("Жанры до изменения:");

        foreach (string genre in genresBefore)
        {
            Console.WriteLine($"- {genre}");
        }

        Console.WriteLine();

        if (genresBefore.Length > 0)
        {
            Console.WriteLine(
                "Попытаемся изменить первый жанр через массив, " +
                "полученный геттером."
            );

            genresBefore[0] = "ИЗМЕНЁННЫЙ ЖАНР";
        }

        string[] genresAfter = track.GetGenres();

        Console.WriteLine();
        Console.WriteLine("Жанры внутри Track после изменения:");

        foreach (string genre in genresAfter)
        {
            Console.WriteLine($"- {genre}");
        }

        Console.WriteLine();
        Console.WriteLine(
            "Если первый жанр остался прежним, " +
            "защитное копирование работает правильно."
        );

        Pause();
    }


    // ============================================================
    // ПОИСК ТРЕКА
    // ============================================================

    private static Track? FindTrack(
        Playlist playlist,
        int number)
    {
        Track[] tracks = playlist.GetTracks();

        foreach (Track track in tracks)
        {
            if (track.Number == number)
            {
                return track;
            }
        }

        return null;
    }


    // ============================================================
    // ВВОД ЦЕЛОГО ЧИСЛА
    // ============================================================

    private static int ReadInt()
    {
        while (true)
        {
            string input = Console.ReadLine() ?? "";

            if (int.TryParse(input, out int value))
            {
                return value;
            }

            Console.Write("Ошибка. Введите целое число: ");
        }
    }


    // ============================================================
    // ПАУЗА
    // ============================================================

    private static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Нажмите Enter, чтобы продолжить...");
        Console.ReadLine();
    }
}