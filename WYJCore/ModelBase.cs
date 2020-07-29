using System;
using System.Diagnostics.CodeAnalysis;
using WYJCore.Extension;

namespace WYJCore
{
    public abstract class ModelBase<T> : IModel,IEquatable<T> where T: IModel
    {
        [SqlSugar.SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        //[SqlSugar.SugarColumn(IsIgnore = true)]
        public virtual string ModelType { get; set; } = typeof(T).Name;


        public bool Equals([AllowNull] T other)
        {
            if (other is null) return false;
            if (object.ReferenceEquals(this, other)) return true;
            return Id == other.Id && ModelType == other.ModelType;
        }

        public override int GetHashCode()
        {
            var h = Id.GetHashCode();
            return Id.GetHashCode();
        }

        public bool IsEmpty()
        {
            return ModelType.IsNullOrEmpty();
        }

        public static string Muid(char Char)
        {
            //68B84974-F6DA-49BA-9A43-2ED66844B969
            char[] cs = new char[36]
            {
                Char,Char,Char,Char,Char,Char,Char,Char,'-',
                Char,Char,Char,Char,'-',
                Char,Char,Char,Char,'-',
                Char,Char,Char,Char,'-',
                Char,Char,Char,Char,Char,Char,Char,Char,Char,Char,Char,Char
            };
            return new string(cs);
        }
    }
}
