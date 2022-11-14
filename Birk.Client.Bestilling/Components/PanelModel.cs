using Birk.Client.Bestilling.Utils.Constants;

namespace Birk.Client.Bestilling.Components
{
    public class PanelModel
    {
        public string Name { get; set; }
        public List<GUIComponent> GUIComponents = new();

        public PanelModel(string name)
        {
            Name = name;
        }

        public void AddGUIComponent(string stringType, string stringIndex)
        {
            GUIType enumType;
            int intIndex;
            try
            {
                if (Enum.TryParse<GUIType>(stringType, out enumType))
                {
                    if (int.TryParse(stringIndex, out intIndex))
                    {
                        GUIComponents.Add(new(enumType, Name, intIndex));
                    }
                }
            }
            catch
            {
                throw;
            }
            
        }        
    }

    public class GUIComponent
    {
        public GUIType GUIType { get; set; }
        public int Index { get; set; }
        public string[] Items { get; set; }

        public GUIComponent(GUIType guiType, string name, int index)
        {
            GUIType = guiType;
            Index = index;

            if (GUIType == GUIType.TextBox) return;

            var content = @Language.Eng[$"{name}{GUIType}{Index}"];

            switch (GUIType)
            {
                case GUIType.Question:
                    Items = content.Split('|');
                    break;
                case GUIType.Description:
                    Items = content.Split('|');
                    break;
                case GUIType.Label:
                    Items = content.Split('|');
                    break;
                case GUIType.Dropdown:
                    Items = content.Split('|');
                    break;
                case GUIType.RadioGroup:
                    Items = content.Split('|');
                    break;
                default:
                    // code block
                    break;
            }
        }

    }

    public enum GUIType
    {
        Question,
        Description,
        Label,
        Dropdown,
        RadioGroup,
        CheckBox,
        Calendar,
        TextBox
    }
}
