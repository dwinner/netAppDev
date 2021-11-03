using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MultiStudio.VsCodeInstallWizard.Mvvm;

namespace MultiStudio.VsCodeInstallWizard.Model
{
    public sealed class DownloadModel
    {
        private const int DownloadСhunk = 0x1000;

        public DownloadModel(string targetFileName)
        {
            if (File.Exists(targetFileName))
            {
                File.Delete(targetFileName);
            }

            TargetFileName = targetFileName;
        }

        public string TargetFileName { get; }

        public event EventHandler<EventArgs<int>> PercentageChanged;

        public async Task GetHttpFileAsync(string path, CancellationToken cancelToken)
        {
            using (var client = new HttpClient())
            using (var responseMsg = await client.GetAsync(path, HttpCompletionOption.ResponseHeadersRead, cancelToken)
                .ConfigureAwait(false))
            {
                var total = responseMsg.Content.Headers.ContentLength ?? -1L;
                var canReportsProgress = total != -1;

                using (var httpStream = await responseMsg.Content.ReadAsStreamAsync().ConfigureAwait(false))
                using (var targetStream = File.OpenWrite(TargetFileName))
                {
                    var totalRead = 0L;
                    var buffer = new byte[DownloadСhunk];
                    var hasMoreToRead = true;

                    do
                    {
                        cancelToken.ThrowIfCancellationRequested();
                        var readCount = await httpStream.ReadAsync(buffer, 0, buffer.Length, cancelToken)
                            .ConfigureAwait(false);
                        if (readCount == 0)
                        {
                            hasMoreToRead = false;
                        }
                        else
                        {
                            await targetStream.WriteAsync(buffer, 0, readCount, cancelToken)
                                .ConfigureAwait(false);

                            totalRead += readCount;
                            if (canReportsProgress)
                            {
                                var downloadPercentage = totalRead * 1d / (total * 1d) * 100;
                                var currentProgress = (int) Math.Floor(downloadPercentage);
                                OnPercentageChanged(new EventArgs<int>(currentProgress));
                            }
                        }
                    } while (hasMoreToRead);
                }
            }
        }

        private void OnPercentageChanged(EventArgs<int> e) => PercentageChanged?.Invoke(this, e);
    }
}