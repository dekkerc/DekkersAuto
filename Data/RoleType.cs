using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    /// <summary>
    /// Enum representing the permission level of the user
    /// </summary>
    public enum RoleType
    {
        /// <summary>
        /// Super user. Has access to crud functionality including creating new users
        /// </summary>
        Admin,
        /// <summary>
        /// User has access to backend and managing inventory but cannot create or manage users
        /// </summary>
        Secretary
    }
}
