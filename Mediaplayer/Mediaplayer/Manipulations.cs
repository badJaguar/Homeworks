using MediaData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;

namespace Mediaplayer
{
    // TODO: Open/Closed principal violated. Also, I want to play not only music but videos too.
    // This class not applicable to play video. We can resolve this problem with a help of using abstractions. 
    // We would have opportunity to create two sequences (AudioManipulations, VideoManipulations)
    // by realization of IManipulations interface.
    public class Manipulations : Media
    {
        public override string[] Songs { get; set; }

        /// <inheritdoc />
        public override void PlayMusic()
        {
            var songsCollection = Songs = new[] {"Song1", "Song2"};
            Console.WriteLine($"{songsCollection[0]} plays");
        }

        /// <inheritdoc />
        public override void PlayVideo(string videoName) => throw new NotImplementedException();

        /// <summary>
        /// Stops the playing music.
        /// </summary>
        public static void Stop() => Console.WriteLine("Music stopped.");

        /// <summary>
        /// Switches to next song.
        /// </summary>
        public static void ToNext() => Console.WriteLine("Switched to next music.");

        /// <summary>
        /// Switches to previous song.
        /// </summary>
        public static void ToPrevious() => Console.WriteLine("Switched to previous music.");

        // TODO: Single responsibility principal violated.
        /// <summary>
        /// Changes file format.
        /// </summary>
        /// <param name="fileName">One or lots of input files.</param>
        public static void FormatChange(string fileName)
        {
            var formats = new FormatType
            {
                Format = new[] { ".mp3", ".flac" }
            };

            var oldFormat = string.Join("\n", formats.Format.Last());
            var newFormat = string.Join("\n", formats.Format.First());

            if (fileName != null)
                Console.WriteLine($"\"{string.Format(fileName)}{oldFormat}\" " +
                                  $"format changed to \"{string.Format(fileName)}{newFormat}\"");
        }
    }
}