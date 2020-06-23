using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Shell.Applications.ContentEditor.Gutters;

namespace Sitecore.DataExchange.Gutters
{
    public class ValueMappingGutter : GutterRenderer
    {
        protected readonly ID ValueMappingTemplateId = new ID("{450C8F38-91C9-4896-957B-6F7DAF18CCFC}");

        protected override GutterIconDescriptor GetIconDescriptor(Item item)
        {
            Assert.ArgumentNotNull(item, nameof(item));
            if (item.TemplateID == ValueMappingTemplateId)
            {
                var gutterIconDescriptor = new GutterIconDescriptor {Icon = TemplateManager.GetTemplate(item).Icon, Tooltip = item.DisplayName};
                gutterIconDescriptor.Click = "dataExchange:valueMappingContextMenu()";
                return gutterIconDescriptor;
            }

            return null;
        }
    }
}