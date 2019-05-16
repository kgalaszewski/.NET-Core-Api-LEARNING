using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    public class HouseKeeperExternalResourcesService : IHouseKeeperExternalResourcesService
    {
        public string SaveStatement(Housekeeper housekeeper, DateTime statementDate)
        {
            var report = new HousekeeperStatementReport(housekeeper.Oid, statementDate);

            if (!report.HasData)
                return string.Empty;

            report.CreateDocument();

            var filename = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                string.Format("Sandpiper Statement {0:yyyy-MM} {1}.pdf", statementDate, housekeeper.FullName));

            report.ExportToPdf(filename);

            return filename;
        }

        public void EmailFile(Housekeeper housekeeper, string filename, string subject)
        {
            var client = new SmtpClient(SystemSettingsHelper.EmailSmtpHost)
            {
                Port = SystemSettingsHelper.EmailPort,
                Credentials =
                    new NetworkCredential(
                        SystemSettingsHelper.EmailUsername,
                        SystemSettingsHelper.EmailPassword)
            };

            var from = new MailAddress(SystemSettingsHelper.EmailFromEmail, SystemSettingsHelper.EmailFromName,
                Encoding.UTF8);
            var to = new MailAddress(housekeeper.Email);

            var message = new MailMessage(from, to)
            {
                Subject = subject,
                SubjectEncoding = Encoding.UTF8,
                Body = housekeeper.StatementEmailBody,
                BodyEncoding = Encoding.UTF8
            };

            message.Attachments.Add(new Attachment(filename));
            client.Send(message);
            message.Dispose();

            File.Delete(filename);
        }

        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }
}
