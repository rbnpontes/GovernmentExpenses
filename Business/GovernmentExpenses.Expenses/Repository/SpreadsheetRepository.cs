using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using GovernmentExpenses.Core;
using GovernmentExpenses.Expenses.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace GovernmentExpenses.Expenses.Repository
{
    /// <summary>
    /// Simple Integration of Google Spreadsheet
    /// </summary>
    internal class SpreadsheetRepository : IRepository<Expense>
    {
        private readonly static string SpreadsheetId = "1diJae92IhDpvr5AAxGCT2yhLdOze51901GTL0Z9V7xM";
        private UserCredential credentials_;
        public int Count => throw new NotImplementedException();
        private ILogger logger_;
        private SheetsService service_;
        private IEnumerable<Expense> data_;
        public SpreadsheetRepository(ILogger logger)
        {
            logger_ = logger;
            Initialize();
        }
        // Font: https://developers.google.com/sheets/api/quickstart/dotnet
        private void LoadCredentials()
        {
            using(FileStream stream = new FileStream($"{AppDomain.CurrentDomain.BaseDirectory}Artifacts\\google-credentials.json",FileMode.Open, FileAccess.Read))
            {
                string credPath = $"{AppDomain.CurrentDomain.BaseDirectory}Artifacts\\token.json";
                credentials_ = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new string[] { SheetsService.Scope.Spreadsheets },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
        }
        private IEnumerable<Expense> FetchValues()
        {
            string range = "expenses!A2:AN";
            SpreadsheetsResource.ValuesResource.GetRequest request =service_.Spreadsheets.Values.Get(SpreadsheetId, range);
            ValueRange response = request.Execute();
            IList<IList<object>> values = response.Values;
            if(values != null && values.Count > 0)
            {
                foreach(var row in values)
                {
                    yield return ExpenseUtils.GetExpenseFromArray(row);
                }
            }
        }
        private void Initialize()
        {
            LoadCredentials();
            service_ = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials_,
                ApplicationName = "GovernmentExpenses",
            });
            data_ = FetchValues() ?? new List<Expense>();
        }
        public IEnumerable<Expense> All()
        {
            return data_;
        }

        public void Commit()
        {
        }

        public void Insert(Expense item)
        {
            throw new NotImplementedException();
        }

        public void Remove(Expense item)
        {
            throw new NotImplementedException();
        }

        public void Update(Expense item)
        {
            int idx = data_.ToList().FindIndex(0, x => x.Id == item.Id);
            ValueRange range = new ValueRange();
            range.MajorDimension = "ROWS";
            range.Values = new List<IList<object>> { ExpenseUtils.GetArrayFromExpense(item) };
            SpreadsheetsResource.ValuesResource.UpdateRequest update = service_.Spreadsheets.Values.Update(range, SpreadsheetId, "expenses!A2:AN"+(idx+2));
            update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            update.Execute();
        }

        public IEnumerable<Expense> Where(Func<Expense, bool> predicate)
        {
            return data_.Where(predicate);
        }
    }
}
