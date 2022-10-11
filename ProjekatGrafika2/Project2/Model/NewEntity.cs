using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project2.Model
{
    public class NewEntity
    {
        private long id;
        private int x;
        private int z;
        private int type;
        private int num;
        private ToolTip toleTips;
        public NewEntity(long id, int x, int z, int type, int num, ToolTip toleTips)
        {
            this.Id = id;
            this.X = x;
            this.Z = z;
            this.Type = type;
            this.Num = num;
            this.ToleTips = toleTips;
        }

        public long Id { get => id; set => id = value; }
        public int X { get => x; set => x = value; }
        public int Z { get => z; set => z = value; }
        public int Type { get => type; set => type = value; }
        public int Num { get => num; set => num = value; }
        public ToolTip ToleTips { get => toleTips; set => toleTips = value; }
    }
}
