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
    public class CardInDeckService : BaseRepository, IService<CardInDeckModel, CardInDeckForm>
    {
        public CardInDeckService() : base("CardInDeck")
        {
        }

        public bool Delete(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.DeleteAsync(new Uri(baseAddress.ToString() + "CardInDeck/" + id.ToString())).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);

            return true;
        }

        public IEnumerable<CardInDeckModel> GetAll()
        {
            using (HttpClient client = CreateHttpClient())
            {
                HttpResponseMessage response = GetResponseMessage(client.GetAsync);
                if (!response.IsSuccessStatusCode) throw new HttpRequestException();
                string jsonString = GetJsonContent(response);
                return JsonConvert.DeserializeObject<IEnumerable<CardInDeckModel>>(jsonString);
            }
        }

        public IEnumerable<CardInDeckModel> GetAllByDeck(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(new Uri(baseAddress.ToString() + "CardInDeck/CardsInDeck/" + id.ToString())).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
            return JsonConvert.DeserializeObject<IEnumerable<CardInDeckModel>>(jsonString); ;
        }

        public CardInDeckModel GetById(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(new Uri(baseAddress.ToString() + "CardInDeck/" + id.ToString())).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
            return JsonConvert.DeserializeObject<CardInDeckModel>(jsonString); ;
        }

        public void Insert(CardInDeckForm form)
        {
            CardInDeckModel model = new CardInDeckModel { DeckId = form.DeckId, CardId = form.CardId, NbCard = form.NbCard, /*UserId = form.UserId, ColorId = form.ColorId, RarityId = form.RarityId, TypeId = form.TypeId, SousTypeId = form.SousTypeId*/ };
            JsonContent entityJson = JsonContent.Create(model);

            using (HttpClient client = CreateHttpClient())
            {
                HttpResponseMessage response = GetResponseMessage(client.PostAsync, entityJson);
                if (!response.IsSuccessStatusCode) throw new HttpRequestException();
                string jsonString = GetJsonContent(response);
            }
        }

        //Version pour une insertion sans formulaire
        public bool Insert(int cardId, int deckId)
        {
            CardInDeckModel model = new CardInDeckModel { DeckId = deckId, CardId = cardId, NbCard = 1 };
            JsonContent entityJson = JsonContent.Create(model);

            using (HttpClient client = CreateHttpClient())
            {
                HttpResponseMessage response = GetResponseMessage(client.PostAsync, entityJson);
                if (!response.IsSuccessStatusCode) throw new HttpRequestException();
                string jsonString = GetJsonContent(response);
                return true;
            }
        }

        public void Update(CardInDeckForm form)
        {
            CardInDeckModel model = new CardInDeckModel { Id = form.Id, DeckId = form.DeckId, CardId = form.CardId, NbCard = form.NbCard };
            JsonContent entityJson = JsonContent.Create(model);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.PutAsync(new Uri(baseAddress.ToString() + "CardInDeck/" + form.Id.ToString()), entityJson).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
        }
    }
}
