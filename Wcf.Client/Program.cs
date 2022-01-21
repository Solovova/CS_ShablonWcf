using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Wcf.Client {
    [ServiceContract]
    public interface IMessageService {
        [OperationContract]
        string[] GetMessages();
    }
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("Press any key to cont...");
            Console.ReadLine();
            string address = "net.tcp://localhost:6565/MessageService";
            NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
            var chanell = new ChannelFactory<IMessageService>(binding);
            var endpoint = new EndpointAddress(address);
            var proxy = chanell.CreateChannel(endpoint);

            var result = proxy?.GetMessages();
            if (result != null) {
                result.ToList().ForEach(x => Console.WriteLine(x));
            }
            Console.ReadLine();

        }
    }
}
