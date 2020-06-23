using System;
using Sitecore.Shell.Framework;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;

namespace Sitecore.DataExchange.Gutters.Commands
{
    [Serializable]
    public class GoToReferenceCommand : Command
    {
        public override void Execute(CommandContext context)
        {
            var item = context.Items[0];
            var id = context.Parameters["id"];

            //Sitecore.Context.ClientPage.ClientResponse.Timer(string.Format("item:load(id={0})", id), 100);
            var parameters = new UrlString();
            parameters.Add("id", id);
            parameters.Add("fo", id);
            Windows.RunApplication("Content Editor", parameters.ToString());
        }
    }
}