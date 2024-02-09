var client = new HttpClient();

var progress = new Progress<double>();
progress.ProgressChanged += (sender, value) => { Console.WriteLine($"percent = {(int)value}"); };

var cancellation = new CancellationTokenSource();
await using var destination = File.OpenWrite("LINQPad6Setup.exe");
await DownloadFileAsync("https://www.linqpad.net/GetFile.aspx?LINQPad6Setup.exe", destination, progress,
      cancellation.Token)
   .ConfigureAwait(false);
return;

async Task CopyStreamWithProgressAsync(Stream input, Stream output, long total, IProgress<double> aProgress,
   CancellationToken token)
{
   const int ioBufferSize = 8 * 1024; // Optimal size depends on your scenario

   // Expected size of input stream may be known from an HTTP header when reading from HTTP. Other streams may have their
   // own protocol for pre-reporting expected size.
   var canReportProgress = total != -1;
   var totalRead = 0L;
   var buffer = new byte[ioBufferSize];
   int read;

   while ((read = await input.ReadAsync(buffer, 0, buffer.Length)
             .ConfigureAwait(false)) > 0)
   {
      token.ThrowIfCancellationRequested();
      await output.WriteAsync(buffer, 0, read)
         .ConfigureAwait(false);
      totalRead += read;
      if (canReportProgress)
      {
         aProgress.Report(totalRead * 1d / (total * 1d) * 100);
      }
   }
}

async Task DownloadFileAsync(string url, Stream aDestination, IProgress<double> aProgress, CancellationToken token)
{
   var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, token)
      .ConfigureAwait(false);
   if (!response.IsSuccessStatusCode)
   {
      throw new Exception($"The request returned with HTTP status code {response.StatusCode}");
   }

   var total = response.Content.Headers.ContentLength ?? -1L;
   await using var source = await response.Content.ReadAsStreamAsync()
      .ConfigureAwait(false);
   await CopyStreamWithProgressAsync(source, aDestination, total, aProgress, token)
      .ConfigureAwait(false);
}