using System;
using System.Collections.Generic;
using System.Text;

namespace Service.DAL.Models
{
    //частная модель данных BaseSheet, которая включает дополнительное поле
    public class SheetAndTableName:BaseSheet
    {
        public virtual string TableName { get; set; }
    }
}
