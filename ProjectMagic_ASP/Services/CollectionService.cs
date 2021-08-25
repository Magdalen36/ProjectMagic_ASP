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
    public class CollectionService : BaseRepository, IService<CollectionModel, CollectionForm>
    {
        public CollectionService() : base("Collection")
        {
        }

        public bool Delete(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.DeleteAsync(new Uri(baseAddress.ToString() + "Collection/" + id.ToString())).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);

            return true;
        }

        public IEnumerable<CollectionModel> GetAll()
        {
            using (HttpClient client = CreateHttpClient())
            {
                HttpResponseMessage response = GetResponseMessage(client.GetAsync);
                if (!response.IsSuccessStatusCode) throw new HttpRequestException();
                string jsonString = GetJsonContent(response);
                return JsonConvert.DeserializeObject<IEnumerable<CollectionModel>>(jsonString);
            }
        }

        public IEnumerable<CollectionModel> GetAllById(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(new Uri(baseAddress.ToString() + "Collection/CollectionUser/" + id.ToString())).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
            return JsonConvert.DeserializeObject<IEnumerable<CollectionModel>>(jsonString); ;
        }

        public CollectionModel GetById(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(new Uri(baseAddress.ToString() + "Collection/" + id.ToString())).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
            return JsonConvert.DeserializeObject<CollectionModel>(jsonString); ;
        }

        public void Insert(CollectionForm form)
        {
            CollectionModel model = new CollectionModel { UserId = form.UserId, CardId = form.CardId, NbCard = form.NbCard };
            JsonContent entityJson = JsonContent.Create(model);

            using (HttpClient client = CreateHttpClient())
            {
                HttpResponseMessage response = GetResponseMessage(client.PostAsync, entityJson);
                if (!response.IsSuccessStatusCode) throw new HttpRequestException();
                string jsonString = GetJsonContent(response);
            }
        }

        //Version pour une insertion sans formulaire
        public bool Insert(int cardId, int userId)
        {
            CollectionModel model = new CollectionModel { UserId = userId, CardId = cardId, NbCard = 1 };
            JsonContent entityJson = JsonContent.Create(model);

            using (HttpClient client = CreateHttpClient())
            {
                HttpResponseMessage response = GetResponseMessage(client.PostAsync, entityJson);
                if (!response.IsSuccessStatusCode) throw new HttpRequestException();
                string jsonString = GetJsonContent(response);
                return true;
            }
        }

        public void Update(CollectionForm form)
        {
            CollectionModel model = new CollectionModel { Id = form.Id, UserId = form.UserId, CardId = form.CardId, NbCard = form.NbCard };
            JsonContent entityJson = JsonContent.Create(model);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.PutAsync(new Uri(baseAddress.ToString() + "Collection/" + form.Id.ToString()), entityJson).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
        }     
    }
}

