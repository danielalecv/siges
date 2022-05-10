using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace siges.Repository
{
    public interface IMailSupport
    {
        public bool SendMessage(string name, string lastname, string email, string priority, string title, string body);
        public bool SendMessage(string name, string lastname, string email, string priority, string title, string body, IList<IFormFile> capturas);
    }
}
