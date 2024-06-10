using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Data
{
    public interface IDataHelper<Table>
    {
        //Read
        List<Table> GetAllData();
        List<Table> Search(string SearchIthem);
        Table Find(int ID);

        Table Find3(string S);

        //Write
        int Add(Table table);

        int Add2(Table table, string S);
        int Edit(int ID, Table table);
        int Edit2(int ID1, int ID2, Table table, decimal D);
        int Delete(int ID);

    }
}
