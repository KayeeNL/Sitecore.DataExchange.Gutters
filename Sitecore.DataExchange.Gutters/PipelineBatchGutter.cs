using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Shell.Applications.ContentEditor.Gutters;

namespace Sitecore.DataExchange.Gutters
{
    public class PipelineBatchGutter : GutterRenderer
    {
        protected readonly ID PipelineBatchTemplateId = new ID("{075C4FBD-F54E-4E6D-BD54-D49BDA0913D8}");

        protected override GutterIconDescriptor GetIconDescriptor(Item item)
        {
            Assert.ArgumentNotNull(item, nameof(item));
            if (item.TemplateID == PipelineBatchTemplateId)
            {
                var gutterIconDescriptor = new GutterIconDescriptor {Icon = TemplateManager.GetTemplate(item).Icon, Tooltip = item.DisplayName};
                gutterIconDescriptor.Click = "dataExchange:pipelineBatchContextMenu()";
                return gutterIconDescriptor;
            }

            return null;
        }
    }
}