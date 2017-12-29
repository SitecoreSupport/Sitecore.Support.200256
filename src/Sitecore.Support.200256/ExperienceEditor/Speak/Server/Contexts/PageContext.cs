
namespace Sitecore.Support.ExperienceEditor.Speak.Server.Contexts
{
    using Sitecore.Pipelines.Save;
    using Sitecore.Shell.Applications.WebEdit.Commands;
    using System.Collections.Generic;
    using Utils;

    public class PageContext : Sitecore.ExperienceEditor.Speak.Server.Contexts.PageContext
    {
        public new SaveArgs GetSaveArgs()
        {
            IEnumerable<PageEditorField> fields = WebUtility.GetFields(base.Item.Database, this.FieldValues);
            string empty = string.Empty;
            string layoutSource = this.LayoutSource;
            SaveArgs saveArgs = PipelineUtil.GenerateSaveArgs(base.Item, fields, empty, layoutSource, string.Empty, WebUtility.GetCurrentLayoutFieldId().ToString());
            saveArgs.HasSheerUI = false;
            ParseXml parseXml = new ParseXml();
            parseXml.Process(saveArgs);
            return saveArgs;
        }
    }
}