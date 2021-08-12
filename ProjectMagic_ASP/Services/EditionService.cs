
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

        //chippotage, à voir si il n'y a pas moyen de rendre ça plus propre
        public bool Delete(int id)
        {    
            HttpClient client = new HttpClient();
            Uri adressUri = new Uri(baseAddress.ToString() +"Edition/"+ id.ToString());
            client.BaseAddress = adressUri;
            HttpResponseMessage response = client.DeleteAsync(adressUri).Result;
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

        public EditionForm GetById(int id)
        {
            throw new NotImplementedException();
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
                //return JsonConvert.DeserializeObject<EditionModel>(jsonString);
            }
        }

        public void Update(EditionForm form)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EditionModel> GetByName(string name)
        {
            HttpClient client = new HttpClient();
            Uri adressUri = new Uri(baseAddress.ToString() + "Edition/SearchByName/" + name);
            client.BaseAddress = adressUri;
            HttpResponseMessage response = client.GetAsync(adressUri).Result;
            if (!response.IsSuccessStatusCode) throw new HttpRequestException();
            string jsonString = GetJsonContent(response);
            return JsonConvert.DeserializeObject<IEnumerable<EditionModel>>(jsonString);
        }
    }
}
