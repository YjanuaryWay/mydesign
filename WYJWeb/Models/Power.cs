using System;

namespace WYJWeb.Models
{
    public class Power
    {
        [SqlSugar.SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime  PlanDate { get; set; }
        public DateTime DoDate { get; set; }

        public bool IsOver => PlanDate < DoDate;
    }
}