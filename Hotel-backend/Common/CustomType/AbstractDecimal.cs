namespace Common
{
    public abstract class AbstractDecimal : IEquatable<decimal>
    {
        private readonly decimal _value;
        public AbstractDecimal(decimal value, string format)
        {
            _value = value;
            Format = format;
        }
        public string Format { get; set; }
        public decimal Value => _value;
        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }


        public static bool operator ==(AbstractDecimal a, AbstractDecimal b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(AbstractDecimal a, AbstractDecimal b)
        {
            return !(a == b);
        }

        protected virtual int GetHashCodeCore()
        {
            return _value.GetHashCode();
        }

        public bool Equals(decimal other)
        {
            return _value == other;
        }

        public static implicit operator decimal(AbstractDecimal decimalValue)
        {
            return decimalValue.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            return this.Equals(obj as AbstractDecimal);
        }
    }




}
