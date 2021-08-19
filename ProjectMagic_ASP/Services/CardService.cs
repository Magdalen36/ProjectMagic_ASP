using Newtonsoft.Json;
using ProjectMagic_ASP.Models;
using ProjectMagic_ASP.Models.Forms;
using ProjectMagic_ASP.Services.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.DeleteAsync(new Uri(baseAddress.ToString() + "Card/" + id.ToString())).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);

            return true;
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
            HttpResponseMessage response = client.GetAsync(new Uri(baseAddress.ToString() + "Card/" + id)).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
            return JsonConvert.DeserializeObject<CardModel>(jsonString);
        }

        public void Insert(CardForm form)
        {
            CardModel model = new CardModel { CardName = form.CardName, Cost =form.Cost, PS = form.PS, Description=form.Description, ColorId = form.ColorId, EditionId=form.EditionId, RarityId=form.RarityId, TypeCardId = form.TypeId, SousTypeCardId = form.SousTypeId };
            JsonContent entityJson = JsonContent.Create(model);

            using (HttpClient client = CreateHttpClient())
            {
                HttpResponseMessage response = GetResponseMessage(client.PostAsync, entityJson);
                if (!response.IsSuccessStatusCode) throw new HttpRequestException();
                string jsonString = GetJsonContent(response);
            }
        }

        public void Update(CardForm form)
        {
            CardModel model = new CardModel { Id= form.Id, CardName = form.CardName, Cost = form.Cost, PS = form.PS, Description = form.Description, ColorId = form.ColorId, EditionId = form.EditionId, RarityId = form.RarityId, TypeCardId = form.TypeId, SousTypeCardId = form.SousTypeId };
            JsonContent entityJson = JsonContent.Create(model);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.PutAsync(new Uri(baseAddress.ToString() + "Card/" + form.Id.ToString()), entityJson).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
        }

        public IEnumerable<CardModel> GetByEditionId(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(new Uri(baseAddress.ToString() + "Card/SearchByEditionId/" + id)).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
            return JsonConvert.DeserializeObject<IEnumerable<CardModel>>(jsonString);
        }
    }
}
