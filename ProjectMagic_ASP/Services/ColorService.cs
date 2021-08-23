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
    public class ColorService : BaseRepository, IService<ColorModel,ColorForm>
    {

        public ColorService(): base("Color")
        {

        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ColorModel> GetAll()
        {
            HttpClient client = new HttpClient();         
            HttpResponseMessage response = client.GetAsync(new Uri(baseAddress.ToString() + "PropCard/GetAllColors/")).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
            return JsonConvert.DeserializeObject<IEnumerable<ColorModel>>(jsonString);
            
        }

        public ColorModel GetById(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(new Uri(baseAddress.ToString() + "PropCard/GetByIdColors/" + id.ToString())).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
            return JsonConvert.DeserializeObject<ColorModel>(jsonString);
        }

        public void Insert(ColorForm form)
        {
            throw new NotImplementedException();
        }

        public void Update(ColorForm form)
        {
            throw new NotImplementedException();
        }
    }
}
