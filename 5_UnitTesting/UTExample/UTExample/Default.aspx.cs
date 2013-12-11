using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UTExample
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            var reverse = new StringReverser();
            var result = reverse.Reverse(txtString.Text);

            fieldsetResults.Visible = true;
            litReversedString.Text = result;
        }

        #region code to show cache contents

        protected override void OnPreRender(EventArgs e)
        {
            DisplayCacheContents();
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

        #endregion
    }
}