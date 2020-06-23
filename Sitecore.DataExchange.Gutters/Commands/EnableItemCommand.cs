using System;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Shell.Framework.Commands;
using Version = Sitecore.Data.Version;

namespace Sitecore.DataExchange.Gutters.Commands
{
    [Serializable]
    public class EnableItemCommand : Command
    {
        public override void Execute(CommandContext context)
        {
            Assert.ArgumentNotNull(context, nameof(context));
            var database = context.Parameters["database"];
            var id = context.Parameters["id"];
            var language = context.Parameters["language"];
            var version = context.Parameters["version"];

            var db = Factory.GetDatabase(database);
            var item = db.GetItem(id, Language.Parse(language), Version.Parse(version));

            using (new EditContext(item))
            {
                item["Enabled"] = "1";
            }
        }
    }
}