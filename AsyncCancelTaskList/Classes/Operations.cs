using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCancelTaskList.Classes
{
    public class Operations
    {

        public delegate void ProcessDelegate(string sender);
        /// <summary>
        /// Pass information to caller
        /// </summary>
        public static event ProcessDelegate OnProcess;

        static readonly HttpClient _httpClient = new() { MaxResponseContentBufferSize = 1_000_000 };

        public static async Task SumPageSizesAsync(CancellationTokenSource cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            int total = 0;

            foreach (string url in MockedData.UrlAddresses)
            {
                int contentLength = await ProcessUrlAsync(url, _httpClient, cancellationToken.Token);
                total += contentLength;
            }

            stopwatch.Stop();

            OnProcess?.Invoke($@"Total bytes returned:  {total:#,#}");

        }

        public static async Task<int> ProcessUrlAsync(string url, HttpClient client, CancellationToken token)
        {

            HttpResponseMessage response = await client.GetAsync(url, token);
            byte[] content = await response.Content.ReadAsByteArrayAsync(token);

            OnProcess?.Invoke($@"{url,-70} {content.Length,10:#,#}");
            return content.Length;
        }



    }
}
