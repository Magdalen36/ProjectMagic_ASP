using Newtonsoft.Json;
using ProjectMagic_ASP.Models;
using ProjectMagic_ASP.Models.Forms;
using ProjectMagic_ASP.Services.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjectMagic_ASP.Services
{
    public class CardService : BaseRepository, IService<CardModel, CardForm>
    {

        public CardService() : base("Card")
        {

        }


        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CardModel> GetAll()
        {
            using (HttpClient client = CreateHttpClient())
            {
                HttpResponseMessage response = GetResponseMessage(client.GetAsync);
                if (!response.IsSuccessStatusCode) throw new HttpRequestException();
                string jsonString = GetJsonContent(response);
                return JsonConvert.DeserializeObject<IEnumerable<CardModel>>(jsonString);
            }
        }

        public CardModel GetById(int id)
        {
            HttpClient client = new HttpClient();
            Uri adressUri = new Uri(baseAddress.ToString() + "Card/" + id);
            client.BaseAddress = adressUri;
            HttpResponseMessage response = client.GetAsync(adressUri).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
            return JsonConvert.DeserializeObject<CardModel>(jsonString);
        }

        public void Insert(CardForm form)
        {
            throw new NotImplementedException();
        }

        public void Update(CardForm form)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CardModel> GetByEditionId(int id)
        {
            HttpClient client = new HttpClient();
            Uri adressUri = new Uri(baseAddress.ToString() + "Card/SearchByEditionId/" + id);
            client.BaseAddress = adressUri;
            HttpResponseMessage response = client.GetAsync(adressUri).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
            return JsonConvert.DeserializeObject<IEnumerable<CardModel>>(jsonString);
        }
    }
}
