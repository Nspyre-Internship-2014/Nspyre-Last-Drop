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
        private static int refreshInterval;
        private static OperationController controller;

        public AutomaticEMailSender(int refreshIntervalOption, OperationController activeController)
        {
            refreshInterval = refreshIntervalOption;
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

                if (userOptions.MailToggle == true)
                {
                    controller.sendMailNotification(u);
                }

            }

            Thread.Sleep(refreshInterval);
        }
    }
}
