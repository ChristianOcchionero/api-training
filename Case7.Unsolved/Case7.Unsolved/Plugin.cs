﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniconta.API.Plugin;
using Uniconta.ClientTools.DataModel;
using Uniconta.Common;
using Uniconta.DataModel;

namespace Case7.Unsolved
{
    public class Plugin : PageEventsBase
    {
        private int numberOfInvoices = 0;

        public override bool OnMenuItemClicked(string ActionType, object sender, object arguments)
        {
            if (ActionType == "PreGenerateInvoice")
            {
                numberOfInvoices = GetNumberOfInvoices();
                return base.OnMenuItemClicked(ActionType, sender, arguments);
            } else if (ActionType == "PostGenerateInvoice")
            {
                var result = base.OnMenuItemClicked(ActionType, sender, arguments);
                if (numberOfInvoices < GetNumberOfInvoices())
                {
                    //TODO Create CreditorOrderline
                }

                return result;
            }
            else
            {
                return base.OnMenuItemClicked(ActionType, sender, arguments);
            }
        }

        private int GetNumberOfInvoices()
        {
            return api.Query<DebtorInvoiceClient>().Result.Length;
        }
    }
}
