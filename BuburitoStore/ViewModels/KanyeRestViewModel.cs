using BuburitoStore.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.Json;
using System.IO;
using System.Windows.Threading;
using System.Threading;

namespace BuburitoStore.ViewModels
{
    public class ApiReponse
    {
        public string quote { get; set; }
    }

    public class KanyeRestViewModel : ObservableObject
    {
        //Prop to update
        private string _quote;

        public string Quote
        {
            get { return _quote; }
            set
            {
                _quote = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand EffectRelayCommand { get; set; }
        public RelayCommand SyncRelayCommand { get; set; }
        const string url = "https://api.kanye.rest/";

        public KanyeRestViewModel()
        {
            EffectRelayCommand = new RelayCommand(GetAPIResultCommand);
            SyncRelayCommand = new RelayCommand(SyncCommand);
            Quote = "Nothing yet";
        }



        private async void GetAPIResultCommand(object sender)
        {
            Quote = await GetApiResultAsync(url);

            //APM method
            //var request = WebRequest.Create(url);
            //request.BeginGetResponse(APMResultCallback, request);

        }
        private void SyncCommand(object sender)
        {
            Quote = GetApiResult(url);
        }

        private string GetApiResult(string URL)
        {
            Thread.Sleep(1000);
            var request = WebRequest.Create(URL);
            var resp = request.GetResponse();
            var stream = resp.GetResponseStream();

            var asString = "";
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                asString = reader.ReadToEnd();
            }
            var data = JsonSerializer.Deserialize<ApiReponse>(asString);
            return data.quote;
        }


        private async Task<string> GetApiResultAsync(string URL)
        {
            await Task.Delay(1000);
            
            //Obsolte but for demonstration purposes, HTTPClient not used
            var request = WebRequest.Create(URL);
            var response = await request.GetResponseAsync();
            var stream = response.GetResponseStream();
            var asString = "";
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                asString = reader.ReadToEnd();
            }

            var data = JsonSerializer.Deserialize<ApiReponse>(asString);
            return data.quote;
        }


        private void APMResultCallback(IAsyncResult result)
        {
            var req = (WebRequest)result.AsyncState;
            var response = req.EndGetResponse(result);
            var stream = response.GetResponseStream();
            var asString = "";
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                asString = reader.ReadToEnd();
            }

            var data = JsonSerializer.Deserialize<ApiReponse>(asString);
            //Conventionally BeginInvoke would be used here so the delegate could be called when the UI thread was free however modern c# asks to await it
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                //cant return anything, has to simply set the value on the object at global scope
                Quote = data.quote;

            });

        }
    }
}
