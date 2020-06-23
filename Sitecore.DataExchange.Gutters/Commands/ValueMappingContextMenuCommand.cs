using System;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;

namespace Sitecore.DataExchange.Gutters.Commands
{
    [Serializable]
    public class ValueMappingContextMenuCommand : Command
    {
        public override void Execute(CommandContext context)
        {
            Assert.ArgumentNotNull(context, nameof(context));

            var menu = new Menu();
            SheerResponse.DisableOutput();

            menu.Add("Source Accessor", "Office/32x32/radio_button_selected.png", string.Concat(new object[] {"dataExchange:referenceMenu(fieldName=", "SourceAccessor", ")"}));
            menu.Add("Target Accessor", "Office/32x32/radio_button_selected.png", string.Concat(new object[] {"dataExchange:referenceMenu(fieldName=", "TargetAccessor", ")"}));
            menu.Add("Source Pre Transformer", "Office/32x32/graph_from.png", string.Concat(new object[] {"dataExchange:referenceMenu(fieldName=", "SourceValuePreTransformer", ")"}));
            menu.Add("Source Transformer", "Office/32x32/graph_from.png", string.Concat(new object[] {"dataExchange:referenceMenu(fieldName=", "SourceValueTransformer", ")"}));

            SheerResponse.EnableOutput();
            SheerResponse.ShowContextMenu(Sitecore.Context.ClientPage.ClientRequest.Control, "right", menu);
        }
    }
}