using System.ComponentModel;

namespace MapDat.Domain.Authorization
{
    public enum Roles
    {

        [Description("Superuser")]
        Superuser,

        [Description("None")]
        None
    }
}
