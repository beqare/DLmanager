using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Windows;

namespace DLmanager
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient = new();
        private CancellationTokenSource _cancellationTokenSource;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            string url = UrlBox.Text;
            if (string.IsNullOrWhiteSpace(url))
            {
                StatusText.Text = "Please enter a valid URL.";
                return;
            }

            _cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = _cancellationTokenSource.Token;

            DownloadButton.IsEnabled = false;
            CancelButton.IsEnabled = true;
            UrlBox.IsEnabled = false;

            try
            {
                var uri = new Uri(url);
                var fileName = System.IO.Path.GetFileName(uri.LocalPath);
                var downloadsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                if (!Directory.Exists(downloadsFolder))
                {
                    Directory.CreateDirectory(downloadsFolder);
                }
                var downloadPath = Path.Combine(downloadsFolder, fileName);

                DownloadProgress.Value = 0;
                StatusText.Text = "Starting download...";

                using var response = await _httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                response.EnsureSuccessStatusCode();

                var totalBytes = response.Content.Headers.ContentLength ?? -1L;
                var canReportProgress = totalBytes != -1;

                using var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
                using var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);

                var buffer = new byte[8192];
                long totalRead = 0;
                int bytesRead;
                var stopwatch = Stopwatch.StartNew();

                while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);
                    totalRead += bytesRead;

                    if (canReportProgress)
                    {
                        double progress = (double)totalRead / totalBytes * 100;
                        double mbRead = totalRead / 1024d / 1024d;
                        double mbTotal = totalBytes / 1024d / 1024d;
                        double speed = mbRead / stopwatch.Elapsed.TotalSeconds;

                        DownloadProgress.Value = progress;
                        StatusText.Text = $"{progress:F1}% - {mbRead:F2} MB / {mbTotal:F2} MB @ {speed:F2} MB/s";
                    }
                    else
                    {
                        StatusText.Text = $"{totalRead / 1024d / 1024d:F2} MB downloaded...";
                    }
                }

                StatusText.Text = $"Download complete: {fileName}";
            }
            catch (OperationCanceledException)
            {
                StatusText.Text = "Download cancelled.";
                DownloadProgress.Value = 0;
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Error: {ex.Message}";
            }
            finally
            {
                DownloadButton.IsEnabled = true;
                CancelButton.IsEnabled = false;
                UrlBox.IsEnabled = true;
                _cancellationTokenSource?.Dispose();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _cancellationTokenSource?.Cancel();
            StatusText.Text = "Cancelling download...";
            CancelButton.IsEnabled = false;
        }
    }
}