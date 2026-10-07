namespace lab1;

public class Playlist
{
    // Внутреннее хранилище
    private Track[] tracks;

    // Количество реально добавленных треков
    private int count;

    // Конструктор
    public Playlist(int capacity = 10)
    {
        if (capacity <= 0)
            throw new ArgumentException("Размер плейлиста должен быть больше нуля.");

        tracks = new Track[capacity];
        count = 0;
    }

    // Добавление трека
    public void Add(Track track)
    {
        if (track == null)
            throw new ArgumentNullException(nameof(track), "Трек не может быть null.");

        if (count >= tracks.Length)
            throw new InvalidOperationException("Плейлист заполнен.");

        tracks[count] = track;
        count++;
    }

    // Удаление трека по номеру
    public bool RemoveByNumber(int number)
    {
        for (int i = 0; i < count; i++)
        {
            if (tracks[i].Number == number)
            {
                // Сдвигаем все элементы после удаляемого
                for (int j = i; j < count - 1; j++)
                {
                    tracks[j] = tracks[j + 1];
                }

                
                count--;

                return true;
            }
        }

        return false;
    }

    // Суммарная длительность всех треков
    public int GetTotalDuration()
    {
        int total = 0;

        for (int i = 0; i < count; i++)
        {
            total += tracks[i].Duration;
        }

        return total;
    }

    // Поиск самого популярного трека
    public Track? GetMostPopular()
    {
        if (count == 0)
            return null;

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

    // Сортировка по количеству прослушиваний
    public void SortByPlays()
    {
        for (int i = 0; i < count - 1; i++)
        {
            for (int j = 0; j < count - 1 - i; j++)
            {
                if (tracks[j].Plays > tracks[j + 1].Plays)
                {
                    Track temp = tracks[j];
                    tracks[j] = tracks[j + 1];
                    tracks[j + 1] = temp;
                }
            }
        }
    }

    // Получение копии массива треков
    public Track[] GetTracks()
    {
        Track[] result = new Track[count];

        for (int i = 0; i < count; i++)
        {
            result[i] = tracks[i];
        }

        return result;
    }
}