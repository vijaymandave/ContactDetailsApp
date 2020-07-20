using System.Web.Configuration;

namespace WebApp.Controllers.Helpers
{
    public class WebApiHelper
    {
        internal string GetWebApiUri()
        {
            return WebConfigurationManager.AppSettings["WebApiAddress"];
        }        
    }
}