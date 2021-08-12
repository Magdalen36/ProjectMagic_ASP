
using Newtonsoft.Json;
using ProjectMagic_ASP.Models;
using ProjectMagic_ASP.Models.Forms;
using ProjectMagic_ASP.Services.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMagic.Services
{
    public class EditionService : BaseRepository, IService<EditionModel, EditionForm>
    {

        public EditionService() : base("Edition")
        {

        }

        public bool Delete(int d)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void Update(EditionForm form)
        {
            throw new NotImplementedException();
        }
    }
}
