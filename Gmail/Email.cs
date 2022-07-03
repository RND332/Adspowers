using ActiveUp.Net.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Adspowers.Gmail
{
    public class Mail 
    {
        public static string GetVerify(string gmail, string password) 
        {
            var mailRepository = new MailRepository(
                            "imap.gmail.com",
                            993,
                            true,
                            gmail,
                            password
                        );

            var emailList = mailRepository.GetAllMails("inbox");

            var noahMessages = emailList.Where(mail => mail.From.Email == "hello@noah.com" && mail.Subject == "Verify your email address" && mail.To.First().Email == gmail);

            var email = noahMessages.First();
            var linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (Match m in linkParser.Matches(email.Summary))
                return m.Value;

            return null;
        }
        public static string GetRef(string gmail, string password)
        {
            var mailRepository = new MailRepository(
                            "imap.gmail.com",
                            993,
                            true,
                            gmail,
                            password
                        );

            var emailList = mailRepository.GetAllMails("inbox");

            var noahMessages = emailList.Where(mail => mail.From.Email == "hello@noah.com" && mail.Subject == "Welcome to NOAH");

            var email = noahMessages.First();
            var linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (Match m in linkParser.Matches(email.Summary))
                return m.Value;

            return null;
        }
    }
    public class MailRepository
    {
        private Imap4Client client;

        public MailRepository(string mailServer, int port, bool ssl, string login, string password)
        {
            if (ssl)
                Client.ConnectSsl(mailServer, port);
            else
                Client.Connect(mailServer, port);
            Client.Login(login, password);
        }

        public IEnumerable<Message> GetAllMails(string mailBox)
        {
            return GetMails(mailBox, "ALL").Cast<Message>();
        }

        public IEnumerable<Message> GetUnreadMails(string mailBox)
        {
            return GetMails(mailBox, "UNSEEN").Cast<Message>();
        }

        protected Imap4Client Client
        {
            get { return client ?? (client = new Imap4Client()); }
        }

        private MessageCollection GetMails(string mailBox, string searchPhrase)
        {
            Mailbox mails = Client.SelectMailbox(mailBox);
            MessageCollection messages = mails.SearchParse(searchPhrase);
            return messages;
        }
    }

}
