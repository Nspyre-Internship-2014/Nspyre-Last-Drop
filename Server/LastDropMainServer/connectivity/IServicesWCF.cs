using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;


namespace LastDropDBOperations
{
    [ServiceContract]
    interface IServicesWCF
    {
        [OperationContract]
        void subscribeToPlant(string mail, string plantName);

        [OperationContract]
        void unsubscribeToPlant(string mail, string plantName);

        [OperationContract]
        bool registerUser(string mail, string password);
    }
}
