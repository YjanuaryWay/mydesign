using System;
using WYJCore;

namespace WYJWeb.Models
{
    /// <summary>
    /// 通知
    /// </summary>
    public class Notification : ModelBase<Notification>
    {

        /// <summary>
        /// 要通知的对象
        /// </summary>
        public int Receiver { get; set; }

        /// <summary>
        /// 通知发布时间
        /// </summary>
        public DateTime PublishTime { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public int InDate { get; set; }

        /// <summary>
        /// 通知内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public bool HasRead { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Css
        {
            get
            {
                if (PublishTime.AddDays(InDate) < DateTime.Today) return CssStyle.danger.ToString();
                return CssStyle.none.ToString();
            }
            set { }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="receiver"></param>
        /// <param name="Content"></param>
        public static void CreateNotification(DbContext<Notification> db,int receiver,string Content)
        {
            db.Save(new Notification
            {
                Receiver=receiver,
                Content= Content,
                PublishTime=DateTime.Now
            }).ExecuteCommand();
        }
    }

}
