

using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Test.Foundation.Models
{
    public class CompareResult
    {
        public bool success;

        private List<string> errorMessages;

        public string GetMessage()
        {
            if (success)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            int count = 0;
            String error = this.errorMessages[0];
            if (this.errorMessages.Count > 1)
            {
                foreach (String message in this.errorMessages)
                {
                    count++;
                    builder.Append(string.Format("{0}. " + message, count));
                    builder.Append(Environment.NewLine);
                }

                error = builder.ToString();
            }
            return error;
        }

        public CompareResult()
        {
            this.success = true;
            this.errorMessages = new List<string>();
        }

        public void AppendMessage(string message)
        {
            this.errorMessages.Add(message);
            this.success = false;
        }

        public void Verify()
        {
            if (!success)
            {
                throw new Exception(GetMessage());
            }
        }
    }
}
