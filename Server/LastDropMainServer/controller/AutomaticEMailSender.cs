using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace LastDropMainServer
{
    class AutomaticEMailSender
    {
        private static OperationController controller;
        private static Dictionary<User, DateTime> lastNotified;

        public AutomaticEMailSender(OperationController activeController)
        {
            lastNotified = new Dictionary<User, DateTime>();
            controller = activeController;

            Thread thread = new Thread(checkAndSendEMails);
            thread.Start();
        }

        private void checkAndSendEMails()
        {
            List<User> userlist = controller.getUserList();
            foreach (User u in userlist)
            {
                UserNotificationOptions userOptions = controller.getUserNotificationOptions(u.Mail);

                if (userOptions.MailToggle == true && 
                    (DateTime.Now.Hour >= userOptions.IFrom.Hours) &&
                    (DateTime.Now.Hour <= userOptions.ITo.Hours) &&
                    (!lastNotified.ContainsKey(u) || ((DateTime.Now - lastNotified[u]).Hours >= userOptions.Interval)))
                {
                    controller.sendMailNotification(u);
                }

            }

            Thread.Sleep(5*60*1000);
        }
    }
}
