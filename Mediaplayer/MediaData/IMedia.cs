namespace MediaData
{
    // TODO: Interface segregation principal violated.
    public interface IMedia
    {
        /// <summary>
        /// Gets or sets an array of songs.
        /// </summary>
        string[] Songs { get; set; }

        /// <summary>
        /// Gets or sets an array of videos.
        /// </summary>
        string[] Videos { get; set; }
    }
}