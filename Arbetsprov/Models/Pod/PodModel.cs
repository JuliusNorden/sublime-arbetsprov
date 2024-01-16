using Newtonsoft.Json;

namespace Arbetsprov.Models.Pod
{
    public class PodModel
    {
        public String? Title { get; set; }
        public string? PublishDateUTC { get; set; }
        public DateTime? Date { get; set; }
        public int? Duration { get; set; }
        public String? URL { get; set; }
    }
}
