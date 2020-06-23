using System;
using Sitecore.Data.Fields;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;

namespace Sitecore.DataExchange.Gutters.Commands
{
    [Serializable]
    public class ReferenceMenuCommand : Command
    {
        public override void Execute(CommandContext context)
        {
            var item = context.Items[0];
            var fieldName = context.Parameters["fieldName"];

            MultilistField multilistField = item.Fields[fieldName];

            var targetIDs = multilistField.TargetIDs;

            var menu = new Menu();
            SheerResponse.DisableOutput();

            foreach (var targetId in targetIDs)
            {
                var item1 = item.Database.GetItem(targetId);

                menu.Add(item1.DisplayName, "Office/32x32/radio_button_selected.png",
                    string.Concat(new object[] {"dataExchange:goToReference(id=", targetId, ")"}));
            }

            SheerResponse.EnableOutput();
            SheerResponse.ShowContextMenu(Sitecore.Context.ClientPage.ClientRequest.Control, "right", menu);
        }
    }
}