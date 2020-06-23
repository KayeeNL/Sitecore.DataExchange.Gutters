using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Sitecore.DataExchange.Attributes;
using Sitecore.Shell.Framework.Commands;

namespace Sitecore.DataExchange.Gutters.Commands
{
    public class DisplayRequiredCommand : Command
    {
        public override void Execute(CommandContext context)
        {
            var item = context.Items[0];

            var type = Type.GetType(item["ProcessorType"]);
            if (type == null)
            {
                Sitecore.Context.ClientPage.ClientResponse.Alert("No Required!");
                return;
            }

            var requiredAttributes = type.GetCustomAttributes<BaseRequiredPluginsAttribute>(true);
            var baseRequiredPluginsAttributes = requiredAttributes as BaseRequiredPluginsAttribute[] ?? requiredAttributes.ToArray();
            if (!baseRequiredPluginsAttributes.Any())
            {
                Sitecore.Context.ClientPage.ClientResponse.Alert("No Required!");
                return;
            }

            var groupBy = baseRequiredPluginsAttributes.GroupBy(a => a.GetType());

            var str = string.Empty;

            foreach (var groupRequired in groupBy)
            {
                var k = 0;
                str = str + ProposeCategoryName(groupRequired.Key.Name) + "\n";
                var pluginList = groupRequired.Select(a => a.RequiredPlugins);
                var pluginArray = pluginList as IEnumerable<Type>[] ?? pluginList.ToArray();
                foreach (var plugins in pluginArray)
                foreach (var plugin in plugins)
                {
                    ++k;
                    str = $"{str}\t{k}. {plugin.Name}\n";
                }

                str = $"{str}\n";
            }

            Sitecore.Context.ClientPage.ClientResponse.Alert(str);

            //var pluginList = baseRequiredPluginsAttributes.Select(a => a.RequiredPlugins);

            //var pluginArray = pluginList as IEnumerable<Type>[] ?? pluginList.ToArray();


            //string str = string.Empty;

            //int k = 0;
            //foreach (var plugins in pluginArray)
            //{
            //    foreach (var plugin in plugins)
            //    {
            //        ++k;
            //        str = str + k + ". " + plugin.Name + "\n";
            //    }

            //}

            //Sitecore.Context.ClientPage.ClientResponse.Alert(str);
        }

        private string[] SplitCamelCase(string source)
        {
            return Regex.Split(source, @"(?<!^)(?=[A-Z])");
        }

        protected virtual string ProposeCategoryName(string source)
        {
            var splitCamelCase = SplitCamelCase(source).ToList();
            splitCamelCase.RemoveAt(splitCamelCase.Count - 1);
            return string.Join(" ", splitCamelCase);
        }
    }
}