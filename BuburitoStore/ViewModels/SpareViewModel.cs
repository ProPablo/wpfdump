using BuburitoStore.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using ImageURL = System.String;

namespace BuburitoStore.ViewModels
{
    public class FoxApiModel
    {
        public ImageURL image { get; set; }
    }

    public class SpareViewModel
    {
        //Prop to update
        public ObservableCollection<ImageURL> FoxImageUrls { get; set; }
        public RelayCommand EffectRelayCommand { get; set; }

        //Make network requests from list
        private List<string> foxes = new List<string> { "https://randomfox.ca/floof/?id=2", "https://randomfox.ca/floof/?id=3", "https://randomfox.ca/floof/?id=4" };

        const int MAX_FOXES = 5;

        public SpareViewModel()
        {
            EffectRelayCommand = new RelayCommand(FillPageAsync);
            FoxImageUrls = new ObservableCollection<ImageURL>();
        }



        //Often labeled as a malpractice, async void used here due to event handling
        private async void FillPageAsync(object sender)
        {
            //Simple model, sequentially add urls one by one
            foreach (var foxAPI in foxes)
            {
                var imageURL = await GetFoxImageURLAsync(foxAPI);
                FoxImageUrls.Add(imageURL);
            }

            ////create task list
            //List<Task<ImageURL>> allRequests = new List<Task<ImageURL>>();
            //foreach (var foxAPI in foxes)
            //{
            //    allRequests.Add(GetFoxImageURLAsync(foxAPI));
            //}
            ////await all
            //var foxImages = await Task.WhenAll(allRequests);
            //foreach (var foxImage in foxImages)
            //{
            //    FoxImageUrls.Add(foxImage);
            //}


            ////Use IasyncEnumerable and await for to set the elements in loop
            //var cancellationTokenSource = new CancellationTokenSource();
            //cancellationTokenSource.CancelAfter(7000);
            ////Able to be subscribed to 
            //Progress<int> progress = new Progress<int>();

            //await foreach (var foxImage in GetFoxesGeneratorAsync(cancellationTokenSource.Token, progress))
            //{
            //    try
            //    {
            //        FoxImageUrls.Add(foxImage);
            //    }
            //    catch (Exception)
            //    {
            //        break;
            //    }
            //}
        }

        private async Task<ImageURL> GetFoxImageURLAsync(string apiURL)
        {
            HttpClient client = new HttpClient();
            var stream = await client.GetStreamAsync(apiURL);
            var data = await JsonSerializer.DeserializeAsync<FoxApiModel>(stream);
            return data.image;
        }

        private async void RunSlowCommand(object sender)
        {
            //Can use to set to Bound value
            var primeNum = await Task.Run(() =>
            {
                return FindPrimeNumber(1000);
            });

        }

        private async IAsyncEnumerable<ImageURL> GetFoxesGeneratorAsync(CancellationToken cancellationToken, IProgress<int> progressReporter)
        {

            int counter = 0;
            while (counter < MAX_FOXES)
            {
                await Task.Delay(1000);
                cancellationToken.ThrowIfCancellationRequested();
                HttpClient client = new HttpClient();
                var stream = await client.GetStreamAsync("https://randomfox.ca/floof/");
                var data = await JsonSerializer.DeserializeAsync<FoxApiModel>(stream);
                counter++;
                progressReporter.Report(counter);
                yield return data.image;
            }
        }


        public long FindPrimeNumber(int n)
        {
            int count = 0;
            long a = 2;
            while (count < n)
            {
                long b = 2;
                int prime = 1;// to check if found a prime
                while (b * b <= a)
                {
                    if (a % b == 0)
                    {
                        prime = 0;
                        break;
                    }
                    b++;
                }
                if (prime > 0)
                {
                    count++;
                }
                a++;
            }
            return (--a);
        }

    }
}
