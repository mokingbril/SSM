using System;
using System.Collections.Generic;

namespace SSM.Models
{
    public partial class Record
    {
        public int Id { get; set; }
        public string StuNo { get; set; }
        public int SubId { get; set; }
        public Nullable<double> Score { get; set; }
        public DateTime ExamTime { get; set; }
        public string Tip { get; set; }
        public Nullable<double> TrueScore { get; set; }

        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
