using System;

using LinqToDB;
using LinqToDB.Mapping;

namespace DataBasesWork
{
    /// <summary>
    /// Database       : weatherdb
    /// Data Source    : localhost
    /// Server Version : 5.5.12
    /// </summary>
    public partial class WeatherDb : LinqToDB.Data.DataConnection
    {
        public ITable<MySqlDataItem> WeatherTable { get { return this.GetTable<MySqlDataItem>(); } }

        public WeatherDb()
        {
            InitDataContext();
            InitMappingSchema();
        }

        public WeatherDb(string configuration)
            : base(configuration)
        {
            InitDataContext();
            InitMappingSchema();
        }

        partial void InitDataContext();
        partial void InitMappingSchema();
    }

    [Table("weathertable")]
    public partial class MySqlDataItem : IDatabaseItem
    {
        [Column, Nullable] public int Id { get; set; } // int(11)
        [Column, Nullable] public DateTime Date { get; set; } // datetime
        [Column, Nullable] public double Temperature { get; set; } // double
        [Column, Nullable] public double TemperatureFeel { get; set; } // double
        [Column, Nullable] public int Wind { get; set; } // int(11)
        [Column, Nullable] public int Pressure { get; set; } // int(11)
        [Column, Nullable] public int Humidity { get; set; } // int(11)
        [Column, Nullable] public int Cloudiness { get; set; } // int(11)
        [Column, Nullable] public int Precipitation { get; set; } // int(11)
        [Column, Nullable] public string City { get; set; } // varchar(255)
    }
}
