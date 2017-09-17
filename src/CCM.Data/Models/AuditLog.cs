using System;

namespace CCM.Data.Models
{
    public class AuditLog
    {
        public AuditLog()
        {
            Id = Guid.NewGuid().ToString();
        }

        public String Id { get; set; }
        public String EntityName { get; set; }
        public String PropertyName { get; set; }
        public String PrimaryKeyValue { get; set; }
        public String OldValue { get; set; }
        public String NewValue { get; set; }
        public DateTime DateChanged { get; set; }
        public String UserId { get; set; }
    }
}
