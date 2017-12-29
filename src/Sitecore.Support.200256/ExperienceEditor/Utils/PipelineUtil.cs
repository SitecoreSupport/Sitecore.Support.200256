using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Pipelines.Save;
using Sitecore.Collections;
using Sitecore.Data.Validators;
using Sitecore.Shell.Applications.WebEdit.Commands;
using Sitecore.Web;
using Sitecore.Xml;

namespace Sitecore.Support.ExperienceEditor.Utils
{
    public static class PipelineUtil
    {
        public static SaveArgs GenerateSaveArgs(Item contextItem, System.Collections.Generic.IEnumerable<PageEditorField> fields, string postAction, string layoutValue, string validatorsKey, string fieldId = null)
        {
            SafeDictionary<FieldDescriptor, string> controlsToValidate;
            Packet packet = WebUtility.CreatePacket(contextItem.Database, fields, out controlsToValidate);
            if (WebEditUtil.CanDesignItem(contextItem))
            {
                WebUtility.AddLayoutField(layoutValue, packet, contextItem, fieldId);
            }
            if (!string.IsNullOrEmpty(validatorsKey))
            {
                ValidatorsMode mode;
                Sitecore.Data.Validators.ValidatorCollection validators = Sitecore.ExperienceEditor.Utils.PipelineUtil.GetValidators(contextItem, controlsToValidate, out mode);
                validators.Key = validatorsKey;
                ValidatorManager.SetValidators(mode, validatorsKey, validators);
            }
            return new SaveArgs(packet.XmlDocument)
            {
                SaveAnimation = false,
                PostAction = postAction,
                PolicyBasedLocking = true
            };
        }

    }
}