﻿using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniconta.API.GeneralLedger;
using Uniconta.API.Plugin;
using Uniconta.API.Service;
using Uniconta.API.System;
using Uniconta.ClientTools;
using Uniconta.ClientTools.DataModel;
using Uniconta.Common;

namespace JournalLoader
{
    public class JournalLoaderPlugin : IPluginBase
    {
        private string error;

        public string Name => "Journal Loader Plugin";

        public CrudAPI Crud { get; private set; }

        public event EventHandler OnExecute;

        public ErrorCodes Execute(UnicontaBaseEntity master, UnicontaBaseEntity currentRow, IEnumerable<UnicontaBaseEntity> source, string command, string args)
        {
            // Plugin must be installed on Control: GL_DailyJournal
            var journal = currentRow as GLDailyJournalClient;
            Crud.LoadCache(typeof(DebtorClient)).Wait();

            // TODO: Change Path
            using (TextFieldParser parser = new TextFieldParser(@"C:\Users\Alexander Banks\api-training\JournalLoader\JournalLoader\Data.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                // Read the first line to get rid of header
                parser.ReadFields();

                var loc = Localization.GetLocalization(Language.da);

                var newJournalLines = new List<GLDailyJournalLineClient>();
                while (!parser.EndOfData)
                {
                    //Process row
                    string[] fields = parser.ReadFields();
                    // TODO: Process fields
                    var date = DateTime.ParseExact(fields[0], "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    var account = fields[1];
                    var accountType = loc.Lookup(fields[2]);
                    var amount = double.Parse(fields[3], CultureInfo.GetCultureInfo("da"));

                    var journalLine = new GLDailyJournalLineClient
                    {
                        // TODO: Insert properties from fields[]
                        Date = date,
                        Account = account,
                        _Account = account,
                        AccountType = accountType,
                        Amount = amount,
                    };
                    journal.Account = account;
                    journalLine.SetMaster(journal);
                    journal.Account = account;
                    newJournalLines.Add(journalLine);
                }

                // Done parsing file. Insert new journal lines.
                var result = Crud.Insert(newJournalLines).Result;
                if (result != ErrorCodes.Succes)
                {
                    // TODO: Do Error Handling.
                    return result;
                }

                // Post Journals
                var postingAPI = new PostingAPI(Crud);
                var res = postingAPI.PostDailyJournal(journal, DateTime.Now, "My Fine Comment", newJournalLines.Count, lines: newJournalLines).Result;

            }
            return ErrorCodes.Succes;
        }

        public string[] GetDependentAssembliesName()
        {
            return new string[] { };
        }

        public string GetErrorDescription()
        {
            return error;
        }

        public void Intialize()
        {
        }

        public void SetAPI(BaseAPI api)
        {
            Crud = api as CrudAPI;
        }

        public void SetMaster(List<UnicontaBaseEntity> masters)
        {
        }
    }
}
