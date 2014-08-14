using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;
using System.Windows.Forms;

namespace LastDropMainServer
{
    [Serializable]
    public sealed class StatusTraceAspect : OnMethodBoundaryAspect
    {
        private readonly string category;
        public static RichTextBox textField;

        public StatusTraceAspect(string category)
        {
            this.category = category;
        }

        public string Category { get { return category; } }

        public override void OnExit(MethodExecutionArgs args)
        {
            if (args.Arguments.Count != 0 && textField != null)
            {
                textField.Text += " - " + this.Category + " service was launched.\n";
            }
        }
    }
}
