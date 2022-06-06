using System;
using Terraria.ModLoader.Config;

namespace AdLibitum.Configuration
{
    /// <summary>
    ///     Extended version of the normal <see cref="LabelAttribute" /> attribute. Automatically localizes keys.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
    public class LibitumLabelAttribute : LabelAttribute
    {
        public LibitumLabelAttribute(string label, bool fullKey = false) : base('$' + GetLabel(label, fullKey)) { }

        private static string GetLabel(string label, bool fullKey) {
            return fullKey ? label : "Mods.AdLibitum." + label;
        }
    }
}