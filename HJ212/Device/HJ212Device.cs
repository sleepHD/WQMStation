namespace WQMStation.HJ212.Device
{
    using IO;
    using WQMStation.IO;
    using System;

    public abstract class HJ212Device : IDisposable
    {
        internal HJ212Transport _transport;

        internal HJ212Device(IStreamResource streamResource)
        {
            _transport = new IO.HJ212Transport(streamResource);
        }
        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                DisposableUtility.Dispose(ref _transport);
        }
    }
}
