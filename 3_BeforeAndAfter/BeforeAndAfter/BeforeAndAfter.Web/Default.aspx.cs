using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeforeAndAfter.Web
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
        }

        protected override void OnPreRender(EventArgs e)
        {
            DisplayCacheContents();
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            var zipService = new ZipService();
            var result = zipService.GetInformationFor(txtZipCode.Text);
            DisplayResult(result);
        }

        void DisplayResult(ZipInformation result)
        {
            litMedianIncome.Text = result.MedianIncome.ToString("c");
            litPopulation.Text = result.Population.ToString("N0");
            litSqMiles.Text = result.SquareMiles.ToString("N0"); ;
            fieldsetResults.Visible = true;
        }

        void DisplayCacheContents()
        {
            listCacheContents.DataSource = GetCacheDebug();
            listCacheContents.DataBind();
        }

        List<string> GetCacheDebug()
        {
            var cacheDebugList = new List<string>();
            foreach (DictionaryEntry cachedItem in HttpContext.Current.Cache)
            {
                var cacheRecord = cachedItem.Key + " - " + cachedItem.Value;
                if (IsExcluded(cacheRecord))
                    continue;
                cacheDebugList.Add(cacheRecord);
            }
            if (!cacheDebugList.Any())
                cacheDebugList.Add("None");
            return cacheDebugList;
        }

        bool IsExcluded(string cacheRecord)
        {
            return
                cacheRecord.Contains("__System.Web.WebPages.Deployment__")
                ||
                cacheRecord.Contains("__AppStartPage__");
        }
    }
}
