using System;
using Terraria.ModLoader.Config;

namespace AdLibitum.Configuration
{
    /// <summary>
    ///     Extended version of the normal <see cref="HeaderAttribute" /> attribute. Automatically localizes keys.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class LibitumHeaderAttribute : HeaderAttribute
    {
        public LibitumHeaderAttribute(string label, bool fullKey = false) : base('$' + GetLabel(label, fullKey)) { }

        private static string GetLabel(string label, bool fullKey)
        {
            return fullKey ? label : "Mods.AdLibitum." + label;
        }
    }
}