using Arbetsprov.Models.Pod;
using Arbetsprov.Models.Program;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Serialization;

namespace Arbetsprov.Controllers
{
    public class ProgramController : Controller
    {
        static HttpClient client = new HttpClient();

        // GET: ProgramController
        public async Task<ActionResult?> Index()
        {
            var programs = await ListPrograms();
            if (programs == null)
            {
                return null;
            }
            List<ProgramModel>? listOfPrograms = programs.Programs;
            if (listOfPrograms == null)
            {
                return null;
            }
            foreach (var program in listOfPrograms)
            {
                var pods = await ListPods(program.Id.ToString());
                if (pods != null)
                {
                    pods.PodFiles?.ForEach(pod =>
                    {
                        pod.Date = JsonConvert.DeserializeObject<DateTime>(@"""" + pod.PublishDateUTC + @"""").ToLocalTime();
                    });
                    program.PodEpisodes = pods.PodFiles;
                }
            }
            return View(listOfPrograms);
        }

        public async Task<ProgramsModel?> ListPrograms()
        {
            string path = "/api/v2/programs?isarchived=false&programcategoryid=133&filter=program.haspod&filterValue=true&format=JSON";
            return await client.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("http://api.sr.se" + path)
            }).Result.Content.ReadFromJsonAsync<ProgramsModel>();
        }

        public async Task<PodsModel?> ListPods(string programId)
        {
            string path = "/api/v2/podfiles?format=JSON&programid=" + programId;
            return await client.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("http://api.sr.se" + path)
            }).Result.Content.ReadFromJsonAsync<PodsModel>();
        }
    }
}
