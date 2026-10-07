namespace lab1;

public class Track
{
    // СТАТИЧЕСКИЕ ПОЛЯ
    // Счётчик созданных треков
    private static int tracksCount = 0;

    // Максимальная длительность трека
    public const int MAX_DURATION = 3600;


    // ПОЛЯ КЛАССА
    private int number;
    private string title;
    private string artist;
    private int duration;
    private int plays;

    // Массив жанров
    private string[] genres;


    // ОСНОВНОЙ КОНСТРУКТОР

    public Track(
        int number,
        string title,
        string artist,
        int duration,
        string[] genres)
    {
        // Проверяем название
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException(
                "Название трека не может быть пустым."
            );
        }

        // Проверяем длительность
        if (duration < 1 || duration > MAX_DURATION)
        {
            throw new ArgumentException(
                "Длительность должна быть от 1 до 3600 секунд."
            );
        }

        // Проверяем массив жанров
        if (genres == null)
        {
            throw new ArgumentNullException(
                nameof(genres),
                "Массив жанров не может быть null."
            );
        }

        // Сохраняем данные
        this.number = number;
        this.title = title;
        this.artist = artist;
        this.duration = duration;

        // сохраняем КОПИЮ массива, а не сам массив
        this.genres = genres.ToArray();

        // При создании трека прослушиваний нет
        this.plays = 0;

        // Увеличиваем статический счётчик
        tracksCount++;
    }


    // КОНСТРУКТОР БЕЗ ЖАНРОВ

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
            new string[] { "Не указан" })
    {
    }

    // УПРОЩЁННЫЙ КОНСТРУКТОР

    public Track(
        int number,
        string title,
        string artist)
        : this(
            number,
            title,
            artist,
            180,
            new string[] { "Не указан" })
    {
    }


    // СВОЙСТВА

    public int Number
    {
        get { return number; }
    }

    public string Title
    {
        get { return title; }
    }

    public string Artist
    {
        get { return artist; }
    }

    public int Duration
    {
        get { return duration; }
    }

    public int Plays
    {
        get { return plays; }
    }


    // ПОЛУЧЕНИЕ ЖАНРОВ

    public string[] GetGenres()
    {
        // Возвращаем КОПИЮ массива.
        // Внешний код не сможет изменить
        // внутренний массив объекта Track.
        return genres.ToArray();
    }


    // СТАТИЧЕСКИЙ МЕТОД

    public static int GetTracksCount()
    {
        return tracksCount;
    }


    // ПРОСЛУШИВАНИЕ ТРЕКА

    public void Play()
    {
        Play(1);
    }


    public void Play(int times)
    {
        if (times <= 0)
        {
            throw new ArgumentException(
                "Количество прослушиваний должно быть больше нуля."
            );
        }

        plays += times;
    }


    // TO STRING

    public override string ToString()
    {
        string genreText = string.Join(", ", genres);

        return
            $"№{number}: {title} - {artist}, " +
            $"длительность: {duration} сек., " +
            $"прослушиваний: {plays}, " +
            $"жанры: {genreText}";
    }
}