using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Shell.Applications.ContentEditor.Gutters;

namespace Sitecore.DataExchange.Gutters
{
    public class ItemDisabledGutter : GutterRenderer
    {
        protected readonly ID EnabledField = new ID("{506BAD49-3DA6-474C-ACB5-7BCB509BFBD7}");

        protected override GutterIconDescriptor GetIconDescriptor(Item item)
        {
            Assert.ArgumentNotNull(item, nameof(item));

            if (item.Fields.Contains(EnabledField) && item.Fields["Enabled"].Value != "1" && item.Fields["Enabled"].Section == "Administration")
            {
                var gutterIconDescriptor = new GutterIconDescriptor {Icon = "Office/32x32/selection_delete.png", Tooltip = item.DisplayName + " is disabled."};
                gutterIconDescriptor.Click = string.Concat("dataExchange:showEnable(id=", item.ID, ",language=", item.Language, ",version=", item.Version, ",database=", item.Database.Name, ")");
                return gutterIconDescriptor;
            }

            return null;
        }
    }
}