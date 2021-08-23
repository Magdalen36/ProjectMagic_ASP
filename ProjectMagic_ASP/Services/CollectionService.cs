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
            throw new NotImplementedException();
        }

        public void Update(CollectionForm form)
        {
            throw new NotImplementedException();
        }
    }
}

//public int Id { get; set; }
//public int UserId { get; set; }
//public int CardId { get; set; }
//public int NbCard { get; set; }
//public int EditionId { get; set; }
//public int ColorId { get; set; }

//public string FirstName { get; set; }
//public string LastName { get; set; }
//public string CardName { get; set; }
//public string EditionName { get; set; }
//public string ColorName { get; set; }