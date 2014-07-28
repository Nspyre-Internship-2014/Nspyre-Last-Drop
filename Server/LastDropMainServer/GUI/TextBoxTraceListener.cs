using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace LastDropMainServer
{
    public class TextBoxTraceListener : TraceListener
    {
        private RichTextBox target;
        private StringSendDelegate invokeWrite;
        private OperationController controller;

        public TextBoxTraceListener(RichTextBox target, OperationController controller)
        {
            this.target = target;
            this.controller = controller;
            invokeWrite = new StringSendDelegate(SendString);

        }

        public override void Write(string message)
        {
            target.Invoke(invokeWrite, new object[] { message });
        }

        public override void WriteLine(string message)
        {
            target.Invoke(invokeWrite, new object[] { message + Environment.NewLine });
        }

        private delegate void StringSendDelegate(string message);
        private void SendString(string message)
        {
            target.Text += message;
        }
    }
}
