using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldLib.Models
{
    public class EmailModel
    {
        private string _body;
        private string _email;
        public string From { get; set; }
        public string To { get; set; }
        public string Body
        {
            get
            {
                return _body;
            }
            set { _body = string.Format("<div class=\"well\">{0}</div>", value); }
        }
        public string Subject { get; set; }


        public EmailModel() { }

        public EmailModel(string to)
        {
            To = to;
        }
        public EmailModel(string from, string to)
        {
            From = from;
            To = to;
        }
        public EmailModel(string from, string to, string message)
        {
            From = from;
            To = to;
            Body = message;
        }


        
    }
}