using System;
using System.Collections.Generic;
using System.Text;

namespace LBON.Extensions.Enums
{
    public enum MaskTypeEnum
    {
        /// <summary>
        /// Masks all characters within the masking region, regardless of type.
        /// </summary>
        All,

        /// <summary>
        /// Masks only alphabetic and numeric characters within the masking region.
        /// </summary>
        AlphaNumericOnly,
	}
}
