﻿namespace 技术点验证
{
    public class IdCard : ValueObject<string>
    {
        public IdCard(string data) : base(data)
        {
        }

        public override bool BizCheckValue()
        {
            return base.Value.Length == 18
                  && (base.Value[17] == 'X'
                        || (base.Value[17] >= '0' && base.Value[17] <= '9'));
        }

        public override string ErrorMsgForCheckValue(string value)
        {
            return $"IdCard 必须为18位,若后一位不是数字，则必须为X.现在的值:{value}";
        }
    }
}