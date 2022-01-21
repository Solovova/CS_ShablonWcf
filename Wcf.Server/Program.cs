using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Wcf.Server {
    [ServiceContract]
    public interface IMessageService {
        [OperationContract]
        string[] GetMessages();
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MessageService : IMessageService {
        public string[] GetMessages() {
            return new string[] {"Message 1", "Message 2"};
        }
    }
    internal class Program {
        static void Main(string[] args) {
            var uris = new Uri[1];
            string address = "net.tcp://localhost:6565/MessageService";
            uris[0] = new Uri(address);
            IMessageService messageService = new MessageService();
            ServiceHost serviceHost = new ServiceHost(messageService,uris);
            var binding = new NetTcpBinding(SecurityMode.None);
            serviceHost.AddServiceEndpoint(typeof(IMessageService),binding,"");
            serviceHost.Opened += ServiceHost_Opened;
            serviceHost.Open();
            Console.ReadLine();
        }

        private static void ServiceHost_Opened(object sender, EventArgs e) {
            Console.WriteLine("Message service started");
        }
    }
}
