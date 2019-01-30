using System;

namespace Mediaplayer
{
    public static class CallerStorage
    {
        /// <summary>
        /// Represents a change format act.
        /// </summary>
        public static void ChangeFormatCaller()
        {
            var songsCollection = new[] { "Song1", "Song2" };
            Console.WriteLine("Please chose a song for format changes: ");
            Console.WriteLine($"{string.Join("\n", songsCollection)}\n{new string('-', 70)}");
            var mySong = Console.ReadLine();

            if (mySong?.Contains(songsCollection[0]) == true)
                Manipulations.FormatChange($"{string.Format(songsCollection[0])}");
            else if (mySong?.Contains(songsCollection[1]) == true)
                Manipulations.FormatChange($"{string.Format(songsCollection[1])}");
            else
                Console.WriteLine($"Please, type a song that exists in songs list.");
        }
    }
}