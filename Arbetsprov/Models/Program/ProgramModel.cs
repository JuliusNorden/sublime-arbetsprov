using Arbetsprov.Models.Pod;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Xml.Serialization;
using Arbetsprov.Models.Channel;

namespace Arbetsprov.Models.Program
{
    public class ProgramModel
    {
        public required int Id { get; set; }
        public String? Name { get; set; }
        public String? Description { get; set; }
        public String? ProgramImage { get; set; }
        public ChannelModel? Channel { get; set; }
        public List<PodModel>? PodEpisodes { get; set; }
    }
}
