using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace AuthSystem.Areas.Identity.Data
{
    public class Additional
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [Url]
        public string Value { get; set; }

        public string UserId {  get; set; }
    }
}
