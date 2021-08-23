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
    public class TypeService : BaseRepository, IService<TypeModel, TypeForm>
    {
        public TypeService() : base("Type")
        {
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TypeModel> GetAll()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(new Uri(baseAddress.ToString() + "PropCard/GetAllTypes/")).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
            return JsonConvert.DeserializeObject<IEnumerable<TypeModel>>(jsonString);
        }

        public TypeModel GetById(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(new Uri(baseAddress.ToString() + "PropCard/GetByIdTypes/" + id.ToString())).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
            return JsonConvert.DeserializeObject<TypeModel>(jsonString);
        }

        public void Insert(TypeForm form)
        {
            throw new NotImplementedException();
        }

        public void Update(TypeForm form)
        {
            throw new NotImplementedException();
        }
    }
}
