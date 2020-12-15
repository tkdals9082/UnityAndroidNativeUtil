namespace KSM.Android.Utility
{
    public static class TTSStatus
    {
        /// <summary>
        /// Denotes a successful operation.
        /// </summary>
        public const int SUCCESS = 0;

        /// <summary>
        /// Denotes a stop requested by a client. It's used only on the service side of the API,
        /// client should never expect to see this result code.
        /// </summary>
        public const int ERROR = -1;

        /// <summary>
        /// Denotes a stop requested by a client. It's used only on the service side of the API,
        /// client should never expect to see this result code.
        /// </summary>
        public const int STOPPED = -2;

        /// <summary>
        /// Denotes a failure of a TTS engine to synthesize the given input.
        /// </summary>
        public const int ERROR_SYNTHESIS = -3;

        /// <summary>
        /// Denotes a failure of a TTS service.
        /// </summary>
        public const int ERROR_SERVICE = -4;

        /// <summary>
        /// Denotes a failure related to the output (audio device or a file).
        /// </summary>
        public const int ERROR_OUTPUT = -5;

        /// <summary>
        /// Denotes a failure caused by a network connectivity problems.
        /// </summary>
        public const int ERROR_NETWORK = -6;

        /// <summary>
        /// Denotes a failure caused by network timeout.
        /// </summary>
        public const int ERROR_NETWORK_TIMEOUT = -7;

        /// <summary>
        /// Denotes a failure caused by an invalid request.
        /// </summary>
        public const int ERROR_INVALID_REQUEST = -8;

        /// <summary>
        /// Denotes a failure caused by an unfinished download of the voice data.
        /// </summary>
        public const int ERROR_NOT_INTSALLED_YET = -9;
    }
    public static class QueueMode
    {
        /// <summary>
        /// Queue mode where all entries in the playback queue (media to be played
        /// and text to be synthesized) are dropped and replaced by the new entry.
        /// Queues are flushed with respect to a given calling app.Entries in the queue
        /// from other callees are not discarded.
        /// </summary>
        public const int QUEUE_FLUSH = 0;

        /// <summary>
        /// Queue mode where the new entry is added at the end of the playback queue.
        /// </summary>
        public const int QUEUE_ADD = 1;

        /// <summary>
        /// Queue mode where the entire playback queue is purged. This is different
        /// from QUEUE_FLUSH in that all entries are purged, not just entries
        /// from a given caller.
        /// </summary>
        public const int QUEUE_DESTROY = 2;
    }
}