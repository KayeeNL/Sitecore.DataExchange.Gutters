using System;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;

namespace Sitecore.DataExchange.Gutters.Commands
{
    [Serializable]
    public class ShowEnableCommand : Command
    {
        public override void Execute(CommandContext context)
        {
            Assert.ArgumentNotNull(context, nameof(context));
            var database = context.Parameters["database"];
            var id = context.Parameters["id"];
            var language = context.Parameters["language"];
            var version = context.Parameters["version"];

            var menu = new Menu();
            SheerResponse.DisableOutput();

            menu.Add("Enable", "Office/32x32/selection.png", string.Concat(new object[] {"dataExchange:enableItem(id=", id, ",language=", language, ",version=", version, ",database=", database, ")"}));

            SheerResponse.EnableOutput();
            SheerResponse.ShowContextMenu(Sitecore.Context.ClientPage.ClientRequest.Control, "right", menu);
        }
    }
}