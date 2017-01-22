using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Acr.UserDialogs;
using ImageSearch.Models;
using ImageSearch.Services;
using ImageSearch.Services.BingSearch;
using MvvmHelpers;
using Newtonsoft.Json;

namespace ImageSearch.ViewModels
{
    public class ImageSearchViewModel
    {
        public ObservableRangeCollection<ImageResult> Images { get; set; }

        public ImageSearchViewModel()
        {
            Images = new ObservableRangeCollection<ImageResult>();
        }

        public async Task<bool> SearchForImagesAsync(string query)
        {
            //Bing Image API
            var url = $"https://api.cognitive.microsoft.com/bing/v5.0/images/" +
                      $"search?q={WebUtility.UrlEncode(query)}" +
                      $"&count=20&offset=0&mkt=en-us&safeSearch=Strict";

            var requestHeaderKey = "Ocp-Apim-Subscription-Key";
            var requestHeaderValue = CognitiveServicesKeys.BingSearch;

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add(requestHeaderKey, requestHeaderValue);

                    var json = await client.GetStringAsync(url);

                    var result = JsonConvert.DeserializeObject<SearchResult>(json);

                    Images.ReplaceRange(result.Images.Select(i => new ImageResult
                    {
                        ContextLink = i.HostPageUrl,
                        FileFormat = i.EncodingFormat,
                        ImageLink = i.ContentUrl,
                        Title = i.Name
                    }));
                }
            }
            catch (Exception ex)
            {
                await UserDialogs.Instance.AlertAsync("Unable to query images: " + ex.Message);
                return false;
            }

            return true;
        }
    }
}
