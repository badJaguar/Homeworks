using System;
using MediaData;

namespace Mediaplayer
{
    public static class CallerStorage
    {
        private static readonly IMedia _media;

        public static void ChangeFormatCaller()
        {
            var songsCollection = new[] { "Song1", "Song2" };
            Console.WriteLine("Please chose a song for format changes: ");
            Console.WriteLine($"{string.Join("\n", songsCollection)}\n{new string('-', 70)}");
            var mySong = Console.ReadLine();

            if (mySong != null && mySong.Contains(songsCollection[0]))
                Manipulations.FormatChange($"{string.Format(songsCollection[0])}");

            else if (mySong != null && mySong.Contains(songsCollection[1]))
                Manipulations.FormatChange($"{string.Format(songsCollection[1])}");

            else
                Console.WriteLine(mySong is null ? $"You have no song as {mySong}" : $"Please, type a song name.");
        }
    }
}