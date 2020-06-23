using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Shell.Applications.ContentEditor.Gutters;

namespace Sitecore.DataExchange.Gutters
{
    public class PipelineStepGutter : GutterRenderer
    {
        protected readonly ID PipelinesFolderId = new ID("{70ED1D4D-3796-400D-A64E-E538D933E12C}");
        protected readonly ID PipelineTemplateId = new ID("{2DC9C843-B841-483C-BB9B-AE0C9386404C}");

        protected override GutterIconDescriptor GetIconDescriptor(Item item)
        {
            Assert.ArgumentNotNull(item, nameof(item));
            if (item.TemplateID != PipelinesFolderId && item.ParentID != ID.Null && item.Parent.TemplateID == PipelineTemplateId)
            {
                var gutterIconDescriptor = new GutterIconDescriptor {Icon = TemplateManager.GetTemplate(item).Icon, Tooltip = item.DisplayName};
                gutterIconDescriptor.Click = "dataExchange:pipelineStepContextMenu()";
                return gutterIconDescriptor;
            }

            return null;
        }
    }
}