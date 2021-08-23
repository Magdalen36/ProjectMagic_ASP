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
    public class SousTypeService : BaseRepository, IService<SousTypeModel, SousTypeForm>
    {
        public SousTypeService() : base("SousType")
        {
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SousTypeModel> GetAll()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(new Uri(baseAddress.ToString() + "PropCard/GetAllSousType/")).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
            return JsonConvert.DeserializeObject<IEnumerable<SousTypeModel>>(jsonString);
        }

        public SousTypeModel GetById(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(new Uri(baseAddress.ToString() + "PropCard/GetByIdSousType/" + id.ToString())).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
            return JsonConvert.DeserializeObject<SousTypeModel>(jsonString);
        }

        public void Insert(SousTypeForm form)
        {
            throw new NotImplementedException();
        }

        public void Update(SousTypeForm form)
        {
            throw new NotImplementedException();
        }
    }
}
