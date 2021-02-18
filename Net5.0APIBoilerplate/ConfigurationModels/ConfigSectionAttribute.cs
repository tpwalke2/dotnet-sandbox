using System;

namespace Net5._0APIBoilerplate.ConfigurationModels
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public class ConfigSectionAttribute : Attribute
    {
        public ConfigSectionAttribute(string sectionRoot)
        {
            SectionRoot = sectionRoot;
        }
        
        public string SectionRoot { get; private set; }
    }
}
