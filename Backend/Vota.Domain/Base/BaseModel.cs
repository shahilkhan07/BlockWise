using System;

namespace Vota.Domain.Base
{
    /// <summary>
    /// Base model
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// Identity
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Is active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Created date.
        /// </summary>  
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Modified At
        /// </summary>
        public DateTime ModifiedAt { get; set; }
    }

    
}
