using JetBrains.Annotations;

namespace LBON.EntityFrameworkCore.Entities
{
    public interface IExtendableObject
    {
        /// <summary>
        /// Gets or sets the extension data.
        /// </summary>
        /// <value>
        /// The extension data.
        /// </value>
        [CanBeNull]
        string ExtensionData { get; set; }
    }
}
