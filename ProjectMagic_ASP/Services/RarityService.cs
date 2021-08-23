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
    public class RarityService : BaseRepository, IService<RarityModel, RarityForm>
    {
        public RarityService(): base("Rarity")
        {

        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RarityModel> GetAll()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(new Uri(baseAddress.ToString() + "PropCard/GetAllRarity/")).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
            return JsonConvert.DeserializeObject<IEnumerable<RarityModel>>(jsonString);
        }

        public RarityModel GetById(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(new Uri(baseAddress.ToString() + "PropCard/GetByIdRarity/" + id.ToString())).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
            return JsonConvert.DeserializeObject<RarityModel>(jsonString);
        }

        public void Insert(RarityForm form)
        {
            throw new NotImplementedException();
        }

        public void Update(RarityForm form)
        {
            throw new NotImplementedException();
        }
    }
}
