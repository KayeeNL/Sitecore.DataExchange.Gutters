using System.Collections.Generic;
using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;

namespace Sitecore.DataExchange.Gutters.Commands
{
    public class DisplayPipelineStepsCommand : Command
    {
        protected readonly ID PipelineBatchTemplateId = new ID("{075C4FBD-F54E-4E6D-BD54-D49BDA0913D8}");

        public override void Execute(CommandContext context)
        {
            Assert.ArgumentNotNull(context, nameof(context));
            var item = context.Items[0];
            if (item.TemplateID != PipelineBatchTemplateId)
            {
                SheerResponse.Alert("The context item is not Pipeline Batch.");
                return;
            }

            var db = item.Database;

            var pipelines = GetPipelines(item);

            var enumerable = pipelines as ID[] ?? pipelines.ToArray();
            if (!enumerable.Any())
            {
                Sitecore.Context.ClientPage.ClientResponse.Alert("Pipelines are not set.");
                return;
            }

            var list = new List<ID>();

            foreach (var pipeline in enumerable) list.AddRange(GetSteps(pipeline, db));

            var str = string.Empty;

            var k = 0;
            foreach (var id in list)
            {
                ++k;
                str = str + k + ". " + db.GetItem(id).DisplayName + "\n";
            }

            Sitecore.Context.ClientPage.ClientResponse.Alert(str);
        }

        protected virtual IEnumerable<ID> GetPipelines(Item item)
        {
            var multilistField = (MultilistField) item.Fields["Pipelines"];

            return multilistField == null ? Enumerable.Empty<ID>() : multilistField.TargetIDs;
        }

        protected virtual List<ID> GetSteps(ID pipelineId, Database database)
        {
            var list = new List<ID>();
            var items = database.GetItem(pipelineId).Children.Where(i => !string.IsNullOrEmpty(i["Enabled"]) && i["Enabled"] != "0");
            foreach (var item in items)
            {
                list.Add(item.ID);
                if (!string.IsNullOrEmpty(item["Pipelines"]))
                {
                    var pipelines = GetPipelines(item);
                    foreach (var pipeline in pipelines) list.AddRange(GetSteps(pipeline, database));
                }
            }

            return list;
        }
    }
}