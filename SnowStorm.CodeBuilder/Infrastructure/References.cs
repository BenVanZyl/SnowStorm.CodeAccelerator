using System;
using System.Collections.Generic;
using System.Text;

namespace SnowStorm.CodeBuilder.Infrastructure
{
    public class References
    {
        public enum ColIndex
        {
            COLUMN_NAME = 0,
            DATA_TYPE = 1,
            COLUMN_DEFAULT = 2,
            CHARACTER_MAXIMUM_LENGTH = 3,
            NUMERIC_PRECISION = 4,
            NUMERIC_PRECISION_RADIX = 5,
            NUMERIC_SCALE = 6,
            DATETIME_PRECISION = 7,
            ORDINAL_POSITION = 8,
            IS_NULLABLE = 9
        }

        public enum KeyTypes
        {
            PrimaryKey = 0,
            ForeignKey = 1
        }
    }
}
