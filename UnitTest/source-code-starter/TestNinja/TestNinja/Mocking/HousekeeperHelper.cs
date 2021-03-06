﻿using System;

namespace TestNinja.Mocking
{
    public class HouseKeeperService
    {
        private IHouseKeeperExternalResourcesService _service;

        public HouseKeeperService(IHouseKeeperExternalResourcesService service = null)
        {
            _service = service ?? new HouseKeeperExternalResourcesService();
        }

        public bool SendStatementEmails(DateTime statementDate, IXtraMessageBox msgBoxer)
        {
            var service = _service ?? new HouseKeeperExternalResourcesService();
            var housekeepers = service.Query<Housekeeper>();

            foreach (var housekeeper in housekeepers)
            {
                if (string.IsNullOrWhiteSpace(housekeeper.Email))
                    continue;

                var statementFilename = service.SaveStatement(housekeeper, statementDate);

                if (string.IsNullOrWhiteSpace(statementFilename))
                    continue;

                try
                {
                    service.EmailFile(housekeeper, statementFilename,
                        string.Format("Sandpiper Statement {0:yyyy-MM} {1}", statementDate, housekeeper.FullName));
                }
                catch (Exception e)
                {
                    msgBoxer.Show(e.Message, string.Format("Email failure: {0}", housekeeper.Email), MessageBoxButtons.OK);
                }
            }

            return true; // fakt, ze nigdy nie da sie zwrocic false w tej metodzie sprawia, ze nie ma sensu zeby byla bool,
            // lepiej zeby byla void 
        }
    }

    public enum MessageBoxButtons
    {
        OK
    }

    public class XtraMessageBox : IXtraMessageBox
    {
        public void Show(string s, string housekeeperStatements, MessageBoxButtons ok)
        {
        }
    }

    public class MainForm
    {
        public bool HousekeeperStatementsSending { get; set; }
    }

    public class DateForm
    {
        public DateForm(string statementDate, object endOfLastMonth)
        {
        }

        public DateTime Date { get; set; }

        public DialogResult ShowDialog()
        {
            return DialogResult.Abort;
        }
    }

    public enum DialogResult
    {
        Abort,
        OK
    }

    public class SystemSettingsHelper
    {
        public static string EmailSmtpHost { get; set; }
        public static int EmailPort { get; set; }
        public static string EmailUsername { get; set; }
        public static string EmailPassword { get; set; }
        public static string EmailFromEmail { get; set; }
        public static string EmailFromName { get; set; }
    }

    public class Housekeeper
    {
        public string Email { get; set; }
        public int Oid { get; set; }
        public string FullName { get; set; }
        public string StatementEmailBody { get; set; }
    }

    public class HousekeeperStatementReport
    {
        public HousekeeperStatementReport(int housekeeperOid, DateTime statementDate)
        {
        }

        public bool HasData { get; set; }

        public void CreateDocument()
        {
        }

        public void ExportToPdf(string filename)
        {
        }
    }
}