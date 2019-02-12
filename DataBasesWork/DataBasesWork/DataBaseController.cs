using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBasesWork
{
    public class DataBaseController
    {

        private IDatabaseItemCollection _db = new MySqlDataBase();

        public void SetData(IDatabaseItem dbItem)
        {
            _db.SetData(dbItem);
        }

        public void SetData(List<IDatabaseItem> dbItems)
        {
            _db.SetData(dbItems);
        }

        public IDatabaseItem GetDataLast(string cityName)
        {
            return _db.GetDataLast(cityName);
        }

        public List<IDatabaseItem> GetDataByDate(DateTime dateStart, DateTime dateEnd, string cityName)
        {
            return _db.GetDataByDate(dateStart, dateEnd, cityName);
        }
    }
}
