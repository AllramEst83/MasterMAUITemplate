using MasterTemplate.Interfaces;

namespace MasterTemplate.Services
{
    public class MainService : IMainService
    {
        public MainService()
        {

        }

        public string GetServiceMessage()
        {
            return "This is a service message";
        }

        public List<string> GetServiceMessages()
        {
            return
            [
                "Message one",
                "Message two",
                "Message three"
            ];
        }
    }
}
