namespace SecretNotebook
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Command
    {
        public enum Commands
        {
            ReadRecord = 1,
            RenameRecord,
            CreateRecord,
            DeleteRecord,
        }
    }
}
