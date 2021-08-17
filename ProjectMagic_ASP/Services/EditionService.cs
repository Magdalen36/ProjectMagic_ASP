
using Newtonsoft.Json;
using ProjectMagic_ASP.Models;
using ProjectMagic_ASP.Models.Forms;
using ProjectMagic_ASP.Services.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMagic.Services
{
    public class EditionService : BaseRepository, IService<EditionModel, EditionForm>
    {

        public EditionService() : base("Edition")
        {

        }

       
        public bool Delete(int id)
        {    
            HttpClient client = new HttpClient();
                //plus propre en mettant ça direct dans le deleteAsync
                //Uri adressUri = new Uri(baseAddress.ToString() +"Edition/"+ id.ToString());
                //client.BaseAddress = adressUri;
            HttpResponseMessage response = client.DeleteAsync(new Uri(baseAddress.ToString() + "Edition/" + id.ToString())).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
            
            return true;
        }

        public IEnumerable<EditionModel> GetAll()
        {
            using (HttpClient client = CreateHttpClient())
            {
                HttpResponseMessage response = GetResponseMessage(client.GetAsync);
                if (!response.IsSuccessStatusCode) throw new HttpRequestException();
                string jsonString = GetJsonContent(response);
                return JsonConvert.DeserializeObject<IEnumerable<EditionModel>>(jsonString);
            }
        }

        public EditionModel GetById(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(new Uri(baseAddress.ToString() + "Edition/" + id.ToString())).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
            return JsonConvert.DeserializeObject<EditionModel>(jsonString);
        }

        public void Insert(EditionForm form)
        {
            EditionModel model = new EditionModel { Name = form.Name, NbMax = form.NbMax };
            JsonContent entityJson = JsonContent.Create(model);

            using (HttpClient client = CreateHttpClient())
            {              
                HttpResponseMessage response = GetResponseMessage(client.PostAsync, entityJson);
                if (!response.IsSuccessStatusCode) throw new HttpRequestException();
                string jsonString = GetJsonContent(response);                
            }
        }

        public void Update(EditionForm form)
        {
            EditionModel model = new EditionModel { Name = form.Name, NbMax = form.NbMax, Id = form.Id};
            JsonContent entityJson = JsonContent.Create(model);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.PutAsync(new Uri(baseAddress.ToString() + "Edition/" + form.Id.ToString()), entityJson).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);

        }


        public IEnumerable<EditionModel> GetByName(string name)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(new Uri(baseAddress.ToString() + "Edition/SearchByName/" + name)).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
            return JsonConvert.DeserializeObject<IEnumerable<EditionModel>>(jsonString);
        }
    }
}
