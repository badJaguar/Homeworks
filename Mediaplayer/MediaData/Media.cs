using System;
using System.Collections.Generic;

namespace MediaData
{
    public abstract class Media : IMedia
    {
        /// <inheritdoc />
        public virtual string[] Songs { get; set; } = {"Song1", "Song2"};

        /// <inheritdoc />
        public string[] Videos { get; set; }

        /// <summary>
        /// Plays music.
        /// </summary>
        public abstract void PlayMusic();

        // TODO: Liskov substitution principal violated.
        /// <summary>
        /// Plays video.
        /// </summary>
        /// <param name="videoName"></param>
        public abstract void PlayVideo(string videoName);
    }
}