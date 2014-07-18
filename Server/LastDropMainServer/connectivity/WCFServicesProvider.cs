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
    public class WCFServicesProvider : IServicesWCF
    {
        private OperationController databaseController = new OperationController();

        public WCFServicesProvider()
        { }

        public void subscribeToPlant(string mail, string plantName)
        {
            databaseController.subscribeToPlant(mail, plantName);
        }

        public void unsubscribeToPlant(string mail, string plantName)
        {
            databaseController.UnsubscribeFromPlant(mail, plantName);
        }

        public bool registerUser(string mail, string password)
        {
            return databaseController.registerUser(mail, password);
        }

    }
}
