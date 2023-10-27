using System.ComponentModel;

namespace Domain
{
    public enum OrderByEnum
    {
        [Description("Ascending")]
        Asc = 0,

        [Description("Descending")]
        Desc = 1,
        
    }
}
