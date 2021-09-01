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
    public class DeckService : BaseRepository, IService<DeckModel, DeckForm>
    {
        public DeckService(): base("Deck")
        {

        }

        public bool Delete(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.DeleteAsync(new Uri(baseAddress.ToString() + "Deck/" + id.ToString())).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);

            return true;
        }

        public IEnumerable<DeckModel> GetAll()
        {
            using (HttpClient client = CreateHttpClient())
            {
                HttpResponseMessage response = GetResponseMessage(client.GetAsync);
                if (!response.IsSuccessStatusCode) throw new HttpRequestException();
                string jsonString = GetJsonContent(response);
                return JsonConvert.DeserializeObject<IEnumerable<DeckModel>>(jsonString);
            }
        }

        public IEnumerable<DeckModel> GetAllById(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(new Uri(baseAddress.ToString() + "Deck/DeckUser/" + id.ToString())).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
            return JsonConvert.DeserializeObject<IEnumerable<DeckModel>>(jsonString); ;
        }

        public DeckModel GetById(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(new Uri(baseAddress.ToString() + "Deck/" + id.ToString())).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
            return JsonConvert.DeserializeObject<DeckModel>(jsonString);
        }

        public void Insert(DeckForm form)
        {
            DeckModel model = new DeckModel { DeckName = form.DeckName, UserId = form.UserId, ColorId = form.ColorId };
            JsonContent entityJson = JsonContent.Create(model);

            using (HttpClient client = CreateHttpClient())
            {
                HttpResponseMessage response = GetResponseMessage(client.PostAsync, entityJson);
                if (!response.IsSuccessStatusCode) throw new HttpRequestException();
                string jsonString = GetJsonContent(response);
            }
        }

        public void Update(DeckForm form)
        {
            DeckModel model = new DeckModel { DeckName = form.DeckName, UserId = form.UserId, Id = form.Id, ColorId = form.ColorId };
            JsonContent entityJson = JsonContent.Create(model);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.PutAsync(new Uri(baseAddress.ToString() + "Deck/" + form.Id.ToString()), entityJson).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
        }
    }
}
