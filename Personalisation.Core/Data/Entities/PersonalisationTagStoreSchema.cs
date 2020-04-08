using NPoco;
using System;
using System.Collections.Generic;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Personalisation.Core.Data.Entities
{
    [TableName("PersonalisationTagStore")]
    [PrimaryKey("MemberId", AutoIncrement = false)]
    [ExplicitColumns]
    public class PersonalisationTagStoreSchema
    {     
        [Column("MemberId")]
        [PrimaryKeyColumn(AutoIncrement = false)]
        public int MemberId { get; set; }

        [SerializedColumn("Tags")]
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        [NullSetting(NullSetting = NullSettings.Null)]
        public List<PersonalisationTagSchema> Tags { get; set; }        

        [Column("Updated")]
        public DateTime Updated { get; set; }
    }
}